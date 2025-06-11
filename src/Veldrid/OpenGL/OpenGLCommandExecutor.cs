﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Text;
using Veldrid.OpenGLBindings;
using static Veldrid.OpenGLBindings.OpenGLNative;
using static Veldrid.OpenGL.OpenGLUtil;

namespace Veldrid.OpenGL
{
    internal unsafe class OpenGLCommandExecutor
    {
        private readonly OpenGLGraphicsDevice gd;
        private readonly GraphicsBackend backend;
        private readonly OpenGLTextureSamplerManager textureSamplerManager;
        private readonly StagingMemoryPool stagingMemoryPool;
        private readonly OpenGLExtensions extensions;
        private readonly OpenGLPlatformInfo platformInfo;
        private readonly GraphicsDeviceFeatures features;
        private readonly Viewport[] viewports = new Viewport[20];

        private Framebuffer fb;
        private bool isSwapchainFb;
        private OpenGLPipeline graphicsPipeline;
        private BoundResourceSetInfo[] graphicsResourceSets = Array.Empty<BoundResourceSetInfo>();
        private bool[] newGraphicsResourceSets = Array.Empty<bool>();
        private OpenGLBuffer[] vertexBuffers = Array.Empty<OpenGLBuffer>();
        private uint[] vbOffsets = Array.Empty<uint>();
        private uint[] vertexAttribDivisors = Array.Empty<uint>();
        private uint vertexAttributesBound;
        private DrawElementsType drawElementsType;
        private uint ibOffset;
        private PrimitiveType primitiveType;

        private OpenGLPipeline computePipeline;
        private BoundResourceSetInfo[] computeResourceSets = Array.Empty<BoundResourceSetInfo>();
        private bool[] newComputeResourceSets = Array.Empty<bool>();

        private bool graphicsPipelineActive;
        private bool vertexLayoutFlushed;

        public OpenGLCommandExecutor(OpenGLGraphicsDevice gd, OpenGLPlatformInfo platformInfo)
        {
            this.gd = gd;
            backend = gd.BackendType;
            extensions = gd.Extensions;
            textureSamplerManager = gd.TextureSamplerManager;
            stagingMemoryPool = gd.StagingMemoryPool;
            this.platformInfo = platformInfo;
            features = gd.Features;
        }

        public void Begin()
        {
        }

        public void ClearColorTarget(uint index, RgbaFloat clearColor)
        {
            if (!isSwapchainFb)
            {
                var bufs = (DrawBuffersEnum)((uint)DrawBuffersEnum.ColorAttachment0 + index);
                GLDrawBuffers(1, &bufs);
                CheckLastError();
            }

            var color = clearColor;
            GLClearColor(color.R, color.G, color.B, color.A);
            CheckLastError();

            if (graphicsPipeline != null && graphicsPipeline.RasterizerState.ScissorTestEnabled)
            {
                GLDisable(EnableCap.ScissorTest);
                CheckLastError();
            }

            GLClear(ClearBufferMask.ColorBufferBit);
            CheckLastError();

            if (graphicsPipeline != null && graphicsPipeline.RasterizerState.ScissorTestEnabled) GLEnable(EnableCap.ScissorTest);

            if (!isSwapchainFb)
            {
                int colorCount = fb.ColorTargets.Count;
                var bufs = stackalloc DrawBuffersEnum[colorCount];
                for (int i = 0; i < colorCount; i++) bufs[i] = DrawBuffersEnum.ColorAttachment0 + i;
                GLDrawBuffers((uint)colorCount, bufs);
                CheckLastError();
            }
        }

        public void ClearDepthStencil(float depth, byte stencil)
        {
            if (graphicsPipeline != null)
            {
                if (!graphicsPipeline.DepthStencilState.DepthWriteEnabled)
                {
                    GLDepthMask(true);
                    CheckLastError();
                }

                if (graphicsPipeline.DepthStencilState.StencilWriteMask != 0xFF)
                {
                    GLStencilMask(0xFF);
                    CheckLastError();
                }

                if (graphicsPipeline.RasterizerState.ScissorTestEnabled)
                {
                    GLDisable(EnableCap.ScissorTest);
                    CheckLastError();
                }
            }

            glClearDepth_Compat(depth);
            CheckLastError();

            GLClearStencil(stencil);
            CheckLastError();

            GLClear(ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
            CheckLastError();

            if (graphicsPipeline != null)
            {
                if (!graphicsPipeline.DepthStencilState.DepthWriteEnabled)
                {
                    GLDepthMask(false);
                    CheckLastError();
                }

                if (graphicsPipeline.DepthStencilState.StencilWriteMask != 0xFF)
                {
                    GLStencilMask(graphicsPipeline.DepthStencilState.StencilWriteMask);
                    CheckLastError();
                }

                if (graphicsPipeline.RasterizerState.ScissorTestEnabled)
                {
                    GLEnable(EnableCap.ScissorTest);
                    CheckLastError();
                }
            }
        }

        public void Draw(uint vertexCount, uint instanceCount, uint vertexStart, uint instanceStart)
        {
            preDrawCommand();

            if (instanceCount == 1 && instanceStart == 0)
            {
                GLDrawArrays(primitiveType, (int)vertexStart, vertexCount);
                CheckLastError();
            }
            else
            {
                if (instanceStart == 0)
                {
                    GLDrawArraysInstanced(primitiveType, (int)vertexStart, vertexCount, instanceCount);
                    CheckLastError();
                }
                else
                {
                    GLDrawArraysInstancedBaseInstance(primitiveType, (int)vertexStart, vertexCount, instanceCount, instanceStart);
                    CheckLastError();
                }
            }
        }

        public void DrawIndexed(uint indexCount, uint instanceCount, uint indexStart, int vertexOffset, uint instanceStart)
        {
            preDrawCommand();

            uint indexSize = drawElementsType == DrawElementsType.UnsignedShort ? 2u : 4u;
            var indices = (void*)(indexStart * indexSize + ibOffset);

            if (instanceCount == 1 && instanceStart == 0)
            {
                if (vertexOffset == 0)
                {
                    GLDrawElements(primitiveType, indexCount, drawElementsType, indices);
                    CheckLastError();
                }
                else
                {
                    GLDrawElementsBaseVertex(primitiveType, indexCount, drawElementsType, indices, vertexOffset);
                    CheckLastError();
                }
            }
            else
            {
                if (instanceStart > 0)
                {
                    GLDrawElementsInstancedBaseVertexBaseInstance(
                        primitiveType,
                        indexCount,
                        drawElementsType,
                        indices,
                        instanceCount,
                        vertexOffset,
                        instanceStart);
                    CheckLastError();
                }
                else if (vertexOffset == 0)
                {
                    GLDrawElementsInstanced(primitiveType, indexCount, drawElementsType, indices, instanceCount);
                    CheckLastError();
                }
                else
                {
                    GLDrawElementsInstancedBaseVertex(
                        primitiveType,
                        indexCount,
                        drawElementsType,
                        indices,
                        instanceCount,
                        vertexOffset);
                    CheckLastError();
                }
            }
        }

        public void DrawIndirect(DeviceBuffer indirectBuffer, uint offset, uint drawCount, uint stride)
        {
            preDrawCommand();

            var glBuffer = Util.AssertSubtype<DeviceBuffer, OpenGLBuffer>(indirectBuffer);
            GLBindBuffer(BufferTarget.DrawIndirectBuffer, glBuffer.Buffer);
            CheckLastError();

            if (extensions.MultiDrawIndirect)
            {
                GLMultiDrawArraysIndirect(primitiveType, (IntPtr)offset, drawCount, stride);
                CheckLastError();
            }
            else
            {
                uint indirect = offset;

                for (uint i = 0; i < drawCount; i++)
                {
                    GLDrawArraysIndirect(primitiveType, (IntPtr)indirect);
                    CheckLastError();

                    indirect += stride;
                }
            }
        }

        public void DrawIndexedIndirect(DeviceBuffer indirectBuffer, uint offset, uint drawCount, uint stride)
        {
            preDrawCommand();

            var glBuffer = Util.AssertSubtype<DeviceBuffer, OpenGLBuffer>(indirectBuffer);
            GLBindBuffer(BufferTarget.DrawIndirectBuffer, glBuffer.Buffer);
            CheckLastError();

            if (extensions.MultiDrawIndirect)
            {
                GLMultiDrawElementsIndirect(primitiveType, drawElementsType, (IntPtr)offset, drawCount, stride);
                CheckLastError();
            }
            else
            {
                uint indirect = offset;

                for (uint i = 0; i < drawCount; i++)
                {
                    GLDrawElementsIndirect(primitiveType, drawElementsType, (IntPtr)indirect);
                    CheckLastError();

                    indirect += stride;
                }
            }
        }

        public void DispatchIndirect(DeviceBuffer indirectBuffer, uint offset)
        {
            preDispatchCommand();

            var glBuffer = Util.AssertSubtype<DeviceBuffer, OpenGLBuffer>(indirectBuffer);
            GLBindBuffer(BufferTarget.DrawIndirectBuffer, glBuffer.Buffer);
            CheckLastError();

            GLDispatchComputeIndirect((IntPtr)offset);
            CheckLastError();

            postDispatchCommand();
        }

        public void End()
        {
        }

        public void SetFramebuffer(Framebuffer fb)
        {
            if (fb is OpenGLFramebuffer glFb)
            {
                if (backend == GraphicsBackend.OpenGL || extensions.ExtSRGBWriteControl)
                {
                    GLEnable(EnableCap.FramebufferSrgb);
                    CheckLastError();
                }

                glFb.EnsureResourcesCreated();
                GLBindFramebuffer(FramebufferTarget.Framebuffer, glFb.Framebuffer);
                CheckLastError();
                isSwapchainFb = false;
            }
            else if (fb is OpenGLSwapchainFramebuffer swapchainFb)
            {
                if (backend == GraphicsBackend.OpenGL || extensions.ExtSRGBWriteControl)
                {
                    if (swapchainFb.DisableSrgbConversion)
                    {
                        GLDisable(EnableCap.FramebufferSrgb);
                        CheckLastError();
                    }
                    else
                    {
                        GLEnable(EnableCap.FramebufferSrgb);
                        CheckLastError();
                    }
                }

                if (platformInfo.SetSwapchainFramebuffer != null)
                    platformInfo.SetSwapchainFramebuffer();
                else
                {
                    GLBindFramebuffer(FramebufferTarget.Framebuffer, 0);
                    CheckLastError();
                }

                isSwapchainFb = true;
            }
            else
                throw new VeldridException("Invalid Framebuffer type: " + fb.GetType().Name);

            this.fb = fb;
        }

        public void SetIndexBuffer(DeviceBuffer ib, IndexFormat format, uint offset)
        {
            var glIb = Util.AssertSubtype<DeviceBuffer, OpenGLBuffer>(ib);
            glIb.EnsureResourcesCreated();

            GLBindBuffer(BufferTarget.ElementArrayBuffer, glIb.Buffer);
            CheckLastError();

            drawElementsType = OpenGLFormats.VdToGLDrawElementsType(format);
            ibOffset = offset;
        }

        public void SetPipeline(Pipeline pipeline)
        {
            if (!pipeline.IsComputePipeline && graphicsPipeline != pipeline)
            {
                graphicsPipeline = Util.AssertSubtype<Pipeline, OpenGLPipeline>(pipeline);
                activateGraphicsPipeline();
                vertexLayoutFlushed = false;
            }
            else if (pipeline.IsComputePipeline && computePipeline != pipeline)
            {
                computePipeline = Util.AssertSubtype<Pipeline, OpenGLPipeline>(pipeline);
                activateComputePipeline();
                vertexLayoutFlushed = false;
            }
        }

        public void GenerateMipmaps(Texture texture)
        {
            var glTex = Util.AssertSubtype<Texture, OpenGLTexture>(texture);
            glTex.EnsureResourcesCreated();

            if (extensions.ArbDirectStateAccess)
            {
                GLGenerateTextureMipmap(glTex.Texture);
                CheckLastError();
            }
            else
            {
                var target = glTex.TextureTarget;
                textureSamplerManager.SetTextureTransient(target, glTex.Texture);
                GLGenerateMipmap(target);
                CheckLastError();
            }
        }

        public void PushDebugGroup(string name)
        {
            if (extensions.KhrDebug)
            {
                int byteCount = Encoding.UTF8.GetByteCount(name);
                byte* utf8Ptr = stackalloc byte[byteCount];
                fixed (char* namePtr = name) Encoding.UTF8.GetBytes(namePtr, name.Length, utf8Ptr, byteCount);
                GLPushDebugGroup(DebugSource.DebugSourceApplication, 0, (uint)byteCount, utf8Ptr);
                CheckLastError();
            }
            else if (extensions.ExtDebugMarker)
            {
                int byteCount = Encoding.UTF8.GetByteCount(name);
                byte* utf8Ptr = stackalloc byte[byteCount];
                fixed (char* namePtr = name) Encoding.UTF8.GetBytes(namePtr, name.Length, utf8Ptr, byteCount);
                GLPushGroupMarker((uint)byteCount, utf8Ptr);
                CheckLastError();
            }
        }

        public void PopDebugGroup()
        {
            if (extensions.KhrDebug)
            {
                GLPopDebugGroup();
                CheckLastError();
            }
            else if (extensions.ExtDebugMarker)
            {
                GLPopGroupMarker();
                CheckLastError();
            }
        }

        public void InsertDebugMarker(string name)
        {
            if (extensions.KhrDebug)
            {
                int byteCount = Encoding.UTF8.GetByteCount(name);
                byte* utf8Ptr = stackalloc byte[byteCount];
                fixed (char* namePtr = name) Encoding.UTF8.GetBytes(namePtr, name.Length, utf8Ptr, byteCount);

                GLDebugMessageInsert(
                    DebugSource.DebugSourceApplication,
                    DebugType.DebugTypeMarker,
                    0,
                    DebugSeverity.DebugSeverityNotification,
                    (uint)byteCount,
                    utf8Ptr);
                CheckLastError();
            }
            else if (extensions.ExtDebugMarker)
            {
                int byteCount = Encoding.UTF8.GetByteCount(name);
                byte* utf8Ptr = stackalloc byte[byteCount];
                fixed (char* namePtr = name) Encoding.UTF8.GetBytes(namePtr, name.Length, utf8Ptr, byteCount);

                GLInsertEventMarker((uint)byteCount, utf8Ptr);
                CheckLastError();
            }
        }

        public void SetGraphicsResourceSet(uint slot, ResourceSet rs, uint dynamicOffsetCount, ref uint dynamicOffsets)
        {
            if (!graphicsResourceSets[slot].Equals(rs, dynamicOffsetCount, ref dynamicOffsets))
            {
                graphicsResourceSets[slot].Offsets.Dispose();
                graphicsResourceSets[slot] = new BoundResourceSetInfo(rs, dynamicOffsetCount, ref dynamicOffsets);
                newGraphicsResourceSets[slot] = true;
            }
        }

        public void SetComputeResourceSet(uint slot, ResourceSet rs, uint dynamicOffsetCount, ref uint dynamicOffsets)
        {
            if (!computeResourceSets[slot].Equals(rs, dynamicOffsetCount, ref dynamicOffsets))
            {
                computeResourceSets[slot].Offsets.Dispose();
                computeResourceSets[slot] = new BoundResourceSetInfo(rs, dynamicOffsetCount, ref dynamicOffsets);
                newComputeResourceSets[slot] = true;
            }
        }

        public void ResolveTexture(Texture source, Texture destination)
        {
            var glSourceTex = Util.AssertSubtype<Texture, OpenGLTexture>(source);
            var glDestinationTex = Util.AssertSubtype<Texture, OpenGLTexture>(destination);
            glSourceTex.EnsureResourcesCreated();
            glDestinationTex.EnsureResourcesCreated();

            uint sourceFramebuffer = glSourceTex.GetFramebuffer(0, 0);
            uint destinationFramebuffer = glDestinationTex.GetFramebuffer(0, 0);

            GLBindFramebuffer(FramebufferTarget.ReadFramebuffer, sourceFramebuffer);
            CheckLastError();

            GLBindFramebuffer(FramebufferTarget.DrawFramebuffer, destinationFramebuffer);
            CheckLastError();

            GLDisable(EnableCap.ScissorTest);
            CheckLastError();

            GLBlitFramebuffer(
                0,
                0,
                (int)source.Width,
                (int)source.Height,
                0,
                0,
                (int)destination.Width,
                (int)destination.Height,
                ClearBufferMask.ColorBufferBit,
                BlitFramebufferFilter.Nearest);
            CheckLastError();
        }

        public void SetScissorRect(uint index, uint x, uint y, uint width, uint height)
        {
            if (backend == GraphicsBackend.OpenGL)
            {
                GLScissorIndexed(
                    index,
                    (int)x,
                    (int)(fb.Height - (int)height - y),
                    width,
                    height);
                CheckLastError();
            }
            else
            {
                if (index == 0)
                {
                    GLScissor(
                        (int)x,
                        (int)(fb.Height - (int)height - y),
                        width,
                        height);
                    CheckLastError();
                }
            }
        }

        public void SetVertexBuffer(uint index, DeviceBuffer vb, uint offset)
        {
            var glVb = Util.AssertSubtype<DeviceBuffer, OpenGLBuffer>(vb);
            glVb.EnsureResourcesCreated();

            Util.EnsureArrayMinimumSize(ref vertexBuffers, index + 1);
            Util.EnsureArrayMinimumSize(ref vbOffsets, index + 1);
            vertexLayoutFlushed = false;
            vertexBuffers[index] = glVb;
            vbOffsets[index] = offset;
        }

        public void SetViewport(uint index, ref Viewport viewport)
        {
            viewports[(int)index] = viewport;

            if (backend == GraphicsBackend.OpenGL)
            {
                float left = viewport.X;
                float bottom = fb.Height - (viewport.Y + viewport.Height);

                GLViewportIndexed(index, left, bottom, viewport.Width, viewport.Height);
                CheckLastError();

                GLDepthRangeIndexed(index, viewport.MinDepth, viewport.MaxDepth);
                CheckLastError();
            }
            else
            {
                if (index == 0)
                {
                    GLViewport((int)viewport.X, (int)viewport.Y, (uint)viewport.Width, (uint)viewport.Height);
                    CheckLastError();

                    GLDepthRangef(viewport.MinDepth, viewport.MaxDepth);
                    CheckLastError();
                }
            }
        }

        public void UpdateBuffer(DeviceBuffer buffer, uint bufferOffsetInBytes, IntPtr dataPtr, uint sizeInBytes)
        {
            var glBuffer = Util.AssertSubtype<DeviceBuffer, OpenGLBuffer>(buffer);
            glBuffer.EnsureResourcesCreated();

            if (extensions.ArbDirectStateAccess)
            {
                GLNamedBufferSubData(
                    glBuffer.Buffer,
                    (IntPtr)bufferOffsetInBytes,
                    sizeInBytes,
                    dataPtr.ToPointer());
                CheckLastError();
            }
            else
            {
                GLBindBuffer(BufferTarget.CopyWriteBuffer, glBuffer.Buffer);
                CheckLastError();
                GLBufferSubData(
                    BufferTarget.CopyWriteBuffer,
                    (IntPtr)bufferOffsetInBytes,
                    sizeInBytes,
                    dataPtr.ToPointer());
                CheckLastError();
            }
        }

        public void UpdateTexture(
            Texture texture,
            IntPtr dataPtr,
            uint x,
            uint y,
            uint z,
            uint width,
            uint height,
            uint depth,
            uint mipLevel,
            uint arrayLayer)
        {
            if (width == 0 || height == 0 || depth == 0) return;

            var glTex = Util.AssertSubtype<Texture, OpenGLTexture>(texture);
            glTex.EnsureResourcesCreated();

            var texTarget = glTex.TextureTarget;

            textureSamplerManager.SetTextureTransient(texTarget, glTex.Texture);
            CheckLastError();

            bool isCompressed = FormatHelpers.IsCompressedFormat(texture.Format);
            uint blockSize = isCompressed ? 4u : 1u;

            uint blockAlignedWidth = Math.Max(width, blockSize);
            uint blockAlignedHeight = Math.Max(height, blockSize);

            uint rowPitch = FormatHelpers.GetRowPitch(blockAlignedWidth, texture.Format);
            uint depthPitch = FormatHelpers.GetDepthPitch(rowPitch, blockAlignedHeight, texture.Format);

            // Compressed textures can specify regions that are larger than the dimensions.
            // We should only pass up to the dimensions to OpenGL, though.
            Util.GetMipDimensions(glTex, mipLevel, out uint mipWidth, out uint mipHeight, out uint _);
            width = Math.Min(width, mipWidth);
            height = Math.Min(height, mipHeight);

            uint unpackAlignment = 4;
            if (!isCompressed) unpackAlignment = FormatSizeHelpers.GetSizeInBytes(glTex.Format);

            if (unpackAlignment < 4)
            {
                GLPixelStorei(PixelStoreParameter.UnpackAlignment, (int)unpackAlignment);
                CheckLastError();
            }

            if (texTarget == TextureTarget.Texture1D)
            {
                if (isCompressed)
                {
                    GLCompressedTexSubImage1D(
                        TextureTarget.Texture1D,
                        (int)mipLevel,
                        (int)x,
                        width,
                        glTex.GLInternalFormat,
                        rowPitch,
                        dataPtr.ToPointer());
                    CheckLastError();
                }
                else
                {
                    GLTexSubImage1D(
                        TextureTarget.Texture1D,
                        (int)mipLevel,
                        (int)x,
                        width,
                        glTex.GLPixelFormat,
                        glTex.GLPixelType,
                        dataPtr.ToPointer());
                    CheckLastError();
                }
            }
            else if (texTarget == TextureTarget.Texture1DArray)
            {
                if (isCompressed)
                {
                    GLCompressedTexSubImage2D(
                        TextureTarget.Texture1DArray,
                        (int)mipLevel,
                        (int)x,
                        (int)arrayLayer,
                        width,
                        1,
                        glTex.GLInternalFormat,
                        rowPitch,
                        dataPtr.ToPointer());
                    CheckLastError();
                }
                else
                {
                    GLTexSubImage2D(
                        TextureTarget.Texture1DArray,
                        (int)mipLevel,
                        (int)x,
                        (int)arrayLayer,
                        width,
                        1,
                        glTex.GLPixelFormat,
                        glTex.GLPixelType,
                        dataPtr.ToPointer());
                    CheckLastError();
                }
            }
            else if (texTarget == TextureTarget.Texture2D)
            {
                if (isCompressed)
                {
                    GLCompressedTexSubImage2D(
                        TextureTarget.Texture2D,
                        (int)mipLevel,
                        (int)x,
                        (int)y,
                        width,
                        height,
                        glTex.GLInternalFormat,
                        depthPitch,
                        dataPtr.ToPointer());
                    CheckLastError();
                }
                else
                {
                    GLTexSubImage2D(
                        TextureTarget.Texture2D,
                        (int)mipLevel,
                        (int)x,
                        (int)y,
                        width,
                        height,
                        glTex.GLPixelFormat,
                        glTex.GLPixelType,
                        dataPtr.ToPointer());
                    CheckLastError();
                }
            }
            else if (texTarget == TextureTarget.Texture2DArray)
            {
                if (isCompressed)
                {
                    GLCompressedTexSubImage3D(
                        TextureTarget.Texture2DArray,
                        (int)mipLevel,
                        (int)x,
                        (int)y,
                        (int)arrayLayer,
                        width,
                        height,
                        1,
                        glTex.GLInternalFormat,
                        depthPitch,
                        dataPtr.ToPointer());
                    CheckLastError();
                }
                else
                {
                    GLTexSubImage3D(
                        TextureTarget.Texture2DArray,
                        (int)mipLevel,
                        (int)x,
                        (int)y,
                        (int)arrayLayer,
                        width,
                        height,
                        1,
                        glTex.GLPixelFormat,
                        glTex.GLPixelType,
                        dataPtr.ToPointer());
                    CheckLastError();
                }
            }
            else if (texTarget == TextureTarget.Texture3D)
            {
                if (isCompressed)
                {
                    GLCompressedTexSubImage3D(
                        TextureTarget.Texture3D,
                        (int)mipLevel,
                        (int)x,
                        (int)y,
                        (int)z,
                        width,
                        height,
                        depth,
                        glTex.GLInternalFormat,
                        depthPitch * depth,
                        dataPtr.ToPointer());
                    CheckLastError();
                }
                else
                {
                    GLTexSubImage3D(
                        TextureTarget.Texture3D,
                        (int)mipLevel,
                        (int)x,
                        (int)y,
                        (int)z,
                        width,
                        height,
                        depth,
                        glTex.GLPixelFormat,
                        glTex.GLPixelType,
                        dataPtr.ToPointer());
                    CheckLastError();
                }
            }
            else if (texTarget == TextureTarget.TextureCubeMap)
            {
                var cubeTarget = getCubeTarget(arrayLayer);

                if (isCompressed)
                {
                    GLCompressedTexSubImage2D(
                        cubeTarget,
                        (int)mipLevel,
                        (int)x,
                        (int)y,
                        width,
                        height,
                        glTex.GLInternalFormat,
                        depthPitch,
                        dataPtr.ToPointer());
                    CheckLastError();
                }
                else
                {
                    GLTexSubImage2D(
                        cubeTarget,
                        (int)mipLevel,
                        (int)x,
                        (int)y,
                        width,
                        height,
                        glTex.GLPixelFormat,
                        glTex.GLPixelType,
                        dataPtr.ToPointer());
                    CheckLastError();
                }
            }
            else if (texTarget == TextureTarget.TextureCubeMapArray)
            {
                if (isCompressed)
                {
                    GLCompressedTexSubImage3D(
                        TextureTarget.TextureCubeMapArray,
                        (int)mipLevel,
                        (int)x,
                        (int)y,
                        (int)arrayLayer,
                        width,
                        height,
                        1,
                        glTex.GLInternalFormat,
                        depthPitch,
                        dataPtr.ToPointer());
                    CheckLastError();
                }
                else
                {
                    GLTexSubImage3D(
                        TextureTarget.TextureCubeMapArray,
                        (int)mipLevel,
                        (int)x,
                        (int)y,
                        (int)arrayLayer,
                        width,
                        height,
                        1,
                        glTex.GLPixelFormat,
                        glTex.GLPixelType,
                        dataPtr.ToPointer());
                    CheckLastError();
                }
            }
            else
                throw new VeldridException($"Invalid OpenGL TextureTarget encountered: {glTex.TextureTarget}.");

            if (unpackAlignment < 4)
            {
                GLPixelStorei(PixelStoreParameter.UnpackAlignment, 4);
                CheckLastError();
            }
        }

        public void CopyBuffer(DeviceBuffer source, uint sourceOffset, DeviceBuffer destination, uint destinationOffset, uint sizeInBytes)
        {
            var srcGLBuffer = Util.AssertSubtype<DeviceBuffer, OpenGLBuffer>(source);
            var dstGLBuffer = Util.AssertSubtype<DeviceBuffer, OpenGLBuffer>(destination);

            srcGLBuffer.EnsureResourcesCreated();
            dstGLBuffer.EnsureResourcesCreated();

            if (extensions.ArbDirectStateAccess)
            {
                GLCopyNamedBufferSubData(
                    srcGLBuffer.Buffer,
                    dstGLBuffer.Buffer,
                    (IntPtr)sourceOffset,
                    (IntPtr)destinationOffset,
                    sizeInBytes);
            }
            else
            {
                GLBindBuffer(BufferTarget.CopyReadBuffer, srcGLBuffer.Buffer);
                CheckLastError();

                GLBindBuffer(BufferTarget.CopyWriteBuffer, dstGLBuffer.Buffer);
                CheckLastError();

                GLCopyBufferSubData(
                    BufferTarget.CopyReadBuffer,
                    BufferTarget.CopyWriteBuffer,
                    (IntPtr)sourceOffset,
                    (IntPtr)destinationOffset,
                    (IntPtr)sizeInBytes);
                CheckLastError();
            }
        }

        public void CopyTexture(
            Texture source,
            uint srcX, uint srcY, uint srcZ,
            uint srcMipLevel,
            uint srcBaseArrayLayer,
            Texture destination,
            uint dstX, uint dstY, uint dstZ,
            uint dstMipLevel,
            uint dstBaseArrayLayer,
            uint width, uint height, uint depth,
            uint layerCount)
        {
            var srcGLTexture = Util.AssertSubtype<Texture, OpenGLTexture>(source);
            var dstGLTexture = Util.AssertSubtype<Texture, OpenGLTexture>(destination);

            srcGLTexture.EnsureResourcesCreated();
            dstGLTexture.EnsureResourcesCreated();

            if (extensions.CopyImage && depth == 1)
            {
                // glCopyImageSubData does not work properly when depth > 1, so use the awful roundabout copy.
                uint srcZOrLayer = Math.Max(srcBaseArrayLayer, srcZ);
                uint dstZOrLayer = Math.Max(dstBaseArrayLayer, dstZ);
                uint depthOrLayerCount = Math.Max(depth, layerCount);
                // Copy width and height are allowed to be a full compressed block size, even if the mip level only contains a
                // region smaller than the block size.
                Util.GetMipDimensions(source, srcMipLevel, out uint mipWidth, out uint mipHeight, out _);
                width = Math.Min(width, mipWidth);
                height = Math.Min(height, mipHeight);
                GLCopyImageSubData(
                    srcGLTexture.Texture, srcGLTexture.TextureTarget, (int)srcMipLevel, (int)srcX, (int)srcY, (int)srcZOrLayer,
                    dstGLTexture.Texture, dstGLTexture.TextureTarget, (int)dstMipLevel, (int)dstX, (int)dstY, (int)dstZOrLayer,
                    width, height, depthOrLayerCount);
                CheckLastError();
            }
            else
            {
                for (uint layer = 0; layer < layerCount; layer++)
                {
                    uint srcLayer = layer + srcBaseArrayLayer;
                    uint dstLayer = layer + dstBaseArrayLayer;
                    copyRoundabout(
                        srcGLTexture, dstGLTexture,
                        srcX, srcY, srcZ, srcMipLevel, srcLayer,
                        dstX, dstY, dstZ, dstMipLevel, dstLayer,
                        width, height, depth);
                }
            }
        }

        internal void Dispatch(uint groupCountX, uint groupCountY, uint groupCountZ)
        {
            preDispatchCommand();

            GLDispatchCompute(groupCountX, groupCountY, groupCountZ);
            CheckLastError();

            postDispatchCommand();
        }

        private static void postDispatchCommand()
        {
            // TODO: Smart barriers?
            GLMemoryBarrier(MemoryBarrierFlags.AllBarrierBits);
            CheckLastError();
        }

        private static void copyWithFbo(
            OpenGLTextureSamplerManager textureSamplerManager,
            OpenGLTexture srcGLTexture, OpenGLTexture dstGLTexture,
            uint srcX, uint srcY, uint srcZ, uint srcMipLevel, uint srcBaseArrayLayer,
            uint dstX, uint dstY, uint dstZ, uint dstMipLevel, uint dstBaseArrayLayer,
            uint width, uint height, uint depth, uint layerCount, uint layer)
        {
            var dstTarget = dstGLTexture.TextureTarget;

            if (dstTarget == TextureTarget.Texture2D)
            {
                GLBindFramebuffer(
                    FramebufferTarget.ReadFramebuffer,
                    srcGLTexture.GetFramebuffer(srcMipLevel, srcBaseArrayLayer + layer));
                CheckLastError();

                textureSamplerManager.SetTextureTransient(TextureTarget.Texture2D, dstGLTexture.Texture);
                CheckLastError();

                GLCopyTexSubImage2D(
                    TextureTarget.Texture2D,
                    (int)dstMipLevel,
                    (int)dstX, (int)dstY,
                    (int)srcX, (int)srcY,
                    width, height);
                CheckLastError();
            }
            else if (dstTarget == TextureTarget.Texture2DArray)
            {
                GLBindFramebuffer(
                    FramebufferTarget.ReadFramebuffer,
                    srcGLTexture.GetFramebuffer(srcMipLevel, srcBaseArrayLayer + layerCount));

                textureSamplerManager.SetTextureTransient(TextureTarget.Texture2DArray, dstGLTexture.Texture);
                CheckLastError();

                GLCopyTexSubImage3D(
                    TextureTarget.Texture2DArray,
                    (int)dstMipLevel,
                    (int)dstX,
                    (int)dstY,
                    (int)(dstBaseArrayLayer + layer),
                    (int)srcX,
                    (int)srcY,
                    width,
                    height);
                CheckLastError();
            }
            else if (dstTarget == TextureTarget.Texture3D)
            {
                textureSamplerManager.SetTextureTransient(TextureTarget.Texture3D, dstGLTexture.Texture);
                CheckLastError();

                for (uint i = srcZ; i < srcZ + depth; i++)
                {
                    GLCopyTexSubImage3D(
                        TextureTarget.Texture3D,
                        (int)dstMipLevel,
                        (int)dstX,
                        (int)dstY,
                        (int)dstZ,
                        (int)srcX,
                        (int)srcY,
                        width,
                        height);
                }

                CheckLastError();
            }
        }

        private void preDrawCommand()
        {
            if (!graphicsPipelineActive) activateGraphicsPipeline();

            flushResourceSets(true);

            if (!vertexLayoutFlushed)
            {
                flushVertexLayouts();
                vertexLayoutFlushed = true;
            }
        }

        private void flushResourceSets(bool graphics)
        {
            uint sets = graphics
                ? (uint)graphicsPipeline.ResourceLayouts.Length
                : (uint)computePipeline.ResourceLayouts.Length;

            for (uint slot = 0; slot < sets; slot++)
            {
                var brsi = graphics ? graphicsResourceSets[slot] : computeResourceSets[slot];
                var glSet = Util.AssertSubtype<ResourceSet, OpenGLResourceSet>(brsi.Set);
                var layoutElements = glSet.Layout.Elements;
                bool isNew = graphics ? newGraphicsResourceSets[slot] : newComputeResourceSets[slot];

                activateResourceSet(slot, graphics, brsi, layoutElements, isNew);
            }

            Util.ClearArray(graphics ? newGraphicsResourceSets : newComputeResourceSets);
        }

        private void flushVertexLayouts()
        {
            uint totalSlotsBound = 0;
            var layouts = graphicsPipeline.VertexLayouts;

            for (int i = 0; i < layouts.Length; i++)
            {
                var input = layouts[i];
                var vb = vertexBuffers[i];
                GLBindBuffer(BufferTarget.ArrayBuffer, vb.Buffer);
                uint offset = 0;
                uint vbOffset = vbOffsets[i];

                for (uint slot = 0; slot < input.Elements.Length; slot++)
                {
                    ref var element = ref input.Elements[slot]; // Large structure -- use by reference.
                    uint actualSlot = totalSlotsBound + slot;
                    if (actualSlot >= vertexAttributesBound) GLEnableVertexAttribArray(actualSlot);
                    var type = OpenGLFormats.VdToGLVertexAttribPointerType(
                        element.Format,
                        out bool normalized,
                        out bool isInteger);

                    uint actualOffset = element.Offset != 0 ? element.Offset : offset;
                    actualOffset += vbOffset;

                    if (isInteger && !normalized)
                    {
                        GLVertexAttribIPointer(
                            actualSlot,
                            FormatHelpers.GetElementCount(element.Format),
                            type,
                            (uint)graphicsPipeline.VertexStrides[i],
                            (void*)actualOffset);
                        CheckLastError();
                    }
                    else
                    {
                        GLVertexAttribPointer(
                            actualSlot,
                            FormatHelpers.GetElementCount(element.Format),
                            type,
                            normalized,
                            (uint)graphicsPipeline.VertexStrides[i],
                            (void*)actualOffset);
                        CheckLastError();
                    }

                    uint stepRate = input.InstanceStepRate;

                    if (vertexAttribDivisors[actualSlot] != stepRate)
                    {
                        GLVertexAttribDivisor(actualSlot, stepRate);
                        vertexAttribDivisors[actualSlot] = stepRate;
                    }

                    offset += FormatSizeHelpers.GetSizeInBytes(element.Format);
                }

                totalSlotsBound += (uint)input.Elements.Length;
            }

            for (uint extraSlot = totalSlotsBound; extraSlot < vertexAttributesBound; extraSlot++) GLDisableVertexAttribArray(extraSlot);

            vertexAttributesBound = totalSlotsBound;
        }

        private void preDispatchCommand()
        {
            if (graphicsPipelineActive) activateComputePipeline();

            flushResourceSets(false);
        }

        private void activateGraphicsPipeline()
        {
            graphicsPipelineActive = true;
            graphicsPipeline.EnsureResourcesCreated();

            Util.EnsureArrayMinimumSize(ref graphicsResourceSets, (uint)graphicsPipeline.ResourceLayouts.Length);
            Util.EnsureArrayMinimumSize(ref newGraphicsResourceSets, (uint)graphicsPipeline.ResourceLayouts.Length);

            // Force ResourceSets to be re-bound.
            for (int i = 0; i < graphicsPipeline.ResourceLayouts.Length; i++) newGraphicsResourceSets[i] = true;

            // Blend State

            var blendState = graphicsPipeline.BlendState;
            GLBlendColor(blendState.BlendFactor.R, blendState.BlendFactor.G, blendState.BlendFactor.B, blendState.BlendFactor.A);
            CheckLastError();

            if (blendState.AlphaToCoverageEnabled)
            {
                GLEnable(EnableCap.SampleAlphaToCoverage);
                CheckLastError();
            }
            else
            {
                GLDisable(EnableCap.SampleAlphaToCoverage);
                CheckLastError();
            }

            if (features.IndependentBlend)
            {
                for (uint i = 0; i < blendState.AttachmentStates.Length; i++)
                {
                    var attachment = blendState.AttachmentStates[i];
                    var colorMask = attachment.ColorWriteMask.GetOrDefault();

                    GLColorMaski(
                        i,
                        (colorMask & ColorWriteMask.Red) == ColorWriteMask.Red,
                        (colorMask & ColorWriteMask.Green) == ColorWriteMask.Green,
                        (colorMask & ColorWriteMask.Blue) == ColorWriteMask.Blue,
                        (colorMask & ColorWriteMask.Alpha) == ColorWriteMask.Alpha);
                    CheckLastError();

                    if (!attachment.BlendEnabled)
                    {
                        GLDisablei(EnableCap.Blend, i);
                        CheckLastError();
                    }
                    else
                    {
                        GLEnablei(EnableCap.Blend, i);
                        CheckLastError();

                        GLBlendFuncSeparatei(
                            i,
                            OpenGLFormats.VdToGLBlendFactorSrc(attachment.SourceColorFactor),
                            OpenGLFormats.VdToGLBlendFactorDest(attachment.DestinationColorFactor),
                            OpenGLFormats.VdToGLBlendFactorSrc(attachment.SourceAlphaFactor),
                            OpenGLFormats.VdToGLBlendFactorDest(attachment.DestinationAlphaFactor));
                        CheckLastError();

                        GLBlendEquationSeparatei(
                            i,
                            OpenGLFormats.VdToGLBlendEquationMode(attachment.ColorFunction),
                            OpenGLFormats.VdToGLBlendEquationMode(attachment.AlphaFunction));
                        CheckLastError();
                    }
                }
            }
            else if (blendState.AttachmentStates.Length > 0)
            {
                var attachment = blendState.AttachmentStates[0];
                var colorMask = attachment.ColorWriteMask.GetOrDefault();

                GLColorMask(
                    (colorMask & ColorWriteMask.Red) == ColorWriteMask.Red,
                    (colorMask & ColorWriteMask.Green) == ColorWriteMask.Green,
                    (colorMask & ColorWriteMask.Blue) == ColorWriteMask.Blue,
                    (colorMask & ColorWriteMask.Alpha) == ColorWriteMask.Alpha);
                CheckLastError();

                if (!attachment.BlendEnabled)
                {
                    GLDisable(EnableCap.Blend);
                    CheckLastError();
                }
                else
                {
                    GLEnable(EnableCap.Blend);
                    CheckLastError();

                    GLBlendFuncSeparate(
                        OpenGLFormats.VdToGLBlendFactorSrc(attachment.SourceColorFactor),
                        OpenGLFormats.VdToGLBlendFactorDest(attachment.DestinationColorFactor),
                        OpenGLFormats.VdToGLBlendFactorSrc(attachment.SourceAlphaFactor),
                        OpenGLFormats.VdToGLBlendFactorDest(attachment.DestinationAlphaFactor));
                    CheckLastError();

                    GLBlendEquationSeparate(
                        OpenGLFormats.VdToGLBlendEquationMode(attachment.ColorFunction),
                        OpenGLFormats.VdToGLBlendEquationMode(attachment.AlphaFunction));
                    CheckLastError();
                }
            }

            // Depth Stencil State

            var dss = graphicsPipeline.DepthStencilState;

            if (!dss.DepthTestEnabled)
            {
                GLDisable(EnableCap.DepthTest);
                CheckLastError();
            }
            else
            {
                GLEnable(EnableCap.DepthTest);
                CheckLastError();

                GLDepthFunc(OpenGLFormats.VdToGLDepthFunction(dss.DepthComparison));
                CheckLastError();
            }

            GLDepthMask(dss.DepthWriteEnabled);
            CheckLastError();

            if (dss.StencilTestEnabled)
            {
                GLEnable(EnableCap.StencilTest);
                CheckLastError();

                GLStencilFuncSeparate(
                    CullFaceMode.Front,
                    OpenGLFormats.VdToGLStencilFunction(dss.StencilFront.Comparison),
                    (int)dss.StencilReference,
                    dss.StencilReadMask);
                CheckLastError();

                GLStencilOpSeparate(
                    CullFaceMode.Front,
                    OpenGLFormats.VdToGLStencilOp(dss.StencilFront.Fail),
                    OpenGLFormats.VdToGLStencilOp(dss.StencilFront.DepthFail),
                    OpenGLFormats.VdToGLStencilOp(dss.StencilFront.Pass));
                CheckLastError();

                GLStencilFuncSeparate(
                    CullFaceMode.Back,
                    OpenGLFormats.VdToGLStencilFunction(dss.StencilBack.Comparison),
                    (int)dss.StencilReference,
                    dss.StencilReadMask);
                CheckLastError();

                GLStencilOpSeparate(
                    CullFaceMode.Back,
                    OpenGLFormats.VdToGLStencilOp(dss.StencilBack.Fail),
                    OpenGLFormats.VdToGLStencilOp(dss.StencilBack.DepthFail),
                    OpenGLFormats.VdToGLStencilOp(dss.StencilBack.Pass));
                CheckLastError();

                GLStencilMask(dss.StencilWriteMask);
                CheckLastError();
            }
            else
            {
                GLDisable(EnableCap.StencilTest);
                CheckLastError();
            }

            // Rasterizer State

            var rs = graphicsPipeline.RasterizerState;

            if (rs.CullMode == FaceCullMode.None)
            {
                GLDisable(EnableCap.CullFace);
                CheckLastError();
            }
            else
            {
                GLEnable(EnableCap.CullFace);
                CheckLastError();

                GLCullFace(OpenGLFormats.VdToGLCullFaceMode(rs.CullMode));
                CheckLastError();
            }

            if (backend == GraphicsBackend.OpenGL)
            {
                GLPolygonMode(MaterialFace.FrontAndBack, OpenGLFormats.VdToGLPolygonMode(rs.FillMode));
                CheckLastError();
            }

            if (!rs.ScissorTestEnabled)
            {
                GLDisable(EnableCap.ScissorTest);
                CheckLastError();
            }
            else
            {
                GLEnable(EnableCap.ScissorTest);
                CheckLastError();
            }

            if (backend == GraphicsBackend.OpenGL)
            {
                if (!rs.DepthClipEnabled)
                {
                    GLEnable(EnableCap.DepthClamp);
                    CheckLastError();
                }
                else
                {
                    GLDisable(EnableCap.DepthClamp);
                    CheckLastError();
                }
            }

            GLFrontFace(OpenGLFormats.VdToGLFrontFaceDirection(rs.FrontFace));
            CheckLastError();

            // Primitive Topology
            primitiveType = OpenGLFormats.VdToGLPrimitiveType(graphicsPipeline.PrimitiveTopology);

            // Shader Set
            GLUseProgram(graphicsPipeline.Program);
            CheckLastError();

            int vertexStridesCount = graphicsPipeline.VertexStrides.Length;
            Util.EnsureArrayMinimumSize(ref vertexBuffers, (uint)vertexStridesCount);
            Util.EnsureArrayMinimumSize(ref vbOffsets, (uint)vertexStridesCount);

            uint totalVertexElements = 0;
            for (int i = 0; i < graphicsPipeline.VertexLayouts.Length; i++) totalVertexElements += (uint)graphicsPipeline.VertexLayouts[i].Elements.Length;
            Util.EnsureArrayMinimumSize(ref vertexAttribDivisors, totalVertexElements);
        }

        private void activateComputePipeline()
        {
            graphicsPipelineActive = false;
            computePipeline.EnsureResourcesCreated();
            Util.EnsureArrayMinimumSize(ref computeResourceSets, (uint)computePipeline.ResourceLayouts.Length);
            Util.EnsureArrayMinimumSize(ref newComputeResourceSets, (uint)computePipeline.ResourceLayouts.Length);

            // Force ResourceSets to be re-bound.
            for (int i = 0; i < computePipeline.ResourceLayouts.Length; i++) newComputeResourceSets[i] = true;

            // Shader Set
            GLUseProgram(computePipeline.Program);
            CheckLastError();
        }

        private void activateResourceSet(
            uint slot,
            bool graphics,
            BoundResourceSetInfo brsi,
            ResourceLayoutElementDescription[] layoutElements,
            bool isNew)
        {
            var glResourceSet = Util.AssertSubtype<ResourceSet, OpenGLResourceSet>(brsi.Set);
            var pipeline = graphics ? graphicsPipeline : computePipeline;
            uint ubBaseIndex = getUniformBaseIndex(slot, graphics);
            uint ssboBaseIndex = getShaderStorageBaseIndex(slot, graphics);

            uint ubOffset = 0;
            uint ssboOffset = 0;
            uint dynamicOffsetIndex = 0;

            for (uint element = 0; element < glResourceSet.Resources.Length; element++)
            {
                var kind = layoutElements[element].Kind;
                var resource = glResourceSet.Resources[(int)element];

                uint bufferOffset = 0;

                if (glResourceSet.Layout.IsDynamicBuffer(element))
                {
                    bufferOffset = brsi.Offsets.Get(dynamicOffsetIndex);
                    dynamicOffsetIndex += 1;
                }

                switch (kind)
                {
                    case ResourceKind.UniformBuffer:
                    {
                        if (!isNew) continue;

                        var range = Util.GetBufferRange(resource, bufferOffset);
                        var glUb = Util.AssertSubtype<DeviceBuffer, OpenGLBuffer>(range.Buffer);

                        glUb.EnsureResourcesCreated();

                        if (pipeline.GetUniformBindingForSlot(slot, element, out var uniformBindingInfo))
                        {
                            if (range.SizeInBytes < uniformBindingInfo.BlockSize)
                            {
                                string name = glResourceSet.Layout.Elements[element].Name;
                                throw new VeldridException(
                                    $"Not enough data in uniform buffer \"{name}\" (slot {slot}, element {element}). Shader expects at least {uniformBindingInfo.BlockSize} bytes, but buffer only contains {range.SizeInBytes} bytes");
                            }

                            GLUniformBlockBinding(pipeline.Program, uniformBindingInfo.BlockLocation, ubBaseIndex + ubOffset);
                            CheckLastError();

                            GLBindBufferRange(
                                BufferRangeTarget.UniformBuffer,
                                ubBaseIndex + ubOffset,
                                glUb.Buffer,
                                (IntPtr)range.Offset,
                                range.SizeInBytes);
                            CheckLastError();

                            ubOffset += 1;
                        }

                        break;
                    }

                    case ResourceKind.StructuredBufferReadWrite:
                    case ResourceKind.StructuredBufferReadOnly:
                    {
                        if (!isNew) continue;

                        var range = Util.GetBufferRange(resource, bufferOffset);
                        var glBuffer = Util.AssertSubtype<DeviceBuffer, OpenGLBuffer>(range.Buffer);

                        glBuffer.EnsureResourcesCreated();

                        if (pipeline.GetStorageBufferBindingForSlot(slot, element, out var shaderStorageBinding))
                        {
                            if (backend == GraphicsBackend.OpenGL)
                            {
                                GLShaderStorageBlockBinding(
                                    pipeline.Program,
                                    shaderStorageBinding.StorageBlockBinding,
                                    ssboBaseIndex + ssboOffset);
                                CheckLastError();

                                GLBindBufferRange(
                                    BufferRangeTarget.ShaderStorageBuffer,
                                    ssboBaseIndex + ssboOffset,
                                    glBuffer.Buffer,
                                    (IntPtr)range.Offset,
                                    range.SizeInBytes);
                                CheckLastError();
                            }
                            else
                            {
                                GLBindBufferRange(
                                    BufferRangeTarget.ShaderStorageBuffer,
                                    shaderStorageBinding.StorageBlockBinding,
                                    glBuffer.Buffer,
                                    (IntPtr)range.Offset,
                                    range.SizeInBytes);
                                CheckLastError();
                            }

                            ssboOffset += 1;
                        }

                        break;
                    }

                    case ResourceKind.TextureReadOnly:
                        var texView = Util.GetTextureView(gd, resource);
                        var glTexView = Util.AssertSubtype<TextureView, OpenGLTextureView>(texView);
                        glTexView.EnsureResourcesCreated();

                        if (pipeline.GetTextureBindingInfo(slot, element, out var textureBindingInfo))
                        {
                            textureSamplerManager.SetTexture((uint)textureBindingInfo.RelativeIndex, glTexView);
                            GLUniform1I(textureBindingInfo.UniformLocation, textureBindingInfo.RelativeIndex);
                            CheckLastError();
                        }

                        break;

                    case ResourceKind.TextureReadWrite:
                        var texViewRw = Util.GetTextureView(gd, resource);
                        var glTexViewRw = Util.AssertSubtype<TextureView, OpenGLTextureView>(texViewRw);
                        glTexViewRw.EnsureResourcesCreated();

                        if (pipeline.GetTextureBindingInfo(slot, element, out var imageBindingInfo))
                        {
                            bool layered = texViewRw.Target.Usage.HasFlag(TextureUsage.Cubemap) || texViewRw.ArrayLayers > 1;

                            if (layered && (texViewRw.BaseArrayLayer > 0
                                            || (texViewRw.ArrayLayers > 1 && texViewRw.ArrayLayers < texViewRw.Target.ArrayLayers)))
                            {
                                throw new VeldridException(
                                    "Cannot bind texture with BaseArrayLayer > 0 and ArrayLayers > 1, or with an incomplete set of array layers (cubemaps have ArrayLayers == 6 implicitly).");
                            }

                            if (backend == GraphicsBackend.OpenGL)
                            {
                                GLBindImageTexture(
                                    (uint)imageBindingInfo.RelativeIndex,
                                    glTexViewRw.Target.Texture,
                                    (int)texViewRw.BaseMipLevel,
                                    layered,
                                    (int)texViewRw.BaseArrayLayer,
                                    TextureAccess.ReadWrite,
                                    glTexViewRw.GetReadWriteSizedInternalFormat());
                                CheckLastError();
                                GLUniform1I(imageBindingInfo.UniformLocation, imageBindingInfo.RelativeIndex);
                                CheckLastError();
                            }
                            else
                            {
                                GLBindImageTexture(
                                    (uint)imageBindingInfo.RelativeIndex,
                                    glTexViewRw.Target.Texture,
                                    (int)texViewRw.BaseMipLevel,
                                    layered,
                                    (int)texViewRw.BaseArrayLayer,
                                    TextureAccess.ReadWrite,
                                    glTexViewRw.GetReadWriteSizedInternalFormat());
                                CheckLastError();
                            }
                        }

                        break;

                    case ResourceKind.Sampler:
                        var glSampler = Util.AssertSubtype<IBindableResource, OpenGLSampler>(resource);
                        glSampler.EnsureResourcesCreated();

                        if (pipeline.GetSamplerBindingInfo(slot, element, out var samplerBindingInfo))
                        {
                            foreach (int index in samplerBindingInfo.RelativeIndices) textureSamplerManager.SetSampler((uint)index, glSampler);
                        }

                        break;

                    default: throw Illegal.Value<ResourceKind>();
                }
            }
        }

        private uint getUniformBaseIndex(uint slot, bool graphics)
        {
            var pipeline = graphics ? graphicsPipeline : computePipeline;
            uint ret = 0;
            for (uint i = 0; i < slot; i++) ret += pipeline.GetUniformBufferCount(i);

            return ret;
        }

        private uint getShaderStorageBaseIndex(uint slot, bool graphics)
        {
            var pipeline = graphics ? graphicsPipeline : computePipeline;
            uint ret = 0;
            for (uint i = 0; i < slot; i++) ret += pipeline.GetShaderStorageBufferCount(i);

            return ret;
        }

        private TextureTarget getCubeTarget(uint arrayLayer)
        {
            switch (arrayLayer)
            {
                case 0:
                    return TextureTarget.TextureCubeMapPositiveX;

                case 1:
                    return TextureTarget.TextureCubeMapNegativeX;

                case 2:
                    return TextureTarget.TextureCubeMapPositiveY;

                case 3:
                    return TextureTarget.TextureCubeMapNegativeY;

                case 4:
                    return TextureTarget.TextureCubeMapPositiveZ;

                case 5:
                    return TextureTarget.TextureCubeMapNegativeZ;

                default:
                    throw new VeldridException("Unexpected array layer in UpdateTexture called on a cubemap texture.");
            }
        }

        private void copyRoundabout(
            OpenGLTexture srcGLTexture, OpenGLTexture dstGLTexture,
            uint srcX, uint srcY, uint srcZ, uint srcMipLevel, uint srcLayer,
            uint dstX, uint dstY, uint dstZ, uint dstMipLevel, uint dstLayer,
            uint width, uint height, uint depth)
        {
            bool isCompressed = FormatHelpers.IsCompressedFormat(srcGLTexture.Format);
            if (srcGLTexture.Format != dstGLTexture.Format) throw new VeldridException("Copying to/from Textures with different formats is not supported.");

            uint packAlignment = 4;
            uint depthSliceSize = 0;
            uint sizeInBytes;
            var srcTarget = srcGLTexture.TextureTarget;

            if (isCompressed)
            {
                textureSamplerManager.SetTextureTransient(srcTarget, srcGLTexture.Texture);
                CheckLastError();

                int compressedSize;
                GLGetTexLevelParameteriv(
                    srcTarget,
                    (int)srcMipLevel,
                    GetTextureParameter.TextureCompressedImageSize,
                    &compressedSize);
                CheckLastError();
                sizeInBytes = (uint)compressedSize;
            }
            else
            {
                uint pixelSize = FormatSizeHelpers.GetSizeInBytes(srcGLTexture.Format);
                packAlignment = pixelSize;
                depthSliceSize = width * height * pixelSize;
                sizeInBytes = depthSliceSize * depth;
            }

            var block = stagingMemoryPool.GetStagingBlock(sizeInBytes);

            if (packAlignment < 4)
            {
                GLPixelStorei(PixelStoreParameter.PackAlignment, (int)packAlignment);
                CheckLastError();
            }

            if (isCompressed)
            {
                if (extensions.ArbDirectStateAccess)
                {
                    GLGetCompressedTextureImage(
                        srcGLTexture.Texture,
                        (int)srcMipLevel,
                        block.SizeInBytes,
                        block.Data);
                    CheckLastError();
                }
                else
                {
                    textureSamplerManager.SetTextureTransient(srcTarget, srcGLTexture.Texture);
                    CheckLastError();

                    GLGetCompressedTexImage(srcTarget, (int)srcMipLevel, block.Data);
                    CheckLastError();
                }

                var dstTarget = dstGLTexture.TextureTarget;
                textureSamplerManager.SetTextureTransient(dstTarget, dstGLTexture.Texture);
                CheckLastError();

                Util.GetMipDimensions(srcGLTexture, srcMipLevel, out uint mipWidth, out uint mipHeight, out uint _);
                uint fullRowPitch = FormatHelpers.GetRowPitch(mipWidth, srcGLTexture.Format);
                uint fullDepthPitch = FormatHelpers.GetDepthPitch(
                    fullRowPitch,
                    mipHeight,
                    srcGLTexture.Format);

                uint denseRowPitch = FormatHelpers.GetRowPitch(width, srcGLTexture.Format);
                uint denseDepthPitch = FormatHelpers.GetDepthPitch(denseRowPitch, height, srcGLTexture.Format);
                uint numRows = FormatHelpers.GetNumRows(height, srcGLTexture.Format);
                uint trueCopySize = denseRowPitch * numRows;
                var trueCopySrc = stagingMemoryPool.GetStagingBlock(trueCopySize);

                uint layerStartOffset = denseDepthPitch * srcLayer;

                Util.CopyTextureRegion(
                    (byte*)block.Data + layerStartOffset,
                    srcX, srcY, srcZ,
                    fullRowPitch, fullDepthPitch,
                    trueCopySrc.Data,
                    0, 0, 0,
                    denseRowPitch,
                    denseDepthPitch,
                    width, height, depth,
                    srcGLTexture.Format);

                UpdateTexture(
                    dstGLTexture,
                    (IntPtr)trueCopySrc.Data,
                    dstX, dstY, dstZ,
                    width, height, 1,
                    dstMipLevel, dstLayer);

                stagingMemoryPool.Free(trueCopySrc);
            }
            else // !isCompressed
            {
                if (extensions.ArbDirectStateAccess)
                {
                    GLGetTextureSubImage(
                        srcGLTexture.Texture, (int)srcMipLevel, (int)srcX, (int)srcY, (int)srcZ,
                        width, height, depth,
                        srcGLTexture.GLPixelFormat, srcGLTexture.GLPixelType, block.SizeInBytes, block.Data);
                    CheckLastError();
                }
                else
                {
                    for (uint layer = 0; layer < depth; layer++)
                    {
                        uint curLayer = srcZ + srcLayer + layer;
                        uint curOffset = depthSliceSize * layer;
                        GLGenFramebuffers(1, out uint readFb);
                        CheckLastError();
                        GLBindFramebuffer(FramebufferTarget.ReadFramebuffer, readFb);
                        CheckLastError();

                        if (srcGLTexture.ArrayLayers > 1 || srcGLTexture.Type == TextureType.Texture3D
                                                         || (srcGLTexture.Usage & TextureUsage.Cubemap) != 0)
                        {
                            GLFramebufferTextureLayer(
                                FramebufferTarget.ReadFramebuffer,
                                GLFramebufferAttachment.ColorAttachment0,
                                srcGLTexture.Texture,
                                (int)srcMipLevel,
                                (int)curLayer);
                            CheckLastError();
                        }
                        else if (srcGLTexture.Type == TextureType.Texture1D)
                        {
                            GLFramebufferTexture1D(
                                FramebufferTarget.ReadFramebuffer,
                                GLFramebufferAttachment.ColorAttachment0,
                                TextureTarget.Texture1D,
                                srcGLTexture.Texture,
                                (int)srcMipLevel);
                            CheckLastError();
                        }
                        else
                        {
                            GLFramebufferTexture2D(
                                FramebufferTarget.ReadFramebuffer,
                                GLFramebufferAttachment.ColorAttachment0,
                                TextureTarget.Texture2D,
                                srcGLTexture.Texture,
                                (int)srcMipLevel);
                            CheckLastError();
                        }

                        CheckLastError();
                        GLReadPixels(
                            (int)srcX, (int)srcY,
                            width, height,
                            srcGLTexture.GLPixelFormat,
                            srcGLTexture.GLPixelType,
                            (byte*)block.Data + curOffset);
                        CheckLastError();
                        GLDeleteFramebuffers(1, ref readFb);
                        CheckLastError();
                    }
                }

                UpdateTexture(
                    dstGLTexture,
                    (IntPtr)block.Data,
                    dstX, dstY, dstZ,
                    width, height, depth, dstMipLevel, dstLayer);
            }

            if (packAlignment < 4)
            {
                GLPixelStorei(PixelStoreParameter.PackAlignment, 4);
                CheckLastError();
            }

            stagingMemoryPool.Free(block);
        }
    }
}

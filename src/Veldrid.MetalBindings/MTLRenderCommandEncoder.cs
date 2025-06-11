// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.InteropServices;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MTLRenderCommandEncoder
    {
        public readonly IntPtr NativePtr;

        public MTLRenderCommandEncoder(IntPtr ptr)
        {
            NativePtr = ptr;
        }

        public bool IsNull => NativePtr == IntPtr.Zero;

        public void SetRenderPipelineState(MTLRenderPipelineState pipelineState)
        {
            objc_msgSend(NativePtr, sel_setRenderPipelineState, pipelineState.NativePtr);
        }

        public void SetVertexBuffer(MTLBuffer buffer, UIntPtr offset, UIntPtr index)
        {
            objc_msgSend(NativePtr, sel_setVertexBuffer,
                buffer.NativePtr,
                offset,
                index);
        }

        public void SetVertexBufferOffset(UIntPtr offset, UIntPtr index)
        {
            objc_msgSend(NativePtr, sel_setVertexBufferOffset,
                offset,
                index);
        }

        public void SetFragmentBuffer(MTLBuffer buffer, UIntPtr offset, UIntPtr index)
        {
            objc_msgSend(NativePtr, sel_setFragmentBuffer,
                buffer.NativePtr,
                offset,
                index);
        }

        public void SetFragmentBufferOffset(UIntPtr offset, UIntPtr index)
        {
            objc_msgSend(NativePtr, sel_setFragmentBufferOffset,
                offset,
                index);
        }

        public void SetVertexTexture(MTLTexture texture, UIntPtr index)
        {
            objc_msgSend(NativePtr, sel_setVertexTexture, texture.NativePtr, index);
        }

        public void SetFragmentTexture(MTLTexture texture, UIntPtr index)
        {
            objc_msgSend(NativePtr, sel_setFragmentTexture, texture.NativePtr, index);
        }

        public void SetVertexSamplerState(MTLSamplerState sampler, UIntPtr index)
        {
            objc_msgSend(NativePtr, sel_setVertexSamplerState, sampler.NativePtr, index);
        }

        public void SetFragmentSamplerState(MTLSamplerState sampler, UIntPtr index)
        {
            objc_msgSend(NativePtr, sel_setFragmentSamplerState, sampler.NativePtr, index);
        }

        public void DrawPrimitives(
            MTLPrimitiveType primitiveType,
            UIntPtr vertexStart,
            UIntPtr vertexCount,
            UIntPtr instanceCount,
            UIntPtr baseInstance)
        {
            objc_msgSend(NativePtr, sel_drawPrimitives0,
                primitiveType, vertexStart, vertexCount, instanceCount, baseInstance);
        }

        public void DrawPrimitives(
            MTLPrimitiveType primitiveType,
            UIntPtr vertexStart,
            UIntPtr vertexCount,
            UIntPtr instanceCount)
        {
            objc_msgSend(NativePtr, sel_drawPrimitives2,
                primitiveType, vertexStart, vertexCount, instanceCount);
        }

        public void DrawPrimitives(MTLPrimitiveType primitiveType, MTLBuffer indirectBuffer, UIntPtr indirectBufferOffset)
        {
            objc_msgSend(NativePtr, sel_drawPrimitives1,
                primitiveType, indirectBuffer, indirectBufferOffset);
        }

        public void DrawIndexedPrimitives(
            MTLPrimitiveType primitiveType,
            UIntPtr indexCount,
            MTLIndexType indexType,
            MTLBuffer indexBuffer,
            UIntPtr indexBufferOffset,
            UIntPtr instanceCount)
        {
            objc_msgSend(NativePtr, sel_drawIndexedPrimitives0,
                primitiveType, indexCount, indexType, indexBuffer.NativePtr, indexBufferOffset, instanceCount);
        }

        public void DrawIndexedPrimitives(
            MTLPrimitiveType primitiveType,
            UIntPtr indexCount,
            MTLIndexType indexType,
            MTLBuffer indexBuffer,
            UIntPtr indexBufferOffset,
            UIntPtr instanceCount,
            IntPtr baseVertex,
            UIntPtr baseInstance)
        {
            objc_msgSend(
                NativePtr,
                sel_drawIndexedPrimitives1,
                primitiveType, indexCount, indexType, indexBuffer.NativePtr, indexBufferOffset, instanceCount, baseVertex, baseInstance);
        }

        public void DrawIndexedPrimitives(
            MTLPrimitiveType primitiveType,
            MTLIndexType indexType,
            MTLBuffer indexBuffer,
            UIntPtr indexBufferOffset,
            MTLBuffer indirectBuffer,
            UIntPtr indirectBufferOffset)
        {
            objc_msgSend(NativePtr, sel_drawIndexedPrimitives2,
                primitiveType,
                indexType,
                indexBuffer,
                indexBufferOffset,
                indirectBuffer,
                indirectBufferOffset);
        }

        public void SetViewport(MTLViewport viewport)
        {
            objc_msgSend(NativePtr, sel_setViewport, viewport);
        }

        public unsafe void SetViewports(MTLViewport* viewports, UIntPtr count)
        {
            objc_msgSend(NativePtr, sel_setViewports, viewports, count);
        }

        public void SetScissorRect(MTLScissorRect scissorRect)
        {
            objc_msgSend(NativePtr, sel_setScissorRect, scissorRect);
        }

        public unsafe void SetScissorRects(MTLScissorRect* scissorRects, UIntPtr count)
        {
            objc_msgSend(NativePtr, sel_setScissorRects, scissorRects, count);
        }

        public void SetCullMode(MTLCullMode cullMode)
        {
            objc_msgSend(NativePtr, sel_setCullMode, (uint)cullMode);
        }

        public void SetFrontFacing(MTLWinding frontFaceWinding)
        {
            objc_msgSend(NativePtr, sel_setFrontFacingWinding, (uint)frontFaceWinding);
        }

        public void SetDepthStencilState(MTLDepthStencilState depthStencilState)
        {
            objc_msgSend(NativePtr, sel_setDepthStencilState, depthStencilState.NativePtr);
        }

        public void SetDepthClipMode(MTLDepthClipMode depthClipMode)
        {
            objc_msgSend(NativePtr, sel_setDepthClipMode, (uint)depthClipMode);
        }

        public void EndEncoding()
        {
            objc_msgSend(NativePtr, sel_endEncoding);
        }

        public void SetStencilReferenceValue(uint stencilReference)
        {
            objc_msgSend(NativePtr, sel_setStencilReferenceValue, stencilReference);
        }

        public void SetBlendColor(float red, float green, float blue, float alpha)
        {
            objc_msgSend(NativePtr, sel_setBlendColor, red, green, blue, alpha);
        }

        public void SetTriangleFillMode(MTLTriangleFillMode fillMode)
        {
            objc_msgSend(NativePtr, sel_setTriangleFillMode, (uint)fillMode);
        }

        public void PushDebugGroup(NSString @string)
        {
            objc_msgSend(NativePtr, Selectors.PUSH_DEBUG_GROUP, @string.NativePtr);
        }

        public void PopDebugGroup()
        {
            objc_msgSend(NativePtr, Selectors.POP_DEBUG_GROUP);
        }

        public void InsertDebugSignpost(NSString @string)
        {
            objc_msgSend(NativePtr, Selectors.INSERT_DEBUG_SIGNPOST, @string.NativePtr);
        }

        private static readonly Selector sel_setRenderPipelineState = "setRenderPipelineState:";
        private static readonly Selector sel_setVertexBuffer = "setVertexBuffer:offset:atIndex:";
        private static readonly Selector sel_setVertexBufferOffset = "setVertexBufferOffset:atIndex:";
        private static readonly Selector sel_setFragmentBuffer = "setFragmentBuffer:offset:atIndex:";
        private static readonly Selector sel_setFragmentBufferOffset = "setFragmentBufferOffset:atIndex:";
        private static readonly Selector sel_setVertexTexture = "setVertexTexture:atIndex:";
        private static readonly Selector sel_setFragmentTexture = "setFragmentTexture:atIndex:";
        private static readonly Selector sel_setVertexSamplerState = "setVertexSamplerState:atIndex:";
        private static readonly Selector sel_setFragmentSamplerState = "setFragmentSamplerState:atIndex:";
        private static readonly Selector sel_drawPrimitives0 = "drawPrimitives:vertexStart:vertexCount:instanceCount:baseInstance:";
        private static readonly Selector sel_drawPrimitives1 = "drawPrimitives:indirectBuffer:indirectBufferOffset:";
        private static readonly Selector sel_drawPrimitives2 = "drawPrimitives:vertexStart:vertexCount:instanceCount:";
        private static readonly Selector sel_drawIndexedPrimitives0 = "drawIndexedPrimitives:indexCount:indexType:indexBuffer:indexBufferOffset:instanceCount:";
        private static readonly Selector sel_drawIndexedPrimitives1 = "drawIndexedPrimitives:indexCount:indexType:indexBuffer:indexBufferOffset:instanceCount:baseVertex:baseInstance:";
        private static readonly Selector sel_drawIndexedPrimitives2 = "drawIndexedPrimitives:indexType:indexBuffer:indexBufferOffset:indirectBuffer:indirectBufferOffset:";
        private static readonly Selector sel_setViewport = "setViewport:";
        private static readonly Selector sel_setViewports = "setViewports:count:";
        private static readonly Selector sel_setScissorRect = "setScissorRect:";
        private static readonly Selector sel_setScissorRects = "setScissorRects:count:";
        private static readonly Selector sel_setCullMode = "setCullMode:";
        private static readonly Selector sel_setFrontFacingWinding = "setFrontFacingWinding:";
        private static readonly Selector sel_setDepthStencilState = "setDepthStencilState:";
        private static readonly Selector sel_setDepthClipMode = "setDepthClipMode:";
        private static readonly Selector sel_endEncoding = "endEncoding";
        private static readonly Selector sel_setStencilReferenceValue = "setStencilReferenceValue:";
        private static readonly Selector sel_setBlendColor = "setBlendColorRed:green:blue:alpha:";
        private static readonly Selector sel_setTriangleFillMode = "setTriangleFillMode:";
    }
}

// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.InteropServices;

namespace Veldrid.OpenGLBindings
{
    // uint = uint
    // GLuint = uint
    // GLuint64 = uint64
    // GLenum = uint
    // Glclampf = 32-bit float, [0, 1]
    public static unsafe class OpenGLNative
    {
        private static Func<string, IntPtr> sGetProcAddress;

        private const CallingConvention call_conv = CallingConvention.Winapi;

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLGenVertexArraysT(uint n, out uint arrays);

        private static GLGenVertexArraysT pGLGenVertexArrays;

        public static void GLGenVertexArrays(uint n, out uint arrays)
        {
            pGLGenVertexArrays(n, out arrays);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate uint GLGetErrorT();

        private static GLGetErrorT pGLGetError;

        public static uint GLGetError()
        {
            return pGLGetError();
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLBindVertexArrayT(uint array);

        private static GLBindVertexArrayT pGLBindVertexArray;

        public static void GLBindVertexArray(uint array)
        {
            pGLBindVertexArray(array);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLClearColorT(float red, float green, float blue, float alpha);

        private static GLClearColorT pGLClearColor;

        public static void GLClearColor(float red, float green, float blue, float alpha)
        {
            pGLClearColor(red, green, blue, alpha);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDrawBufferT(DrawBufferMode mode);

        private static GLDrawBufferT pGLDrawBuffer;

        public static void GLDrawBuffer(DrawBufferMode mode)
        {
            pGLDrawBuffer(mode);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDrawBuffersT(uint n, DrawBuffersEnum* bufs);

        private static GLDrawBuffersT pGLDrawBuffers;

        public static void GLDrawBuffers(uint n, DrawBuffersEnum* bufs)
        {
            pGLDrawBuffers(n, bufs);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLClearT(ClearBufferMask mask);

        private static GLClearT pGLClear;

        public static void GLClear(ClearBufferMask mask)
        {
            pGLClear(mask);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLClearDepthT(double depth);

        private static GLClearDepthT pGLClearDepth;

        public static void GLClearDepth(double depth)
        {
            pGLClearDepth(depth);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLClearDepthfT(float depth);

        private static GLClearDepthfT pGLClearDepthf;

        public static void GLClearDepthf(float depth)
        {
            pGLClearDepthf(depth);
        }

        private static GLClearDepthfT pGLClearDepthfCompat;

        public static void glClearDepth_Compat(float depth)
        {
            pGLClearDepthfCompat(depth);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDrawElementsT(PrimitiveType mode, uint count, DrawElementsType type, void* indices);

        private static GLDrawElementsT pGLDrawElements;

        public static void GLDrawElements(PrimitiveType mode, uint count, DrawElementsType type, void* indices)
        {
            pGLDrawElements(mode, count, type, indices);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDrawElementsBaseVertexT(
            PrimitiveType mode,
            uint count,
            DrawElementsType type,
            void* indices,
            int basevertex);

        private static GLDrawElementsBaseVertexT pGLDrawElementsBaseVertex;

        public static void GLDrawElementsBaseVertex(
            PrimitiveType mode,
            uint count,
            DrawElementsType type,
            void* indices,
            int basevertex)
        {
            pGLDrawElementsBaseVertex(mode, count, type, indices, basevertex);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDrawElementsInstancedT(
            PrimitiveType mode,
            uint count,
            DrawElementsType type,
            void* indices,
            uint primcount);

        private static GLDrawElementsInstancedT pGLDrawElementsInstanced;

        public static void GLDrawElementsInstanced(
            PrimitiveType mode,
            uint count,
            DrawElementsType type,
            void* indices,
            uint primcount)
        {
            pGLDrawElementsInstanced(mode, count, type, indices, primcount);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDrawElementsInstancedBaseVertexT(
            PrimitiveType mode,
            uint count,
            DrawElementsType type,
            void* indices,
            uint primcount,
            int basevertex);

        private static GLDrawElementsInstancedBaseVertexT pGLDrawElementsInstancedBaseVertex;

        public static void GLDrawElementsInstancedBaseVertex(
            PrimitiveType mode,
            uint count,
            DrawElementsType type,
            void* indices,
            uint primcount,
            int basevertex)
        {
            pGLDrawElementsInstancedBaseVertex(mode, count, type, indices, primcount, basevertex);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDrawElementsInstancedBaseVertexBaseInstanceT(
            PrimitiveType mode,
            uint count,
            DrawElementsType type,
            void* indices,
            uint primcount,
            int basevertex,
            uint baseinstance);

        private static GLDrawElementsInstancedBaseVertexBaseInstanceT pGLDrawElementsInstancedBaseVertexBaseInstance;

        public static void GLDrawElementsInstancedBaseVertexBaseInstance(
            PrimitiveType mode,
            uint count,
            DrawElementsType type,
            void* indices,
            uint primcount,
            int basevertex,
            uint baseinstance)
        {
            pGLDrawElementsInstancedBaseVertexBaseInstance(
                mode, count, type, indices, primcount, basevertex, baseinstance);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDrawArraysT(PrimitiveType mode, int first, uint count);

        private static GLDrawArraysT pGLDrawArrays;

        public static void GLDrawArrays(PrimitiveType mode, int first, uint count)
        {
            pGLDrawArrays(mode, first, count);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDrawArraysInstancedT(PrimitiveType mode, int first, uint count, uint primcount);

        private static GLDrawArraysInstancedT pGLDrawArraysInstanced;

        public static void GLDrawArraysInstanced(PrimitiveType mode, int first, uint count, uint primcount)
        {
            pGLDrawArraysInstanced(mode, first, count, primcount);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDrawArraysInstancedBaseInstanceT(
            PrimitiveType mode,
            int first,
            uint count,
            uint primcount,
            uint baseinstance);

        private static GLDrawArraysInstancedBaseInstanceT pGLDrawArraysInstancedBaseInstance;

        public static void GLDrawArraysInstancedBaseInstance(
            PrimitiveType mode,
            int first,
            uint count,
            uint primcount,
            uint baseinstance)
        {
            pGLDrawArraysInstancedBaseInstance(mode, first, count, primcount, baseinstance);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLGenBuffersT(uint n, out uint buffers);

        private static GLGenBuffersT pGLGenBuffers;

        public static void GLGenBuffers(uint n, out uint buffers)
        {
            pGLGenBuffers(n, out buffers);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDeleteBuffersT(uint n, ref uint buffers);

        private static GLDeleteBuffersT pGLDeleteBuffers;

        public static void GLDeleteBuffers(uint n, ref uint buffers)
        {
            pGLDeleteBuffers(n, ref buffers);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLGenFramebuffersT(uint n, out uint ids);

        private static GLGenFramebuffersT pGLGenFramebuffers;

        public static void GLGenFramebuffers(uint n, out uint ids)
        {
            pGLGenFramebuffers(n, out ids);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLActiveTextureT(TextureUnit texture);

        private static GLActiveTextureT pGLActiveTexture;

        public static void GLActiveTexture(TextureUnit texture)
        {
            pGLActiveTexture(texture);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLFramebufferTexture1Dt(
            FramebufferTarget target,
            GLFramebufferAttachment attachment,
            TextureTarget textarget,
            uint texture,
            int level);

        private static GLFramebufferTexture1Dt pGLFramebufferTexture1D;

        public static void GLFramebufferTexture1D(
            FramebufferTarget target,
            GLFramebufferAttachment attachment,
            TextureTarget textarget,
            uint texture,
            int level)
        {
            pGLFramebufferTexture1D(target, attachment, textarget, texture, level);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLFramebufferTexture2Dt(
            FramebufferTarget target,
            GLFramebufferAttachment attachment,
            TextureTarget textarget,
            uint texture,
            int level);

        private static GLFramebufferTexture2Dt pGLFramebufferTexture2D;

        public static void GLFramebufferTexture2D(
            FramebufferTarget target,
            GLFramebufferAttachment attachment,
            TextureTarget textarget,
            uint texture,
            int level)
        {
            pGLFramebufferTexture2D(target, attachment, textarget, texture, level);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLBindTextureT(TextureTarget target, uint texture);

        private static GLBindTextureT pGLBindTexture;

        public static void GLBindTexture(TextureTarget target, uint texture)
        {
            pGLBindTexture(target, texture);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLBindFramebufferT(FramebufferTarget target, uint framebuffer);

        private static GLBindFramebufferT pGLBindFramebuffer;

        public static void GLBindFramebuffer(FramebufferTarget target, uint framebuffer)
        {
            pGLBindFramebuffer(target, framebuffer);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDeleteFramebuffersT(uint n, ref uint framebuffers);

        private static GLDeleteFramebuffersT pGLDeleteFramebuffers;

        public static void GLDeleteFramebuffers(uint n, ref uint framebuffers)
        {
            pGLDeleteFramebuffers(n, ref framebuffers);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLGenTexturesT(uint n, out uint textures);

        private static GLGenTexturesT pGLGenTextures;

        public static void GLGenTextures(uint n, out uint textures)
        {
            pGLGenTextures(n, out textures);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDeleteTexturesT(uint n, ref uint textures);

        private static GLDeleteTexturesT pGLDeleteTextures;

        public static void GLDeleteTextures(uint n, ref uint textures)
        {
            pGLDeleteTextures(n, ref textures);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate FramebufferErrorCode GLCheckFramebufferStatusT(FramebufferTarget target);

        private static GLCheckFramebufferStatusT pGLCheckFramebufferStatus;

        public static FramebufferErrorCode GLCheckFramebufferStatus(FramebufferTarget target)
        {
            return pGLCheckFramebufferStatus(target);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLBindBufferT(BufferTarget target, uint buffer);

        private static GLBindBufferT pGLBindBuffer;

        public static void GLBindBuffer(BufferTarget target, uint buffer)
        {
            pGLBindBuffer(target, buffer);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLViewportIndexedfT(uint index, float x, float y, float w, float h);

        private static GLViewportIndexedfT pGLViewportIndexedf;

        public static void GLViewportIndexed(uint index, float x, float y, float w, float h)
        {
            pGLViewportIndexedf(index, x, y, w, h);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLViewportT(int x, int y, uint width, uint height);

        private static GLViewportT pGLViewport;

        public static void GLViewport(int x, int y, uint width, uint height)
        {
            pGLViewport(x, y, width, height);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDepthRangeIndexedT(uint index, double nearVal, double farVal);

        private static GLDepthRangeIndexedT pGLDepthRangeIndexed;

        public static void GLDepthRangeIndexed(uint index, double nearVal, double farVal)
        {
            pGLDepthRangeIndexed(index, nearVal, farVal);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDepthRangefT(float n, float f);

        private static GLDepthRangefT pGLDepthRangef;

        public static void GLDepthRangef(float n, float f)
        {
            pGLDepthRangef(n, f);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLBufferSubDataT(BufferTarget target, IntPtr offset, UIntPtr size, void* data);

        private static GLBufferSubDataT pGLBufferSubData;

        public static void GLBufferSubData(BufferTarget target, IntPtr offset, UIntPtr size, void* data)
        {
            pGLBufferSubData(target, offset, size, data);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLNamedBufferSubDataT(uint buffer, IntPtr offset, uint size, void* data);

        private static GLNamedBufferSubDataT pGLNamedBufferSubData;

        public static void GLNamedBufferSubData(uint buffer, IntPtr offset, uint size, void* data)
        {
            pGLNamedBufferSubData(buffer, offset, size, data);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLScissorIndexedT(uint index, int left, int bottom, uint width, uint height);

        private static GLScissorIndexedT pGLScissorIndexed;

        public static void GLScissorIndexed(uint index, int left, int bottom, uint width, uint height)
        {
            pGLScissorIndexed(index, left, bottom, width, height);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLScissorT(int x, int y, uint width, uint height);

        private static GLScissorT pGLScissor;

        public static void GLScissor(int x, int y, uint width, uint height)
        {
            pGLScissor(x, y, width, height);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLPixelStoreiT(PixelStoreParameter pname, int param);

        private static GLPixelStoreiT pGLPixelStorei;

        public static void GLPixelStorei(PixelStoreParameter pname, int param)
        {
            pGLPixelStorei(pname, param);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLTexSubImage1Dt(
            TextureTarget target,
            int level,
            int xoffset,
            uint width,
            GLPixelFormat format,
            GLPixelType type,
            void* pixels);

        private static GLTexSubImage1Dt pGLTexSubImage1D;

        public static void GLTexSubImage1D(
            TextureTarget target,
            int level,
            int xoffset,
            uint width,
            GLPixelFormat format,
            GLPixelType type,
            void* pixels)
        {
            pGLTexSubImage1D(target, level, xoffset, width, format, type, pixels);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLTexSubImage2Dt(
            TextureTarget target,
            int level,
            int xoffset,
            int yoffset,
            uint width,
            uint height,
            GLPixelFormat format,
            GLPixelType type,
            void* pixels);

        private static GLTexSubImage2Dt pGLTexSubImage2D;

        public static void GLTexSubImage2D(
            TextureTarget target,
            int level,
            int xoffset,
            int yoffset,
            uint width,
            uint height,
            GLPixelFormat format,
            GLPixelType type,
            void* pixels)
        {
            pGLTexSubImage2D(target, level, xoffset, yoffset, width, height, format, type, pixels);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLTexSubImage3Dt(
            TextureTarget target,
            int level,
            int xoffset,
            int yoffset,
            int zoffset,
            uint width,
            uint height,
            uint depth,
            GLPixelFormat format,
            GLPixelType type,
            void* pixels);

        private static GLTexSubImage3Dt pGLTexSubImage3D;

        public static void GLTexSubImage3D(
            TextureTarget target,
            int level,
            int xoffset,
            int yoffset,
            int zoffset,
            uint width,
            uint height,
            uint depth,
            GLPixelFormat format,
            GLPixelType type,
            void* pixels)
        {
            pGLTexSubImage3D(target, level, xoffset, yoffset, zoffset, width, height, depth, format, type, pixels);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLShaderSourceT(uint shader, uint count, byte** @string, int* length);

        private static GLShaderSourceT pGLShaderSource;

        public static void GLShaderSource(uint shader, uint count, byte** @string, int* length)
        {
            pGLShaderSource(shader, count, @string, length);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate uint GLCreateShaderT(ShaderType shaderType);

        private static GLCreateShaderT pGLCreateShader;

        public static uint GLCreateShader(ShaderType shaderType)
        {
            return pGLCreateShader(shaderType);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLCompileShaderT(uint shader);

        private static GLCompileShaderT pGLCompileShader;

        public static void GLCompileShader(uint shader)
        {
            pGLCompileShader(shader);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLGetShaderivT(uint shader, ShaderParameter pname, int* @params);

        private static GLGetShaderivT pGLGetShaderiv;

        public static void GLGetShaderiv(uint shader, ShaderParameter pname, int* @params)
        {
            pGLGetShaderiv(shader, pname, @params);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLGetShaderInfoLogT(uint shader, uint maxLength, uint* length, byte* infoLog);

        private static GLGetShaderInfoLogT pGLGetShaderInfoLog;

        public static void GLGetShaderInfoLog(uint shader, uint maxLength, uint* length, byte* infoLog)
        {
            pGLGetShaderInfoLog(shader, maxLength, length, infoLog);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDeleteShaderT(uint shader);

        private static GLDeleteShaderT pGLDeleteShader;

        public static void GLDeleteShader(uint shader)
        {
            pGLDeleteShader(shader);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLGenSamplersT(uint n, out uint samplers);

        private static GLGenSamplersT pGLGenSamplers;

        public static void GLGenSamplers(uint n, out uint samplers)
        {
            pGLGenSamplers(n, out samplers);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLSamplerParameterfT(uint sampler, SamplerParameterName pname, float param);

        private static GLSamplerParameterfT pGLSamplerParameterf;

        public static void GLSamplerParameterf(uint sampler, SamplerParameterName pname, float param)
        {
            pGLSamplerParameterf(sampler, pname, param);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLSamplerParameteriT(uint sampler, SamplerParameterName pname, int param);

        private static GLSamplerParameteriT pGLSamplerParameteri;

        public static void GLSamplerParameteri(uint sampler, SamplerParameterName pname, int param)
        {
            pGLSamplerParameteri(sampler, pname, param);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLSamplerParameterfvT(uint sampler, SamplerParameterName pname, float* @params);

        private static GLSamplerParameterfvT pGLSamplerParameterfv;

        public static void GLSamplerParameterfv(uint sampler, SamplerParameterName pname, float* @params)
        {
            pGLSamplerParameterfv(sampler, pname, @params);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLBindSamplerT(uint unit, uint sampler);

        private static GLBindSamplerT pGLBindSampler;

        public static void GLBindSampler(uint unit, uint sampler)
        {
            pGLBindSampler(unit, sampler);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDeleteSamplersT(uint n, ref uint samplers);

        private static GLDeleteSamplersT pGLDeleteSamplers;

        public static void GLDeleteSamplers(uint n, ref uint samplers)
        {
            pGLDeleteSamplers(n, ref samplers);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLColorMaskT(
            GLBoolean red,
            GLBoolean green,
            GLBoolean blue,
            GLBoolean alpha);

        private static GLColorMaskT pGLColorMask;

        public static void GLColorMask(
            GLBoolean red,
            GLBoolean green,
            GLBoolean blue,
            GLBoolean alpha)
        {
            pGLColorMask(red, green, blue, alpha);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLColorMaskiT(
            uint buf,
            GLBoolean red,
            GLBoolean green,
            GLBoolean blue,
            GLBoolean alpha);

        private static GLColorMaskiT pGLColorMaski;

        public static void GLColorMaski(
            uint buf,
            GLBoolean red,
            GLBoolean green,
            GLBoolean blue,
            GLBoolean alpha)
        {
            pGLColorMaski(buf, red, green, blue, alpha);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLBlendFuncSeparateiT(
            uint buf,
            BlendingFactorSrc srcRGB,
            BlendingFactorDest dstRGB,
            BlendingFactorSrc srcAlpha,
            BlendingFactorDest dstAlpha);

        private static GLBlendFuncSeparateiT pGLBlendFuncSeparatei;

        public static void GLBlendFuncSeparatei(
            uint buf,
            BlendingFactorSrc srcRGB,
            BlendingFactorDest dstRGB,
            BlendingFactorSrc srcAlpha,
            BlendingFactorDest dstAlpha)
        {
            pGLBlendFuncSeparatei(buf, srcRGB, dstRGB, srcAlpha, dstAlpha);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLBlendFuncSeparateT(
            BlendingFactorSrc srcRGB,
            BlendingFactorDest dstRGB,
            BlendingFactorSrc srcAlpha,
            BlendingFactorDest dstAlpha);

        private static GLBlendFuncSeparateT pGLBlendFuncSeparate;

        public static void GLBlendFuncSeparate(
            BlendingFactorSrc srcRGB,
            BlendingFactorDest dstRGB,
            BlendingFactorSrc srcAlpha,
            BlendingFactorDest dstAlpha)
        {
            pGLBlendFuncSeparate(srcRGB, dstRGB, srcAlpha, dstAlpha);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLEnableT(EnableCap cap);

        private static GLEnableT pGLEnable;

        public static void GLEnable(EnableCap cap)
        {
            pGLEnable(cap);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLEnableiT(EnableCap cap, uint index);

        private static GLEnableiT pGLEnablei;

        public static void GLEnablei(EnableCap cap, uint index)
        {
            pGLEnablei(cap, index);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDisableT(EnableCap cap);

        private static GLDisableT pGLDisable;

        public static void GLDisable(EnableCap cap)
        {
            pGLDisable(cap);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDisableiT(EnableCap cap, uint index);

        private static GLDisableiT pGLDisablei;

        public static void GLDisablei(EnableCap cap, uint index)
        {
            pGLDisablei(cap, index);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLBlendEquationSeparateiT(uint buf, BlendEquationMode modeRGB, BlendEquationMode modeAlpha);

        private static GLBlendEquationSeparateiT pGLBlendEquationSeparatei;

        public static void GLBlendEquationSeparatei(uint buf, BlendEquationMode modeRGB, BlendEquationMode modeAlpha)
        {
            pGLBlendEquationSeparatei(buf, modeRGB, modeAlpha);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLBlendEquationSeparateT(BlendEquationMode modeRGB, BlendEquationMode modeAlpha);

        private static GLBlendEquationSeparateT pGLBlendEquationSeparate;

        public static void GLBlendEquationSeparate(BlendEquationMode modeRGB, BlendEquationMode modeAlpha)
        {
            pGLBlendEquationSeparate(modeRGB, modeAlpha);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLBlendColorT(float red, float green, float blue, float alpha);

        private static GLBlendColorT pGLBlendColor;

        public static void GLBlendColor(float red, float green, float blue, float alpha)
        {
            pGLBlendColor(red, green, blue, alpha);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDepthFuncT(DepthFunction func);

        private static GLDepthFuncT pGLDepthFunc;

        public static void GLDepthFunc(DepthFunction func)
        {
            pGLDepthFunc(func);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDepthMaskT(GLBoolean flag);

        private static GLDepthMaskT pGLDepthMask;

        public static void GLDepthMask(GLBoolean flag)
        {
            pGLDepthMask(flag);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLCullFaceT(CullFaceMode mode);

        private static GLCullFaceT pGLCullFace;

        public static void GLCullFace(CullFaceMode mode)
        {
            pGLCullFace(mode);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLPolygonModeT(MaterialFace face, PolygonMode mode);

        private static GLPolygonModeT pGLPolygonMode;

        public static void GLPolygonMode(MaterialFace face, PolygonMode mode)
        {
            pGLPolygonMode(face, mode);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate uint GLCreateProgramT();

        private static GLCreateProgramT pGLCreateProgram;

        public static uint GLCreateProgram()
        {
            return pGLCreateProgram();
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLAttachShaderT(uint program, uint shader);

        private static GLAttachShaderT pGLAttachShader;

        public static void GLAttachShader(uint program, uint shader)
        {
            pGLAttachShader(program, shader);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLBindAttribLocationT(uint program, uint index, byte* name);

        private static GLBindAttribLocationT pGLBindAttribLocation;

        public static void GLBindAttribLocation(uint program, uint index, byte* name)
        {
            pGLBindAttribLocation(program, index, name);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLLinkProgramT(uint program);

        private static GLLinkProgramT pGLLinkProgram;

        public static void GLLinkProgram(uint program)
        {
            pGLLinkProgram(program);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLGetProgramivT(uint program, GetProgramParameterName pname, int* @params);

        private static GLGetProgramivT pGLGetProgramiv;

        public static void GLGetProgramiv(uint program, GetProgramParameterName pname, int* @params)
        {
            pGLGetProgramiv(program, pname, @params);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLGetProgramInfoLogT(uint program, uint maxLength, uint* length, byte* infoLog);

        private static GLGetProgramInfoLogT pGLGetProgramInfoLog;

        public static void GLGetProgramInfoLog(uint program, uint maxLength, uint* length, byte* infoLog)
        {
            pGLGetProgramInfoLog(program, maxLength, length, infoLog);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLUniformBlockBindingT(uint program, uint uniformBlockIndex, uint uniformBlockBinding);

        private static GLUniformBlockBindingT pGLUniformBlockBinding;

        public static void GLUniformBlockBinding(uint program, uint uniformBlockIndex, uint uniformBlockBinding)
        {
            pGLUniformBlockBinding(program, uniformBlockIndex, uniformBlockBinding);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDeleteProgramT(uint program);

        private static GLDeleteProgramT pGLDeleteProgram;

        public static void GLDeleteProgram(uint program)
        {
            pGLDeleteProgram(program);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLUniform1It(int location, int v0);

        private static GLUniform1It pGLUniform1I;

        public static void GLUniform1I(int location, int v0)
        {
            pGLUniform1I(location, v0);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate uint GLGetUniformBlockIndexT(uint program, byte* uniformBlockName);

        private static GLGetUniformBlockIndexT pGLGetUniformBlockIndex;

        public static uint GLGetUniformBlockIndex(uint program, byte* uniformBlockName)
        {
            return pGLGetUniformBlockIndex(program, uniformBlockName);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate int GLGetUniformLocationT(uint program, byte* name);

        private static GLGetUniformLocationT pGLGetUniformLocation;

        public static int GLGetUniformLocation(uint program, byte* name)
        {
            return pGLGetUniformLocation(program, name);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate int GLGetAttribLocationT(uint program, byte* name);

        private static GLGetAttribLocationT pGLGetAttribLocation;

        public static int GLGetAttribLocation(uint program, byte* name)
        {
            return pGLGetAttribLocation(program, name);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLUseProgramT(uint program);

        private static GLUseProgramT pGLUseProgram;

        public static void GLUseProgram(uint program)
        {
            pGLUseProgram(program);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLBindBufferRangeT(
            BufferRangeTarget target,
            uint index,
            uint buffer,
            IntPtr offset,
            UIntPtr size);

        private static GLBindBufferRangeT pGLBindBufferRange;

        public static void GLBindBufferRange(
            BufferRangeTarget target,
            uint index,
            uint buffer,
            IntPtr offset,
            UIntPtr size)
        {
            pGLBindBufferRange(target, index, buffer, offset, size);
        }

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void DebugProc(
            DebugSource source,
            DebugType type,
            uint id,
            DebugSeverity severity,
            uint length,
            byte* message,
            void* userParam);

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDebugMessageCallbackT(DebugProc callback, void* userParam);

        private static GLDebugMessageCallbackT pGLDebugMessageCallback;

        public static void GLDebugMessageCallback(DebugProc callback, void* userParam)
        {
            pGLDebugMessageCallback(callback, userParam);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLBufferDataT(BufferTarget target, UIntPtr size, void* data, BufferUsageHint usage);

        private static GLBufferDataT pGLBufferData;

        public static void GLBufferData(BufferTarget target, UIntPtr size, void* data, BufferUsageHint usage)
        {
            pGLBufferData(target, size, data, usage);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLNamedBufferDataT(uint buffer, uint size, void* data, BufferUsageHint usage);

        private static GLNamedBufferDataT pGLNamedBufferData;

        public static void GLNamedBufferData(uint buffer, uint size, void* data, BufferUsageHint usage)
        {
            pGLNamedBufferData(buffer, size, data, usage);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLTexImage1Dt(
            TextureTarget target,
            int level,
            PixelInternalFormat internalFormat,
            uint width,
            int border,
            GLPixelFormat format,
            GLPixelType type,
            void* data);

        private static GLTexImage1Dt pGLTexImage1D;

        public static void GLTexImage1D(
            TextureTarget target,
            int level,
            PixelInternalFormat internalFormat,
            uint width,
            int border,
            GLPixelFormat format,
            GLPixelType type,
            void* data)
        {
            pGLTexImage1D(target, level, internalFormat, width, border, format, type, data);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLTexImage2Dt(
            TextureTarget target,
            int level,
            PixelInternalFormat internalFormat,
            uint width,
            uint height,
            int border,
            GLPixelFormat format,
            GLPixelType type,
            void* data);

        private static GLTexImage2Dt pGLTexImage2D;

        public static void GLTexImage2D(
            TextureTarget target,
            int level,
            PixelInternalFormat internalFormat,
            uint width,
            uint height,
            int border,
            GLPixelFormat format,
            GLPixelType type,
            void* data)
        {
            pGLTexImage2D(target, level, internalFormat, width, height, border, format, type, data);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLTexImage3Dt(
            TextureTarget target,
            int level,
            PixelInternalFormat internalFormat,
            uint width,
            uint height,
            uint depth,
            int border,
            GLPixelFormat format,
            GLPixelType type,
            void* data);

        private static GLTexImage3Dt pGLTexImage3D;

        public static void GLTexImage3D(
            TextureTarget target,
            int level,
            PixelInternalFormat internalFormat,
            uint width,
            uint height,
            uint depth,
            int border,
            GLPixelFormat format,
            GLPixelType type,
            void* data)
        {
            pGLTexImage3D(target, level, internalFormat, width, height, depth, border, format, type, data);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLEnableVertexAttribArrayT(uint index);

        private static GLEnableVertexAttribArrayT pGLEnableVertexAttribArray;

        public static void GLEnableVertexAttribArray(uint index)
        {
            pGLEnableVertexAttribArray(index);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDisableVertexAttribArrayT(uint index);

        private static GLDisableVertexAttribArrayT pGLDisableVertexAttribArray;

        public static void GLDisableVertexAttribArray(uint index)
        {
            pGLDisableVertexAttribArray(index);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLVertexAttribPointerT(
            uint index,
            int size,
            VertexAttribPointerType type,
            GLBoolean normalized,
            uint stride,
            void* pointer);

        private static GLVertexAttribPointerT pGLVertexAttribPointer;

        public static void GLVertexAttribPointer(
            uint index,
            int size,
            VertexAttribPointerType type,
            GLBoolean normalized,
            uint stride,
            void* pointer)
        {
            pGLVertexAttribPointer(index, size, type, normalized, stride, pointer);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLVertexAttribIPointerT(
            uint index,
            int size,
            VertexAttribPointerType type,
            uint stride,
            void* pointer);

        private static GLVertexAttribIPointerT pGLVertexAttribIPointer;

        public static void GLVertexAttribIPointer(
            uint index,
            int size,
            VertexAttribPointerType type,
            uint stride,
            void* pointer)
        {
            pGLVertexAttribIPointer(index, size, type, stride, pointer);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLVertexAttribDivisorT(uint index, uint divisor);

        private static GLVertexAttribDivisorT pGLVertexAttribDivisor;

        public static void GLVertexAttribDivisor(uint index, uint divisor)
        {
            pGLVertexAttribDivisor(index, divisor);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLFrontFaceT(FrontFaceDirection mode);

        private static GLFrontFaceT pGLFrontFace;

        public static void GLFrontFace(FrontFaceDirection mode)
        {
            pGLFrontFace(mode);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLGetIntegervT(GetPName pname, int* data);

        private static GLGetIntegervT pGLGetIntegerv;

        public static void GLGetIntegerv(GetPName pname, int* data)
        {
            pGLGetIntegerv(pname, data);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLBindTextureUnitT(uint unit, uint texture);

        private static GLBindTextureUnitT pGLBindTextureUnit;

        public static void GLBindTextureUnit(uint unit, uint texture)
        {
            pGLBindTextureUnit(unit, texture);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLTexParameteriT(TextureTarget target, TextureParameterName pname, int param);

        private static GLTexParameteriT pGLTexParameteri;

        public static void GLTexParameteri(TextureTarget target, TextureParameterName pname, int param)
        {
            pGLTexParameteri(target, pname, param);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate byte* GLGetStringT(StringName name);

        private static GLGetStringT pGLGetString;

        public static byte* GLGetString(StringName name)
        {
            return pGLGetString(name);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate byte* GLGetStringiT(StringNameIndexed name, uint index);

        private static GLGetStringiT pGLGetStringi;

        public static byte* GLGetStringi(StringNameIndexed name, uint index)
        {
            return pGLGetStringi(name, index);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLObjectLabelT(ObjectLabelIdentifier identifier, uint name, uint length, byte* label);

        private static GLObjectLabelT pGLObjectLabel;

        public static void GLObjectLabel(ObjectLabelIdentifier identifier, uint name, uint length, byte* label)
        {
            pGLObjectLabel(identifier, name, length, label);
        }

        /// <summary>
        /// Indicates whether the glObjectLabel function was successfully loaded.
        /// Some drivers advertise KHR_Debug support, but return null for this function pointer.
        /// </summary>
        public static bool HasGlObjectLabel => pGLObjectLabel != null;

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLTexImage2DMultisampleT(
            TextureTarget target,
            uint samples,
            PixelInternalFormat internalformat,
            uint width,
            uint height,
            GLBoolean fixedsamplelocations);

        private static GLTexImage2DMultisampleT pGLTexImage2DMultisample;

        public static void GLTexImage2DMultiSample(
            TextureTarget target,
            uint samples,
            PixelInternalFormat internalformat,
            uint width,
            uint height,
            GLBoolean fixedsamplelocations)
        {
            pGLTexImage2DMultisample(
                target,
                samples,
                internalformat,
                width,
                height,
                fixedsamplelocations);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLTexImage3DMultisampleT(
            TextureTarget target,
            uint samples,
            PixelInternalFormat internalformat,
            uint width,
            uint height,
            uint depth,
            GLBoolean fixedsamplelocations);

        private static GLTexImage3DMultisampleT pGLTexImage3DMultisample;

        public static void GLTexImage3DMultisample(
            TextureTarget target,
            uint samples,
            PixelInternalFormat internalformat,
            uint width,
            uint height,
            uint depth,
            GLBoolean fixedsamplelocations)
        {
            pGLTexImage3DMultisample(
                target,
                samples,
                internalformat,
                width,
                height,
                depth,
                fixedsamplelocations);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLBlitFramebufferT(
            int srcX0,
            int srcY0,
            int srcX1,
            int srcY1,
            int dstX0,
            int dstY0,
            int dstX1,
            int dstY1,
            ClearBufferMask mask,
            BlitFramebufferFilter filter);

        private static GLBlitFramebufferT pGLBlitFramebuffer;

        public static void GLBlitFramebuffer(
            int srcX0,
            int srcY0,
            int srcX1,
            int srcY1,
            int dstX0,
            int dstY0,
            int dstX1,
            int dstY1,
            ClearBufferMask mask,
            BlitFramebufferFilter filter)
        {
            pGLBlitFramebuffer(srcX0, srcY0, srcX1, srcY1, dstX0, dstY0, dstX1, dstY1, mask, filter);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLFramebufferTextureLayerT(
            FramebufferTarget target,
            GLFramebufferAttachment attachment,
            uint texture,
            int level,
            int layer);

        private static GLFramebufferTextureLayerT pGLFramebufferTextureLayer;

        public static void GLFramebufferTextureLayer(
            FramebufferTarget target,
            GLFramebufferAttachment attachment,
            uint texture,
            int level,
            int layer)
        {
            pGLFramebufferTextureLayer(target, attachment, texture, level, layer);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDispatchComputeT(uint numGroupsX, uint numGroupsY, uint numGroupsZ);

        private static GLDispatchComputeT pGLDispatchCompute;

        public static void GLDispatchCompute(uint numGroupsX, uint numGroupsY, uint numGroupsZ)
        {
            pGLDispatchCompute(numGroupsX, numGroupsY, numGroupsZ);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate uint GLGetProgramInterfaceivT(uint program, ProgramInterface programInterface, ProgramInterfaceParameterName pname, int* @params);

        private static GLGetProgramInterfaceivT pGLGetProgramInterfaceiv;

        public static uint GLGetProgramInterfaceiv(uint program, ProgramInterface programInterface, ProgramInterfaceParameterName pname, int* @params)
        {
            return pGLGetProgramInterfaceiv(program, programInterface, pname, @params);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate uint GLGetProgramResourceIndexT(uint program, ProgramInterface programInterface, byte* name);

        private static GLGetProgramResourceIndexT pGLGetProgramResourceIndex;

        public static uint GLGetProgramResourceIndex(uint program, ProgramInterface programInterface, byte* name)
        {
            return pGLGetProgramResourceIndex(program, programInterface, name);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate uint GLGetProgramResourceNameT(uint program, ProgramInterface programInterface, uint index, uint bufSize, uint* length, byte* name);

        private static GLGetProgramResourceNameT pGLGetProgramResourceName;

        public static uint GLGetProgramResourceName(uint program, ProgramInterface programInterface, uint index, uint bufSize, uint* length, byte* name)
        {
            return pGLGetProgramResourceName(program, programInterface, index, bufSize, length, name);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLShaderStorageBlockBindingT(uint program, uint storageBlockIndex, uint storageBlockBinding);

        private static GLShaderStorageBlockBindingT pGLShaderStorageBlockBinding;

        public static void GLShaderStorageBlockBinding(uint program, uint storageBlockIndex, uint storageBlockBinding)
        {
            pGLShaderStorageBlockBinding(program, storageBlockIndex, storageBlockBinding);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDrawElementsIndirectT(PrimitiveType mode, DrawElementsType type, IntPtr indirect);

        private static GLDrawElementsIndirectT pGLDrawElementsIndirect;

        public static void GLDrawElementsIndirect(PrimitiveType mode, DrawElementsType type, IntPtr indirect)
        {
            pGLDrawElementsIndirect(mode, type, indirect);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLMultiDrawElementsIndirectT(
            PrimitiveType mode,
            DrawElementsType type,
            IntPtr indirect,
            uint drawcount,
            uint stride);

        private static GLMultiDrawElementsIndirectT pGLMultiDrawElementsIndirect;

        public static void GLMultiDrawElementsIndirect(
            PrimitiveType mode,
            DrawElementsType type,
            IntPtr indirect,
            uint drawcount,
            uint stride)
        {
            pGLMultiDrawElementsIndirect(mode, type, indirect, drawcount, stride);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDrawArraysIndirectT(PrimitiveType mode, IntPtr indirect);

        private static GLDrawArraysIndirectT pGLDrawArraysIndirect;

        public static void GLDrawArraysIndirect(PrimitiveType mode, IntPtr indirect)
        {
            pGLDrawArraysIndirect(mode, indirect);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLMultiDrawArraysIndirectT(PrimitiveType mode, IntPtr indirect, uint drawcount, uint stride);

        private static GLMultiDrawArraysIndirectT pGLMultiDrawArraysIndirect;

        public static void GLMultiDrawArraysIndirect(PrimitiveType mode, IntPtr indirect, uint drawcount, uint stride)
        {
            pGLMultiDrawArraysIndirect(mode, indirect, drawcount, stride);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDispatchComputeIndirectT(IntPtr indirect);

        private static GLDispatchComputeIndirectT pGLDispatchComputeIndirect;

        public static void GLDispatchComputeIndirect(IntPtr indirect)
        {
            pGLDispatchComputeIndirect(indirect);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLBindImageTextureT(
            uint unit​,
            uint texture​,
            int level​,
            GLBoolean layered​,
            int layer​,
            TextureAccess access​,
            SizedInternalFormat format​);

        private static GLBindImageTextureT pGLBindImageTexture;

        public static void GLBindImageTexture(
            uint unit​,
            uint texture​,
            int level​,
            GLBoolean layered​,
            int layer​,
            TextureAccess access​,
            SizedInternalFormat format​)
        {
            pGLBindImageTexture(unit, texture, level, layered, layer, access, format);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLMemoryBarrierT(MemoryBarrierFlags barriers);

        private static GLMemoryBarrierT pGLMemoryBarrier;

        public static void GLMemoryBarrier(MemoryBarrierFlags barriers)
        {
            pGLMemoryBarrier(barriers);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLTexStorage1Dt(
            TextureTarget target,
            uint levels,
            SizedInternalFormat internalformat,
            uint width);

        private static GLTexStorage1Dt pGLTexStorage1D;

        public static void GLTexStorage1D(TextureTarget target, uint levels, SizedInternalFormat internalformat, uint width)
        {
            pGLTexStorage1D(target, levels, internalformat, width);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLTexStorage2Dt(
            TextureTarget target,
            uint levels,
            SizedInternalFormat internalformat,
            uint width,
            uint height);

        private static GLTexStorage2Dt pGLTexStorage2D;

        public static void GLTexStorage2D(
            TextureTarget target,
            uint levels,
            SizedInternalFormat internalformat,
            uint width,
            uint height)
        {
            pGLTexStorage2D(target, levels, internalformat, width, height);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLTexStorage3Dt(
            TextureTarget target,
            uint levels,
            SizedInternalFormat internalformat,
            uint width,
            uint height,
            uint depth);

        private static GLTexStorage3Dt pGLTexStorage3D;

        public static void GLTexStorage3D(
            TextureTarget target,
            uint levels,
            SizedInternalFormat internalformat,
            uint width,
            uint height,
            uint depth)
        {
            pGLTexStorage3D(target, levels, internalformat, width, height, depth);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLTextureStorage1Dt(
            uint texture,
            uint levels,
            SizedInternalFormat internalformat,
            uint width);

        private static GLTextureStorage1Dt pGLTextureStorage1D;

        public static void GLTextureStorage1D(uint texture, uint levels, SizedInternalFormat internalformat, uint width)
        {
            pGLTextureStorage1D(texture, levels, internalformat, width);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLTextureStorage2Dt(
            uint texture,
            uint levels,
            SizedInternalFormat internalformat,
            uint width,
            uint height);

        private static GLTextureStorage2Dt pGLTextureStorage2D;

        public static void GLTextureStorage2D(
            uint texture,
            uint levels,
            SizedInternalFormat internalformat,
            uint width,
            uint height)
        {
            pGLTextureStorage2D(texture, levels, internalformat, width, height);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLTextureStorage3Dt(
            uint texture,
            uint levels,
            SizedInternalFormat internalformat,
            uint width,
            uint height,
            uint depth);

        private static GLTextureStorage3Dt pGLTextureStorage3D;

        public static void GLTextureStorage3D(
            uint texture,
            uint levels,
            SizedInternalFormat internalformat,
            uint width,
            uint height,
            uint depth)
        {
            pGLTextureStorage3D(texture, levels, internalformat, width, height, depth);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLTextureStorage2DMultisampleT(
            uint texture,
            uint samples,
            SizedInternalFormat internalformat,
            uint width,
            uint height,
            GLBoolean fixedsamplelocations);

        private static GLTextureStorage2DMultisampleT pGLTextureStorage2DMultisample;

        public static void GLTextureStorage2DMultisample(
            uint texture,
            uint samples,
            SizedInternalFormat internalformat,
            uint width,
            uint height,
            GLBoolean fixedsamplelocations)
        {
            pGLTextureStorage2DMultisample(texture, samples, internalformat, width, height, fixedsamplelocations);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLTextureStorage3DMultisampleT(
            uint texture,
            uint samples,
            SizedInternalFormat internalformat,
            uint width,
            uint height,
            uint depth,
            GLBoolean fixedsamplelocations);

        private static GLTextureStorage3DMultisampleT pGLTextureStorage3DMultisample;

        public static void GLTextureStorage3DMultisample(
            uint texture,
            uint samples,
            SizedInternalFormat internalformat,
            uint width,
            uint height,
            uint depth,
            GLBoolean fixedsamplelocations)
        {
            pGLTextureStorage3DMultisample(texture, samples, internalformat, width, height, depth, fixedsamplelocations);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLTexStorage2DMultisampleT(
            TextureTarget target,
            uint samples,
            SizedInternalFormat internalformat,
            uint width,
            uint height,
            GLBoolean fixedsamplelocations);

        private static GLTexStorage2DMultisampleT pGLTexStorage2DMultisample;

        public static void GLTexStorage2DMultisample(
            TextureTarget target,
            uint samples,
            SizedInternalFormat internalformat,
            uint width,
            uint height,
            GLBoolean fixedsamplelocations)
        {
            pGLTexStorage2DMultisample(target, samples, internalformat, width, height, fixedsamplelocations);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLTexStorage3DMultisampleT(
            TextureTarget target,
            uint samples,
            SizedInternalFormat internalformat,
            uint width,
            uint height,
            uint depth,
            GLBoolean fixedsamplelocations);

        private static GLTexStorage3DMultisampleT pGLTexStorage3DMultisample;

        public static void GLTexStorage3DMultisample(
            TextureTarget target,
            uint samples,
            SizedInternalFormat internalformat,
            uint width,
            uint height,
            uint depth,
            GLBoolean fixedsamplelocations)
        {
            pGLTexStorage3DMultisample(target, samples, internalformat, width, height, depth, fixedsamplelocations);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLTextureViewT(
            uint texture,
            TextureTarget target,
            uint origtexture,
            PixelInternalFormat internalformat,
            uint minlevel,
            uint numlevels,
            uint minlayer,
            uint numlayers);

        private static GLTextureViewT pGLTextureView;

        public static void GLTextureView(
            uint texture,
            TextureTarget target,
            uint origtexture,
            PixelInternalFormat internalformat,
            uint minlevel,
            uint numlevels,
            uint minlayer,
            uint numlayers)
        {
            pGLTextureView(texture, target, origtexture, internalformat, minlevel, numlevels, minlayer, numlayers);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void* GLMapBufferT(BufferTarget target, BufferAccess access);

        private static GLMapBufferT pGLMapBuffer;

        public static void* GLMapBuffer(BufferTarget target, BufferAccess access)
        {
            return pGLMapBuffer(target, access);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void* GLMapNamedBufferT(uint buffer, BufferAccess access);

        private static GLMapNamedBufferT pGLMapNamedBuffer;

        public static void* GLMapNamedBuffer(uint buffer, BufferAccess access)
        {
            return pGLMapNamedBuffer(buffer, access);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate GLBoolean GLUnmapBufferT(BufferTarget target);

        private static GLUnmapBufferT pGLUnmapBuffer;

        public static GLBoolean GLUnmapBuffer(BufferTarget target)
        {
            return pGLUnmapBuffer(target);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate GLBoolean GLUnmapNamedBufferT(uint buffer);

        private static GLUnmapNamedBufferT pGLUnmapNamedBuffer;

        public static GLBoolean GLUnmapNamedBuffer(uint buffer)
        {
            return pGLUnmapNamedBuffer(buffer);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLCopyBufferSubDataT(
            BufferTarget readTarget,
            BufferTarget writeTarget,
            IntPtr readOffset,
            IntPtr writeOffset,
            IntPtr size);

        private static GLCopyBufferSubDataT pGLCopyBufferSubData;

        public static void GLCopyBufferSubData(
            BufferTarget readTarget,
            BufferTarget writeTarget,
            IntPtr readOffset,
            IntPtr writeOffset,
            IntPtr size)
        {
            pGLCopyBufferSubData(readTarget, writeTarget, readOffset, writeOffset, size);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLCopyTexSubImage2Dt(
            TextureTarget target,
            int level,
            int xoffset,
            int yoffset,
            int x,
            int y,
            uint width,
            uint height);

        private static GLCopyTexSubImage2Dt pGLCopyTexSubImage2D;

        public static void GLCopyTexSubImage2D(
            TextureTarget target,
            int level,
            int xoffset,
            int yoffset,
            int x,
            int y,
            uint width,
            uint height)
        {
            pGLCopyTexSubImage2D(target, level, xoffset, yoffset, x, y, width, height);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLCopyTexSubImage3Dt(
            TextureTarget target,
            int level,
            int xoffset,
            int yoffset,
            int zoffset,
            int x,
            int y,
            uint width,
            uint height);

        private static GLCopyTexSubImage3Dt pGLCopyTexSubImage3D;

        public static void GLCopyTexSubImage3D(
            TextureTarget target,
            int level,
            int xoffset,
            int yoffset,
            int zoffset,
            int x,
            int y,
            uint width,
            uint height)
        {
            pGLCopyTexSubImage3D(target, level, xoffset, yoffset, zoffset, x, y, width, height);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void* GLMapBufferRangeT(BufferTarget target, IntPtr offset, IntPtr length, BufferAccessMask access);

        private static GLMapBufferRangeT pGLMapBufferRange;

        public static void* GLMapBufferRange(BufferTarget target, IntPtr offset, IntPtr length, BufferAccessMask access)
        {
            return pGLMapBufferRange(target, offset, length, access);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void* GLMapNamedBufferRangeT(uint buffer, IntPtr offset, uint length, BufferAccessMask access);

        private static GLMapNamedBufferRangeT pGLMapNamedBufferRange;

        public static void* GLMapNamedBufferRange(uint buffer, IntPtr offset, uint length, BufferAccessMask access)
        {
            return pGLMapNamedBufferRange(buffer, offset, length, access);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLGetTexImageT(
            TextureTarget target,
            int level,
            GLPixelFormat format,
            GLPixelType type,
            void* pixels);

        private static GLGetTexImageT pGLGetTexImage;

        public static void GLGetTexImage(TextureTarget target, int level, GLPixelFormat format, GLPixelType type, void* pixels)
        {
            pGLGetTexImage(target, level, format, type, pixels);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLGetTextureSubImageT(
            uint texture,
            int level,
            int xoffset,
            int yoffset,
            int zoffset,
            uint width,
            uint height,
            uint depth,
            GLPixelFormat format,
            GLPixelType type,
            uint bufSize,
            void* pixels);

        private static GLGetTextureSubImageT pGLGetTextureSubImage;

        public static void GLGetTextureSubImage(
            uint texture,
            int level,
            int xoffset,
            int yoffset,
            int zoffset,
            uint width,
            uint height,
            uint depth,
            GLPixelFormat format,
            GLPixelType type,
            uint bufSize,
            void* pixels)
        {
            pGLGetTextureSubImage(
                texture,
                level,
                xoffset,
                yoffset,
                zoffset,
                width,
                height,
                depth,
                format,
                type,
                bufSize,
                pixels);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLCopyNamedBufferSubDataT(
            uint readBuffer,
            uint writeBuffer,
            IntPtr readOffset,
            IntPtr writeOffset,
            uint size);

        private static GLCopyNamedBufferSubDataT pGLCopyNamedBufferSubData;

        public static void GLCopyNamedBufferSubData(
            uint readBuffer,
            uint writeBuffer,
            IntPtr readOffset,
            IntPtr writeOffset,
            uint size)
        {
            pGLCopyNamedBufferSubData(readBuffer, writeBuffer, readOffset, writeOffset, size);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLCreateBuffersT(uint n, uint* buffers);

        private static GLCreateBuffersT pGLCreateBuffers;

        public static void GLCreateBuffers(uint n, uint* buffers)
        {
            pGLCreateBuffers(n, buffers);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLCreateTexturesT(TextureTarget target, uint n, uint* textures);

        private static GLCreateTexturesT pGLCreateTextures;

        public static void GLCreateTextures(TextureTarget target, uint n, uint* textures)
        {
            pGLCreateTextures(target, n, textures);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLCompressedTexSubImage1Dt(
            TextureTarget target,
            int level,
            int xoffset,
            uint width,
            PixelInternalFormat internalformat,
            uint imageSize,
            void* data);

        private static GLCompressedTexSubImage1Dt pGLCompressedTexSubImage1D;

        public static void GLCompressedTexSubImage1D(
            TextureTarget target,
            int level,
            int xoffset,
            uint width,
            PixelInternalFormat internalformat,
            uint imageSize,
            void* data)
        {
            pGLCompressedTexSubImage1D(target, level, xoffset, width, internalformat, imageSize, data);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLCompressedTexSubImage2Dt(
            TextureTarget target,
            int level,
            int xoffset,
            int yoffset,
            uint width,
            uint height,
            PixelInternalFormat format,
            uint imageSize,
            void* data);

        private static GLCompressedTexSubImage2Dt pGLCompressedTexSubImage2D;

        public static void GLCompressedTexSubImage2D(
            TextureTarget target,
            int level,
            int xoffset,
            int yoffset,
            uint width,
            uint height,
            PixelInternalFormat format,
            uint imageSize,
            void* data)
        {
            pGLCompressedTexSubImage2D(target, level, xoffset, yoffset, width, height, format, imageSize, data);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLCompressedTexSubImage3Dt(
            TextureTarget target,
            int level,
            int xoffset,
            int yoffset,
            int zoffset,
            uint width,
            uint height,
            uint depth,
            PixelInternalFormat format,
            uint imageSize,
            void* data);

        private static GLCompressedTexSubImage3Dt pGLCompressedTexSubImage3D;

        public static void GLCompressedTexSubImage3D(
            TextureTarget target,
            int level,
            int xoffset,
            int yoffset,
            int zoffset,
            uint width,
            uint height,
            uint depth,
            PixelInternalFormat format,
            uint imageSize,
            void* data)
        {
            pGLCompressedTexSubImage3D(
                target,
                level,
                xoffset,
                yoffset,
                zoffset,
                width,
                height,
                depth,
                format,
                imageSize,
                data);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLCopyImageSubDataT(
            uint srcName,
            TextureTarget srcTarget,
            int srcLevel,
            int srcX,
            int srcY,
            int srcZ,
            uint dstName,
            TextureTarget dstTarget,
            int dstLevel,
            int dstX,
            int dstY,
            int dstZ,
            uint srcWidth,
            uint srcHeight,
            uint srcDepth);

        private static GLCopyImageSubDataT pGLCopyImageSubData;

        public static void GLCopyImageSubData(
            uint srcName,
            TextureTarget srcTarget,
            int srcLevel,
            int srcX,
            int srcY,
            int srcZ,
            uint dstName,
            TextureTarget dstTarget,
            int dstLevel,
            int dstX,
            int dstY,
            int dstZ,
            uint srcWidth,
            uint srcHeight,
            uint srcDepth)
        {
            pGLCopyImageSubData(
                srcName, srcTarget,
                srcLevel, srcX, srcY, srcZ,
                dstName, dstTarget,
                dstLevel, dstX, dstY, dstZ,
                srcWidth, srcHeight, srcDepth);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLStencilFuncSeparateT(CullFaceMode face, StencilFunction func, int @ref, uint mask);

        private static GLStencilFuncSeparateT pGLStencilFuncSeparate;

        public static void GLStencilFuncSeparate(CullFaceMode face, StencilFunction func, int @ref, uint mask)
        {
            pGLStencilFuncSeparate(face, func, @ref, mask);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLStencilOpSeparateT(
            CullFaceMode face,
            StencilOp sfail,
            StencilOp dpfail,
            StencilOp dppass);

        private static GLStencilOpSeparateT pGLStencilOpSeparate;

        public static void GLStencilOpSeparate(
            CullFaceMode face,
            StencilOp sfail,
            StencilOp dpfail,
            StencilOp dppass)
        {
            pGLStencilOpSeparate(face, sfail, dpfail, dppass);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLStencilMaskT(uint mask);

        private static GLStencilMaskT pGLStencilMask;

        public static void GLStencilMask(uint mask)
        {
            pGLStencilMask(mask);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLClearStencilT(int s);

        private static GLClearStencilT pGLClearStencil;

        public static void GLClearStencil(int s)
        {
            pGLClearStencil(s);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLGetActiveUniformBlockivT(
            uint program,
            uint uniformBlockIndex,
            ActiveUniformBlockParameter pname,
            int* @params);

        private static GLGetActiveUniformBlockivT pGLGetActiveUniformBlockiv;

        public static void GLGetActiveUniformBlockiv(
            uint program,
            uint uniformBlockIndex,
            ActiveUniformBlockParameter pname,
            int* @params)
        {
            pGLGetActiveUniformBlockiv(program, uniformBlockIndex, pname, @params);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLGetActiveUniformBlockNameT(
            uint program,
            uint uniformBlockIndex,
            uint bufSize,
            uint* length,
            byte* uniformBlockName);

        private static GLGetActiveUniformBlockNameT pGLGetActiveUniformBlockName;

        public static void GLGetActiveUniformBlockName(
            uint program,
            uint uniformBlockIndex,
            uint bufSize,
            uint* length,
            byte* uniformBlockName)
        {
            pGLGetActiveUniformBlockName(program, uniformBlockIndex, bufSize, length, uniformBlockName);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLGetActiveUniformT(
            uint program,
            uint index,
            uint bufSize,
            uint* length,
            int* size,
            uint* type,
            byte* name);

        private static GLGetActiveUniformT pGLGetActiveUniform;

        public static void GLGetActiveUniform(
            uint program,
            uint index,
            uint bufSize,
            uint* length,
            int* size,
            uint* type,
            byte* name)
        {
            pGLGetActiveUniform(program, index, bufSize, length, size, type, name);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLGetCompressedTexImageT(TextureTarget target, int level, void* pixels);

        private static GLGetCompressedTexImageT pGLGetCompressedTexImage;

        public static void GLGetCompressedTexImage(TextureTarget target, int level, void* pixels)
        {
            pGLGetCompressedTexImage(target, level, pixels);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLGetCompressedTextureImageT(uint texture, int level, uint bufSize, void* pixels);

        private static GLGetCompressedTextureImageT pGLGetCompressedTextureImage;

        public static void GLGetCompressedTextureImage(uint texture, int level, uint bufSize, void* pixels)
        {
            pGLGetCompressedTextureImage(texture, level, bufSize, pixels);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLGetTexLevelParameterivT(
            TextureTarget target,
            int level,
            GetTextureParameter pname,
            int* @params);

        private static GLGetTexLevelParameterivT pGLGetTexLevelParameteriv;

        public static void GLGetTexLevelParameteriv(
            TextureTarget target,
            int level,
            GetTextureParameter pname,
            int* @params)
        {
            pGLGetTexLevelParameteriv(target, level, pname, @params);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLFramebufferRenderbufferT(
            FramebufferTarget target,
            GLFramebufferAttachment attachment,
            RenderbufferTarget renderbuffertarget,
            uint renderbuffer);

        private static GLFramebufferRenderbufferT pGLFramebufferRenderbuffer;

        public static void GLFramebufferRenderbuffer(
            FramebufferTarget target,
            GLFramebufferAttachment attachment,
            RenderbufferTarget renderbuffertarget,
            uint renderbuffer)
        {
            pGLFramebufferRenderbuffer(target, attachment, renderbuffertarget, renderbuffer);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLRenderbufferStorageT(
            RenderbufferTarget target,
            uint internalformat,
            uint width,
            uint height);

        private static GLRenderbufferStorageT pGLRenderbufferStorage;

        public static void GLRenderbufferStorage(
            RenderbufferTarget target,
            uint internalFormat,
            uint width,
            uint height)
        {
            pGLRenderbufferStorage(target, internalFormat, width, height);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLGetRenderbufferParameterivT(
            RenderbufferTarget target,
            RenderbufferPname pname,
            out int parameters);

        private static GLGetRenderbufferParameterivT pGLGetRenderbufferParameteriv;

        public static void GLGetRenderbufferParameteriv(
            RenderbufferTarget target,
            RenderbufferPname pname,
            out int parameters)
        {
            pGLGetRenderbufferParameteriv(target, pname, out parameters);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLGenRenderbuffersT(uint count, out uint names);

        private static GLGenRenderbuffersT pGLGenRenderbuffers;

        public static void GLGenRenderbuffers(uint count, out uint names)
        {
            pGLGenRenderbuffers(count, out names);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLBindRenderbufferT(RenderbufferTarget bindPoint, uint name);

        private static GLBindRenderbufferT pGLBindRenderbuffer;

        public static void GLBindRenderbuffer(RenderbufferTarget bindPoint, uint name)
        {
            pGLBindRenderbuffer(bindPoint, name);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLGenerateMipmapT(TextureTarget target);

        private static GLGenerateMipmapT pGLGenerateMipmap;

        public static void GLGenerateMipmap(TextureTarget target)
        {
            pGLGenerateMipmap(target);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLGenerateTextureMipmapT(uint texture);

        private static GLGenerateTextureMipmapT pGLGenerateTextureMipmap;

        public static void GLGenerateTextureMipmap(uint texture)
        {
            pGLGenerateTextureMipmap(texture);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLClipControlT(ClipControlOrigin origin, ClipControlDepthRange depth);

        private static GLClipControlT pGLClipControl;

        public static void GLClipControl(ClipControlOrigin origin, ClipControlDepthRange depth)
        {
            pGLClipControl(origin, depth);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLGetFramebufferAttachmentParameterivT(
            FramebufferTarget target,
            GLFramebufferAttachment attachment,
            FramebufferParameterName pname,
            int* @params);

        private static GLGetFramebufferAttachmentParameterivT pGLGetFramebufferAttachmentParameteriv;

        public static void GLGetFramebufferAttachmentParameteriv(
            FramebufferTarget target,
            GLFramebufferAttachment attachment,
            FramebufferParameterName pname,
            int* @params)
        {
            pGLGetFramebufferAttachmentParameteriv(target, attachment, pname, @params);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLFlushT();

        private static GLFlushT pGLFlush;

        public static void GLFlush()
        {
            pGLFlush();
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLFinishT();

        private static GLFinishT pGLFinish;

        public static void GLFinish()
        {
            pGLFinish();
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLPushDebugGroupT(DebugSource source, uint id, uint length, byte* message);

        private static GLPushDebugGroupT pGLPushDebugGroup;

        public static void GLPushDebugGroup(DebugSource source, uint id, uint length, byte* message)
        {
            pGLPushDebugGroup(source, id, length, message);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLPopDebugGroupT();

        private static GLPopDebugGroupT pGLPopDebugGroup;

        public static void GLPopDebugGroup()
        {
            pGLPopDebugGroup();
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLDebugMessageInsertT(
            DebugSource source,
            DebugType type,
            uint id,
            DebugSeverity severity,
            uint length,
            byte* message);

        private static GLDebugMessageInsertT pGLDebugMessageInsert;

        public static void GLDebugMessageInsert(
            DebugSource source,
            DebugType type,
            uint id,
            DebugSeverity severity,
            uint length,
            byte* message)
        {
            pGLDebugMessageInsert(source, type, id, severity, length, message);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLInsertEventMarkerT(uint length, byte* marker);

        private static GLInsertEventMarkerT pGLInsertEventMarker;

        public static void GLInsertEventMarker(uint length, byte* marker)
        {
            pGLInsertEventMarker(length, marker);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLPushGroupMarkerExtT(uint length, byte* marker);

        private static GLPushGroupMarkerExtT pGLPushGroupMarker;

        public static void GLPushGroupMarker(uint length, byte* marker)
        {
            pGLPushGroupMarker(length, marker);
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLPopGroupMarkerExtT();

        private static GLPopGroupMarkerExtT pGLPopGroupMarker;

        public static void GLPopGroupMarker()
        {
            pGLPopGroupMarker();
        }

        [UnmanagedFunctionPointer(call_conv)]
        private delegate void GLReadPixelsT(
            int x,
            int y,
            uint width,
            uint height,
            GLPixelFormat format,
            GLPixelType type,
            void* data);

        private static GLReadPixelsT pGLReadPixels;

        public static void GLReadPixels(
            int x,
            int y,
            uint width,
            uint height,
            GLPixelFormat format,
            GLPixelType type,
            void* data)
        {
            pGLReadPixels(x, y, width, height, format, type, data);
        }

        public static void LoadGetString(IntPtr glContext, Func<string, IntPtr> getProcAddress)
        {
            sGetProcAddress = getProcAddress;
            loadFunction("glGetString", out pGLGetString);
        }

        public static void LoadAllFunctions(IntPtr glContext, Func<string, IntPtr> getProcAddress, bool gles)
        {
            sGetProcAddress = getProcAddress;

            // Common functions

            loadFunction("glCompressedTexSubImage2D", out pGLCompressedTexSubImage2D);
            loadFunction("glCompressedTexSubImage3D", out pGLCompressedTexSubImage3D);
            loadFunction("glStencilFuncSeparate", out pGLStencilFuncSeparate);
            loadFunction("glStencilOpSeparate", out pGLStencilOpSeparate);
            loadFunction("glStencilMask", out pGLStencilMask);
            loadFunction("glClearStencil", out pGLClearStencil);
            loadFunction("glGetActiveUniformBlockiv", out pGLGetActiveUniformBlockiv);
            loadFunction("glGetActiveUniformBlockName", out pGLGetActiveUniformBlockName);
            loadFunction("glGetActiveUniform", out pGLGetActiveUniform);
            loadFunction("glGetCompressedTexImage", out pGLGetCompressedTexImage);
            loadFunction("glGetCompressedTextureImage", out pGLGetCompressedTextureImage);
            loadFunction("glGetTexLevelParameteriv", out pGLGetTexLevelParameteriv);
            loadFunction("glTexImage1D", out pGLTexImage1D);
            loadFunction("glCompressedTexImage1D", out pGLCompressedTexSubImage1D);

            loadFunction("glGenVertexArrays", out pGLGenVertexArrays);
            loadFunction("glGetError", out pGLGetError);
            loadFunction("glBindVertexArray", out pGLBindVertexArray);
            loadFunction("glClearColor", out pGLClearColor);
            loadFunction("glDrawBuffer", out pGLDrawBuffer);
            loadFunction("glDrawBuffers", out pGLDrawBuffers);
            loadFunction("glClear", out pGLClear);
            loadFunction("glClearDepth", out pGLClearDepth);
            loadFunction("glClearDepthf", out pGLClearDepthf);

            if (pGLClearDepthf != null)
            {
                pGLClearDepthfCompat = pGLClearDepthf;
            }
            else
            {
                pGLClearDepthfCompat = depth => pGLClearDepth(depth);
            }

            loadFunction("glDrawElements", out pGLDrawElements);
            loadFunction("glDrawElementsBaseVertex", out pGLDrawElementsBaseVertex);
            loadFunction("glDrawElementsInstanced", out pGLDrawElementsInstanced);
            loadFunction("glDrawElementsInstancedBaseVertex", out pGLDrawElementsInstancedBaseVertex);
            loadFunction("glDrawArrays", out pGLDrawArrays);
            loadFunction("glDrawArraysInstanced", out pGLDrawArraysInstanced);
            loadFunction("glDrawArraysInstancedBaseInstance", out pGLDrawArraysInstancedBaseInstance);
            loadFunction("glGenBuffers", out pGLGenBuffers);
            loadFunction("glDeleteBuffers", out pGLDeleteBuffers);
            loadFunction("glGenFramebuffers", out pGLGenFramebuffers);
            loadFunction("glActiveTexture", out pGLActiveTexture);
            loadFunction("glFramebufferTexture2D", out pGLFramebufferTexture2D);
            loadFunction("glBindTexture", out pGLBindTexture);
            loadFunction("glBindFramebuffer", out pGLBindFramebuffer);
            loadFunction("glDeleteFramebuffers", out pGLDeleteFramebuffers);
            loadFunction("glGenTextures", out pGLGenTextures);
            loadFunction("glDeleteTextures", out pGLDeleteTextures);
            loadFunction("glCheckFramebufferStatus", out pGLCheckFramebufferStatus);
            loadFunction("glBindBuffer", out pGLBindBuffer);
            loadFunction("glDepthRangeIndexed", out pGLDepthRangeIndexed);
            loadFunction("glBufferSubData", out pGLBufferSubData);
            loadFunction("glNamedBufferSubData", out pGLNamedBufferSubData);
            loadFunction("glScissorIndexed", out pGLScissorIndexed);
            loadFunction("glTexSubImage1D", out pGLTexSubImage1D);
            loadFunction("glTexSubImage2D", out pGLTexSubImage2D);
            loadFunction("glTexSubImage3D", out pGLTexSubImage3D);
            loadFunction("glPixelStorei", out pGLPixelStorei);
            loadFunction("glShaderSource", out pGLShaderSource);
            loadFunction("glCreateShader", out pGLCreateShader);
            loadFunction("glCompileShader", out pGLCompileShader);
            loadFunction("glGetShaderiv", out pGLGetShaderiv);
            loadFunction("glGetShaderInfoLog", out pGLGetShaderInfoLog);
            loadFunction("glDeleteShader", out pGLDeleteShader);
            loadFunction("glGenSamplers", out pGLGenSamplers);
            loadFunction("glSamplerParameterf", out pGLSamplerParameterf);
            loadFunction("glSamplerParameteri", out pGLSamplerParameteri);
            loadFunction("glSamplerParameterfv", out pGLSamplerParameterfv);
            loadFunction("glBindSampler", out pGLBindSampler);
            loadFunction("glDeleteSamplers", out pGLDeleteSamplers);
            loadFunction("glColorMaski", out pGLColorMaski);
            loadFunction("glColorMask", out pGLColorMask);
            loadFunction("glBlendFuncSeparatei", out pGLBlendFuncSeparatei);
            loadFunction("glBlendFuncSeparate", out pGLBlendFuncSeparate);
            loadFunction("glEnable", out pGLEnable);
            loadFunction("glEnablei", out pGLEnablei);
            loadFunction("glDisable", out pGLDisable);
            loadFunction("glDisablei", out pGLDisablei);
            loadFunction("glBlendEquationSeparatei", out pGLBlendEquationSeparatei);
            loadFunction("glBlendEquationSeparate", out pGLBlendEquationSeparate);
            loadFunction("glBlendColor", out pGLBlendColor);
            loadFunction("glDepthFunc", out pGLDepthFunc);
            loadFunction("glDepthMask", out pGLDepthMask);
            loadFunction("glCullFace", out pGLCullFace);
            loadFunction("glCreateProgram", out pGLCreateProgram);
            loadFunction("glAttachShader", out pGLAttachShader);
            loadFunction("glBindAttribLocation", out pGLBindAttribLocation);
            loadFunction("glLinkProgram", out pGLLinkProgram);
            loadFunction("glGetProgramiv", out pGLGetProgramiv);
            loadFunction("glGetProgramInfoLog", out pGLGetProgramInfoLog);
            loadFunction("glGetProgramInterfaceiv", out pGLGetProgramInterfaceiv);
            loadFunction("glGetProgramResourceIndex", out pGLGetProgramResourceIndex);
            loadFunction("glGetProgramResourceName", out pGLGetProgramResourceName);
            loadFunction("glUniformBlockBinding", out pGLUniformBlockBinding);
            loadFunction("glDeleteProgram", out pGLDeleteProgram);
            loadFunction("glUniform1i", out pGLUniform1I);
            loadFunction("glGetUniformBlockIndex", out pGLGetUniformBlockIndex);
            loadFunction("glGetUniformLocation", out pGLGetUniformLocation);
            loadFunction("glGetAttribLocation", out pGLGetAttribLocation);
            loadFunction("glUseProgram", out pGLUseProgram);
            loadFunction("glBindBufferRange", out pGLBindBufferRange);
            loadFunction("glDebugMessageCallback", out pGLDebugMessageCallback);
            loadFunction("glBufferData", out pGLBufferData);
            loadFunction("glNamedBufferData", out pGLNamedBufferData);
            loadFunction("glTexImage2D", out pGLTexImage2D);
            loadFunction("glTexImage3D", out pGLTexImage3D);
            loadFunction("glEnableVertexAttribArray", out pGLEnableVertexAttribArray);
            loadFunction("glDisableVertexAttribArray", out pGLDisableVertexAttribArray);
            loadFunction("glVertexAttribPointer", out pGLVertexAttribPointer);
            loadFunction("glVertexAttribIPointer", out pGLVertexAttribIPointer);
            loadFunction("glVertexAttribDivisor", out pGLVertexAttribDivisor);
            loadFunction("glFrontFace", out pGLFrontFace);
            loadFunction("glGetIntegerv", out pGLGetIntegerv);
            loadFunction("glBindTextureUnit", out pGLBindTextureUnit);
            loadFunction("glTexParameteri", out pGLTexParameteri);
            loadFunction("glGetStringi", out pGLGetStringi);
            loadFunction("glObjectLabel", out pGLObjectLabel);
            loadFunction("glTexImage2DMultisample", out pGLTexImage2DMultisample);
            loadFunction("glTexImage3DMultisample", out pGLTexImage3DMultisample);
            loadFunction("glBlitFramebuffer", out pGLBlitFramebuffer);
            loadFunction("glFramebufferTextureLayer", out pGLFramebufferTextureLayer);
            loadFunction("glDispatchCompute", out pGLDispatchCompute);
            loadFunction("glShaderStorageBlockBinding", out pGLShaderStorageBlockBinding);
            loadFunction("glDrawElementsIndirect", out pGLDrawElementsIndirect);
            loadFunction("glMultiDrawElementsIndirect", out pGLMultiDrawElementsIndirect);
            loadFunction("glDrawArraysIndirect", out pGLDrawArraysIndirect);
            loadFunction("glMultiDrawArraysIndirect", out pGLMultiDrawArraysIndirect);
            loadFunction("glDispatchComputeIndirect", out pGLDispatchComputeIndirect);
            loadFunction("glBindImageTexture", out pGLBindImageTexture);
            loadFunction("glMemoryBarrier", out pGLMemoryBarrier);
            loadFunction("glTexStorage1D", out pGLTexStorage1D);
            loadFunction("glTexStorage2D", out pGLTexStorage2D);
            loadFunction("glTexStorage3D", out pGLTexStorage3D);
            loadFunction("glTextureStorage1D", out pGLTextureStorage1D);
            loadFunction("glTextureStorage2D", out pGLTextureStorage2D);
            loadFunction("glTextureStorage3D", out pGLTextureStorage3D);
            loadFunction("glTextureStorage2DMultisample", out pGLTextureStorage2DMultisample);
            loadFunction("glTextureStorage3DMultisample", out pGLTextureStorage3DMultisample);
            loadFunction("glTexStorage2DMultisample", out pGLTexStorage2DMultisample);
            loadFunction("glTexStorage3DMultisample", out pGLTexStorage3DMultisample);

            loadFunction("glMapBuffer", out pGLMapBuffer);
            loadFunction("glMapNamedBuffer", out pGLMapNamedBuffer);
            loadFunction("glUnmapBuffer", out pGLUnmapBuffer);
            loadFunction("glUnmapNamedBuffer", out pGLUnmapNamedBuffer);
            loadFunction("glCopyBufferSubData", out pGLCopyBufferSubData);
            loadFunction("glCopyTexSubImage2D", out pGLCopyTexSubImage2D);
            loadFunction("glCopyTexSubImage3D", out pGLCopyTexSubImage3D);
            loadFunction("glMapBufferRange", out pGLMapBufferRange);
            loadFunction("glMapNamedBufferRange", out pGLMapNamedBufferRange);
            loadFunction("glGetTextureSubImage", out pGLGetTextureSubImage);
            loadFunction("glCopyNamedBufferSubData", out pGLCopyNamedBufferSubData);
            loadFunction("glCreateBuffers", out pGLCreateBuffers);
            loadFunction("glCreateTextures", out pGLCreateTextures);
            loadFunction("glGenerateMipmap", out pGLGenerateMipmap);
            loadFunction("glGetFramebufferAttachmentParameteriv", out pGLGetFramebufferAttachmentParameteriv);
            loadFunction("glFlush", out pGLFlush);
            loadFunction("glFinish", out pGLFinish);

            loadFunction("glPushDebugGroup", out pGLPushDebugGroup);
            loadFunction("glPopDebugGroup", out pGLPopDebugGroup);
            loadFunction("glDebugMessageInsert", out pGLDebugMessageInsert);

            loadFunction("glReadPixels", out pGLReadPixels);

            if (!gles)
            {
                loadFunction("glFramebufferTexture1D", out pGLFramebufferTexture1D);
                loadFunction("glGetTexImage", out pGLGetTexImage);
                loadFunction("glPolygonMode", out pGLPolygonMode);
                loadFunction("glViewportIndexedf", out pGLViewportIndexedf);
                loadFunction("glCopyImageSubData", out pGLCopyImageSubData);
                loadFunction("glTextureView", out pGLTextureView);
                loadFunction("glGenerateTextureMipmap", out pGLGenerateTextureMipmap);
                loadFunction("glClipControl", out pGLClipControl);
                loadFunction("glDrawElementsInstancedBaseVertexBaseInstance", out pGLDrawElementsInstancedBaseVertexBaseInstance);
            }
            else
            {
                loadFunction("glViewport", out pGLViewport);
                loadFunction("glDepthRangef", out pGLDepthRangef);
                loadFunction("glScissor", out pGLScissor);
                loadFunction("glCopyImageSubData", out pGLCopyImageSubData);

                if (pGLCopyImageSubData == null)
                {
                    loadFunction("glCopyImageSubDataOES", out pGLCopyImageSubData);
                }

                if (pGLCopyImageSubData == null)
                {
                    loadFunction("glCopyImageSubDataEXT", out pGLCopyImageSubData);
                }

                loadFunction("glTextureView", out pGLTextureView);

                if (pGLTextureView == null)
                {
                    loadFunction("glTextureViewOES", out pGLTextureView);
                }

                loadFunction("glRenderbufferStorage", out pGLRenderbufferStorage);
                loadFunction("glFramebufferRenderbuffer", out pGLFramebufferRenderbuffer);
                loadFunction("glGetRenderbufferParameteriv", out pGLGetRenderbufferParameteriv);
                loadFunction("glGenRenderbuffers", out pGLGenRenderbuffers);
                loadFunction("glBindRenderbuffer", out pGLBindRenderbuffer);
                loadFunction("glInsertEventMarker", out pGLInsertEventMarker);
                loadFunction("glPushGroupMarker", out pGLPushGroupMarker);
                loadFunction("glPopGroupMarker", out pGLPopGroupMarker);
            }
        }

        private static void loadFunction<T>(string name, out T field)
        {
            IntPtr funcPtr = sGetProcAddress(name);

            if (funcPtr != IntPtr.Zero)
            {
                field = Marshal.GetDelegateForFunctionPointer<T>(funcPtr);
            }
            else
            {
                field = default(T);
            }
        }

        private static void loadFunction<T>(out T field)
        {
            // Slow version using reflection -- prefer above.
            string name = typeof(T).Name;
            name = name.Substring(0, name.Length - 2); // Remove _t
            loadFunction(name, out field);
        }
    }
}

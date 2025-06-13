// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.InteropServices;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MTLRenderPipelineDescriptor(IntPtr ptr)
    {
        public readonly IntPtr NativePtr = ptr;

        public static MTLRenderPipelineDescriptor New()
        {
            var cls = new ObjCClass("MTLRenderPipelineDescriptor");
            var ret = cls.AllocInit<MTLRenderPipelineDescriptor>();
            return ret;
        }

        public MTLFunction VertexFunction
        {
            get => objc_msgSend<MTLFunction>(NativePtr, sel_vertexFunction);
            set => objc_msgSend(NativePtr, sel_setVertexFunction, value.NativePtr);
        }

        public MTLFunction FragmentFunction
        {
            get => objc_msgSend<MTLFunction>(NativePtr, sel_fragmentFunction);
            set => objc_msgSend(NativePtr, sel_setFragmentFunction, value.NativePtr);
        }

        public MTLRenderPipelineColorAttachmentDescriptorArray ColorAttachments
            => objc_msgSend<MTLRenderPipelineColorAttachmentDescriptorArray>(NativePtr, sel_colorAttachments);

        public MTLPixelFormat DepthAttachmentPixelFormat
        {
            get => (MTLPixelFormat)uint_objc_msgSend(NativePtr, sel_depthAttachmentPixelFormat);
            set => objc_msgSend(NativePtr, sel_setDepthAttachmentPixelFormat, (uint)value);
        }

        public MTLPixelFormat StencilAttachmentPixelFormat
        {
            get => (MTLPixelFormat)uint_objc_msgSend(NativePtr, sel_stencilAttachmentPixelFormat);
            set => objc_msgSend(NativePtr, sel_setStencilAttachmentPixelFormat, (uint)value);
        }

        public UIntPtr SampleCount
        {
            get => UIntPtr_objc_msgSend(NativePtr, sel_sampleCount);
            set => objc_msgSend(NativePtr, sel_setSampleCount, value);
        }

        public MTLVertexDescriptor VertexDescriptor => objc_msgSend<MTLVertexDescriptor>(NativePtr, sel_vertexDescriptor);

        public Bool8 AlphaToCoverageEnabled
        {
            get => bool8_objc_msgSend(NativePtr, sel_isAlphaToCoverageEnabled);
            set => objc_msgSend(NativePtr, sel_setAlphaToCoverageEnabled, value);
        }

        private static readonly Selector sel_vertexFunction = "vertexFunction";
        private static readonly Selector sel_setVertexFunction = "setVertexFunction:";
        private static readonly Selector sel_fragmentFunction = "fragmentFunction";
        private static readonly Selector sel_setFragmentFunction = "setFragmentFunction:";
        private static readonly Selector sel_colorAttachments = "colorAttachments";
        private static readonly Selector sel_depthAttachmentPixelFormat = "depthAttachmentPixelFormat";
        private static readonly Selector sel_setDepthAttachmentPixelFormat = "setDepthAttachmentPixelFormat:";
        private static readonly Selector sel_stencilAttachmentPixelFormat = "stencilAttachmentPixelFormat";
        private static readonly Selector sel_setStencilAttachmentPixelFormat = "setStencilAttachmentPixelFormat:";
        private static readonly Selector sel_sampleCount = "sampleCount";
        private static readonly Selector sel_setSampleCount = "setSampleCount:";
        private static readonly Selector sel_vertexDescriptor = "vertexDescriptor";
        private static readonly Selector sel_isAlphaToCoverageEnabled = "isAlphaToCoverageEnabled";
        private static readonly Selector sel_setAlphaToCoverageEnabled = "setAlphaToCoverageEnabled:";
    }
}

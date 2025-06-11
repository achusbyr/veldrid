// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    public struct MTLDepthStencilDescriptor
    {
        public readonly IntPtr NativePtr;

        public MTLDepthStencilDescriptor(IntPtr ptr)
        {
            NativePtr = ptr;
        }

        public MTLCompareFunction DepthCompareFunction
        {
            get => (MTLCompareFunction)uint_objc_msgSend(NativePtr, sel_depth_compare_function);
            set => objc_msgSend(NativePtr, sel_set_depth_compare_function, (uint)value);
        }

        public Bool8 DepthWriteEnabled
        {
            get => bool8_objc_msgSend(NativePtr, sel_is_depth_write_enabled);
            set => objc_msgSend(NativePtr, sel_set_depth_write_enabled, value);
        }

        public MTLStencilDescriptor BackFaceStencil
        {
            get => objc_msgSend<MTLStencilDescriptor>(NativePtr, sel_back_face_stencil);
            set => objc_msgSend(NativePtr, sel_set_back_face_stencil, value.NativePtr);
        }

        public MTLStencilDescriptor FrontFaceStencil
        {
            get => objc_msgSend<MTLStencilDescriptor>(NativePtr, sel_front_face_stencil);
            set => objc_msgSend(NativePtr, sel_set_front_face_stencil, value.NativePtr);
        }

        private static readonly Selector sel_depth_compare_function = "depthCompareFunction";
        private static readonly Selector sel_set_depth_compare_function = "setDepthCompareFunction:";
        private static readonly Selector sel_is_depth_write_enabled = "isDepthWriteEnabled";
        private static readonly Selector sel_set_depth_write_enabled = "setDepthWriteEnabled:";
        private static readonly Selector sel_back_face_stencil = "backFaceStencil";
        private static readonly Selector sel_set_back_face_stencil = "setBackFaceStencil:";
        private static readonly Selector sel_front_face_stencil = "frontFaceStencil";
        private static readonly Selector sel_set_front_face_stencil = "setFrontFaceStencil:";
    }
}

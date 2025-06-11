// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    public struct MTLStencilDescriptor
    {
        public readonly IntPtr NativePtr;

        public MTLStencilOperation StencilFailureOperation
        {
            get => (MTLStencilOperation)uint_objc_msgSend(NativePtr, sel_stencil_failure_operation);
            set => objc_msgSend(NativePtr, sel_set_stencil_failure_operation, (uint)value);
        }

        public MTLStencilOperation DepthFailureOperation
        {
            get => (MTLStencilOperation)uint_objc_msgSend(NativePtr, sel_depth_failure_operation);
            set => objc_msgSend(NativePtr, sel_set_depth_failure_operation, (uint)value);
        }

        public MTLStencilOperation DepthStencilPassOperation
        {
            get => (MTLStencilOperation)uint_objc_msgSend(NativePtr, sel_depth_stencil_pass_operation);
            set => objc_msgSend(NativePtr, sel_set_depth_stencil_pass_operation, (uint)value);
        }

        public MTLCompareFunction StencilCompareFunction
        {
            get => (MTLCompareFunction)uint_objc_msgSend(NativePtr, sel_stencil_compare_function);
            set => objc_msgSend(NativePtr, sel_set_stencil_compare_function, (uint)value);
        }

        public uint ReadMask
        {
            get => uint_objc_msgSend(NativePtr, sel_read_mask);
            set => objc_msgSend(NativePtr, sel_set_read_mask, value);
        }

        public uint WriteMask
        {
            get => uint_objc_msgSend(NativePtr, sel_write_mask);
            set => objc_msgSend(NativePtr, sel_set_write_mask, value);
        }

        private static readonly Selector sel_depth_failure_operation = "depthFailureOperation";
        private static readonly Selector sel_stencil_failure_operation = "stencilFailureOperation";
        private static readonly Selector sel_set_stencil_failure_operation = "setStencilFailureOperation:";
        private static readonly Selector sel_set_depth_failure_operation = "setDepthFailureOperation:";
        private static readonly Selector sel_depth_stencil_pass_operation = "depthStencilPassOperation";
        private static readonly Selector sel_set_depth_stencil_pass_operation = "setDepthStencilPassOperation:";
        private static readonly Selector sel_stencil_compare_function = "stencilCompareFunction";
        private static readonly Selector sel_set_stencil_compare_function = "setStencilCompareFunction:";
        private static readonly Selector sel_read_mask = "readMask";
        private static readonly Selector sel_set_read_mask = "setReadMask:";
        private static readonly Selector sel_write_mask = "writeMask";
        private static readonly Selector sel_set_write_mask = "setWriteMask:";
    }
}

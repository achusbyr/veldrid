// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    public struct MTLVertexBufferLayoutDescriptor(IntPtr ptr)
    {
        public readonly IntPtr NativePtr = ptr;

        public MTLVertexStepFunction StepFunction
        {
            get => (MTLVertexStepFunction)uint_objc_msgSend(NativePtr, sel_step_function);
            set => objc_msgSend(NativePtr, sel_set_step_function, (uint)value);
        }

        public UIntPtr Stride
        {
            get => UIntPtr_objc_msgSend(NativePtr, sel_stride);
            set => objc_msgSend(NativePtr, sel_set_stride, value);
        }

        public UIntPtr StepRate
        {
            get => UIntPtr_objc_msgSend(NativePtr, sel_step_rate);
            set => objc_msgSend(NativePtr, sel_set_step_rate, value);
        }

        private static readonly Selector sel_step_function = "stepFunction";
        private static readonly Selector sel_set_step_function = "setStepFunction:";
        private static readonly Selector sel_stride = "stride";
        private static readonly Selector sel_set_stride = "setStride:";
        private static readonly Selector sel_step_rate = "stepRate";
        private static readonly Selector sel_set_step_rate = "setStepRate:";
    }
}

// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    public struct MTLComputePipelineDescriptor
    {
        public readonly IntPtr NativePtr;

        public MTLFunction ComputeFunction
        {
            get => objc_msgSend<MTLFunction>(NativePtr, sel_compute_function);
            set => objc_msgSend(NativePtr, sel_set_compute_function, value.NativePtr);
        }

        public MTLPipelineBufferDescriptorArray Buffers
            => objc_msgSend<MTLPipelineBufferDescriptorArray>(NativePtr, sel_buffers);

        private static readonly Selector sel_compute_function = "computeFunction";
        private static readonly Selector sel_set_compute_function = "setComputeFunction:";
        private static readonly Selector sel_buffers = "buffers";
    }
}

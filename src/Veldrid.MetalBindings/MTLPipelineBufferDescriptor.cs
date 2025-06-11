// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    public struct MTLPipelineBufferDescriptor
    {
        public readonly IntPtr NativePtr;

        public MTLPipelineBufferDescriptor(IntPtr ptr)
        {
            NativePtr = ptr;
        }

        public MTLMutability Mutability
        {
            get => (MTLMutability)uint_objc_msgSend(NativePtr, sel_mutability);
            set => objc_msgSend(NativePtr, sel_set_mutability, (uint)value);
        }

        private static readonly Selector sel_mutability = "mutability";
        private static readonly Selector sel_set_mutability = "setMutability:";
    }
}

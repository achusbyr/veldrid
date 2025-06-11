// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace Veldrid.MetalBindings
{
    public struct MTLComputePipelineState
    {
        public readonly IntPtr NativePtr;

        public MTLComputePipelineState(IntPtr ptr)
        {
            NativePtr = ptr;
        }

        public bool IsNull => NativePtr == IntPtr.Zero;
    }
}

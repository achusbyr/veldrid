// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace Veldrid.MetalBindings
{
    public struct MTLComputePipelineState(IntPtr ptr)
    {
        public readonly IntPtr NativePtr = ptr;

        public bool IsNull => NativePtr == IntPtr.Zero;
    }
}

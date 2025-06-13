// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    public struct NSArray(IntPtr ptr)
    {
        public readonly IntPtr NativePtr = ptr;

        public UIntPtr Count => UIntPtr_objc_msgSend(NativePtr, sel_count);
        private static readonly Selector sel_count = "count";
    }
}

// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace Veldrid.MetalBindings
{
    public struct NSDictionary
    {
        public readonly IntPtr NativePtr;

        public UIntPtr Count => ObjectiveCRuntime.UIntPtr_objc_msgSend(NativePtr, sel_count);

        private static readonly Selector sel_count = "count";
    }
}

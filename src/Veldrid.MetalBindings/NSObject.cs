// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    public struct NSObject(IntPtr ptr)
    {
        public readonly IntPtr NativePtr = ptr;

        public Bool8 IsKindOfClass(IntPtr @class)
        {
            return bool8_objc_msgSend(NativePtr, sel_is_kind_of_class, @class);
        }

        private static readonly Selector sel_is_kind_of_class = "isKindOfClass:";
    }
}

// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace Veldrid.MetalBindings
{
    public struct MTLFunctionConstantValues
    {
        public readonly IntPtr NativePtr;

        public static MTLFunctionConstantValues New()
        {
            return s_class.AllocInit<MTLFunctionConstantValues>();
        }

        public unsafe void SetConstantValuetypeatIndex(void* value, MTLDataType type, UIntPtr index)
        {
            ObjectiveCRuntime.objc_msgSend(NativePtr, sel_set_constant_valuetypeat_index, value, (uint)type, index);
        }

        private static readonly ObjCClass s_class = new ObjCClass(nameof(MTLFunctionConstantValues));
        private static readonly Selector sel_set_constant_valuetypeat_index = "setConstantValue:type:atIndex:";
    }
}

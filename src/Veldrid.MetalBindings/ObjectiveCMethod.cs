// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace Veldrid.MetalBindings
{
    public struct ObjectiveCMethod(IntPtr ptr)
    {
        public readonly IntPtr NativePtr = ptr;

        public static implicit operator IntPtr(ObjectiveCMethod method)
        {
            return method.NativePtr;
        }

        public static implicit operator ObjectiveCMethod(IntPtr ptr)
        {
            return new ObjectiveCMethod(ptr);
        }

        public Selector GetSelector()
        {
            return ObjectiveCRuntime.method_getName(this);
        }

        public string GetName()
        {
            return GetSelector().Name;
        }
    }
}

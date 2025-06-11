// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace Veldrid.MetalBindings
{
    public struct NSAutoreleasePool : IDisposable
    {
        private static readonly ObjCClass s_class = new ObjCClass(nameof(NSAutoreleasePool));
        public readonly IntPtr NativePtr;

        public NSAutoreleasePool(IntPtr ptr)
        {
            NativePtr = ptr;
        }

        public static NSAutoreleasePool Begin()
        {
            return s_class.AllocInit<NSAutoreleasePool>();
        }

        public void Dispose()
        {
            ObjectiveCRuntime.Release(NativePtr);
        }
    }
}

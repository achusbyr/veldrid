// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.InteropServices;

namespace Veldrid.MetalBindings
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MTLBuffer(IntPtr ptr)
    {
        public readonly IntPtr NativePtr = ptr;

        public bool IsNull => NativePtr == IntPtr.Zero;

        public void* Contents()
        {
            return ObjectiveCRuntime.IntPtr_objc_msgSend(NativePtr, sel_contents).ToPointer();
        }

        public UIntPtr Length => ObjectiveCRuntime.UIntPtr_objc_msgSend(NativePtr, sel_length);

        public void DidModifyRange(NSRange range)
        {
            ObjectiveCRuntime.objc_msgSend(NativePtr, sel_didModifyRange, range);
        }

        public void AddDebugMarker(NSString marker, NSRange range)
        {
            ObjectiveCRuntime.objc_msgSend(NativePtr, sel_addDebugMarker, marker.NativePtr, range);
        }

        public void RemoveAllDebugMarkers()
        {
            ObjectiveCRuntime.objc_msgSend(NativePtr, sel_removeAllDebugMarkers);
        }

        private static readonly Selector sel_contents = "contents";
        private static readonly Selector sel_length = "length";
        private static readonly Selector sel_didModifyRange = "didModifyRange:";
        private static readonly Selector sel_addDebugMarker = "addDebugMarker:range:";
        private static readonly Selector sel_removeAllDebugMarkers = "removeAllDebugMarkers";
    }
}

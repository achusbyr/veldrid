// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    public struct NSWindow
    {
        public readonly IntPtr NativePtr;

        public NSWindow(IntPtr ptr)
        {
            NativePtr = ptr;
        }

        public NSView ContentView => objc_msgSend<NSView>(NativePtr, sel_content_view);

        private static readonly Selector sel_content_view = "contentView";
    }
}

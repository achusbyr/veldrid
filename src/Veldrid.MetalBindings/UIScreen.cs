// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    public struct UIScreen
    {
        public readonly IntPtr NativePtr;

        public UIScreen(IntPtr ptr)
        {
            NativePtr = ptr;
        }

        public CGFloat NativeScale => CGFloat_objc_msgSend(NativePtr, sel_native_scale);

        public static UIScreen MainScreen
            => objc_msgSend<UIScreen>(new ObjCClass(nameof(UIScreen)), sel_main_screen);

        private static readonly Selector sel_native_scale = "nativeScale";
        private static readonly Selector sel_main_screen = "mainScreen";
    }
}

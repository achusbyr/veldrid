// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.InteropServices;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    public struct NSView
    {
        public readonly IntPtr NativePtr;

        public static implicit operator IntPtr(NSView nsView)
        {
            return nsView.NativePtr;
        }

        public NSView(IntPtr ptr)
        {
            NativePtr = ptr;
        }

        public Bool8 WantsLayer
        {
            get => bool8_objc_msgSend(NativePtr, sel_wants_layer);
            set => objc_msgSend(NativePtr, sel_set_wants_layer, value);
        }

        public IntPtr Layer
        {
            get => IntPtr_objc_msgSend(NativePtr, sel_layer);
            set => objc_msgSend(NativePtr, sel_set_layer, value);
        }

        public CGRect Frame =>
            RuntimeInformation.ProcessArchitecture == Architecture.Arm64
                ? CGRect_objc_msgSend(NativePtr, sel_frame)
                : objc_msgSend_stret<CGRect>(NativePtr, sel_frame);

        private static readonly Selector sel_wants_layer = "wantsLayer";
        private static readonly Selector sel_set_wants_layer = "setWantsLayer:";
        private static readonly Selector sel_layer = "layer";
        private static readonly Selector sel_set_layer = "setLayer:";
        private static readonly Selector sel_frame = "frame";
    }
}

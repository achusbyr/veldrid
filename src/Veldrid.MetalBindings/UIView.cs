// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.InteropServices;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    public struct UIView(IntPtr ptr)
    {
        public readonly IntPtr NativePtr = ptr;

        public CALayer Layer => objc_msgSend<CALayer>(NativePtr, sel_layer);

        public CGRect Frame =>
            RuntimeInformation.ProcessArchitecture == Architecture.Arm64
                ? CGRect_objc_msgSend(NativePtr, sel_frame)
                : objc_msgSend_stret<CGRect>(NativePtr, sel_frame);

        private static readonly Selector sel_layer = "layer";
        private static readonly Selector sel_frame = "frame";
    }
}

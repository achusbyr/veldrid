// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    public struct CALayer
    {
        public readonly IntPtr NativePtr;

        public static implicit operator IntPtr(CALayer c)
        {
            return c.NativePtr;
        }

        public CALayer(IntPtr ptr)
        {
            NativePtr = ptr;
        }

        public void AddSublayer(IntPtr layer)
        {
            objc_msgSend(NativePtr, sel_add_sublayer, layer);
        }

        private static readonly Selector sel_add_sublayer = "addSublayer:";
    }
}

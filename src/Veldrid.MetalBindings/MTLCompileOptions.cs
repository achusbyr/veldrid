// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.InteropServices;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MTLCompileOptions
    {
        public readonly IntPtr NativePtr;

        public static implicit operator IntPtr(MTLCompileOptions mco)
        {
            return mco.NativePtr;
        }

        public static MTLCompileOptions New()
        {
            return s_class.AllocInit<MTLCompileOptions>();
        }

        public Bool8 FastMathEnabled
        {
            get => bool8_objc_msgSend(NativePtr, sel_fastMathEnabled);
            set => objc_msgSend(NativePtr, sel_setFastMathEnabled, value);
        }

        public MTLLanguageVersion LanguageVersion
        {
            get => (MTLLanguageVersion)uint_objc_msgSend(NativePtr, sel_languageVersion);
            set => objc_msgSend(NativePtr, sel_setLanguageVersion, (uint)value);
        }

        private static readonly ObjCClass s_class = new ObjCClass(nameof(MTLCompileOptions));
        private static readonly Selector sel_fastMathEnabled = "fastMathEnabled";
        private static readonly Selector sel_setFastMathEnabled = "setFastMathEnabled:";
        private static readonly Selector sel_languageVersion = "languageVersion";
        private static readonly Selector sel_setLanguageVersion = "setLanguageVersion:";
    }
}

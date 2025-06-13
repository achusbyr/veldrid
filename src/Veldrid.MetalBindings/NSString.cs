// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    public unsafe struct NSString(IntPtr ptr)
    {
        public readonly IntPtr NativePtr = ptr;

        public static implicit operator IntPtr(NSString nss)
        {
            return nss.NativePtr;
        }

        public static NSString New(string s)
        {
            var nss = s_class.Alloc<NSString>();

            fixed (char* utf16Ptr = s)
            {
                UIntPtr length = (UIntPtr)s.Length;
                IntPtr newString = IntPtr_objc_msgSend(nss, sel_init_with_characters, (IntPtr)utf16Ptr, length);
                return new NSString(newString);
            }
        }

        public string GetValue()
        {
            byte* utf8Ptr = bytePtr_objc_msgSend(NativePtr, sel_utf8_string);
            return MTLUtil.GetUtf8String(utf8Ptr);
        }

        private static readonly ObjCClass s_class = new ObjCClass(nameof(NSString));
        private static readonly Selector sel_init_with_characters = "initWithCharacters:length:";
        private static readonly Selector sel_utf8_string = "UTF8String";
    }
}

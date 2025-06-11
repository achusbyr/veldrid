// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Veldrid.MetalBindings
{
    public unsafe struct ObjCClass
    {
        public readonly IntPtr NativePtr;

        public static implicit operator IntPtr(ObjCClass c)
        {
            return c.NativePtr;
        }

        public ObjCClass(string name)
        {
            int byteCount = Encoding.UTF8.GetMaxByteCount(name.Length);
            byte* utf8BytesPtr = stackalloc byte[byteCount];

            fixed (char* namePtr = name)
            {
                Encoding.UTF8.GetBytes(namePtr, name.Length, utf8BytesPtr, byteCount);
            }

            NativePtr = ObjectiveCRuntime.objc_getClass(utf8BytesPtr);
        }

        public IntPtr GetProperty(string propertyName)
        {
            int byteCount = Encoding.UTF8.GetMaxByteCount(propertyName.Length);
            byte* utf8BytesPtr = stackalloc byte[byteCount];

            fixed (char* namePtr = propertyName)
            {
                Encoding.UTF8.GetBytes(namePtr, propertyName.Length, utf8BytesPtr, byteCount);
            }

            return ObjectiveCRuntime.class_getProperty(this, utf8BytesPtr);
        }

        public string Name => MTLUtil.GetUtf8String(ObjectiveCRuntime.class_getName(this));

        public T Alloc<T>() where T : struct
        {
            IntPtr value = ObjectiveCRuntime.IntPtr_objc_msgSend(NativePtr, Selectors.ALLOC);
            return Unsafe.AsRef<T>(&value);
        }

        public T AllocInit<T>() where T : struct
        {
            IntPtr value = ObjectiveCRuntime.IntPtr_objc_msgSend(NativePtr, Selectors.ALLOC);
            ObjectiveCRuntime.objc_msgSend(value, Selectors.INIT);
            return Unsafe.AsRef<T>(&value);
        }

        public ObjectiveCMethod* class_copyMethodList(out uint count)
        {
            return ObjectiveCRuntime.class_copyMethodList(this, out count);
        }
    }
}

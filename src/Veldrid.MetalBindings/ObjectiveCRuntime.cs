// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Veldrid.MetalBindings
{
    public static unsafe class ObjectiveCRuntime
    {
        private const string obj_c_library = "/usr/lib/libobjc.A.dylib";

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, float a);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, double a);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, CGRect a);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, IntPtr a, uint b);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, IntPtr a, NSRange b);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, MTLSize a, MTLSize b);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, IntPtr c, UIntPtr d, MTLSize e);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, MTLClearColor a);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, CGSize a);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, IntPtr a, UIntPtr b, UIntPtr c);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, UIntPtr b, UIntPtr c);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, void* a, UIntPtr b, UIntPtr c);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, MTLPrimitiveType a, UIntPtr b, UIntPtr c, UIntPtr d, UIntPtr e);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, MTLPrimitiveType a, UIntPtr b, UIntPtr c, UIntPtr d);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, NSRange a);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, UIntPtr a);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, MTLCommandBufferHandler a);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, IntPtr a, UIntPtr b);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, MTLViewport a);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, MTLScissorRect a);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, void* a, uint b, UIntPtr c);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, void* a, UIntPtr b);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, MTLPrimitiveType a, UIntPtr b, MTLIndexType c, IntPtr d, UIntPtr e, UIntPtr f);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, MTLPrimitiveType a, MTLBuffer b, UIntPtr c);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(
            IntPtr receiver,
            Selector selector,
            MTLPrimitiveType a,
            UIntPtr b,
            MTLIndexType c,
            IntPtr d,
            UIntPtr e,
            UIntPtr f,
            IntPtr g,
            UIntPtr h);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(
            IntPtr receiver,
            Selector selector,
            MTLPrimitiveType a,
            MTLIndexType b,
            MTLBuffer c,
            UIntPtr d,
            MTLBuffer e,
            UIntPtr f);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(
            IntPtr receiver,
            Selector selector,
            MTLBuffer a,
            UIntPtr b,
            MTLBuffer c,
            UIntPtr d,
            UIntPtr e);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(
            IntPtr receiver,
            Selector selector,
            IntPtr a,
            UIntPtr b,
            UIntPtr c,
            UIntPtr d,
            MTLSize e,
            IntPtr f,
            UIntPtr g,
            UIntPtr h,
            MTLOrigin i);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(
            IntPtr receiver,
            Selector selector,
            MTLRegion a,
            UIntPtr b,
            UIntPtr c,
            IntPtr d,
            UIntPtr e,
            UIntPtr f);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(
            IntPtr receiver,
            Selector selector,
            MTLTexture a,
            UIntPtr b,
            UIntPtr c,
            MTLOrigin d,
            MTLSize e,
            MTLBuffer f,
            UIntPtr g,
            UIntPtr h,
            UIntPtr i);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(
            IntPtr receiver,
            Selector selector,
            MTLTexture sourceTexture,
            UIntPtr sourceSlice,
            UIntPtr sourceLevel,
            MTLOrigin sourceOrigin,
            MTLSize sourceSize,
            MTLTexture destinationTexture,
            UIntPtr destinationSlice,
            UIntPtr destinationLevel,
            MTLOrigin destinationOrigin);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern byte* bytePtr_objc_msgSend(IntPtr receiver, Selector selector);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern CGSize CGSize_objc_msgSend(IntPtr receiver, Selector selector);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern byte byte_objc_msgSend(IntPtr receiver, Selector selector);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern Bool8 bool8_objc_msgSend(IntPtr receiver, Selector selector);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern Bool8 bool8_objc_msgSend(IntPtr receiver, Selector selector, UIntPtr a);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern Bool8 bool8_objc_msgSend(IntPtr receiver, Selector selector, IntPtr a);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern Bool8 bool8_objc_msgSend(IntPtr receiver, Selector selector, UIntPtr a, IntPtr b);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern Bool8 bool8_objc_msgSend(IntPtr receiver, Selector selector, uint a);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern uint uint_objc_msgSend(IntPtr receiver, Selector selector);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern float float_objc_msgSend(IntPtr receiver, Selector selector);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern CGFloat CGFloat_objc_msgSend(IntPtr receiver, Selector selector);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern double double_objc_msgSend(IntPtr receiver, Selector selector);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern IntPtr IntPtr_objc_msgSend(IntPtr receiver, Selector selector);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern IntPtr IntPtr_objc_msgSend(IntPtr receiver, Selector selector, IntPtr a);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern IntPtr IntPtr_objc_msgSend(IntPtr receiver, Selector selector, IntPtr a, out NSError error);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern IntPtr IntPtr_objc_msgSend(IntPtr receiver, Selector selector, uint a, uint b, NSRange c, NSRange d);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern IntPtr IntPtr_objc_msgSend(IntPtr receiver, Selector selector, MTLComputePipelineDescriptor a, uint b, IntPtr c, out NSError error);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern IntPtr IntPtr_objc_msgSend(IntPtr receiver, Selector selector, uint a);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern IntPtr IntPtr_objc_msgSend(IntPtr receiver, Selector selector, UIntPtr a);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern IntPtr IntPtr_objc_msgSend(IntPtr receiver, Selector selector, IntPtr a, IntPtr b, out NSError error);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern IntPtr IntPtr_objc_msgSend(IntPtr receiver, Selector selector, IntPtr a, UIntPtr b);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern IntPtr IntPtr_objc_msgSend(IntPtr receiver, Selector selector, UIntPtr b, MTLResourceOptions c);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern IntPtr IntPtr_objc_msgSend(IntPtr receiver, Selector selector, void* a, UIntPtr b, MTLResourceOptions c);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern UIntPtr UIntPtr_objc_msgSend(IntPtr receiver, Selector selector);

        public static T objc_msgSend<T>(IntPtr receiver, Selector selector) where T : struct
        {
            IntPtr value = IntPtr_objc_msgSend(receiver, selector);
            return Unsafe.AsRef<T>(&value);
        }

        public static T objc_msgSend<T>(IntPtr receiver, Selector selector, IntPtr a) where T : struct
        {
            IntPtr value = IntPtr_objc_msgSend(receiver, selector, a);
            return Unsafe.AsRef<T>(&value);
        }

        public static string string_objc_msgSend(IntPtr receiver, Selector selector)
        {
            return objc_msgSend<NSString>(receiver, selector).GetValue();
        }

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, byte b);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, Bool8 b);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, uint b);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, float a, float b, float c, float d);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern void objc_msgSend(IntPtr receiver, Selector selector, IntPtr b);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend_stret")]
        public static extern void objc_msgSend_stret(void* retPtr, IntPtr receiver, Selector selector);

        public static T objc_msgSend_stret<T>(IntPtr receiver, Selector selector) where T : struct
        {
            T ret = default(T);
            objc_msgSend_stret(Unsafe.AsPointer(ref ret), receiver, selector);
            return ret;
        }

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern MTLClearColor MTLClearColor_objc_msgSend(IntPtr receiver, Selector selector);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern MTLSize MTLSize_objc_msgSend(IntPtr receiver, Selector selector);

        [DllImport(obj_c_library, EntryPoint = "objc_msgSend")]
        public static extern CGRect CGRect_objc_msgSend(IntPtr receiver, Selector selector);

        // TODO: This should check the current processor type, struct size, etc.
        // At the moment there is no need because all existing occurences of
        // this can safely use the non-stret versions everywhere.
        public static bool UseStret<T>()
        {
            return false;
        }

        [DllImport(obj_c_library)]
        public static extern IntPtr sel_registerName(byte* namePtr);

        [DllImport(obj_c_library)]
        public static extern byte* sel_getName(IntPtr selector);

        [DllImport(obj_c_library)]
        public static extern IntPtr objc_getClass(byte* namePtr);

        [DllImport(obj_c_library)]
        public static extern ObjCClass object_getClass(IntPtr obj);

        [DllImport(obj_c_library)]
        public static extern IntPtr class_getProperty(ObjCClass cls, byte* namePtr);

        [DllImport(obj_c_library)]
        public static extern byte* class_getName(ObjCClass cls);

        [DllImport(obj_c_library)]
        public static extern byte* property_copyAttributeValue(IntPtr property, byte* attributeNamePtr);

        [DllImport(obj_c_library)]
        public static extern Selector method_getName(ObjectiveCMethod method);

        [DllImport(obj_c_library)]
        public static extern ObjectiveCMethod* class_copyMethodList(ObjCClass cls, out uint outCount);

        [DllImport(obj_c_library)]
        public static extern void free(IntPtr receiver);

        public static void Retain(IntPtr receiver)
        {
            objc_msgSend(receiver, sel_retain);
        }

        public static void Release(IntPtr receiver)
        {
            objc_msgSend(receiver, sel_release);
        }

        public static ulong GetRetainCount(IntPtr receiver)
        {
            return UIntPtr_objc_msgSend(receiver, sel_retain_count);
        }

        private static readonly Selector sel_retain = "retain";
        private static readonly Selector sel_release = "release";
        private static readonly Selector sel_retain_count = "retainCount";
    }
}

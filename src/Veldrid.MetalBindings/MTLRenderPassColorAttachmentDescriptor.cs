// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    public struct MTLRenderPassColorAttachmentDescriptor
    {
        public readonly IntPtr NativePtr;

        public MTLRenderPassColorAttachmentDescriptor(IntPtr ptr)
        {
            NativePtr = ptr;
        }

        public MTLTexture Texture
        {
            get => objc_msgSend<MTLTexture>(NativePtr, Selectors.TEXTURE);
            set => objc_msgSend(NativePtr, Selectors.SET_TEXTURE, value.NativePtr);
        }

        public MTLLoadAction LoadAction
        {
            get => (MTLLoadAction)uint_objc_msgSend(NativePtr, Selectors.LOAD_ACTION);
            set => objc_msgSend(NativePtr, Selectors.SET_LOAD_ACTION, (uint)value);
        }

        public MTLStoreAction StoreAction
        {
            get => (MTLStoreAction)uint_objc_msgSend(NativePtr, Selectors.STORE_ACTION);
            set => objc_msgSend(NativePtr, Selectors.SET_STORE_ACTION, (uint)value);
        }

        public MTLTexture ResolveTexture
        {
            get => objc_msgSend<MTLTexture>(NativePtr, Selectors.RESOLVE_TEXTURE);
            set => objc_msgSend(NativePtr, Selectors.SET_RESOLVE_TEXTURE, value.NativePtr);
        }

        public MTLClearColor ClearColor
        {
            get
            {
                if (UseStret<MTLClearColor>())
                {
                    return objc_msgSend_stret<MTLClearColor>(NativePtr, sel_clear_color);
                }

                return MTLClearColor_objc_msgSend(NativePtr, sel_clear_color);
            }
            set => objc_msgSend(NativePtr, sel_set_clear_color, value);
        }

        public UIntPtr Slice
        {
            get => UIntPtr_objc_msgSend(NativePtr, Selectors.SLICE);
            set => objc_msgSend(NativePtr, Selectors.SET_SLICE, value);
        }

        public UIntPtr Level
        {
            get => UIntPtr_objc_msgSend(NativePtr, Selectors.LEVEL);
            set => objc_msgSend(NativePtr, Selectors.SET_LEVEL, value);
        }

        private static readonly Selector sel_clear_color = "clearColor";
        private static readonly Selector sel_set_clear_color = "setClearColor:";
    }
}

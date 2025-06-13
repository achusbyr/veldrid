// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    public struct MTLRenderPassDepthAttachmentDescriptor(IntPtr ptr)
    {
        public readonly IntPtr NativePtr = ptr;

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

        public double ClearDepth
        {
            get => double_objc_msgSend(NativePtr, sel_clear_depth);
            set => objc_msgSend(NativePtr, sel_set_clear_depth, value);
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

        private static readonly Selector sel_clear_depth = "clearDepth";
        private static readonly Selector sel_set_clear_depth = "setClearDepth:";
    }
}

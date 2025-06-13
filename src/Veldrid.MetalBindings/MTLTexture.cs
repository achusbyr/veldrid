// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.InteropServices;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MTLTexture(IntPtr ptr)
    {
        public readonly IntPtr NativePtr = ptr;

        public bool IsNull => NativePtr == IntPtr.Zero;

        public void ReplaceRegion(
            MTLRegion region,
            UIntPtr mipmapLevel,
            UIntPtr slice,
            void* pixelBytes,
            UIntPtr bytesPerRow,
            UIntPtr bytesPerImage)
        {
            objc_msgSend(NativePtr, sel_replaceRegion,
                region,
                mipmapLevel,
                slice,
                (IntPtr)pixelBytes,
                bytesPerRow,
                bytesPerImage);
        }

        public MTLTexture NewTextureView(
            MTLPixelFormat pixelFormat,
            MTLTextureType textureType,
            NSRange levelRange,
            NSRange sliceRange)
        {
            IntPtr ret = IntPtr_objc_msgSend(NativePtr, sel_newTextureView,
                (uint)pixelFormat, (uint)textureType, levelRange, sliceRange);
            return new MTLTexture(ret);
        }

        private static readonly Selector sel_replaceRegion = "replaceRegion:mipmapLevel:slice:withBytes:bytesPerRow:bytesPerImage:";
        private static readonly Selector sel_newTextureView = "newTextureViewWithPixelFormat:textureType:levels:slices:";
    }
}

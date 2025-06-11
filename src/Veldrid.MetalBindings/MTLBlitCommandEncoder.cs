// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.InteropServices;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    public struct MTLBlitCommandEncoder
    {
        public readonly IntPtr NativePtr;
        public bool IsNull => NativePtr == IntPtr.Zero;

        public void Copy(
            MTLBuffer sourceBuffer,
            UIntPtr sourceOffset,
            MTLBuffer destinationBuffer,
            UIntPtr destinationOffset,
            UIntPtr size)
        {
            objc_msgSend(
                NativePtr,
                sel_copy_from_buffer0,
                sourceBuffer, sourceOffset, destinationBuffer, destinationOffset, size);
        }

        public void CopyFromBuffer(
            MTLBuffer sourceBuffer,
            UIntPtr sourceOffset,
            UIntPtr sourceBytesPerRow,
            UIntPtr sourceBytesPerImage,
            MTLSize sourceSize,
            MTLTexture destinationTexture,
            UIntPtr destinationSlice,
            UIntPtr destinationLevel,
            MTLOrigin destinationOrigin,
            bool isMacOS)
        {
            if (!isMacOS)
            {
                copyFromBuffer_iOS(
                    NativePtr,
                    sourceBuffer.NativePtr,
                    sourceOffset,
                    sourceBytesPerRow,
                    sourceBytesPerImage,
                    sourceSize,
                    destinationTexture.NativePtr,
                    destinationSlice,
                    destinationLevel,
                    destinationOrigin.X,
                    destinationOrigin.Y,
                    destinationOrigin.Z);
            }
            else
            {
                objc_msgSend(NativePtr,
                    sel_copy_from_buffer1,
                    sourceBuffer.NativePtr,
                    sourceOffset,
                    sourceBytesPerRow,
                    sourceBytesPerImage,
                    sourceSize,
                    destinationTexture.NativePtr,
                    destinationSlice,
                    destinationLevel,
                    destinationOrigin);
            }
        }

        [DllImport("@rpath/metal_mono_workaround.framework/metal_mono_workaround", EntryPoint = "copyFromBuffer")]
        private static extern void copyFromBuffer_iOS(
            IntPtr encoder,
            IntPtr sourceBuffer,
            UIntPtr sourceOffset,
            UIntPtr sourceBytesPerRow,
            UIntPtr sourceBytesPerImage,
            MTLSize sourceSize,
            IntPtr destinationTexture,
            UIntPtr destinationSlice,
            UIntPtr destinationLevel,
            UIntPtr destinationOriginX,
            UIntPtr destinationOriginY,
            UIntPtr destinationOriginZ);

        public void CopyTextureToBuffer(
            MTLTexture sourceTexture,
            UIntPtr sourceSlice,
            UIntPtr sourceLevel,
            MTLOrigin sourceOrigin,
            MTLSize sourceSize,
            MTLBuffer destinationBuffer,
            UIntPtr destinationOffset,
            UIntPtr destinationBytesPerRow,
            UIntPtr destinationBytesPerImage)
        {
            objc_msgSend(NativePtr, sel_copy_from_texture0,
                sourceTexture,
                sourceSlice,
                sourceLevel,
                sourceOrigin,
                sourceSize,
                destinationBuffer,
                destinationOffset,
                destinationBytesPerRow,
                destinationBytesPerImage);
        }

        public void GenerateMipmapsForTexture(MTLTexture texture)
        {
            objc_msgSend(NativePtr, sel_generate_mipmaps_for_texture, texture.NativePtr);
        }

        public void SynchronizeResource(IntPtr resource)
        {
            objc_msgSend(NativePtr, sel_synchronize_resource, resource);
        }

        public void EndEncoding()
        {
            objc_msgSend(NativePtr, sel_end_encoding);
        }

        public void PushDebugGroup(NSString @string)
        {
            objc_msgSend(NativePtr, Selectors.PUSH_DEBUG_GROUP, @string.NativePtr);
        }

        public void PopDebugGroup()
        {
            objc_msgSend(NativePtr, Selectors.POP_DEBUG_GROUP);
        }

        public void InsertDebugSignpost(NSString @string)
        {
            objc_msgSend(NativePtr, Selectors.INSERT_DEBUG_SIGNPOST, @string.NativePtr);
        }

        public void CopyFromTexture(
            MTLTexture sourceTexture,
            UIntPtr sourceSlice,
            UIntPtr sourceLevel,
            MTLOrigin sourceOrigin,
            MTLSize sourceSize,
            MTLTexture destinationTexture,
            UIntPtr destinationSlice,
            UIntPtr destinationLevel,
            MTLOrigin destinationOrigin,
            bool isMacOS)
        {
            if (!isMacOS)
            {
                copyFromTexture_iOS(
                    NativePtr,
                    sourceTexture.NativePtr,
                    sourceSlice,
                    sourceLevel,
                    sourceOrigin,
                    sourceSize,
                    destinationTexture.NativePtr,
                    destinationSlice,
                    destinationLevel,
                    destinationOrigin.X,
                    destinationOrigin.Y,
                    destinationOrigin.Z);
            }
            else
            {
                objc_msgSend(NativePtr, sel_copy_from_texture1,
                    sourceTexture,
                    sourceSlice,
                    sourceLevel,
                    sourceOrigin,
                    sourceSize,
                    destinationTexture,
                    destinationSlice,
                    destinationLevel,
                    destinationOrigin);
            }
        }

        [DllImport("@rpath/metal_mono_workaround.framework/metal_mono_workaround", EntryPoint = "copyFromTexture")]
        private static extern void copyFromTexture_iOS(
            IntPtr encoder,
            IntPtr sourceTexture,
            UIntPtr sourceSlice,
            UIntPtr sourceLevel,
            MTLOrigin sourceOrigin,
            MTLSize sourceSize,
            IntPtr destinationTexture,
            UIntPtr destinationSlice,
            UIntPtr destinationLevel,
            UIntPtr destinationOriginX,
            UIntPtr destinationOriginY,
            UIntPtr destinationOriginZ);

        private static readonly Selector sel_copy_from_buffer0 = "copyFromBuffer:sourceOffset:toBuffer:destinationOffset:size:";

        private static readonly Selector sel_copy_from_buffer1 =
            "copyFromBuffer:sourceOffset:sourceBytesPerRow:sourceBytesPerImage:sourceSize:toTexture:destinationSlice:destinationLevel:destinationOrigin:";

        private static readonly Selector sel_copy_from_texture0 =
            "copyFromTexture:sourceSlice:sourceLevel:sourceOrigin:sourceSize:toBuffer:destinationOffset:destinationBytesPerRow:destinationBytesPerImage:";

        private static readonly Selector sel_copy_from_texture1 = "copyFromTexture:sourceSlice:sourceLevel:sourceOrigin:sourceSize:toTexture:destinationSlice:destinationLevel:destinationOrigin:";
        private static readonly Selector sel_generate_mipmaps_for_texture = "generateMipmapsForTexture:";
        private static readonly Selector sel_synchronize_resource = "synchronizeResource:";
        private static readonly Selector sel_end_encoding = "endEncoding";
    }
}

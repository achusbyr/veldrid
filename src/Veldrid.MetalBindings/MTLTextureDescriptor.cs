// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    public struct MTLTextureDescriptor
    {
        private static readonly ObjCClass s_class = new ObjCClass(nameof(MTLTextureDescriptor));
        public readonly IntPtr NativePtr;

        public static MTLTextureDescriptor New()
        {
            return s_class.AllocInit<MTLTextureDescriptor>();
        }

        public MTLTextureType TextureType
        {
            get => (MTLTextureType)uint_objc_msgSend(NativePtr, sel_texture_type);
            set => objc_msgSend(NativePtr, sel_set_texture_type, (uint)value);
        }

        public MTLPixelFormat PixelFormat
        {
            get => (MTLPixelFormat)uint_objc_msgSend(NativePtr, Selectors.PIXEL_FORMAT);
            set => objc_msgSend(NativePtr, Selectors.SET_PIXEL_FORMAT, (uint)value);
        }

        public UIntPtr Width
        {
            get => UIntPtr_objc_msgSend(NativePtr, sel_width);
            set => objc_msgSend(NativePtr, sel_set_width, value);
        }

        public UIntPtr Height
        {
            get => UIntPtr_objc_msgSend(NativePtr, sel_height);
            set => objc_msgSend(NativePtr, sel_set_height, value);
        }

        public UIntPtr Depth
        {
            get => UIntPtr_objc_msgSend(NativePtr, sel_depth);
            set => objc_msgSend(NativePtr, sel_set_depth, value);
        }

        public UIntPtr MipmapLevelCount
        {
            get => UIntPtr_objc_msgSend(NativePtr, sel_mipmap_level_count);
            set => objc_msgSend(NativePtr, sel_set_mipmap_level_count, value);
        }

        public UIntPtr SampleCount
        {
            get => UIntPtr_objc_msgSend(NativePtr, sel_sample_count);
            set => objc_msgSend(NativePtr, sel_set_sample_count, value);
        }

        public UIntPtr ArrayLength
        {
            get => UIntPtr_objc_msgSend(NativePtr, sel_array_length);
            set => objc_msgSend(NativePtr, sel_set_array_length, value);
        }

        public MTLResourceOptions ResourceOptions
        {
            get => (MTLResourceOptions)uint_objc_msgSend(NativePtr, sel_resource_options);
            set => objc_msgSend(NativePtr, sel_set_resource_options, (uint)value);
        }

        public MtlcpuCacheMode CpuCacheMode
        {
            get => (MtlcpuCacheMode)uint_objc_msgSend(NativePtr, sel_cpu_cache_mode);
            set => objc_msgSend(NativePtr, sel_set_cpu_cache_mode, (uint)value);
        }

        public MTLStorageMode StorageMode
        {
            get => (MTLStorageMode)uint_objc_msgSend(NativePtr, sel_storage_mode);
            set => objc_msgSend(NativePtr, sel_set_storage_mode, (uint)value);
        }

        public MTLTextureUsage TextureUsage
        {
            get => (MTLTextureUsage)uint_objc_msgSend(NativePtr, sel_texture_usage);
            set => objc_msgSend(NativePtr, sel_set_texture_usage, (uint)value);
        }

        private static readonly Selector sel_texture_type = "textureType";
        private static readonly Selector sel_set_texture_type = "setTextureType:";
        private static readonly Selector sel_width = "width";
        private static readonly Selector sel_set_width = "setWidth:";
        private static readonly Selector sel_height = "height";
        private static readonly Selector sel_set_height = "setHeight:";
        private static readonly Selector sel_depth = "depth";
        private static readonly Selector sel_set_depth = "setDepth:";
        private static readonly Selector sel_mipmap_level_count = "mipmapLevelCount";
        private static readonly Selector sel_set_mipmap_level_count = "setMipmapLevelCount:";
        private static readonly Selector sel_sample_count = "sampleCount";
        private static readonly Selector sel_set_sample_count = "setSampleCount:";
        private static readonly Selector sel_array_length = "arrayLength";
        private static readonly Selector sel_set_array_length = "setArrayLength:";
        private static readonly Selector sel_resource_options = "resourceOptions";
        private static readonly Selector sel_set_resource_options = "setResourceOptions:";
        private static readonly Selector sel_cpu_cache_mode = "cpuCacheMode";
        private static readonly Selector sel_set_cpu_cache_mode = "setCpuCacheMode:";
        private static readonly Selector sel_storage_mode = "storageMode";
        private static readonly Selector sel_set_storage_mode = "setStorageMode:";
        private static readonly Selector sel_texture_usage = "textureUsage";
        private static readonly Selector sel_set_texture_usage = "setTextureUsage:";
    }
}

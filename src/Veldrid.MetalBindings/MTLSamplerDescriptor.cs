// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    public struct MTLSamplerDescriptor
    {
        private static readonly ObjCClass s_class = new ObjCClass(nameof(MTLSamplerDescriptor));
        public readonly IntPtr NativePtr;

        public static MTLSamplerDescriptor New()
        {
            return s_class.AllocInit<MTLSamplerDescriptor>();
        }

        public MTLSamplerAddressMode RAddressMode
        {
            get => (MTLSamplerAddressMode)uint_objc_msgSend(NativePtr, sel_r_address_mode);
            set => objc_msgSend(NativePtr, sel_set_r_address_mode, (uint)value);
        }

        public MTLSamplerAddressMode SAddressMode
        {
            get => (MTLSamplerAddressMode)uint_objc_msgSend(NativePtr, sel_s_address_mode);
            set => objc_msgSend(NativePtr, sel_set_s_address_mode, (uint)value);
        }

        public MTLSamplerAddressMode TAddressMode
        {
            get => (MTLSamplerAddressMode)uint_objc_msgSend(NativePtr, sel_t_address_mode);
            set => objc_msgSend(NativePtr, sel_set_t_address_mode, (uint)value);
        }

        public MTLSamplerMinMagFilter MinFilter
        {
            get => (MTLSamplerMinMagFilter)uint_objc_msgSend(NativePtr, sel_min_filter);
            set => objc_msgSend(NativePtr, sel_set_min_filter, (uint)value);
        }

        public MTLSamplerMinMagFilter MagFilter
        {
            get => (MTLSamplerMinMagFilter)uint_objc_msgSend(NativePtr, sel_mag_filter);
            set => objc_msgSend(NativePtr, sel_set_mag_filter, (uint)value);
        }

        public MTLSamplerMipFilter MipFilter
        {
            get => (MTLSamplerMipFilter)uint_objc_msgSend(NativePtr, sel_mip_filter);
            set => objc_msgSend(NativePtr, sel_set_mip_filter, (uint)value);
        }

        public float LodMinClamp
        {
            get => float_objc_msgSend(NativePtr, sel_lod_min_clamp);
            set => objc_msgSend(NativePtr, sel_set_lod_min_clamp, value);
        }

        public float LodMaxClamp
        {
            get => float_objc_msgSend(NativePtr, sel_lod_max_clamp);
            set => objc_msgSend(NativePtr, sel_set_lod_max_clamp, value);
        }

        public Bool8 LodAverage
        {
            get => bool8_objc_msgSend(NativePtr, sel_lod_average);
            set => objc_msgSend(NativePtr, sel_set_lod_average, value);
        }

        public UIntPtr MaxAnisotropy
        {
            get => UIntPtr_objc_msgSend(NativePtr, sel_max_anisotropy);
            set => objc_msgSend(NativePtr, sel_set_ma_anisotropy, value);
        }

        public MTLCompareFunction CompareFunction
        {
            get => (MTLCompareFunction)uint_objc_msgSend(NativePtr, sel_compare_function);
            set => objc_msgSend(NativePtr, sel_set_compare_function, (uint)value);
        }

        public MTLSamplerBorderColor BorderColor
        {
            get => (MTLSamplerBorderColor)uint_objc_msgSend(NativePtr, sel_border_color);
            set => objc_msgSend(NativePtr, sel_set_border_color, (uint)value);
        }

        private static readonly Selector sel_r_address_mode = "rAddressMode";
        private static readonly Selector sel_set_r_address_mode = "setRAddressMode:";
        private static readonly Selector sel_s_address_mode = "sAddressMode";
        private static readonly Selector sel_set_s_address_mode = "setSAddressMode:";
        private static readonly Selector sel_t_address_mode = "tAddressMode";
        private static readonly Selector sel_set_t_address_mode = "setTAddressMode:";
        private static readonly Selector sel_min_filter = "minFilter";
        private static readonly Selector sel_set_min_filter = "setMinFilter:";
        private static readonly Selector sel_mag_filter = "magFilter";
        private static readonly Selector sel_set_mag_filter = "setMagFilter:";
        private static readonly Selector sel_mip_filter = "mipFilter";
        private static readonly Selector sel_set_mip_filter = "setMipFilter:";
        private static readonly Selector sel_lod_min_clamp = "lodMinClamp";
        private static readonly Selector sel_set_lod_min_clamp = "setLodMinClamp:";
        private static readonly Selector sel_lod_max_clamp = "lodMaxClamp";
        private static readonly Selector sel_set_lod_max_clamp = "setLodMaxClamp:";
        private static readonly Selector sel_lod_average = "lodAverage";
        private static readonly Selector sel_set_lod_average = "setLodAverage:";
        private static readonly Selector sel_max_anisotropy = "maxAnisotropy";
        private static readonly Selector sel_set_ma_anisotropy = "setMaxAnisotropy:";
        private static readonly Selector sel_compare_function = "compareFunction";
        private static readonly Selector sel_set_compare_function = "setCompareFunction:";
        private static readonly Selector sel_border_color = "borderColor";
        private static readonly Selector sel_set_border_color = "setBorderColor:";
    }
}

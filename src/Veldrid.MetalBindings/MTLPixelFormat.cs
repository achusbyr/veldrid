// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid.MetalBindings
{
    public enum MTLPixelFormat : uint
    {
        Invalid = 0,

        /* Normal 8 bit formats */

        A8Unorm = 1,

        R8Unorm = 10,
        R8Unorm_sRGB = 11,

        R8Snorm = 12,
        R8Uint = 13,
        R8Sint = 14,

        /* Normal 16 bit formats */

        R16Unorm = 20,
        R16Snorm = 22,
        R16Uint = 23,
        R16Sint = 24,
        R16Float = 25,

        RG8Unorm = 30,
        RG8Unorm_sRGB = 31,
        RG8Snorm = 32,
        RG8Uint = 33,
        RG8Sint = 34,

        /* Packed 16 bit formats */

        B5G6R5Unorm = 40,
        A1BGR5Unorm = 41,
        ABGR4Unorm = 42,
        BGR5A1Unorm = 43,

        /* Normal 32 bit formats */

        R32Uint = 53,
        R32Sint = 54,
        R32Float = 55,

        RG16Unorm = 60,
        RG16Snorm = 62,
        RG16Uint = 63,
        RG16Sint = 64,
        RG16Float = 65,

        RGBA8Unorm = 70,
        RGBA8Unorm_sRGB = 71,
        RGBA8Snorm = 72,
        RGBA8Uint = 73,
        RGBA8Sint = 74,

        BGRA8Unorm = 80,
        BGRA8Unorm_sRGB = 81,

        /* Packed 32 bit formats */

        RGB10A2Unorm = 90,
        RGB10A2Uint = 91,

        RG11B10Float = 92,
        RGB9E5Float = 93,

        BGR10_XR = 554,
        BGR10_XR_sRGB = 555,

        /* Normal 64 bit formats */

        RG32Uint = 103,
        RG32Sint = 104,
        RG32Float = 105,

        RGBA16Unorm = 110,
        RGBA16Snorm = 112,
        RGBA16Uint = 113,
        RGBA16Sint = 114,
        RGBA16Float = 115,

        BGRA10_XR = 552,
        BGRA10_XR_sRGB = 553,

        /* Normal 128 bit formats */

        RGBA32Uint = 123,
        RGBA32Sint = 124,
        RGBA32Float = 125,

        /* Compressed formats. */

        /* S3TC/DXT */
        BC1_RGBA = 130,
        BC1_RGBA_sRGB = 131,
        BC2_RGBA = 132,
        BC2_RGBA_sRGB = 133,
        BC3_RGBA = 134,
        BC3_RGBA_sRGB = 135,

        /* RGTC */
        BC4_RUnorm = 140,
        BC4_RSnorm = 141,
        BC5_RGUnorm = 142,
        BC5_RGSnorm = 143,

        /* BPTC */
        BC6H_RGBFloat = 150,
        BC6H_RGBUfloat = 151,
        BC7_RGBAUnorm = 152,
        BC7_RGBAUnorm_sRGB = 153,

        /* PVRTC */
        PVRTC_RGB_2BPP = 160,
        PVRTC_RGB_2BPP_sRGB = 161,
        PVRTC_RGB_4BPP = 162,
        PVRTC_RGB_4BPP_sRGB = 163,
        PVRTC_RGBA_2BPP = 164,
        PVRTC_RGBA_2BPP_sRGB = 165,
        PVRTC_RGBA_4BPP = 166,
        PVRTC_RGBA_4BPP_sRGB = 167,

        /* ETC2 */
        EAC_R11Unorm = 170,
        EAC_R11Snorm = 172,
        EAC_RG11Unorm = 174,
        EAC_RG11Snorm = 176,
        EAC_RGBA8 = 178,
        EAC_RGBA8_sRGB = 179,

        ETC2_RGB8 = 180,
        ETC2_RGB8_sRGB = 181,
        ETC2_RGB8A1 = 182,
        ETC2_RGB8A1_sRGB = 183,

        /* ASTC */
        ASTC_4x4_sRGB = 186,
        ASTC_5x4_sRGB = 187,
        ASTC_5x5_sRGB = 188,
        ASTC_6x5_sRGB = 189,
        ASTC_6x6_sRGB = 190,
        ASTC_8x5_sRGB = 192,
        ASTC_8x6_sRGB = 193,
        ASTC_8x8_sRGB = 194,
        ASTC_10x5_sRGB = 195,
        ASTC_10x6_sRGB = 196,
        ASTC_10x8_sRGB = 197,
        ASTC_10x10_sRGB = 198,
        ASTC_12x10_sRGB = 199,
        ASTC_12x12_sRGB = 200,

        ASTC_4x4_LDR = 204,
        ASTC_5x4_LDR = 205,
        ASTC_5x5_LDR = 206,
        ASTC_6x5_LDR = 207,
        ASTC_6x6_LDR = 208,
        ASTC_8x5_LDR = 210,
        ASTC_8x6_LDR = 211,
        ASTC_8x8_LDR = 212,
        ASTC_10x5_LDR = 213,
        ASTC_10x6_LDR = 214,
        ASTC_10x8_LDR = 215,
        ASTC_10x10_LDR = 216,
        ASTC_12x10_LDR = 217,
        ASTC_12x12_LDR = 218,

        /*!
         @constant GBGR422
         @abstract A pixel format where the red and green channels are subsampled horizontally.  Two pixels are stored in 32 bits, with shared red and blue values, and unique green values.
         @discussion This format is equivelent to YUY2, YUYV, yuvs, or GL_RGB_422_APPLE/GL_UNSIGNED_SHORT_8_8_REV_APPLE.   The component order, from lowest addressed byte to highest, is Y0, Cb, Y1, Cr.  There is no implicit colorspace conversion from YUV to RGB, the shader will receive (Cr, Y, Cb, 1).  422 textures must have a width that is a multiple of 2, and can only be used for 2D non-mipmap textures.  When sampling, ClampToEdge is the only usable wrap mode.
         */
        GBGR422 = 240,

        /*!
         @constant BGRG422
         @abstract A pixel format where the red and green channels are subsampled horizontally.  Two pixels are stored in 32 bits, with shared red and blue values, and unique green values.
         @discussion This format is equivelent to UYVY, 2vuy, or GL_RGB_422_APPLE/GL_UNSIGNED_SHORT_8_8_APPLE. The component order, from lowest addressed byte to highest, is Cb, Y0, Cr, Y1.  There is no implicit colorspace conversion from YUV to RGB, the shader will receive (Cr, Y, Cb, 1).  422 textures must have a width that is a multiple of 2, and can only be used for 2D non-mipmap textures.  When sampling, ClampToEdge is the only usable wrap mode.
         */
        BGRG422 = 241,

        /* Depth */

        Depth16Unorm = 250,
        Depth32Float = 252,

        /* Stencil */

        Stencil8 = 253,

        /* Depth Stencil */

        Depth24Unorm_Stencil8 = 255,
        Depth32Float_Stencil8 = 260,

        X32_Stencil8 = 261,
        X24_Stencil8 = 262,
    }
}

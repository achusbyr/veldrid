﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid
{
    /// <summary>
    ///     The format of data stored in a <see cref="Texture" />.
    ///     Each name is a compound identifier, where each component denotes a name and a number of bits used to store that
    ///     component. The final component identifies the storage type of each component. "Float" identifies a signed,
    ///     floating-point
    ///     type, UNorm identifies an unsigned integer type which is normalized, meaning it occupies the full space of the
    ///     integer
    ///     type. The SRgb suffix for normalized integer formats indicates that the RGB components are stored in sRGB format.
    /// </summary>
    public enum PixelFormat : byte
    {
        /// <summary>
        ///     RGBA component order. Each component is an 8-bit unsigned normalized integer.
        /// </summary>
        R8G8B8A8UNorm,

        /// <summary>
        ///     BGRA component order. Each component is an 8-bit unsigned normalized integer.
        /// </summary>
        B8G8R8A8UNorm,

        /// <summary>
        ///     Single-channel, 8-bit unsigned normalized integer.
        /// </summary>
        R8UNorm,

        /// <summary>
        ///     Single-channel, 16-bit unsigned normalized integer. Can be used as a depth format.
        /// </summary>
        R16UNorm,

        /// <summary>
        ///     RGBA component order. Each component is a 32-bit signed floating-point value.
        /// </summary>
        R32G32B32A32Float,

        /// <summary>
        ///     Single-channel, 32-bit signed floating-point value. Can be used as a depth format.
        /// </summary>
        R32Float,

        /// <summary>
        ///     BC3 block compressed format.
        /// </summary>
        Bc3UNorm,

        /// <summary>
        ///     A depth-stencil format where the depth is stored in a 24-bit unsigned normalized integer, and the stencil is stored
        ///     in an 8-bit unsigned integer.
        /// </summary>
        D24UNormS8UInt,

        /// <summary>
        ///     A depth-stencil format where the depth is stored in a 32-bit signed floating-point value, and the stencil is stored
        ///     in an 8-bit unsigned integer.
        /// </summary>
        D32FloatS8UInt,

        /// <summary>
        ///     RGBA component order. Each component is a 32-bit unsigned integer.
        /// </summary>
        R32G32B32A32UInt,

        /// <summary>
        ///     RG component order. Each component is an 8-bit signed normalized integer.
        /// </summary>
        R8G8SNorm,

        /// <summary>
        ///     BC1 block compressed format with no alpha.
        /// </summary>
        Bc1RgbUNorm,

        /// <summary>
        ///     BC1 block compressed format with a single-bit alpha channel.
        /// </summary>
        Bc1RgbaUNorm,

        /// <summary>
        ///     BC2 block compressed format.
        /// </summary>
        Bc2UNorm,

        /// <summary>
        ///     A 32-bit packed format. The 10-bit R component occupies bits 0..9, the 10-bit G component occupies bits 10..19,
        ///     the 10-bit A component occupies 20..29, and the 2-bit A component occupies bits 30..31. Each value is an unsigned,
        ///     normalized integer.
        /// </summary>
        R10G10B10A2UNorm,

        /// <summary>
        ///     A 32-bit packed format. The 10-bit R component occupies bits 0..9, the 10-bit G component occupies bits 10..19,
        ///     the 10-bit A component occupies 20..29, and the 2-bit A component occupies bits 30..31. Each value is an unsigned
        ///     integer.
        /// </summary>
        R10G10B10A2UInt,

        /// <summary>
        ///     A 32-bit packed format. The 11-bit R componnent occupies bits 0..10, the 11-bit G component occupies bits 11..21,
        ///     and the 10-bit B component occupies bits 22..31. Each value is an unsigned floating point value.
        /// </summary>
        R11G11B10Float,

        /// <summary>
        ///     Single-channel, 8-bit signed normalized integer.
        /// </summary>
        R8SNorm,

        /// <summary>
        ///     Single-channel, 8-bit unsigned integer.
        /// </summary>
        R8UInt,

        /// <summary>
        ///     Single-channel, 8-bit signed integer.
        /// </summary>
        R8SInt,

        /// <summary>
        ///     Single-channel, 16-bit signed normalized integer.
        /// </summary>
        R16SNorm,

        /// <summary>
        ///     Single-channel, 16-bit unsigned integer.
        /// </summary>
        R16UInt,

        /// <summary>
        ///     Single-channel, 16-bit signed integer.
        /// </summary>
        R16SInt,

        /// <summary>
        ///     Single-channel, 16-bit signed floating-point value.
        /// </summary>
        R16Float,

        /// <summary>
        ///     Single-channel, 32-bit unsigned integer
        /// </summary>
        R32UInt,

        /// <summary>
        ///     Single-channel, 32-bit signed integer
        /// </summary>
        R32SInt,

        /// <summary>
        ///     RG component order. Each component is an 8-bit unsigned normalized integer.
        /// </summary>
        R8G8UNorm,

        /// <summary>
        ///     RG component order. Each component is an 8-bit unsigned integer.
        /// </summary>
        R8G8UInt,

        /// <summary>
        ///     RG component order. Each component is an 8-bit signed integer.
        /// </summary>
        R8G8SInt,

        /// <summary>
        ///     RG component order. Each component is a 16-bit unsigned normalized integer.
        /// </summary>
        R16G16UNorm,

        /// <summary>
        ///     RG component order. Each component is a 16-bit signed normalized integer.
        /// </summary>
        R16G16SNorm,

        /// <summary>
        ///     RG component order. Each component is a 16-bit unsigned integer.
        /// </summary>
        R16G16UInt,

        /// <summary>
        ///     RG component order. Each component is a 16-bit signed integer.
        /// </summary>
        R16G16SInt,

        /// <summary>
        ///     RG component order. Each component is a 16-bit signed floating-point value.
        /// </summary>
        R16G16Float,

        /// <summary>
        ///     RG component order. Each component is a 32-bit unsigned integer.
        /// </summary>
        R32G32UInt,

        /// <summary>
        ///     RG component order. Each component is a 32-bit signed integer.
        /// </summary>
        R32G32SInt,

        /// <summary>
        ///     RG component order. Each component is a 32-bit signed floating-point value.
        /// </summary>
        R32G32Float,

        /// <summary>
        ///     RGBA component order. Each component is an 8-bit signed normalized integer.
        /// </summary>
        R8G8B8A8SNorm,

        /// <summary>
        ///     RGBA component order. Each component is an 8-bit unsigned integer.
        /// </summary>
        R8G8B8A8UInt,

        /// <summary>
        ///     RGBA component order. Each component is an 8-bit signed integer.
        /// </summary>
        R8G8B8A8SInt,

        /// <summary>
        ///     RGBA component order. Each component is a 16-bit unsigned normalized integer.
        /// </summary>
        R16G16B16A16UNorm,

        /// <summary>
        ///     RGBA component order. Each component is a 16-bit signed normalized integer.
        /// </summary>
        R16G16B16A16SNorm,

        /// <summary>
        ///     RGBA component order. Each component is a 16-bit unsigned integer.
        /// </summary>
        R16G16B16A16UInt,

        /// <summary>
        ///     RGBA component order. Each component is a 16-bit signed integer.
        /// </summary>
        R16G16B16A16SInt,

        /// <summary>
        ///     RGBA component order. Each component is a 16-bit floating-point value.
        /// </summary>
        R16G16B16A16Float,

        /// <summary>
        ///     RGBA component order. Each component is a 32-bit signed integer.
        /// </summary>
        R32G32B32A32SInt,

        /// <summary>
        ///     A 64-bit, 4x4 block-compressed format storing unsigned normalized RGB data.
        /// </summary>
        Etc2R8G8B8UNorm,

        /// <summary>
        ///     A 64-bit, 4x4 block-compressed format storing unsigned normalized RGB data, as well as 1 bit of alpha data.
        /// </summary>
        Etc2R8G8B8A1UNorm,

        /// <summary>
        ///     A 128-bit, 4x4 block-compressed format storing 64 bits of unsigned normalized RGB data, as well as 64 bits of alpha
        ///     data.
        /// </summary>
        Etc2R8G8B8A8UNorm,

        /// <summary>
        ///     BC4 block compressed format, unsigned normalized values.
        /// </summary>
        Bc4UNorm,

        /// <summary>
        ///     BC4 block compressed format, signed normalized values.
        /// </summary>
        Bc4SNorm,

        /// <summary>
        ///     BC5 block compressed format, unsigned normalized values.
        /// </summary>
        Bc5UNorm,

        /// <summary>
        ///     BC5 block compressed format, signed normalized values.
        /// </summary>
        Bc5SNorm,

        /// <summary>
        ///     BC7 block compressed format.
        /// </summary>
        Bc7UNorm,

        /// <summary>
        ///     RGBA component order. Each component is an 8-bit unsigned normalized integer.
        ///     This is an sRGB format.
        /// </summary>
        R8G8B8A8UNormSRgb,

        /// <summary>
        ///     BGRA component order. Each component is an 8-bit unsigned normalized integer.
        ///     This is an sRGB format.
        /// </summary>
        B8G8R8A8UNormSRgb,

        /// <summary>
        ///     BC1 block compressed format with no alpha.
        ///     This is an sRGB format.
        /// </summary>
        Bc1RgbUNormSRgb,

        /// <summary>
        ///     BC1 block compressed format with a single-bit alpha channel.
        ///     This is an sRGB format.
        /// </summary>
        Bc1RgbaUNormSRgb,

        /// <summary>
        ///     BC2 block compressed format.
        ///     This is an sRGB format.
        /// </summary>
        Bc2UNormSRgb,

        /// <summary>
        ///     BC3 block compressed format.
        ///     This is an sRGB format.
        /// </summary>
        Bc3UNormSRgb,

        /// <summary>
        ///     BC7 block compressed format.
        ///     This is an sRGB format.
        /// </summary>
        Bc7UNormSRgb
    }
}

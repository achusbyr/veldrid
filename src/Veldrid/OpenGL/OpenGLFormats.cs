﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Diagnostics;
using Veldrid.OpenGLBindings;

namespace Veldrid.OpenGL
{
    internal static class OpenGLFormats
    {
        internal static DrawElementsType VdToGLDrawElementsType(IndexFormat format)
        {
            switch (format)
            {
                case IndexFormat.UInt16:
                    return DrawElementsType.UnsignedShort;

                case IndexFormat.UInt32:
                    return DrawElementsType.UnsignedInt;

                default:
                    throw Illegal.Value<IndexFormat>();
            }
        }

        internal static ShaderType VdToGLShaderType(ShaderStages stage)
        {
            switch (stage)
            {
                case ShaderStages.Vertex:
                    return ShaderType.VertexShader;

                case ShaderStages.Geometry:
                    return ShaderType.GeometryShader;

                case ShaderStages.TessellationControl:
                    return ShaderType.TessControlShader;

                case ShaderStages.TessellationEvaluation:
                    return ShaderType.TessEvaluationShader;

                case ShaderStages.Fragment:
                    return ShaderType.FragmentShader;

                case ShaderStages.Compute:
                    return ShaderType.ComputeShader;

                default:
                    throw Illegal.Value<ShaderStages>();
            }
        }

        internal static PixelInternalFormat VdToGLPixelInternalFormat(PixelFormat format)
        {
            switch (format)
            {
                case PixelFormat.R8UNorm:
                    return PixelInternalFormat.R8;

                case PixelFormat.R8SNorm:
                    return PixelInternalFormat.R8Snorm;

                case PixelFormat.R8UInt:
                    return PixelInternalFormat.R8ui;

                case PixelFormat.R8SInt:
                    return PixelInternalFormat.R8i;

                case PixelFormat.R16UNorm:
                    return PixelInternalFormat.R16;

                case PixelFormat.R16SNorm:
                    return PixelInternalFormat.R16Snorm;

                case PixelFormat.R16UInt:
                    return PixelInternalFormat.R16ui;

                case PixelFormat.R16SInt:
                    return PixelInternalFormat.R16i;

                case PixelFormat.R16Float:
                    return PixelInternalFormat.R16f;

                case PixelFormat.R32UInt:
                    return PixelInternalFormat.R32ui;

                case PixelFormat.R32SInt:
                    return PixelInternalFormat.R32i;

                case PixelFormat.R32Float:
                    return PixelInternalFormat.R32f;

                case PixelFormat.R8G8UNorm:
                    return PixelInternalFormat.Rg8;

                case PixelFormat.R8G8SNorm:
                    return PixelInternalFormat.Rg8Snorm;

                case PixelFormat.R8G8UInt:
                    return PixelInternalFormat.Rg8ui;

                case PixelFormat.R8G8SInt:
                    return PixelInternalFormat.Rg8i;

                case PixelFormat.R16G16UNorm:
                    return PixelInternalFormat.Rg16;

                case PixelFormat.R16G16SNorm:
                    return PixelInternalFormat.Rg16Snorm;

                case PixelFormat.R16G16UInt:
                    return PixelInternalFormat.Rg16ui;

                case PixelFormat.R16G16SInt:
                    return PixelInternalFormat.Rg16i;

                case PixelFormat.R16G16Float:
                    return PixelInternalFormat.Rg16f;

                case PixelFormat.R32G32UInt:
                    return PixelInternalFormat.Rg32ui;

                case PixelFormat.R32G32SInt:
                    return PixelInternalFormat.Rg32i;

                case PixelFormat.R32G32Float:
                    return PixelInternalFormat.Rg32f;

                case PixelFormat.R8G8B8A8UNorm:
                    return PixelInternalFormat.Rgba8;

                case PixelFormat.R8G8B8A8UNormSRgb:
                    return PixelInternalFormat.Srgb8Alpha8;

                case PixelFormat.R8G8B8A8SNorm:
                    return PixelInternalFormat.Rgba8Snorm;

                case PixelFormat.R8G8B8A8UInt:
                    return PixelInternalFormat.Rgba8ui;

                case PixelFormat.R8G8B8A8SInt:
                    return PixelInternalFormat.Rgba8i;

                case PixelFormat.R16G16B16A16UNorm:
                    return PixelInternalFormat.Rgba16;

                case PixelFormat.R16G16B16A16SNorm:
                    return PixelInternalFormat.Rgba16Snorm;

                case PixelFormat.R16G16B16A16UInt:
                    return PixelInternalFormat.Rgba16ui;

                case PixelFormat.R16G16B16A16SInt:
                    return PixelInternalFormat.Rgba16i;

                case PixelFormat.R16G16B16A16Float:
                    return PixelInternalFormat.Rgba16f;

                case PixelFormat.R32G32B32A32Float:
                    return PixelInternalFormat.Rgba32f;

                case PixelFormat.R32G32B32A32UInt:
                    return PixelInternalFormat.Rgba32ui;

                case PixelFormat.R32G32B32A32SInt:
                    return PixelInternalFormat.Rgba32i;

                case PixelFormat.B8G8R8A8UNorm:
                    return PixelInternalFormat.Rgba;

                case PixelFormat.B8G8R8A8UNormSRgb:
                    return PixelInternalFormat.Srgb8Alpha8;

                case PixelFormat.Bc1RgbUNorm:
                    return PixelInternalFormat.CompressedRgbS3tcDxt1Ext;

                case PixelFormat.Bc1RgbUNormSRgb:
                    return PixelInternalFormat.CompressedSrgbS3tcDxt1Ext;

                case PixelFormat.Bc1RgbaUNorm:
                    return PixelInternalFormat.CompressedRgbaS3tcDxt1Ext;

                case PixelFormat.Bc1RgbaUNormSRgb:
                    return PixelInternalFormat.CompressedSrgbAlphaS3tcDxt1Ext;

                case PixelFormat.Bc2UNorm:
                    return PixelInternalFormat.CompressedRgbaS3tcDxt3Ext;

                case PixelFormat.Bc2UNormSRgb:
                    return PixelInternalFormat.CompressedSrgbAlphaS3tcDxt3Ext;

                case PixelFormat.Bc3UNorm:
                    return PixelInternalFormat.CompressedRgbaS3tcDxt5Ext;

                case PixelFormat.Bc3UNormSRgb:
                    return PixelInternalFormat.CompressedSrgbAlphaS3tcDxt5Ext;

                case PixelFormat.Bc4UNorm:
                    return PixelInternalFormat.CompressedRedRgtc1;

                case PixelFormat.Bc4SNorm:
                    return PixelInternalFormat.CompressedSignedRedRgtc1;

                case PixelFormat.Bc5UNorm:
                    return PixelInternalFormat.CompressedRgRgtc2;

                case PixelFormat.Bc5SNorm:
                    return PixelInternalFormat.CompressedSignedRgRgtc2;

                case PixelFormat.Bc7UNorm:
                    return PixelInternalFormat.CompressedRgbaBptcUnorm;

                case PixelFormat.Bc7UNormSRgb:
                    return PixelInternalFormat.CompressedSrgbAlphaBptcUnorm;

                case PixelFormat.Etc2R8G8B8UNorm:
                    return PixelInternalFormat.CompressedRgb8Etc2;

                case PixelFormat.Etc2R8G8B8A1UNorm:
                    return PixelInternalFormat.CompressedRgb8PunchthroughAlpha1Etc2;

                case PixelFormat.Etc2R8G8B8A8UNorm:
                    return PixelInternalFormat.CompressedRgba8Etc2Eac;

                case PixelFormat.D32FloatS8UInt:
                    return PixelInternalFormat.Depth32fStencil8;

                case PixelFormat.D24UNormS8UInt:
                    return PixelInternalFormat.Depth24Stencil8;

                case PixelFormat.R10G10B10A2UNorm:
                    return PixelInternalFormat.Rgb10A2;

                case PixelFormat.R10G10B10A2UInt:
                    return PixelInternalFormat.Rgb10A2ui;

                case PixelFormat.R11G11B10Float:
                    return PixelInternalFormat.R11fG11fB10f;

                default:
                    throw Illegal.Value<PixelFormat>();
            }
        }

        internal static TextureWrapMode VdToGLTextureWrapMode(SamplerAddressMode mode)
        {
            switch (mode)
            {
                case SamplerAddressMode.Wrap:
                    return TextureWrapMode.Repeat;

                case SamplerAddressMode.Mirror:
                    return TextureWrapMode.MirroredRepeat;

                case SamplerAddressMode.Clamp:
                    return TextureWrapMode.ClampToEdge;

                case SamplerAddressMode.Border:
                    return TextureWrapMode.ClampToBorder;

                default:
                    throw Illegal.Value<SamplerAddressMode>();
            }
        }

        internal static GLPixelFormat VdToGLPixelFormat(PixelFormat format)
        {
            switch (format)
            {
                case PixelFormat.R8UNorm:
                case PixelFormat.R16UNorm:
                case PixelFormat.R16Float:
                case PixelFormat.R32Float:
                case PixelFormat.Bc4UNorm:
                    return GLPixelFormat.Red;

                case PixelFormat.R8SNorm:
                case PixelFormat.R8UInt:
                case PixelFormat.R8SInt:
                case PixelFormat.R16SNorm:
                case PixelFormat.R16UInt:
                case PixelFormat.R16SInt:
                case PixelFormat.R32UInt:
                case PixelFormat.R32SInt:
                case PixelFormat.Bc4SNorm:
                    return GLPixelFormat.RedInteger;

                case PixelFormat.R8G8UNorm:
                case PixelFormat.R16G16UNorm:
                case PixelFormat.R16G16Float:
                case PixelFormat.R32G32Float:
                case PixelFormat.Bc5UNorm:
                    return GLPixelFormat.Rg;

                case PixelFormat.R8G8SNorm:
                case PixelFormat.R8G8UInt:
                case PixelFormat.R8G8SInt:
                case PixelFormat.R16G16SNorm:
                case PixelFormat.R16G16UInt:
                case PixelFormat.R16G16SInt:
                case PixelFormat.R32G32UInt:
                case PixelFormat.R32G32SInt:
                case PixelFormat.Bc5SNorm:
                    return GLPixelFormat.RgInteger;

                case PixelFormat.R8G8B8A8UNorm:
                case PixelFormat.R8G8B8A8UNormSRgb:
                case PixelFormat.R16G16B16A16UNorm:
                case PixelFormat.R16G16B16A16Float:
                case PixelFormat.R32G32B32A32Float:
                    return GLPixelFormat.Rgba;

                case PixelFormat.B8G8R8A8UNorm:
                case PixelFormat.B8G8R8A8UNormSRgb:
                    return GLPixelFormat.Bgra;

                case PixelFormat.R8G8B8A8SNorm:
                case PixelFormat.R8G8B8A8UInt:
                case PixelFormat.R8G8B8A8SInt:
                case PixelFormat.R16G16B16A16SNorm:
                case PixelFormat.R16G16B16A16UInt:
                case PixelFormat.R16G16B16A16SInt:
                case PixelFormat.R32G32B32A32UInt:
                case PixelFormat.R32G32B32A32SInt:
                    return GLPixelFormat.RgbaInteger;

                case PixelFormat.Bc1RgbUNorm:
                case PixelFormat.Bc1RgbUNormSRgb:
                case PixelFormat.Etc2R8G8B8UNorm:
                    return GLPixelFormat.Rgb;

                case PixelFormat.Bc1RgbaUNorm:
                case PixelFormat.Bc1RgbaUNormSRgb:
                case PixelFormat.Bc2UNorm:
                case PixelFormat.Bc2UNormSRgb:
                case PixelFormat.Bc3UNorm:
                case PixelFormat.Bc3UNormSRgb:
                case PixelFormat.Bc7UNorm:
                case PixelFormat.Bc7UNormSRgb:
                case PixelFormat.Etc2R8G8B8A1UNorm:
                case PixelFormat.Etc2R8G8B8A8UNorm:
                    return GLPixelFormat.Rgba;

                case PixelFormat.D24UNormS8UInt:
                    return GLPixelFormat.DepthStencil;

                case PixelFormat.D32FloatS8UInt:
                    return GLPixelFormat.DepthStencil;

                case PixelFormat.R10G10B10A2UNorm:
                    return GLPixelFormat.Rgba;

                case PixelFormat.R10G10B10A2UInt:
                    return GLPixelFormat.RgbaInteger;

                case PixelFormat.R11G11B10Float:
                    return GLPixelFormat.Rgb;

                default:
                    throw Illegal.Value<PixelFormat>();
            }
        }

        internal static GLPixelType VdToGLPixelType(PixelFormat format)
        {
            switch (format)
            {
                case PixelFormat.R8UNorm:
                case PixelFormat.R8UInt:
                case PixelFormat.R8G8UNorm:
                case PixelFormat.R8G8UInt:
                case PixelFormat.R8G8B8A8UNorm:
                case PixelFormat.R8G8B8A8UNormSRgb:
                case PixelFormat.R8G8B8A8UInt:
                case PixelFormat.B8G8R8A8UNorm:
                case PixelFormat.B8G8R8A8UNormSRgb:
                    return GLPixelType.UnsignedByte;

                case PixelFormat.R8SNorm:
                case PixelFormat.R8SInt:
                case PixelFormat.R8G8SNorm:
                case PixelFormat.R8G8SInt:
                case PixelFormat.R8G8B8A8SNorm:
                case PixelFormat.R8G8B8A8SInt:
                case PixelFormat.Bc4SNorm:
                case PixelFormat.Bc5SNorm:
                    return GLPixelType.Byte;

                case PixelFormat.R16UNorm:
                case PixelFormat.R16UInt:
                case PixelFormat.R16G16UNorm:
                case PixelFormat.R16G16UInt:
                case PixelFormat.R16G16B16A16UNorm:
                case PixelFormat.R16G16B16A16UInt:
                    return GLPixelType.UnsignedShort;

                case PixelFormat.R16SNorm:
                case PixelFormat.R16SInt:
                case PixelFormat.R16G16SNorm:
                case PixelFormat.R16G16SInt:
                case PixelFormat.R16G16B16A16SNorm:
                case PixelFormat.R16G16B16A16SInt:
                    return GLPixelType.Short;

                case PixelFormat.R32UInt:
                case PixelFormat.R32G32UInt:
                case PixelFormat.R32G32B32A32UInt:
                    return GLPixelType.UnsignedInt;

                case PixelFormat.R32SInt:
                case PixelFormat.R32G32SInt:
                case PixelFormat.R32G32B32A32SInt:
                    return GLPixelType.Int;

                case PixelFormat.R16Float:
                case PixelFormat.R16G16Float:
                case PixelFormat.R16G16B16A16Float:
                    return GLPixelType.HalfFloat;

                case PixelFormat.R32Float:
                case PixelFormat.R32G32Float:
                case PixelFormat.R32G32B32A32Float:
                    return GLPixelType.Float;

                case PixelFormat.Bc1RgbUNorm:
                case PixelFormat.Bc1RgbUNormSRgb:
                case PixelFormat.Bc1RgbaUNorm:
                case PixelFormat.Bc1RgbaUNormSRgb:
                case PixelFormat.Bc2UNorm:
                case PixelFormat.Bc2UNormSRgb:
                case PixelFormat.Bc3UNorm:
                case PixelFormat.Bc3UNormSRgb:
                case PixelFormat.Bc4UNorm:
                case PixelFormat.Bc5UNorm:
                case PixelFormat.Bc7UNorm:
                case PixelFormat.Bc7UNormSRgb:
                case PixelFormat.Etc2R8G8B8UNorm:
                case PixelFormat.Etc2R8G8B8A1UNorm:
                case PixelFormat.Etc2R8G8B8A8UNorm:
                    return GLPixelType.UnsignedByte; // ?

                case PixelFormat.D32FloatS8UInt:
                    return GLPixelType.Float32UnsignedInt248Rev;

                case PixelFormat.D24UNormS8UInt:
                    return GLPixelType.UnsignedInt248;

                case PixelFormat.R10G10B10A2UNorm:
                case PixelFormat.R10G10B10A2UInt:
                    return GLPixelType.UnsignedInt1010102;

                case PixelFormat.R11G11B10Float:
                    return GLPixelType.UnsignedInt10F11F11FRev;

                default:
                    throw Illegal.Value<PixelFormat>();
            }
        }

        internal static SizedInternalFormat VdToGLSizedInternalFormat(PixelFormat format, bool depthFormat)
        {
            switch (format)
            {
                case PixelFormat.R8UNorm:
                    return SizedInternalFormat.R8;

                case PixelFormat.R8SNorm:
                    return SizedInternalFormat.R8i;

                case PixelFormat.R8UInt:
                    return SizedInternalFormat.R8ui;

                case PixelFormat.R8SInt:
                    return SizedInternalFormat.R8i;

                case PixelFormat.R16UNorm:
                    return depthFormat ? (SizedInternalFormat)PixelInternalFormat.DepthComponent16 : SizedInternalFormat.R16;

                case PixelFormat.R16SNorm:
                    return SizedInternalFormat.R16i;

                case PixelFormat.R16UInt:
                    return SizedInternalFormat.R16ui;

                case PixelFormat.R16SInt:
                    return SizedInternalFormat.R16i;

                case PixelFormat.R16Float:
                    return SizedInternalFormat.R16f;

                case PixelFormat.R32UInt:
                    return SizedInternalFormat.R32ui;

                case PixelFormat.R32SInt:
                    return SizedInternalFormat.R32i;

                case PixelFormat.R32Float:
                    return depthFormat ? (SizedInternalFormat)PixelInternalFormat.DepthComponent32f : SizedInternalFormat.R32f;

                case PixelFormat.R8G8UNorm:
                    return SizedInternalFormat.Rg8;

                case PixelFormat.R8G8SNorm:
                    return SizedInternalFormat.Rg8i;

                case PixelFormat.R8G8UInt:
                    return SizedInternalFormat.Rg8ui;

                case PixelFormat.R8G8SInt:
                    return SizedInternalFormat.Rg8i;

                case PixelFormat.R16G16UNorm:
                    return SizedInternalFormat.Rg16;

                case PixelFormat.R16G16SNorm:
                    return SizedInternalFormat.Rg16i;

                case PixelFormat.R16G16UInt:
                    return SizedInternalFormat.Rg16ui;

                case PixelFormat.R16G16SInt:
                    return SizedInternalFormat.Rg16i;

                case PixelFormat.R16G16Float:
                    return SizedInternalFormat.Rg16f;

                case PixelFormat.R32G32UInt:
                    return SizedInternalFormat.Rg32ui;

                case PixelFormat.R32G32SInt:
                    return SizedInternalFormat.Rg32i;

                case PixelFormat.R32G32Float:
                    return SizedInternalFormat.Rg32f;

                case PixelFormat.R8G8B8A8UNorm:
                    return SizedInternalFormat.Rgba8;

                case PixelFormat.R8G8B8A8UNormSRgb:
                    return (SizedInternalFormat)PixelInternalFormat.Srgb8Alpha8;

                case PixelFormat.R8G8B8A8SNorm:
                    return SizedInternalFormat.Rgba8i;

                case PixelFormat.R8G8B8A8UInt:
                    return SizedInternalFormat.Rgba8ui;

                case PixelFormat.R8G8B8A8SInt:
                    return SizedInternalFormat.Rgba8i;

                case PixelFormat.B8G8R8A8UNorm:
                    return SizedInternalFormat.Rgba8;

                case PixelFormat.B8G8R8A8UNormSRgb:
                    return (SizedInternalFormat)PixelInternalFormat.Srgb8Alpha8;

                case PixelFormat.R16G16B16A16UNorm:
                    return SizedInternalFormat.Rgba16;

                case PixelFormat.R16G16B16A16SNorm:
                    return SizedInternalFormat.Rgba16i;

                case PixelFormat.R16G16B16A16UInt:
                    return SizedInternalFormat.Rgba16ui;

                case PixelFormat.R16G16B16A16SInt:
                    return SizedInternalFormat.Rgba16i;

                case PixelFormat.R16G16B16A16Float:
                    return SizedInternalFormat.Rgba16f;

                case PixelFormat.R32G32B32A32UInt:
                    return SizedInternalFormat.Rgba32ui;

                case PixelFormat.R32G32B32A32SInt:
                    return SizedInternalFormat.Rgba32i;

                case PixelFormat.R32G32B32A32Float:
                    return SizedInternalFormat.Rgba32f;

                case PixelFormat.Bc1RgbUNorm:
                    return (SizedInternalFormat)PixelInternalFormat.CompressedRgbS3tcDxt1Ext;

                case PixelFormat.Bc1RgbUNormSRgb:
                    return (SizedInternalFormat)PixelInternalFormat.CompressedSrgbS3tcDxt1Ext;

                case PixelFormat.Bc1RgbaUNorm:
                    return (SizedInternalFormat)PixelInternalFormat.CompressedRgbaS3tcDxt1Ext;

                case PixelFormat.Bc1RgbaUNormSRgb:
                    return (SizedInternalFormat)PixelInternalFormat.CompressedSrgbAlphaS3tcDxt1Ext;

                case PixelFormat.Bc2UNorm:
                    return (SizedInternalFormat)PixelInternalFormat.CompressedRgbaS3tcDxt3Ext;

                case PixelFormat.Bc2UNormSRgb:
                    return (SizedInternalFormat)PixelInternalFormat.CompressedSrgbAlphaS3tcDxt3Ext;

                case PixelFormat.Bc3UNorm:
                    return (SizedInternalFormat)PixelInternalFormat.CompressedRgbaS3tcDxt5Ext;

                case PixelFormat.Bc3UNormSRgb:
                    return (SizedInternalFormat)PixelInternalFormat.CompressedSrgbAlphaS3tcDxt5Ext;

                case PixelFormat.Bc4UNorm:
                    return (SizedInternalFormat)PixelInternalFormat.CompressedRedRgtc1;

                case PixelFormat.Bc4SNorm:
                    return (SizedInternalFormat)PixelInternalFormat.CompressedSignedRedRgtc1;

                case PixelFormat.Bc5UNorm:
                    return (SizedInternalFormat)PixelInternalFormat.CompressedRgRgtc2;

                case PixelFormat.Bc5SNorm:
                    return (SizedInternalFormat)PixelInternalFormat.CompressedSignedRgRgtc2;

                case PixelFormat.Bc7UNorm:
                    return (SizedInternalFormat)PixelInternalFormat.CompressedRgbaBptcUnorm;

                case PixelFormat.Bc7UNormSRgb:
                    return (SizedInternalFormat)PixelInternalFormat.CompressedSrgbAlphaBptcUnorm;

                case PixelFormat.Etc2R8G8B8UNorm:
                    return (SizedInternalFormat)PixelInternalFormat.CompressedRgb8Etc2;

                case PixelFormat.Etc2R8G8B8A1UNorm:
                    return (SizedInternalFormat)PixelInternalFormat.CompressedRgb8PunchthroughAlpha1Etc2;

                case PixelFormat.Etc2R8G8B8A8UNorm:
                    return (SizedInternalFormat)PixelInternalFormat.CompressedRgba8Etc2Eac;

                case PixelFormat.D32FloatS8UInt:
                    Debug.Assert(depthFormat);
                    return (SizedInternalFormat)PixelInternalFormat.Depth32fStencil8;

                case PixelFormat.D24UNormS8UInt:
                    Debug.Assert(depthFormat);
                    return (SizedInternalFormat)PixelInternalFormat.Depth24Stencil8;

                case PixelFormat.R10G10B10A2UNorm:
                    return (SizedInternalFormat)PixelInternalFormat.Rgb10A2;

                case PixelFormat.R10G10B10A2UInt:
                    return (SizedInternalFormat)PixelInternalFormat.Rgb10A2ui;

                case PixelFormat.R11G11B10Float:
                    return (SizedInternalFormat)PixelInternalFormat.R11fG11fB10f;

                default:
                    throw Illegal.Value<PixelFormat>();
            }
        }

        internal static void VdToGLTextureMinMagFilter(SamplerFilter filter, bool mip, out TextureMinFilter min, out TextureMagFilter mag)
        {
            switch (filter)
            {
                case SamplerFilter.Anisotropic:
                case SamplerFilter.MinPointMagPointMipPoint:
                    min = mip ? TextureMinFilter.NearestMipmapNearest : TextureMinFilter.Nearest;
                    mag = TextureMagFilter.Nearest;
                    break;

                case SamplerFilter.MinPointMagPointMipLinear:
                    min = mip ? TextureMinFilter.NearestMipmapLinear : TextureMinFilter.Nearest;
                    mag = TextureMagFilter.Nearest;
                    break;

                case SamplerFilter.MinPointMagLinearMipPoint:
                    min = mip ? TextureMinFilter.NearestMipmapNearest : TextureMinFilter.Nearest;
                    mag = TextureMagFilter.Linear;
                    break;

                case SamplerFilter.MinPointMagLinearMipLinear:
                    min = mip ? TextureMinFilter.NearestMipmapLinear : TextureMinFilter.Nearest;
                    mag = TextureMagFilter.Linear;
                    break;

                case SamplerFilter.MinLinearMagPointMipPoint:
                    min = mip ? TextureMinFilter.LinearMipmapNearest : TextureMinFilter.Linear;
                    mag = TextureMagFilter.Nearest;
                    break;

                case SamplerFilter.MinLinearMagPointMipLinear:
                    min = mip ? TextureMinFilter.LinearMipmapLinear : TextureMinFilter.Linear;
                    mag = TextureMagFilter.Nearest;
                    break;

                case SamplerFilter.MinLinearMagLinearMipPoint:
                    min = mip ? TextureMinFilter.LinearMipmapNearest : TextureMinFilter.Linear;
                    mag = TextureMagFilter.Linear;
                    break;

                case SamplerFilter.MinLinearMagLinearMipLinear:
                    min = mip ? TextureMinFilter.LinearMipmapLinear : TextureMinFilter.Linear;
                    mag = TextureMagFilter.Linear;
                    break;

                default:
                    throw Illegal.Value<SamplerFilter>();
            }
        }

        internal static BufferAccessMask VdToGLMapMode(MapMode mode)
        {
            switch (mode)
            {
                case MapMode.Read:
                    return BufferAccessMask.Read;

                case MapMode.Write:
                    return BufferAccessMask.Write | BufferAccessMask.InvalidateBuffer;

                case MapMode.ReadWrite:
                    return BufferAccessMask.Read | BufferAccessMask.Write;

                default:
                    throw Illegal.Value<MapMode>();
            }
        }

        internal static VertexAttribPointerType VdToGLVertexAttribPointerType(
            VertexElementFormat format,
            out bool normalized,
            out bool isInteger)
        {
            switch (format)
            {
                case VertexElementFormat.Float1:
                case VertexElementFormat.Float2:
                case VertexElementFormat.Float3:
                case VertexElementFormat.Float4:
                    normalized = false;
                    isInteger = false;
                    return VertexAttribPointerType.Float;

                case VertexElementFormat.Half1:
                case VertexElementFormat.Half2:
                case VertexElementFormat.Half4:
                    normalized = false;
                    isInteger = false;
                    return VertexAttribPointerType.HalfFloat;

                case VertexElementFormat.Byte2Norm:
                case VertexElementFormat.Byte4Norm:
                    normalized = true;
                    isInteger = true;
                    return VertexAttribPointerType.UnsignedByte;

                case VertexElementFormat.Byte2:
                case VertexElementFormat.Byte4:
                    normalized = false;
                    isInteger = true;
                    return VertexAttribPointerType.UnsignedByte;

                case VertexElementFormat.SByte2Norm:
                case VertexElementFormat.SByte4Norm:
                    normalized = true;
                    isInteger = true;
                    return VertexAttribPointerType.Byte;

                case VertexElementFormat.SByte2:
                case VertexElementFormat.SByte4:
                    normalized = false;
                    isInteger = true;
                    return VertexAttribPointerType.Byte;

                case VertexElementFormat.UShort2Norm:
                case VertexElementFormat.UShort4Norm:
                    normalized = true;
                    isInteger = true;
                    return VertexAttribPointerType.UnsignedShort;

                case VertexElementFormat.UShort2:
                case VertexElementFormat.UShort4:
                    normalized = false;
                    isInteger = true;
                    return VertexAttribPointerType.UnsignedShort;

                case VertexElementFormat.Short2Norm:
                case VertexElementFormat.Short4Norm:
                    normalized = true;
                    isInteger = true;
                    return VertexAttribPointerType.Short;

                case VertexElementFormat.Short2:
                case VertexElementFormat.Short4:
                    normalized = false;
                    isInteger = true;
                    return VertexAttribPointerType.Short;

                case VertexElementFormat.UInt1:
                case VertexElementFormat.UInt2:
                case VertexElementFormat.UInt3:
                case VertexElementFormat.UInt4:
                    normalized = false;
                    isInteger = true;
                    return VertexAttribPointerType.UnsignedInt;

                case VertexElementFormat.Int1:
                case VertexElementFormat.Int2:
                case VertexElementFormat.Int3:
                case VertexElementFormat.Int4:
                    normalized = false;
                    isInteger = true;
                    return VertexAttribPointerType.Int;

                default:
                    throw Illegal.Value<VertexElementFormat>();
            }
        }

        internal static bool IsFormatSupported(OpenGLExtensions extensions, PixelFormat format, GraphicsBackend backend)
        {
            switch (format)
            {
                case PixelFormat.Etc2R8G8B8UNorm:
                case PixelFormat.Etc2R8G8B8A1UNorm:
                case PixelFormat.Etc2R8G8B8A8UNorm:
                    return extensions.GlesVersion(3, 0) || extensions.GLVersion(4, 3);

                case PixelFormat.Bc1RgbUNorm:
                case PixelFormat.Bc1RgbUNormSRgb:
                case PixelFormat.Bc1RgbaUNorm:
                case PixelFormat.Bc1RgbaUNormSRgb:
                case PixelFormat.Bc2UNorm:
                case PixelFormat.Bc2UNormSRgb:
                case PixelFormat.Bc3UNorm:
                case PixelFormat.Bc3UNormSRgb:
                    return extensions.IsExtensionSupported("GL_EXT_texture_compression_s3tc");

                case PixelFormat.Bc4UNorm:
                case PixelFormat.Bc4SNorm:
                case PixelFormat.Bc5UNorm:
                case PixelFormat.Bc5SNorm:
                    return extensions.GLVersion(3, 0) || extensions.IsExtensionSupported("GL_ARB_texture_compression_rgtc");

                case PixelFormat.Bc7UNorm:
                case PixelFormat.Bc7UNormSRgb:
                    return extensions.GLVersion(4, 2) || extensions.IsExtensionSupported("GL_ARB_texture_compression_bptc")
                                                      || extensions.IsExtensionSupported("GL_EXT_texture_compression_bptc");

                case PixelFormat.B8G8R8A8UNorm:
                case PixelFormat.B8G8R8A8UNormSRgb:
                case PixelFormat.R10G10B10A2UInt:
                case PixelFormat.R10G10B10A2UNorm:
                    return backend == GraphicsBackend.OpenGL;

                default:
                    return true;
            }
        }

        internal static DepthFunction VdToGLDepthFunction(ComparisonKind value)
        {
            switch (value)
            {
                case ComparisonKind.Never:
                    return DepthFunction.Never;

                case ComparisonKind.Less:
                    return DepthFunction.Less;

                case ComparisonKind.Equal:
                    return DepthFunction.Equal;

                case ComparisonKind.LessEqual:
                    return DepthFunction.Lequal;

                case ComparisonKind.Greater:
                    return DepthFunction.Greater;

                case ComparisonKind.NotEqual:
                    return DepthFunction.Notequal;

                case ComparisonKind.GreaterEqual:
                    return DepthFunction.Gequal;

                case ComparisonKind.Always:
                    return DepthFunction.Always;

                default:
                    throw Illegal.Value<ComparisonKind>();
            }
        }

        internal static BlendingFactorSrc VdToGLBlendFactorSrc(BlendFactor factor)
        {
            switch (factor)
            {
                case BlendFactor.Zero:
                    return BlendingFactorSrc.Zero;

                case BlendFactor.One:
                    return BlendingFactorSrc.One;

                case BlendFactor.SourceAlpha:
                    return BlendingFactorSrc.SrcAlpha;

                case BlendFactor.InverseSourceAlpha:
                    return BlendingFactorSrc.OneMinusSrcAlpha;

                case BlendFactor.DestinationAlpha:
                    return BlendingFactorSrc.DstAlpha;

                case BlendFactor.InverseDestinationAlpha:
                    return BlendingFactorSrc.OneMinusDstAlpha;

                case BlendFactor.SourceColor:
                    return BlendingFactorSrc.SrcColor;

                case BlendFactor.InverseSourceColor:
                    return BlendingFactorSrc.OneMinusSrcColor;

                case BlendFactor.DestinationColor:
                    return BlendingFactorSrc.DstColor;

                case BlendFactor.InverseDestinationColor:
                    return BlendingFactorSrc.OneMinusDstColor;

                case BlendFactor.BlendFactor:
                    return BlendingFactorSrc.ConstantColor;

                case BlendFactor.InverseBlendFactor:
                    return BlendingFactorSrc.OneMinusConstantColor;

                default:
                    throw Illegal.Value<BlendFactor>();
            }
        }

        internal static BlendEquationMode VdToGLBlendEquationMode(BlendFunction function)
        {
            switch (function)
            {
                case BlendFunction.Add:
                    return BlendEquationMode.FuncAdd;

                case BlendFunction.Subtract:
                    return BlendEquationMode.FuncSubtract;

                case BlendFunction.ReverseSubtract:
                    return BlendEquationMode.FuncReverseSubtract;

                case BlendFunction.Minimum:
                    return BlendEquationMode.Min;

                case BlendFunction.Maximum:
                    return BlendEquationMode.Max;

                default:
                    throw Illegal.Value<BlendFunction>();
            }
        }

        internal static PolygonMode VdToGLPolygonMode(PolygonFillMode fillMode)
        {
            switch (fillMode)
            {
                case PolygonFillMode.Solid:
                    return PolygonMode.Fill;

                case PolygonFillMode.Wireframe:
                    return PolygonMode.Line;

                default:
                    throw Illegal.Value<PolygonFillMode>();
            }
        }

        internal static StencilFunction VdToGLStencilFunction(ComparisonKind comparison)
        {
            switch (comparison)
            {
                case ComparisonKind.Never:
                    return StencilFunction.Never;

                case ComparisonKind.Less:
                    return StencilFunction.Less;

                case ComparisonKind.Equal:
                    return StencilFunction.Equal;

                case ComparisonKind.LessEqual:
                    return StencilFunction.Lequal;

                case ComparisonKind.Greater:
                    return StencilFunction.Greater;

                case ComparisonKind.NotEqual:
                    return StencilFunction.Notequal;

                case ComparisonKind.GreaterEqual:
                    return StencilFunction.Gequal;

                case ComparisonKind.Always:
                    return StencilFunction.Always;

                default:
                    throw Illegal.Value<ComparisonKind>();
            }
        }

        internal static StencilOp VdToGLStencilOp(StencilOperation op)
        {
            switch (op)
            {
                case StencilOperation.Keep:
                    return StencilOp.Keep;

                case StencilOperation.Zero:
                    return StencilOp.Zero;

                case StencilOperation.Replace:
                    return StencilOp.Replace;

                case StencilOperation.IncrementAndClamp:
                    return StencilOp.Incr;

                case StencilOperation.DecrementAndClamp:
                    return StencilOp.Decr;

                case StencilOperation.Invert:
                    return StencilOp.Invert;

                case StencilOperation.IncrementAndWrap:
                    return StencilOp.IncrWrap;

                case StencilOperation.DecrementAndWrap:
                    return StencilOp.DecrWrap;

                default:
                    throw Illegal.Value<StencilOperation>();
            }
        }

        internal static CullFaceMode VdToGLCullFaceMode(FaceCullMode cullMode)
        {
            switch (cullMode)
            {
                case FaceCullMode.Back:
                    return CullFaceMode.Back;

                case FaceCullMode.Front:
                    return CullFaceMode.Front;

                default:
                    throw Illegal.Value<FaceCullMode>();
            }
        }

        internal static PrimitiveType VdToGLPrimitiveType(PrimitiveTopology primitiveTopology)
        {
            switch (primitiveTopology)
            {
                case PrimitiveTopology.TriangleList:
                    return PrimitiveType.Triangles;

                case PrimitiveTopology.TriangleStrip:
                    return PrimitiveType.TriangleStrip;

                case PrimitiveTopology.LineList:
                    return PrimitiveType.Lines;

                case PrimitiveTopology.LineStrip:
                    return PrimitiveType.LineStrip;

                case PrimitiveTopology.PointList:
                    return PrimitiveType.Points;

                default:
                    throw Illegal.Value<PrimitiveTopology>();
            }
        }

        internal static FrontFaceDirection VdToGLFrontFaceDirection(FrontFace frontFace)
        {
            switch (frontFace)
            {
                case FrontFace.Clockwise:
                    return FrontFaceDirection.Cw;

                case FrontFace.CounterClockwise:
                    return FrontFaceDirection.Ccw;

                default:
                    throw Illegal.Value<FrontFace>();
            }
        }

        internal static BlendingFactorDest VdToGLBlendFactorDest(BlendFactor factor)
        {
            switch (factor)
            {
                case BlendFactor.Zero:
                    return BlendingFactorDest.Zero;

                case BlendFactor.One:
                    return BlendingFactorDest.One;

                case BlendFactor.SourceAlpha:
                    return BlendingFactorDest.SrcAlpha;

                case BlendFactor.InverseSourceAlpha:
                    return BlendingFactorDest.OneMinusSrcAlpha;

                case BlendFactor.DestinationAlpha:
                    return BlendingFactorDest.DstAlpha;

                case BlendFactor.InverseDestinationAlpha:
                    return BlendingFactorDest.OneMinusDstAlpha;

                case BlendFactor.SourceColor:
                    return BlendingFactorDest.SrcColor;

                case BlendFactor.InverseSourceColor:
                    return BlendingFactorDest.OneMinusSrcColor;

                case BlendFactor.DestinationColor:
                    return BlendingFactorDest.DstColor;

                case BlendFactor.InverseDestinationColor:
                    return BlendingFactorDest.OneMinusDstColor;

                case BlendFactor.BlendFactor:
                    return BlendingFactorDest.ConstantColor;

                case BlendFactor.InverseBlendFactor:
                    return BlendingFactorDest.OneMinusConstantColor;

                default:
                    throw Illegal.Value<BlendFactor>();
            }
        }
    }
}

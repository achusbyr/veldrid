﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Vortice.Direct3D;
using Vortice.Direct3D11;

namespace Veldrid.D3D11
{
    internal static class D3D11Util
    {
        public static int ComputeSubresource(uint mipLevel, uint mipLevelCount, uint arrayLayer)
        {
            return (int)(arrayLayer * mipLevelCount + mipLevel);
        }

        internal static ShaderResourceViewDescription GetSrvDesc(
            D3D11Texture tex,
            uint baseMipLevel,
            uint levelCount,
            uint baseArrayLayer,
            uint layerCount,
            PixelFormat format)
        {
            var srvDesc = new ShaderResourceViewDescription
            {
                Format = D3D11Formats.GetViewFormat(
                    D3D11Formats.ToDxgiFormat(format, (tex.Usage & TextureUsage.DepthStencil) != 0))
            };

            if ((tex.Usage & TextureUsage.Cubemap) == TextureUsage.Cubemap)
            {
                if (tex.ArrayLayers == 1)
                {
                    srvDesc.ViewDimension = ShaderResourceViewDimension.TextureCube;
                    srvDesc.TextureCube.MostDetailedMip = (int)baseMipLevel;
                    srvDesc.TextureCube.MipLevels = (int)levelCount;
                }
                else
                {
                    srvDesc.ViewDimension = ShaderResourceViewDimension.TextureCubeArray;
                    srvDesc.TextureCubeArray.MostDetailedMip = (int)baseMipLevel;
                    srvDesc.TextureCubeArray.MipLevels = (int)levelCount;
                    srvDesc.TextureCubeArray.First2DArrayFace = (int)baseArrayLayer;
                    srvDesc.TextureCubeArray.NumCubes = (int)tex.ArrayLayers;
                }
            }
            else if (tex.Depth == 1)
            {
                if (tex.ArrayLayers == 1)
                {
                    if (tex.Type == TextureType.Texture1D)
                    {
                        srvDesc.ViewDimension = ShaderResourceViewDimension.Texture1D;
                        srvDesc.Texture1D.MostDetailedMip = (int)baseMipLevel;
                        srvDesc.Texture1D.MipLevels = (int)levelCount;
                    }
                    else
                    {
                        srvDesc.ViewDimension = tex.SampleCount == TextureSampleCount.Count1 ? ShaderResourceViewDimension.Texture2D : ShaderResourceViewDimension.Texture2DMultisampled;
                        srvDesc.Texture2D.MostDetailedMip = (int)baseMipLevel;
                        srvDesc.Texture2D.MipLevels = (int)levelCount;
                    }
                }
                else
                {
                    if (tex.Type == TextureType.Texture1D)
                    {
                        srvDesc.ViewDimension = ShaderResourceViewDimension.Texture1DArray;
                        srvDesc.Texture1DArray.MostDetailedMip = (int)baseMipLevel;
                        srvDesc.Texture1DArray.MipLevels = (int)levelCount;
                        srvDesc.Texture1DArray.FirstArraySlice = (int)baseArrayLayer;
                        srvDesc.Texture1DArray.ArraySize = (int)layerCount;
                    }
                    else
                    {
                        srvDesc.ViewDimension = ShaderResourceViewDimension.Texture2DArray;
                        srvDesc.Texture2DArray.MostDetailedMip = (int)baseMipLevel;
                        srvDesc.Texture2DArray.MipLevels = (int)levelCount;
                        srvDesc.Texture2DArray.FirstArraySlice = (int)baseArrayLayer;
                        srvDesc.Texture2DArray.ArraySize = (int)layerCount;
                    }
                }
            }
            else
            {
                srvDesc.ViewDimension = ShaderResourceViewDimension.Texture3D;
                srvDesc.Texture3D.MostDetailedMip = (int)baseMipLevel;
                srvDesc.Texture3D.MipLevels = (int)levelCount;
            }

            return srvDesc;
        }

        internal static int GetSyncInterval(bool syncToVBlank)
        {
            return syncToVBlank ? 1 : 0;
        }
    }
}

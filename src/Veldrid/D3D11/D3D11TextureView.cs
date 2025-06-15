﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using Vortice.Direct3D11;

namespace Veldrid.D3D11
{
    internal class D3D11TextureView : TextureView
    {
        public ID3D11ShaderResourceView ShaderResourceView { get; }
        public ID3D11UnorderedAccessView UnorderedAccessView { get; }

        public override bool IsDisposed => disposed;

        public override string Name
        {
            get => name;
            set
            {
                name = value;
                if (ShaderResourceView != null) ShaderResourceView.DebugName = value + "_SRV";

                if (UnorderedAccessView != null) UnorderedAccessView.DebugName = value + "_UAV";
            }
        }

        private string name;
        private bool disposed;

        public D3D11TextureView(D3D11GraphicsDevice gd, ref TextureViewDescription description)
            : base(ref description)
        {
            var device = gd.Device;
            var d3dTex = Util.AssertSubtype<Texture, D3D11Texture>(description.Target);
            var srvDesc = D3D11Util.GetSrvDesc(
                d3dTex,
                description.BaseMipLevel,
                description.MipLevels,
                description.BaseArrayLayer,
                description.ArrayLayers,
                Format);

            ShaderResourceView = device.CreateShaderResourceView(d3dTex.DeviceTexture, srvDesc);

            // See: https://github.com/ppy/veldrid/issues/53
            GC.SuppressFinalize(ShaderResourceView);

            if ((d3dTex.Usage & TextureUsage.Storage) == TextureUsage.Storage)
            {
                var uavDesc = new UnorderedAccessViewDescription
                {
                    Format = D3D11Formats.GetViewFormat(d3dTex.DxgiFormat)
                };

                if ((d3dTex.Usage & TextureUsage.Cubemap) == TextureUsage.Cubemap)
                    throw new NotSupportedException();

                if (d3dTex.Depth == 1)
                {
                    if (d3dTex.ArrayLayers == 1)
                    {
                        if (d3dTex.Type == TextureType.Texture1D)
                        {
                            uavDesc.ViewDimension = UnorderedAccessViewDimension.Texture1D;
                            uavDesc.Texture1D.MipSlice = description.BaseMipLevel;
                        }
                        else
                        {
                            uavDesc.ViewDimension = UnorderedAccessViewDimension.Texture2D;
                            uavDesc.Texture2D.MipSlice = description.BaseMipLevel;
                        }
                    }
                    else
                    {
                        if (d3dTex.Type == TextureType.Texture1D)
                        {
                            uavDesc.ViewDimension = UnorderedAccessViewDimension.Texture1DArray;
                            uavDesc.Texture1DArray.MipSlice = description.BaseMipLevel;
                            uavDesc.Texture1DArray.FirstArraySlice = description.BaseArrayLayer;
                            uavDesc.Texture1DArray.ArraySize = description.ArrayLayers;
                        }
                        else
                        {
                            uavDesc.ViewDimension = UnorderedAccessViewDimension.Texture2DArray;
                            uavDesc.Texture2DArray.MipSlice = description.BaseMipLevel;
                            uavDesc.Texture2DArray.FirstArraySlice = description.BaseArrayLayer;
                            uavDesc.Texture2DArray.ArraySize = description.ArrayLayers;
                        }
                    }
                }
                else
                {
                    uavDesc.ViewDimension = UnorderedAccessViewDimension.Texture3D;
                    uavDesc.Texture3D.MipSlice = description.BaseMipLevel;

                    // Map the entire range of the 3D texture.
                    uavDesc.Texture3D.FirstWSlice = 0;
                    uavDesc.Texture3D.WSize = d3dTex.Depth;
                }

                UnorderedAccessView = device.CreateUnorderedAccessView(d3dTex.DeviceTexture, uavDesc);

                // See: https://github.com/ppy/veldrid/issues/53
                GC.SuppressFinalize(UnorderedAccessView);
            }
        }

        #region Disposal

        public override void Dispose()
        {
            if (!disposed)
            {
                ShaderResourceView?.Dispose();
                UnorderedAccessView?.Dispose();
                disposed = true;
            }
        }

        #endregion
    }
}

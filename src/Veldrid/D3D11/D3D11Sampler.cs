﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using Vortice.Direct3D11;
using Vortice.Mathematics;

namespace Veldrid.D3D11
{
    internal class D3D11Sampler : Sampler
    {
        public ID3D11SamplerState DeviceSampler { get; }

        public override bool IsDisposed => DeviceSampler.NativePointer == IntPtr.Zero;

        public override string Name
        {
            get => name;
            set
            {
                name = value;
                DeviceSampler.DebugName = value;
            }
        }

        private string name;

        public D3D11Sampler(ID3D11Device device, ref SamplerDescription description)
        {
            var comparision = description.ComparisonKind == null ? ComparisonFunction.Never : D3D11Formats.VdToD3D11ComparisonFunc(description.ComparisonKind.Value);
            var samplerStateDesc = new Vortice.Direct3D11.SamplerDescription
            {
                AddressU = D3D11Formats.VdToD3D11AddressMode(description.AddressModeU),
                AddressV = D3D11Formats.VdToD3D11AddressMode(description.AddressModeV),
                AddressW = D3D11Formats.VdToD3D11AddressMode(description.AddressModeW),
                Filter = D3D11Formats.ToD3D11Filter(description.Filter, description.ComparisonKind.HasValue),
                MinLOD = description.MinimumLod,
                MaxLOD = description.MaximumLod,
                MaxAnisotropy = description.MaximumAnisotropy,
                ComparisonFunc = comparision,
                MipLODBias = description.LodBias,
                BorderColor = toRawColor4(description.BorderColor)
            };

            DeviceSampler = device.CreateSamplerState(samplerStateDesc);
        }

        #region Disposal

        public override void Dispose()
        {
            DeviceSampler.Dispose();
        }

        #endregion

        private static Color4 toRawColor4(SamplerBorderColor borderColor)
        {
            switch (borderColor)
            {
                case SamplerBorderColor.TransparentBlack:
                    return new Color4(0, 0, 0, 0);

                case SamplerBorderColor.OpaqueBlack:
                    return new Color4(0, 0, 0, 1);

                case SamplerBorderColor.OpaqueWhite:
                    return new Color4(1, 1, 1, 1);

                default:
                    throw Illegal.Value<SamplerBorderColor>();
            }
        }
    }
}

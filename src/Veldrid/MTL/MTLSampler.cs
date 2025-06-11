// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using Veldrid.MetalBindings;

namespace Veldrid.MTL
{
    internal class MtlSampler : Sampler
    {
        public MTLSamplerState DeviceSampler { get; }

        public override bool IsDisposed => disposed;

        public override string Name { get; set; }
        private bool disposed;

        public MtlSampler(ref SamplerDescription description, MtlGraphicsDevice gd)
        {
            MtlFormats.GetMinMagMipFilter(
                description.Filter,
                out var min,
                out var mag,
                out var mip);

            var mtlDesc = MTLSamplerDescriptor.New();
            mtlDesc.SAddressMode = MtlFormats.VdToMtlAddressMode(description.AddressModeU);
            mtlDesc.TAddressMode = MtlFormats.VdToMtlAddressMode(description.AddressModeV);
            mtlDesc.RAddressMode = MtlFormats.VdToMtlAddressMode(description.AddressModeW);
            mtlDesc.MinFilter = min;
            mtlDesc.MagFilter = mag;
            mtlDesc.MipFilter = mip;
            if (gd.MetalFeatures.IsMacOS) mtlDesc.BorderColor = MtlFormats.VdToMtlBorderColor(description.BorderColor);

            if (description.ComparisonKind != null) mtlDesc.CompareFunction = MtlFormats.VdToMtlCompareFunction(description.ComparisonKind.Value);
            mtlDesc.LodMinClamp = description.MinimumLod;
            mtlDesc.LodMaxClamp = description.MaximumLod;
            mtlDesc.MaxAnisotropy = Math.Max(1, description.MaximumAnisotropy);
            DeviceSampler = gd.Device.NewSamplerStateWithDescriptor(mtlDesc);
            ObjectiveCRuntime.Release(mtlDesc.NativePtr);
        }

        #region Disposal

        public override void Dispose()
        {
            if (!disposed)
            {
                disposed = true;
                ObjectiveCRuntime.Release(DeviceSampler.NativePtr);
            }
        }

        #endregion
    }
}

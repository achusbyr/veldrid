﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Vulkan;
using static Vulkan.VulkanNative;

namespace Veldrid.Vk
{
    internal unsafe class VkSampler : Sampler
    {
        public Vulkan.VkSampler DeviceSampler => sampler;

        public ResourceRefCount RefCount { get; }

        public override bool IsDisposed => disposed;

        public override string Name
        {
            get => name;
            set
            {
                name = value;
                gd.SetResourceName(this, value);
            }
        }

        private readonly VkGraphicsDevice gd;
        private readonly Vulkan.VkSampler sampler;
        private bool disposed;
        private string name;

        public VkSampler(VkGraphicsDevice gd, ref SamplerDescription description)
        {
            this.gd = gd;
            VkFormats.GetFilterParams(description.Filter, out var minFilter, out var magFilter, out var mipmapMode);

            var samplerCi = new VkSamplerCreateInfo
            {
                sType = VkStructureType.SamplerCreateInfo,
                addressModeU = VkFormats.VdToVkSamplerAddressMode(description.AddressModeU),
                addressModeV = VkFormats.VdToVkSamplerAddressMode(description.AddressModeV),
                addressModeW = VkFormats.VdToVkSamplerAddressMode(description.AddressModeW),
                minFilter = minFilter,
                magFilter = magFilter,
                mipmapMode = mipmapMode,
                compareEnable = description.ComparisonKind != null,
                compareOp = description.ComparisonKind != null
                    ? VkFormats.VdToVkCompareOp(description.ComparisonKind.Value)
                    : VkCompareOp.Never,
                anisotropyEnable = description.Filter == SamplerFilter.Anisotropic,
                maxAnisotropy = description.MaximumAnisotropy,
                minLod = description.MinimumLod,
                maxLod = description.MaximumLod,
                mipLodBias = description.LodBias,
                borderColor = VkFormats.VdToVkSamplerBorderColor(description.BorderColor)
            };

            vkCreateSampler(this.gd.Device, ref samplerCi, null, out sampler);
            RefCount = new ResourceRefCount(disposeCore);
        }

        #region Disposal

        public override void Dispose()
        {
            RefCount.Decrement();
        }

        #endregion

        private void disposeCore()
        {
            if (!disposed)
            {
                vkDestroySampler(gd.Device, sampler, null);
                disposed = true;
            }
        }
    }
}

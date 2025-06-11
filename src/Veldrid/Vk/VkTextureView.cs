﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Vulkan;
using static Vulkan.VulkanNative;

namespace Veldrid.Vk
{
    internal unsafe class VkTextureView : TextureView
    {
        public VkImageView ImageView => imageView;

        public new VkTexture Target => (VkTexture)base.Target;

        public ResourceRefCount RefCount { get; }

        public override bool IsDisposed => destroyed;

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
        private readonly VkImageView imageView;
        private bool destroyed;
        private string name;

        public VkTextureView(VkGraphicsDevice gd, ref TextureViewDescription description)
            : base(ref description)
        {
            this.gd = gd;
            var imageViewCi = VkImageViewCreateInfo.New();
            var tex = Util.AssertSubtype<Texture, VkTexture>(description.Target);
            imageViewCi.image = tex.OptimalDeviceImage;
            imageViewCi.format = VkFormats.VdToVkPixelFormat(Format, (Target.Usage & TextureUsage.DepthStencil) != 0);

            var aspectFlags = (description.Target.Usage & TextureUsage.DepthStencil) == TextureUsage.DepthStencil
                ? VkImageAspectFlags.Depth
                : VkImageAspectFlags.Color;

            imageViewCi.subresourceRange = new VkImageSubresourceRange(
                aspectFlags,
                description.BaseMipLevel,
                description.MipLevels,
                description.BaseArrayLayer,
                description.ArrayLayers);

            if ((tex.Usage & TextureUsage.Cubemap) == TextureUsage.Cubemap)
            {
                imageViewCi.viewType = description.ArrayLayers == 1 ? VkImageViewType.ImageCube : VkImageViewType.ImageCubeArray;
                imageViewCi.subresourceRange.layerCount *= 6;
            }
            else
            {
                switch (tex.Type)
                {
                    case TextureType.Texture1D:
                        imageViewCi.viewType = description.ArrayLayers == 1
                            ? VkImageViewType.Image1D
                            : VkImageViewType.Image1DArray;
                        break;

                    case TextureType.Texture2D:
                        imageViewCi.viewType = description.ArrayLayers == 1
                            ? VkImageViewType.Image2D
                            : VkImageViewType.Image2DArray;
                        break;

                    case TextureType.Texture3D:
                        imageViewCi.viewType = VkImageViewType.Image3D;
                        break;
                }
            }

            vkCreateImageView(this.gd.Device, ref imageViewCi, null, out imageView);
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
            if (!destroyed)
            {
                destroyed = true;
                vkDestroyImageView(gd.Device, ImageView, null);
            }
        }
    }
}

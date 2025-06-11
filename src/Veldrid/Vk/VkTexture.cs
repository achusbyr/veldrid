﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Diagnostics;
using Vulkan;
using static Vulkan.VulkanNative;
using static Veldrid.Vk.VulkanUtil;

namespace Veldrid.Vk
{
    internal unsafe class VkTexture : Texture
    {
        public override uint Width => width;

        public override uint Height => height;

        public override uint Depth => depth;

        public override PixelFormat Format => format;

        public override uint MipLevels { get; }

        public override uint ArrayLayers { get; }
        public uint ActualArrayLayers { get; }

        public override TextureUsage Usage { get; }

        public override TextureType Type { get; }

        public override TextureSampleCount SampleCount { get; }

        public override bool IsDisposed => destroyed;

        public VkImage OptimalDeviceImage => optimalImage;
        public Vulkan.VkBuffer StagingBuffer => stagingBuffer;
        public VkMemoryBlock Memory => memoryBlock;

        public VkFormat VkFormat { get; }
        public VkSampleCountFlags VkSampleCount { get; }

        public ResourceRefCount RefCount { get; }
        public bool IsSwapchainTexture { get; }

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
        private readonly VkImage optimalImage;
        private readonly VkMemoryBlock memoryBlock;
        private readonly Vulkan.VkBuffer stagingBuffer;
        private PixelFormat format; // Static for regular images -- may change for shared staging images
        private bool destroyed;

        // Immutable except for shared staging Textures.
        private uint width;
        private uint height;
        private uint depth;

        private readonly VkImageLayout[] imageLayouts;
        private string name;

        internal VkTexture(VkGraphicsDevice gd, ref TextureDescription description)
        {
            this.gd = gd;
            width = description.Width;
            height = description.Height;
            depth = description.Depth;
            MipLevels = description.MipLevels;
            ArrayLayers = description.ArrayLayers;
            bool isCubemap = (description.Usage & TextureUsage.Cubemap) == TextureUsage.Cubemap;
            ActualArrayLayers = isCubemap
                ? 6 * ArrayLayers
                : ArrayLayers;
            format = description.Format;
            Usage = description.Usage;
            Type = description.Type;
            SampleCount = description.SampleCount;
            VkSampleCount = VkFormats.VdToVkSampleCount(SampleCount);
            VkFormat = VkFormats.VdToVkPixelFormat(Format, (description.Usage & TextureUsage.DepthStencil) == TextureUsage.DepthStencil);

            bool isStaging = (Usage & TextureUsage.Staging) == TextureUsage.Staging;

            if (!isStaging)
            {
                var imageCi = VkImageCreateInfo.New();
                imageCi.mipLevels = MipLevels;
                imageCi.arrayLayers = ActualArrayLayers;
                imageCi.imageType = VkFormats.VdToVkTextureType(Type);
                imageCi.extent.width = Width;
                imageCi.extent.height = Height;
                imageCi.extent.depth = Depth;
                imageCi.initialLayout = VkImageLayout.Preinitialized;
                imageCi.usage = VkFormats.VdToVkTextureUsage(Usage);
                imageCi.tiling = VkImageTiling.Optimal;
                imageCi.format = VkFormat;
                imageCi.flags = VkImageCreateFlags.MutableFormat;

                imageCi.samples = VkSampleCount;
                if (isCubemap) imageCi.flags |= VkImageCreateFlags.CubeCompatible;

                uint subresourceCount = MipLevels * ActualArrayLayers * Depth;
                var result = vkCreateImage(gd.Device, ref imageCi, null, out optimalImage);
                CheckResult(result);

                VkMemoryRequirements memoryRequirements;
                bool prefersDedicatedAllocation;

                if (this.gd.GetImageMemoryRequirements2 != null)
                {
                    var memReqsInfo2 = VkImageMemoryRequirementsInfo2KHR.New();
                    memReqsInfo2.image = optimalImage;
                    var memReqs2 = VkMemoryRequirements2KHR.New();
                    var dedicatedReqs = VkMemoryDedicatedRequirementsKHR.New();
                    memReqs2.pNext = &dedicatedReqs;
                    this.gd.GetImageMemoryRequirements2(this.gd.Device, &memReqsInfo2, &memReqs2);
                    memoryRequirements = memReqs2.memoryRequirements;
                    prefersDedicatedAllocation = dedicatedReqs.prefersDedicatedAllocation || dedicatedReqs.requiresDedicatedAllocation;
                }
                else
                {
                    vkGetImageMemoryRequirements(gd.Device, optimalImage, out memoryRequirements);
                    prefersDedicatedAllocation = false;
                }

                var memoryToken = gd.MemoryManager.Allocate(
                    gd.PhysicalDeviceMemProperties,
                    memoryRequirements.memoryTypeBits,
                    VkMemoryPropertyFlags.DeviceLocal,
                    false,
                    memoryRequirements.size,
                    memoryRequirements.alignment,
                    prefersDedicatedAllocation,
                    optimalImage,
                    Vulkan.VkBuffer.Null);
                memoryBlock = memoryToken;
                result = vkBindImageMemory(gd.Device, optimalImage, memoryBlock.DeviceMemory, memoryBlock.Offset);
                CheckResult(result);

                imageLayouts = new VkImageLayout[subresourceCount];
                for (int i = 0; i < imageLayouts.Length; i++) imageLayouts[i] = VkImageLayout.Preinitialized;
            }
            else // isStaging
            {
                uint depthPitch = FormatHelpers.GetDepthPitch(
                    FormatHelpers.GetRowPitch(Width, Format),
                    Height,
                    Format);
                uint stagingSize = depthPitch * Depth;

                for (uint level = 1; level < MipLevels; level++)
                {
                    Util.GetMipDimensions(this, level, out uint mipWidth, out uint mipHeight, out uint mipDepth);

                    depthPitch = FormatHelpers.GetDepthPitch(
                        FormatHelpers.GetRowPitch(mipWidth, Format),
                        mipHeight,
                        Format);

                    stagingSize += depthPitch * mipDepth;
                }

                stagingSize *= ArrayLayers;

                var bufferCi = VkBufferCreateInfo.New();
                bufferCi.usage = VkBufferUsageFlags.TransferSrc | VkBufferUsageFlags.TransferDst;
                bufferCi.size = stagingSize;
                var result = vkCreateBuffer(this.gd.Device, ref bufferCi, null, out stagingBuffer);
                CheckResult(result);

                VkMemoryRequirements bufferMemReqs;
                bool prefersDedicatedAllocation;

                if (this.gd.GetBufferMemoryRequirements2 != null)
                {
                    var memReqInfo2 = VkBufferMemoryRequirementsInfo2KHR.New();
                    memReqInfo2.buffer = stagingBuffer;
                    var memReqs2 = VkMemoryRequirements2KHR.New();
                    var dedicatedReqs = VkMemoryDedicatedRequirementsKHR.New();
                    memReqs2.pNext = &dedicatedReqs;
                    this.gd.GetBufferMemoryRequirements2(this.gd.Device, &memReqInfo2, &memReqs2);
                    bufferMemReqs = memReqs2.memoryRequirements;
                    prefersDedicatedAllocation = dedicatedReqs.prefersDedicatedAllocation || dedicatedReqs.requiresDedicatedAllocation;
                }
                else
                {
                    vkGetBufferMemoryRequirements(gd.Device, stagingBuffer, out bufferMemReqs);
                    prefersDedicatedAllocation = false;
                }

                // Use "host cached" memory when available, for better performance of GPU -> CPU transfers
                var propertyFlags = VkMemoryPropertyFlags.HostVisible | VkMemoryPropertyFlags.HostCoherent | VkMemoryPropertyFlags.HostCached;
                if (!TryFindMemoryType(this.gd.PhysicalDeviceMemProperties, bufferMemReqs.memoryTypeBits, propertyFlags, out _)) propertyFlags ^= VkMemoryPropertyFlags.HostCached;
                memoryBlock = this.gd.MemoryManager.Allocate(
                    this.gd.PhysicalDeviceMemProperties,
                    bufferMemReqs.memoryTypeBits,
                    propertyFlags,
                    true,
                    bufferMemReqs.size,
                    bufferMemReqs.alignment,
                    prefersDedicatedAllocation,
                    VkImage.Null,
                    stagingBuffer);

                result = vkBindBufferMemory(this.gd.Device, stagingBuffer, memoryBlock.DeviceMemory, memoryBlock.Offset);
                CheckResult(result);
            }

            clearIfRenderTarget();
            transitionIfSampled();
            RefCount = new ResourceRefCount(refCountedDispose);
        }

        // Used to construct Swapchain textures.
        internal VkTexture(
            VkGraphicsDevice gd,
            uint width,
            uint height,
            uint mipLevels,
            uint arrayLayers,
            VkFormat vkFormat,
            TextureUsage usage,
            TextureSampleCount sampleCount,
            VkImage existingImage)
        {
            Debug.Assert(width > 0 && height > 0);
            this.gd = gd;
            MipLevels = mipLevels;
            this.width = width;
            this.height = height;
            depth = 1;
            VkFormat = vkFormat;
            format = VkFormats.VkToVdPixelFormat(VkFormat);
            ArrayLayers = arrayLayers;
            Usage = usage;
            Type = TextureType.Texture2D;
            SampleCount = sampleCount;
            VkSampleCount = VkFormats.VdToVkSampleCount(sampleCount);
            optimalImage = existingImage;
            imageLayouts = new[] { VkImageLayout.Undefined };
            IsSwapchainTexture = true;

            clearIfRenderTarget();
            RefCount = new ResourceRefCount(DisposeCore);
        }

        internal VkSubresourceLayout GetSubresourceLayout(uint subresource)
        {
            bool staging = stagingBuffer.Handle != 0;
            Util.GetMipLevelAndArrayLayer(this, subresource, out uint mipLevel, out uint arrayLayer);

            if (!staging)
            {
                var aspect = (Usage & TextureUsage.DepthStencil) == TextureUsage.DepthStencil
                    ? VkImageAspectFlags.Depth | VkImageAspectFlags.Stencil
                    : VkImageAspectFlags.Color;
                var imageSubresource = new VkImageSubresource
                {
                    arrayLayer = arrayLayer,
                    mipLevel = mipLevel,
                    aspectMask = aspect
                };

                vkGetImageSubresourceLayout(gd.Device, optimalImage, ref imageSubresource, out var layout);
                return layout;
            }
            else
            {
                Util.GetMipDimensions(this, mipLevel, out uint mipWidth, out uint mipHeight, out uint _);
                uint rowPitch = FormatHelpers.GetRowPitch(mipWidth, Format);
                uint depthPitch = FormatHelpers.GetDepthPitch(rowPitch, mipHeight, Format);

                var layout = new VkSubresourceLayout
                {
                    rowPitch = rowPitch,
                    depthPitch = depthPitch,
                    arrayPitch = depthPitch,
                    size = depthPitch,
                    offset = Util.ComputeSubresourceOffset(this, mipLevel, arrayLayer)
                };

                return layout;
            }
        }

        internal void TransitionImageLayout(
            VkCommandBuffer cb,
            uint baseMipLevel,
            uint levelCount,
            uint baseArrayLayer,
            uint layerCount,
            VkImageLayout newLayout)
        {
            if (stagingBuffer != Vulkan.VkBuffer.Null) return;

            var oldLayout = imageLayouts[CalculateSubresource(baseMipLevel, baseArrayLayer)];
#if DEBUG
            for (uint level = 0; level < levelCount; level++)
            {
                for (uint layer = 0; layer < layerCount; layer++)
                {
                    if (imageLayouts[CalculateSubresource(baseMipLevel + level, baseArrayLayer + layer)] != oldLayout)
                        throw new VeldridException("Unexpected image layout.");
                }
            }
#endif
            if (oldLayout != newLayout)
            {
                VkImageAspectFlags aspectMask;

                if ((Usage & TextureUsage.DepthStencil) != 0)
                {
                    aspectMask = FormatHelpers.IsStencilFormat(Format)
                        ? VkImageAspectFlags.Depth | VkImageAspectFlags.Stencil
                        : VkImageAspectFlags.Depth;
                }
                else
                    aspectMask = VkImageAspectFlags.Color;

                VulkanUtil.TransitionImageLayout(
                    cb,
                    OptimalDeviceImage,
                    baseMipLevel,
                    levelCount,
                    baseArrayLayer,
                    layerCount,
                    aspectMask,
                    imageLayouts[CalculateSubresource(baseMipLevel, baseArrayLayer)],
                    newLayout);

                for (uint level = 0; level < levelCount; level++)
                {
                    for (uint layer = 0; layer < layerCount; layer++) imageLayouts[CalculateSubresource(baseMipLevel + level, baseArrayLayer + layer)] = newLayout;
                }
            }
        }

        internal void TransitionImageLayoutNonmatching(
            VkCommandBuffer cb,
            uint baseMipLevel,
            uint levelCount,
            uint baseArrayLayer,
            uint layerCount,
            VkImageLayout newLayout)
        {
            if (stagingBuffer != Vulkan.VkBuffer.Null) return;

            for (uint level = baseMipLevel; level < baseMipLevel + levelCount; level++)
            {
                for (uint layer = baseArrayLayer; layer < baseArrayLayer + layerCount; layer++)
                {
                    uint subresource = CalculateSubresource(level, layer);
                    var oldLayout = imageLayouts[subresource];

                    if (oldLayout != newLayout)
                    {
                        VkImageAspectFlags aspectMask;

                        if ((Usage & TextureUsage.DepthStencil) != 0)
                        {
                            aspectMask = FormatHelpers.IsStencilFormat(Format)
                                ? VkImageAspectFlags.Depth | VkImageAspectFlags.Stencil
                                : VkImageAspectFlags.Depth;
                        }
                        else
                            aspectMask = VkImageAspectFlags.Color;

                        VulkanUtil.TransitionImageLayout(
                            cb,
                            OptimalDeviceImage,
                            level,
                            1,
                            layer,
                            1,
                            aspectMask,
                            oldLayout,
                            newLayout);

                        imageLayouts[subresource] = newLayout;
                    }
                }
            }
        }

        internal VkImageLayout GetImageLayout(uint mipLevel, uint arrayLayer)
        {
            return imageLayouts[CalculateSubresource(mipLevel, arrayLayer)];
        }

        internal void SetStagingDimensions(uint width, uint height, uint depth, PixelFormat format)
        {
            Debug.Assert(stagingBuffer != Vulkan.VkBuffer.Null);
            Debug.Assert(Usage == TextureUsage.Staging);
            this.width = width;
            this.height = height;
            this.depth = depth;
            this.format = format;
        }

        internal void SetImageLayout(uint mipLevel, uint arrayLayer, VkImageLayout layout)
        {
            imageLayouts[CalculateSubresource(mipLevel, arrayLayer)] = layout;
        }

        private void clearIfRenderTarget()
        {
            // If the image is going to be used as a render target, we need to clear the data before its first use.
            if ((Usage & TextureUsage.RenderTarget) != 0)
                gd.ClearColorTexture(this, new VkClearColorValue(0, 0, 0, 0));
            else if ((Usage & TextureUsage.DepthStencil) != 0) gd.ClearDepthTexture(this, new VkClearDepthStencilValue(0, 0));
        }

        private void transitionIfSampled()
        {
            if ((Usage & TextureUsage.Sampled) != 0) gd.TransitionImageLayout(this, VkImageLayout.ShaderReadOnlyOptimal);
        }

        private void refCountedDispose()
        {
            if (!destroyed)
            {
                base.Dispose();

                destroyed = true;

                bool isStaging = (Usage & TextureUsage.Staging) == TextureUsage.Staging;
                if (isStaging)
                    vkDestroyBuffer(gd.Device, stagingBuffer, null);
                else
                    vkDestroyImage(gd.Device, optimalImage, null);

                if (memoryBlock.DeviceMemory.Handle != 0) gd.MemoryManager.Free(memoryBlock);
            }
        }

        private protected override void DisposeCore()
        {
            RefCount.Decrement();
        }
    }
}

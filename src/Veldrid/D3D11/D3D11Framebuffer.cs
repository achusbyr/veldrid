﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Vortice.Direct3D11;

namespace Veldrid.D3D11
{
    internal class D3D11Framebuffer : Framebuffer
    {
        public ID3D11RenderTargetView[] RenderTargetViews { get; }
        public ID3D11DepthStencilView DepthStencilView { get; }

        public override bool IsDisposed => disposed;

        public override string Name
        {
            get => name;
            set
            {
                name = value;
                for (int i = 0; i < RenderTargetViews.Length; i++) RenderTargetViews[i].DebugName = value + "_RTV" + i;

                if (DepthStencilView != null) DepthStencilView.DebugName = value + "_DSV";
            }
        }

        // Only non-null if this is the Framebuffer for a Swapchain.
        internal D3D11Swapchain Swapchain { get; set; }
        private string name;
        private bool disposed;

        public D3D11Framebuffer(ID3D11Device device, ref FramebufferDescription description)
            : base(description.DepthTarget, description.ColorTargets)
        {
            if (description.DepthTarget != null)
            {
                var d3dDepthTarget = Util.AssertSubtype<Texture, D3D11Texture>(description.DepthTarget.Value.Target);
                var dsvDesc = new DepthStencilViewDescription
                {
                    Format = D3D11Formats.GetDepthFormat(d3dDepthTarget.Format)
                };

                if (d3dDepthTarget.ArrayLayers == 1)
                {
                    if (d3dDepthTarget.SampleCount == TextureSampleCount.Count1)
                    {
                        dsvDesc.ViewDimension = DepthStencilViewDimension.Texture2D;
                        dsvDesc.Texture2D.MipSlice = description.DepthTarget.Value.MipLevel;
                    }
                    else
                        dsvDesc.ViewDimension = DepthStencilViewDimension.Texture2DMultisampled;
                }
                else
                {
                    if (d3dDepthTarget.SampleCount == TextureSampleCount.Count1)
                    {
                        dsvDesc.ViewDimension = DepthStencilViewDimension.Texture2DArray;
                        dsvDesc.Texture2DArray.FirstArraySlice = description.DepthTarget.Value.ArrayLayer;
                        dsvDesc.Texture2DArray.ArraySize = 1;
                        dsvDesc.Texture2DArray.MipSlice = description.DepthTarget.Value.MipLevel;
                    }
                    else
                    {
                        dsvDesc.ViewDimension = DepthStencilViewDimension.Texture2DMultisampledArray;
                        dsvDesc.Texture2DMSArray.FirstArraySlice = description.DepthTarget.Value.ArrayLayer;
                        dsvDesc.Texture2DMSArray.ArraySize = 1;
                    }
                }

                DepthStencilView = device.CreateDepthStencilView(d3dDepthTarget.DeviceTexture, dsvDesc);
            }

            if (description.ColorTargets != null && description.ColorTargets.Length > 0)
            {
                RenderTargetViews = new ID3D11RenderTargetView[description.ColorTargets.Length];

                for (int i = 0; i < RenderTargetViews.Length; i++)
                {
                    var d3dColorTarget = Util.AssertSubtype<Texture, D3D11Texture>(description.ColorTargets[i].Target);
                    var rtvDesc = new RenderTargetViewDescription
                    {
                        Format = D3D11Formats.ToDxgiFormat(d3dColorTarget.Format, false)
                    };

                    if (d3dColorTarget.ArrayLayers > 1 || (d3dColorTarget.Usage & TextureUsage.Cubemap) != 0)
                    {
                        if (d3dColorTarget.SampleCount == TextureSampleCount.Count1)
                        {
                            rtvDesc.ViewDimension = RenderTargetViewDimension.Texture2DArray;
                            rtvDesc.Texture2DArray = new Texture2DArrayRenderTargetView
                            {
                                ArraySize = 1,
                                FirstArraySlice = description.ColorTargets[i].ArrayLayer,
                                MipSlice = description.ColorTargets[i].MipLevel
                            };
                        }
                        else
                        {
                            rtvDesc.ViewDimension = RenderTargetViewDimension.Texture2DMultisampledArray;
                            rtvDesc.Texture2DMSArray = new Texture2DMultisampledArrayRenderTargetView
                            {
                                ArraySize = 1,
                                FirstArraySlice = description.ColorTargets[i].ArrayLayer
                            };
                        }
                    }
                    else
                    {
                        if (d3dColorTarget.SampleCount == TextureSampleCount.Count1)
                        {
                            rtvDesc.ViewDimension = RenderTargetViewDimension.Texture2D;
                            rtvDesc.Texture2D.MipSlice = description.ColorTargets[i].MipLevel;
                        }
                        else
                            rtvDesc.ViewDimension = RenderTargetViewDimension.Texture2DMultisampled;
                    }

                    RenderTargetViews[i] = device.CreateRenderTargetView(d3dColorTarget.DeviceTexture, rtvDesc);
                }
            }
            else
                RenderTargetViews = [];
        }

        #region Disposal

        public override void Dispose()
        {
            if (!disposed)
            {
                DepthStencilView?.Dispose();
                foreach (var rtv in RenderTargetViews) rtv.Dispose();

                disposed = true;
            }
        }

        #endregion
    }
}

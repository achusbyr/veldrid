// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using Veldrid.MetalBindings;

namespace Veldrid.MTL
{
    internal class MtlSwapchain : Swapchain
    {
        public override Framebuffer Framebuffer => framebuffer;

        public override bool IsDisposed => disposed;

        public CAMetalDrawable CurrentDrawable => drawable;

        public override bool SyncToVerticalBlank
        {
            get => syncToVerticalBlank;
            set
            {
                if (syncToVerticalBlank != value) setSyncToVerticalBlank(value);
            }
        }

        public override string Name { get; set; }
        private readonly MtlSwapchainFramebuffer framebuffer;
        private readonly MtlGraphicsDevice gd;
        private CAMetalLayer metalLayer;
        private UIView uiView; // Valid only when a UIViewSwapchainSource is used.
        private bool syncToVerticalBlank;
        private bool disposed;

        private CAMetalDrawable drawable;

        public MtlSwapchain(MtlGraphicsDevice gd, ref SwapchainDescription description)
        {
            this.gd = gd;
            syncToVerticalBlank = description.SyncToVerticalBlank;

            uint width;
            uint height;

            var source = description.Source;

            if (source is NSWindowSwapchainSource nsWindowSource)
            {
                var nswindow = new NSWindow(nsWindowSource.NSWindow);
                var contentView = nswindow.ContentView;
                var windowContentSize = contentView.Frame.Size;
                width = (uint)windowContentSize.Width;
                height = (uint)windowContentSize.Height;

                if (!CAMetalLayer.TryCast(contentView.Layer, out metalLayer))
                {
                    metalLayer = CAMetalLayer.New();
                    contentView.WantsLayer = true;
                    contentView.Layer = metalLayer.NativePtr;
                }
            }
            else if (source is NSViewSwapchainSource nsViewSource)
            {
                var contentView = new NSView(nsViewSource.NSView);
                var windowContentSize = contentView.Frame.Size;
                width = (uint)windowContentSize.Width;
                height = (uint)windowContentSize.Height;

                if (!CAMetalLayer.TryCast(contentView.Layer, out metalLayer))
                {
                    metalLayer = CAMetalLayer.New();
                    contentView.WantsLayer = true;
                    contentView.Layer = metalLayer.NativePtr;
                }
            }
            else if (source is UIViewSwapchainSource uiViewSource)
            {
                uiView = new UIView(uiViewSource.UIView);
                var viewSize = uiView.Frame.Size;
                width = (uint)viewSize.Width;
                height = (uint)viewSize.Height;

                if (!CAMetalLayer.TryCast(uiView.Layer, out metalLayer))
                {
                    metalLayer = CAMetalLayer.New();
                    metalLayer.Frame = uiView.Frame;
                    metalLayer.Opaque = true;
                    uiView.Layer.AddSublayer(metalLayer.NativePtr);
                }
            }
            else
                throw new VeldridException("A Metal Swapchain can only be created from an NSWindow, NSView, or UIView.");

            var format = description.ColorSrgb
                ? PixelFormat.B8G8R8A8UNormSRgb
                : PixelFormat.B8G8R8A8UNorm;

            metalLayer.Device = this.gd.Device;
            metalLayer.PixelFormat = MtlFormats.VdToMtlPixelFormat(format, false);
            metalLayer.FramebufferOnly = true;
            metalLayer.DrawableSize = new CGSize(width, height);

            setSyncToVerticalBlank(syncToVerticalBlank);

            framebuffer = new MtlSwapchainFramebuffer(
                gd,
                this,
                description.DepthFormat,
                format);

            getNextDrawable();
        }

        #region Disposal

        public override void Dispose()
        {
            if (drawable.NativePtr != IntPtr.Zero) ObjectiveCRuntime.Release(drawable.NativePtr);
            framebuffer.Dispose();
            ObjectiveCRuntime.Release(metalLayer.NativePtr);

            disposed = true;
        }

        #endregion

        public override void Resize(uint width, uint height)
        {
            if (uiView.NativePtr != IntPtr.Zero)
                metalLayer.Frame = uiView.Frame;

            metalLayer.DrawableSize = new CGSize(width, height);

            getNextDrawable();
        }

        public bool EnsureDrawableAvailable()
        {
            return !drawable.IsNull || getNextDrawable();
        }

        public void InvalidateDrawable()
        {
            ObjectiveCRuntime.Release(drawable.NativePtr);
            drawable = default;
        }

        private bool getNextDrawable()
        {
            if (!drawable.IsNull) ObjectiveCRuntime.Release(drawable.NativePtr);

            using (NSAutoreleasePool.Begin())
            {
                drawable = metalLayer.NextDrawable();

                if (!drawable.IsNull)
                {
                    ObjectiveCRuntime.Retain(drawable.NativePtr);
                    framebuffer.UpdateTextures(drawable, metalLayer.DrawableSize);
                    return true;
                }

                return false;
            }
        }

        private void setSyncToVerticalBlank(bool value)
        {
            syncToVerticalBlank = value;

            if (gd.MetalFeatures.MaxFeatureSet == MTLFeatureSet.macOS_GPUFamily1_v3
                || gd.MetalFeatures.MaxFeatureSet == MTLFeatureSet.macOS_GPUFamily1_v4
                || gd.MetalFeatures.MaxFeatureSet == MTLFeatureSet.macOS_GPUFamily2_v1)
                metalLayer.DisplaySyncEnabled = value;
        }
    }
}

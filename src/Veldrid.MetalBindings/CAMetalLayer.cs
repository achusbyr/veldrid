// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    public struct CAMetalLayer
    {
        public readonly IntPtr NativePtr;

        public CAMetalLayer(IntPtr ptr)
        {
            NativePtr = ptr;
        }

        public static CAMetalLayer New()
        {
            return s_class.AllocInit<CAMetalLayer>();
        }

        public static bool TryCast(IntPtr layerPointer, out CAMetalLayer metalLayer)
        {
            var layerObject = new NSObject(layerPointer);

            if (layerObject.IsKindOfClass(s_class))
            {
                metalLayer = new CAMetalLayer(layerPointer);
                return true;
            }

            metalLayer = default;
            return false;
        }

        public MTLDevice Device
        {
            get => objc_msgSend<MTLDevice>(NativePtr, sel_device);
            set => objc_msgSend(NativePtr, sel_set_device, value);
        }

        public MTLPixelFormat PixelFormat
        {
            get => (MTLPixelFormat)uint_objc_msgSend(NativePtr, sel_pixel_format);
            set => objc_msgSend(NativePtr, sel_set_pixel_format, (uint)value);
        }

        public Bool8 FramebufferOnly
        {
            get => bool8_objc_msgSend(NativePtr, sel_framebuffer_only);
            set => objc_msgSend(NativePtr, sel_set_framebuffer_only, value);
        }

        public CGSize DrawableSize
        {
            get => CGSize_objc_msgSend(NativePtr, sel_drawable_size);
            set => objc_msgSend(NativePtr, sel_set_drawable_size, value);
        }

        public CGRect Frame
        {
            get => CGRect_objc_msgSend(NativePtr, sel_frame);
            set => objc_msgSend(NativePtr, sel_set_frame, value);
        }

        public Bool8 Opaque
        {
            get => bool8_objc_msgSend(NativePtr, sel_is_opaque);
            set => objc_msgSend(NativePtr, sel_set_opaque, value);
        }

        public CAMetalDrawable NextDrawable()
        {
            return objc_msgSend<CAMetalDrawable>(NativePtr, sel_next_drawable);
        }

        public Bool8 DisplaySyncEnabled
        {
            get => bool8_objc_msgSend(NativePtr, sel_display_sync_enabled);
            set => objc_msgSend(NativePtr, sel_set_display_sync_enabled, value);
        }

        private static readonly ObjCClass s_class = new ObjCClass(nameof(CAMetalLayer));
        private static readonly Selector sel_device = "device";
        private static readonly Selector sel_set_device = "setDevice:";
        private static readonly Selector sel_pixel_format = "pixelFormat";
        private static readonly Selector sel_set_pixel_format = "setPixelFormat:";
        private static readonly Selector sel_framebuffer_only = "framebufferOnly";
        private static readonly Selector sel_set_framebuffer_only = "setFramebufferOnly:";
        private static readonly Selector sel_drawable_size = "drawableSize";
        private static readonly Selector sel_set_drawable_size = "setDrawableSize:";
        private static readonly Selector sel_frame = "frame";
        private static readonly Selector sel_set_frame = "setFrame:";
        private static readonly Selector sel_is_opaque = "isOpaque";
        private static readonly Selector sel_set_opaque = "setOpaque:";
        private static readonly Selector sel_display_sync_enabled = "displaySyncEnabled";
        private static readonly Selector sel_set_display_sync_enabled = "setDisplaySyncEnabled:";
        private static readonly Selector sel_next_drawable = "nextDrawable";
    }
}

﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using Veldrid.Android;
using Veldrid.MetalBindings;
using Vulkan;
using Vulkan.Android;
using Vulkan.Wayland;
using Vulkan.Xlib;
using static Vulkan.VulkanNative;
using static Veldrid.Vk.VulkanUtil;

namespace Veldrid.Vk
{
    internal static unsafe class VkSurfaceUtil
    {
        internal static VkSurfaceKHR CreateSurface(VkGraphicsDevice gd, VkInstance instance, SwapchainSource swapchainSource)
        {
            // TODO a null GD is passed from VkSurfaceSource.CreateSurface for compatibility
            //      when VkSurfaceInfo is removed we do not have to handle gd == null anymore
            bool doCheck = gd != null;

            if (doCheck && !gd.HasSurfaceExtension(CommonStrings.VkKhrSurfaceExtensionName))
                throw new VeldridException($"The required instance extension was not available: {CommonStrings.VkKhrSurfaceExtensionName}");

            switch (swapchainSource)
            {
                case XlibSwapchainSource xlibSource:
                    if (doCheck && !gd.HasSurfaceExtension(CommonStrings.VkKhrXlibSurfaceExtensionName))
                        throw new VeldridException($"The required instance extension was not available: {CommonStrings.VkKhrXlibSurfaceExtensionName}");

                    return createXlib(instance, xlibSource);

                case WaylandSwapchainSource waylandSource:
                    if (doCheck && !gd.HasSurfaceExtension(CommonStrings.VkKhrWaylandSurfaceExtensionName))
                        throw new VeldridException($"The required instance extension was not available: {CommonStrings.VkKhrWaylandSurfaceExtensionName}");

                    return createWayland(instance, waylandSource);

                case Win32SwapchainSource win32Source:
                    if (doCheck && !gd.HasSurfaceExtension(CommonStrings.VkKhrWin32SurfaceExtensionName))
                        throw new VeldridException($"The required instance extension was not available: {CommonStrings.VkKhrWin32SurfaceExtensionName}");

                    return createWin32(instance, win32Source);

                case AndroidSurfaceSwapchainSource androidSource:
                    if (doCheck && !gd.HasSurfaceExtension(CommonStrings.VkKhrAndroidSurfaceExtensionName))
                        throw new VeldridException($"The required instance extension was not available: {CommonStrings.VkKhrAndroidSurfaceExtensionName}");

                    return createAndroidSurface(instance, androidSource);

                case NSWindowSwapchainSource nsWindowSource:
                    if (doCheck)
                    {
                        bool hasMetalExtension = gd.HasSurfaceExtension(CommonStrings.VkExtMetalSurfaceExtensionName);
                        if (hasMetalExtension || gd.HasSurfaceExtension(CommonStrings.VkMvkMacosSurfaceExtensionName))
                            return createNSWindowSurface(gd, instance, nsWindowSource, hasMetalExtension);

                        throw new VeldridException("Neither macOS surface extension was available: " +
                                                   $"{CommonStrings.VkMvkMacosSurfaceExtensionName}, {CommonStrings.VkExtMetalSurfaceExtensionName}");
                    }

                    return createNSWindowSurface(null, instance, nsWindowSource, false);

                case NSViewSwapchainSource nsViewSource:
                    if (doCheck)
                    {
                        bool hasMetalExtension = gd.HasSurfaceExtension(CommonStrings.VkExtMetalSurfaceExtensionName);
                        if (hasMetalExtension || gd.HasSurfaceExtension(CommonStrings.VkMvkMacosSurfaceExtensionName))
                            return createNSViewSurface(gd, instance, nsViewSource, hasMetalExtension);

                        throw new VeldridException("Neither macOS surface extension was available: " +
                                                   $"{CommonStrings.VkMvkMacosSurfaceExtensionName}, {CommonStrings.VkExtMetalSurfaceExtensionName}");
                    }

                    return createNSViewSurface(null, instance, nsViewSource, false);

                case UIViewSwapchainSource uiViewSource:
                    if (doCheck)
                    {
                        bool hasMetalExtension = gd.HasSurfaceExtension(CommonStrings.VkExtMetalSurfaceExtensionName);
                        if (hasMetalExtension || gd.HasSurfaceExtension(CommonStrings.VkMvkIOSSurfaceExtensionName))
                            return createUIViewSurface(gd, instance, uiViewSource, hasMetalExtension);

                        throw new VeldridException("Neither macOS surface extension was available: " +
                                                   $"{CommonStrings.VkMvkMacosSurfaceExtensionName}, {CommonStrings.VkMvkIOSSurfaceExtensionName}");
                    }

                    return createUIViewSurface(null, instance, uiViewSource, false);

                default:
                    throw new VeldridException("The provided SwapchainSource cannot be used to create a Vulkan surface.");
            }
        }

        private static VkSurfaceKHR createWin32(VkInstance instance, Win32SwapchainSource win32Source)
        {
            var surfaceCi = VkWin32SurfaceCreateInfoKHR.New();
            surfaceCi.hwnd = win32Source.Hwnd;
            surfaceCi.hinstance = win32Source.Hinstance;
            var result = vkCreateWin32SurfaceKHR(instance, ref surfaceCi, null, out var surface);
            CheckResult(result);
            return surface;
        }

        private static VkSurfaceKHR createXlib(VkInstance instance, XlibSwapchainSource xlibSource)
        {
            var xsci = VkXlibSurfaceCreateInfoKHR.New();
            xsci.dpy = (Display*)xlibSource.Display;
            xsci.window = new Window { Value = xlibSource.Window };
            var result = vkCreateXlibSurfaceKHR(instance, ref xsci, null, out var surface);
            CheckResult(result);
            return surface;
        }

        private static VkSurfaceKHR createWayland(VkInstance instance, WaylandSwapchainSource waylandSource)
        {
            var wsci = VkWaylandSurfaceCreateInfoKHR.New();
            wsci.display = (wl_display*)waylandSource.Display;
            wsci.surface = (wl_surface*)waylandSource.Surface;
            var result = vkCreateWaylandSurfaceKHR(instance, ref wsci, null, out var surface);
            CheckResult(result);
            return surface;
        }

        private static VkSurfaceKHR createAndroidSurface(VkInstance instance, AndroidSurfaceSwapchainSource androidSource)
        {
            IntPtr aNativeWindow = AndroidRuntime.ANativeWindow_fromSurface(androidSource.JniEnv, androidSource.Surface);

            var androidSurfaceCi = VkAndroidSurfaceCreateInfoKHR.New();
            androidSurfaceCi.window = (ANativeWindow*)aNativeWindow;
            var result = vkCreateAndroidSurfaceKHR(instance, ref androidSurfaceCi, null, out var surface);
            CheckResult(result);
            return surface;
        }

        private static VkSurfaceKHR createNSWindowSurface(VkGraphicsDevice gd, VkInstance instance, NSWindowSwapchainSource nsWindowSource, bool hasExtMetalSurface)
        {
            var nswindow = new NSWindow(nsWindowSource.NSWindow);
            return createNSViewSurface(gd, instance, new NSViewSwapchainSource(nswindow.ContentView), hasExtMetalSurface);
        }

        private static VkSurfaceKHR createNSViewSurface(VkGraphicsDevice gd, VkInstance instance, NSViewSwapchainSource nsViewSource, bool hasExtMetalSurface)
        {
            var contentView = new NSView(nsViewSource.NSView);

            if (!CAMetalLayer.TryCast(contentView.Layer, out var metalLayer))
            {
                metalLayer = CAMetalLayer.New();
                contentView.WantsLayer = true;
                contentView.Layer = metalLayer.NativePtr;
            }

            if (hasExtMetalSurface)
            {
                var surfaceCi = new VkMetalSurfaceCreateInfoExt
                {
                    SType = VkMetalSurfaceCreateInfoExt.VK_STRUCTURE_TYPE_METAL_SURFACE_CREATE_INFO_EXT,
                    PLayer = metalLayer.NativePtr.ToPointer()
                };
                VkSurfaceKHR surface;
                var result = gd.CreateMetalSurfaceExt(instance, &surfaceCi, null, &surface);
                CheckResult(result);
                return surface;
            }
            else
            {
                var surfaceCi = VkMacOSSurfaceCreateInfoMVK.New();
                surfaceCi.pView = contentView.NativePtr.ToPointer();
                var result = vkCreateMacOSSurfaceMVK(instance, ref surfaceCi, null, out var surface);
                CheckResult(result);
                return surface;
            }
        }

        private static VkSurfaceKHR createUIViewSurface(VkGraphicsDevice gd, VkInstance instance, UIViewSwapchainSource uiViewSource, bool hasExtMetalSurface)
        {
            var uiView = new UIView(uiViewSource.UIView);

            if (!CAMetalLayer.TryCast(uiView.Layer, out var metalLayer))
            {
                metalLayer = CAMetalLayer.New();
                metalLayer.Frame = uiView.Frame;
                metalLayer.Opaque = true;
                uiView.Layer.AddSublayer(metalLayer.NativePtr);
            }

            if (hasExtMetalSurface)
            {
                var surfaceCi = new VkMetalSurfaceCreateInfoExt
                {
                    SType = VkMetalSurfaceCreateInfoExt.VK_STRUCTURE_TYPE_METAL_SURFACE_CREATE_INFO_EXT,
                    PLayer = metalLayer.NativePtr.ToPointer()
                };
                VkSurfaceKHR surface;
                var result = gd.CreateMetalSurfaceExt(instance, &surfaceCi, null, &surface);
                CheckResult(result);
                return surface;
            }
            else
            {
                var surfaceCi = VkIOSSurfaceCreateInfoMVK.New();
                surfaceCi.pView = uiView.NativePtr.ToPointer();
                vkCreateIOSSurfaceMVK(instance, ref surfaceCi, null, out var surface);
                return surface;
            }
        }
    }
}

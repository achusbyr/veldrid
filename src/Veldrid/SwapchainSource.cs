// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace Veldrid
{
    /// <summary>
    ///     A platform-specific object representing a renderable surface.
    ///     A SwapchainSource can be created with one of several static factory methods.
    ///     A SwapchainSource is used to describe a Swapchain (see <see cref="SwapchainDescription" />).
    /// </summary>
    public abstract class SwapchainSource
    {
        internal SwapchainSource()
        {
        }

        /// <summary>
        ///     Creates a new SwapchainSource for a Win32 window.
        /// </summary>
        /// <param name="hwnd">The Win32 window handle.</param>
        /// <param name="hinstance">The Win32 instance handle.</param>
        /// <returns>
        ///     A new SwapchainSource which can be used to create a <see cref="Swapchain" /> for the given Win32 window.
        /// </returns>
        public static SwapchainSource CreateWin32(IntPtr hwnd, IntPtr hinstance)
        {
            return new Win32SwapchainSource(hwnd, hinstance);
        }

        /// <summary>
        ///     Creates a new SwapchainSource for a UWP SwapChain panel.
        /// </summary>
        /// <param name="swapChainPanel">
        ///     A COM object which must implement the <see cref="Vortice.DXGI.ISwapChainPanelNative" />
        ///     or <see cref="Vortice.DXGI.ISwapChainBackgroundPanelNative" /> interface. Generally, this should be a
        ///     SwapChainPanel
        ///     or SwapChainBackgroundPanel contained in your application window.
        /// </param>
        /// <param name="logicalDpi">The logical DPI of the swapchain panel.</param>
        /// <returns>
        ///     A new SwapchainSource which can be used to create a <see cref="Swapchain" /> for the given UWP panel.
        /// </returns>
        public static SwapchainSource CreateUwp(object swapChainPanel, float logicalDpi)
        {
            return new UwpSwapchainSource(swapChainPanel, logicalDpi);
        }

        /// <summary>
        ///     Creates a new SwapchainSource from the given Xlib information.
        /// </summary>
        /// <param name="display">An Xlib Display.</param>
        /// <param name="window">An Xlib Window.</param>
        /// <returns>
        ///     A new SwapchainSource which can be used to create a <see cref="Swapchain" /> for the given Xlib window.
        /// </returns>
        public static SwapchainSource CreateXlib(IntPtr display, IntPtr window)
        {
            return new XlibSwapchainSource(display, window);
        }

        /// <summary>
        ///     Creates a new SwapchainSource from the given Wayland information.
        /// </summary>
        /// <param name="display">The Wayland display proxy.</param>
        /// <param name="surface">The Wayland surface proxy to map.</param>
        /// <returns>
        ///     A new SwapchainSource which can be used to create a <see cref="Swapchain" /> for the given Wayland surface.
        /// </returns>
        public static SwapchainSource CreateWayland(IntPtr display, IntPtr surface)
        {
            return new WaylandSwapchainSource(display, surface);
        }

        /// <summary>
        ///     Creates a new SwapchainSource for the given NSWindow.
        /// </summary>
        /// <param name="nsWindow">A pointer to an NSWindow.</param>
        /// <returns>
        ///     A new SwapchainSource which can be used to create a Metal <see cref="Swapchain" /> for the given NSWindow.
        /// </returns>
        public static SwapchainSource CreateNSWindow(IntPtr nsWindow)
        {
            return new NSWindowSwapchainSource(nsWindow);
        }

        /// <summary>
        ///     Creates a new SwapchainSource for the given UIView.
        /// </summary>
        /// <param name="uiView">The UIView's native handle.</param>
        /// <returns>
        ///     A new SwapchainSource which can be used to create a Metal <see cref="Swapchain" /> or an OpenGLES
        ///     <see cref="GraphicsDevice" /> for the given UIView.
        /// </returns>
        public static SwapchainSource CreateUIView(IntPtr uiView)
        {
            return new UIViewSwapchainSource(uiView);
        }

        /// <summary>
        ///     Creates a new SwapchainSource for the given Android Surface.
        /// </summary>
        /// <param name="surfaceHandle">The handle of the Android Surface.</param>
        /// <param name="jniEnv">The Java Native Interface Environment handle.</param>
        /// <returns>
        ///     A new SwapchainSource which can be used to create a Vulkan <see cref="Swapchain" /> or an OpenGLES
        ///     <see cref="GraphicsDevice" /> for the given Android Surface.
        /// </returns>
        public static SwapchainSource CreateAndroidSurface(IntPtr surfaceHandle, IntPtr jniEnv)
        {
            return new AndroidSurfaceSwapchainSource(surfaceHandle, jniEnv);
        }

        /// <summary>
        ///     Creates a new SwapchainSource for the given NSView.
        /// </summary>
        /// <param name="nsView">A pointer to an NSView.</param>
        /// <returns>
        ///     A new SwapchainSource which can be used to create a Metal <see cref="Swapchain" /> for the given NSView.
        /// </returns>
        public static SwapchainSource CreateNSView(IntPtr nsView)
        {
            return new NSViewSwapchainSource(nsView);
        }
    }

    internal class Win32SwapchainSource(IntPtr hwnd, IntPtr hinstance) : SwapchainSource
    {
        public IntPtr Hwnd { get; } = hwnd;
        public IntPtr Hinstance { get; } = hinstance;
    }

    internal class UwpSwapchainSource(object swapChainPanelNative, float logicalDpi) : SwapchainSource
    {
        public object SwapChainPanelNative { get; } = swapChainPanelNative;
        public float LogicalDpi { get; } = logicalDpi;
    }

    internal class XlibSwapchainSource(IntPtr display, IntPtr window) : SwapchainSource
    {
        public IntPtr Display { get; } = display;
        public IntPtr Window { get; } = window;
    }

    internal class WaylandSwapchainSource(IntPtr display, IntPtr surface) : SwapchainSource
    {
        public IntPtr Display { get; } = display;
        public IntPtr Surface { get; } = surface;
    }

    internal class NSWindowSwapchainSource(IntPtr nsWindow) : SwapchainSource
    {
        public IntPtr NSWindow { get; } = nsWindow;
    }

    internal class UIViewSwapchainSource(IntPtr uiView) : SwapchainSource
    {
        public IntPtr UIView { get; } = uiView;
    }

    internal class AndroidSurfaceSwapchainSource(IntPtr surfaceHandle, IntPtr jniEnv) : SwapchainSource
    {
        public IntPtr Surface { get; } = surfaceHandle;
        public IntPtr JniEnv { get; } = jniEnv;
    }

    internal class NSViewSwapchainSource(IntPtr nsView) : SwapchainSource
    {
        public IntPtr NSView { get; } = nsView;
    }
}

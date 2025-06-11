﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace Veldrid
{
    /// <summary>
    ///     A device resource providing the ability to present rendered images to a visible surface.
    ///     See <see cref="SwapchainDescription" />.
    /// </summary>
    public abstract class Swapchain : IDeviceResource, IDisposable
    {
        /// <summary>
        ///     Gets a <see cref="Framebuffer" /> representing the render targets of this instance.
        /// </summary>
        public abstract Framebuffer Framebuffer { get; }

        /// <summary>
        ///     A bool indicating whether this instance has been disposed.
        /// </summary>
        public abstract bool IsDisposed { get; }

        /// <summary>
        ///     Gets or sets whether presentation of this Swapchain will be synchronized to the window system's vertical refresh
        ///     rate.
        /// </summary>
        public abstract bool SyncToVerticalBlank { get; set; }

        /// <summary>
        ///     A string identifying this instance. Can be used to differentiate between objects in graphics debuggers and other
        ///     tools.
        /// </summary>
        public abstract string Name { get; set; }

        #region Disposal

        /// <summary>
        ///     Frees unmanaged device resources controlled by this instance.
        /// </summary>
        public abstract void Dispose();

        #endregion

        /// <summary>
        ///     Resizes the renderable Textures managed by this instance to the given dimensions.
        /// </summary>
        /// <param name="width">The new width of the Swapchain.</param>
        /// <param name="height">The new height of the Swapchain.</param>
        public abstract void Resize(uint width, uint height);
    }
}

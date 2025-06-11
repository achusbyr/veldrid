﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace Veldrid
{
    /// <summary>
    ///     A bindable device resource which provides a shader with access to a sampled <see cref="Texture" /> object.
    ///     See <see cref="TextureViewDescription" />.
    /// </summary>
    public abstract class TextureView : IBindableResource, IDeviceResource, IDisposable
    {
        /// <summary>
        ///     The target <see cref="Texture" /> object to be sampled via this instance.
        /// </summary>
        public Texture Target { get; }

        /// <summary>
        ///     The base mip level visible in the view.
        /// </summary>
        public uint BaseMipLevel { get; }

        /// <summary>
        ///     The number of mip levels visible in the view.
        /// </summary>
        public uint MipLevels { get; }

        /// <summary>
        ///     The base array layer visible in the view.
        /// </summary>
        public uint BaseArrayLayer { get; }

        /// <summary>
        ///     The number of array layers visible in the view.
        /// </summary>
        public uint ArrayLayers { get; }

        /// <summary>
        ///     The format used to interpret the contents of the target Texture. This may be different from the target Texture's
        ///     true storage format, but it will be the same size.
        /// </summary>
        public PixelFormat Format { get; }

        /// <summary>
        ///     A bool indicating whether this instance has been disposed.
        /// </summary>
        public abstract bool IsDisposed { get; }

        /// <summary>
        ///     A string identifying this instance. Can be used to differentiate between objects in graphics debuggers and other
        ///     tools.
        /// </summary>
        public abstract string Name { get; set; }

        internal TextureView(ref TextureViewDescription description)
        {
            Target = description.Target;
            BaseMipLevel = description.BaseMipLevel;
            MipLevels = description.MipLevels;
            BaseArrayLayer = description.BaseArrayLayer;
            ArrayLayers = description.ArrayLayers;
            Format = description.Format ?? description.Target.Format;
        }

        #region Disposal

        /// <summary>
        ///     Frees unmanaged device resources controlled by this instance.
        /// </summary>
        public abstract void Dispose();

        #endregion
    }
}

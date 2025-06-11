﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#if !EXCLUDE_D3D11_BACKEND
using System;
using Veldrid.D3D11;
using Vortice.Direct3D;

namespace Veldrid
{
    /// <summary>
    ///     Exposes Direct3D 11-specific functionality,
    ///     useful for interoperating with native components which interface directly with Direct3D 11.
    ///     Can only be used on <see cref="GraphicsBackend.Direct3D11" />.
    /// </summary>
    public class BackendInfoD3D11
    {
        /// <summary>
        ///     Gets a pointer to the ID3D11Device controlled by the GraphicsDevice.
        /// </summary>
        public IntPtr Device => gd.Device.NativePointer;

        /// <summary>
        ///     Gets a pointer to the IAdapter used to create the GraphicsDevice.
        /// </summary>
        public IntPtr Adapter => gd.Adapter.NativePointer;

        /// <summary>
        ///     Returns the feature level of the D3D11 device.
        /// </summary>
        public FeatureLevel FeatureLevel => gd.Device.FeatureLevel;

        /// <summary>
        ///     Gets the PCI ID of the hardware device.
        /// </summary>
        public int DeviceId => gd.DeviceId;

        private readonly D3D11GraphicsDevice gd;

        internal BackendInfoD3D11(D3D11GraphicsDevice gd)
        {
            this.gd = gd;
        }

        /// <summary>
        ///     Gets a pointer to the native texture wrapped by the given Veldrid Texture. Depending on the instance's TextureType,
        ///     this will be a pointer to an ID3D11Texture1D, an ID3D11Texture2D, or an ID3D11Texture3D.
        /// </summary>
        /// <returns>
        ///     A pointer to the Veldrid Texture's underlying ID3D11Texture1D, ID3D11Texture2D, or ID3D11Texture3D. The type
        ///     of this object depends on the parameter's TextureType.
        /// </returns>
        public IntPtr GetTexturePointer(Texture texture)
        {
            return Util.AssertSubtype<Texture, D3D11Texture>(texture).DeviceTexture.NativePointer;
        }
    }
}
#endif

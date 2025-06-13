// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace Veldrid.D3D11
{
    /// <summary>
    ///     A structure describing Direct3D11-specific device creation options.
    /// </summary>
    public struct D3D11DeviceOptions
    {
        /// <summary>
        ///     Native pointer to an adapter.
        /// </summary>
        public IntPtr AdapterPtr;

        /// <summary>
        ///     Set of device specific flags.
        ///     See <see cref="Vortice.Direct3D11.DeviceCreationFlags" /> for details.
        /// </summary>
        public uint DeviceCreationFlags;
    }
}

﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid
{
    /// <summary>
    ///     A resource owned by a <see cref="GraphicsDevice" />, which can be given a string identifier for debugging and
    ///     informational purposes.
    /// </summary>
    public interface IDeviceResource
    {
        /// <summary>
        ///     A string identifying this instance. Can be used to differentiate between objects in graphics debuggers and other
        ///     tools.
        /// </summary>
        string Name { get; set; }
    }
}

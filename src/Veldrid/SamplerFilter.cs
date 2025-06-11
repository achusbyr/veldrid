﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid
{
    /// <summary>
    ///     Determines how texture values are sampled from a texture.
    /// </summary>
    public enum SamplerFilter : byte
    {
        /// <summary>
        ///     Point sampling is used for minification, magnification, and mip-level sampling.
        /// </summary>
        MinPointMagPointMipPoint,

        /// <summary>
        ///     Point sampling is used for minification and magnification; linear interpolation is used for mip-level sampling.
        /// </summary>
        MinPointMagPointMipLinear,

        /// <summary>
        ///     Point sampling is used for minification and mip-level sampling; linear interpolation is used for mip-level
        ///     sampling.
        /// </summary>
        MinPointMagLinearMipPoint,

        /// <summary>
        ///     Point sampling is used for minification; linear interpolation is used for magnification and mip-level sampling.
        /// </summary>
        MinPointMagLinearMipLinear,

        /// <summary>
        ///     Linear interpolation is used for minifcation; point sampling is used for magnification and mip-level sampling.
        /// </summary>
        MinLinearMagPointMipPoint,

        /// <summary>
        ///     Linear interpolation is used for minification and mip-level sampling; point sampling is used for magnification.
        /// </summary>
        MinLinearMagPointMipLinear,

        /// <summary>
        ///     Linear interpolation is used for minification and magnification, and point sampling is used for mip-level sampling.
        /// </summary>
        MinLinearMagLinearMipPoint,

        /// <summary>
        ///     Linear interpolation is used for minification, magnification, and mip-level sampling.
        /// </summary>
        MinLinearMagLinearMipLinear,

        /// <summary>
        ///     Anisotropic filtering is used. The maximum anisotropy is controlled by
        ///     <see cref="SamplerDescription.MaximumAnisotropy" />.
        /// </summary>
        Anisotropic
    }
}

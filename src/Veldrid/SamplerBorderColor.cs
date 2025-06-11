// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid
{
    /// <summary>
    ///     A constant color that is sampled when <see cref="SamplerAddressMode.Border" /> is used.
    /// </summary>
    public enum SamplerBorderColor : byte
    {
        /// <summary>
        ///     Transparent Black (0, 0, 0, 0)
        /// </summary>
        TransparentBlack,

        /// <summary>
        ///     Opaque Black (0, 0, 0, 1)
        /// </summary>
        OpaqueBlack,

        /// <summary>
        ///     Opaque White (1, 1, 1, 1)
        /// </summary>
        OpaqueWhite
    }
}

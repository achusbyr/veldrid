// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid
{
    /// <summary>
    ///     Describes the number of samples to use in a <see cref="Texture" />.
    /// </summary>
    public enum TextureSampleCount : byte
    {
        /// <summary>
        ///     1 sample (no multi-sampling).
        /// </summary>
        Count1,

        /// <summary>
        ///     2 Samples.
        /// </summary>
        Count2,

        /// <summary>
        ///     4 Samples.
        /// </summary>
        Count4,

        /// <summary>
        ///     8 Samples.
        /// </summary>
        Count8,

        /// <summary>
        ///     16 Samples.
        /// </summary>
        Count16,

        /// <summary>
        ///     32 Samples.
        /// </summary>
        Count32
    }
}

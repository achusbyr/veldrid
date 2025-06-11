// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid
{
    internal static class BlendHelper
    {
        /// <summary>
        ///     Given a nullable <see cref="ColorWriteMask" />, returns the mask as non-null, or <see cref="ColorWriteMask.All" />
        ///     if null.
        /// </summary>
        /// <param name="mask">A nullable mask.</param>
        /// <returns>The non-nullable mask.</returns>
        public static ColorWriteMask GetOrDefault(this ColorWriteMask? mask)
        {
            return mask ?? ColorWriteMask.All;
        }
    }
}

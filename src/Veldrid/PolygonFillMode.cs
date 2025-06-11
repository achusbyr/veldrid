// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid
{
    /// <summary>
    ///     Indicates how the rasterizer will fill polygons.
    /// </summary>
    public enum PolygonFillMode : byte
    {
        /// <summary>
        ///     Polygons are filled completely.
        /// </summary>
        Solid,

        /// <summary>
        ///     Polygons are outlined in a "wireframe" style.
        /// </summary>
        Wireframe
    }
}

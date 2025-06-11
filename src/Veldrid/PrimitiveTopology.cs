// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid
{
    /// <summary>
    ///     Determines how a sequence of vertices is interepreted by the rasterizer.
    /// </summary>
    public enum PrimitiveTopology : byte
    {
        /// <summary>
        ///     A list of isolated, 3-element triangles.
        /// </summary>
        TriangleList,

        /// <summary>
        ///     A series of connected triangles.
        /// </summary>
        TriangleStrip,

        /// <summary>
        ///     A series of isolated, 2-element line segments.
        /// </summary>
        LineList,

        /// <summary>
        ///     A series of connected line segments.
        /// </summary>
        LineStrip,

        /// <summary>
        ///     A series of isolated points.
        /// </summary>
        PointList
    }
}

// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid
{
    /// <summary>
    ///     Indicates which face will be culled.
    /// </summary>
    public enum FaceCullMode : byte
    {
        /// <summary>
        ///     The back face.
        /// </summary>
        Back,

        /// <summary>
        ///     The front face.
        /// </summary>
        Front,

        /// <summary>
        ///     No face culling.
        /// </summary>
        None
    }
}

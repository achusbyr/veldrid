// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid
{
    /// <summary>
    ///     Identifies a particular binding model used when connecting elements in a <see cref="ResourceLayout" /> with
    ///     resources
    ///     defined in API-specific shader code.
    /// </summary>
    public enum ResourceBindingModel
    {
        /// <summary>
        ///     The default binding model.
        /// </summary>
        Default = 0,

        /// <summary>
        ///     An improved binding model which enables greater shader reuse and authoring flexibility.
        /// </summary>
        Improved = 1
    }
}

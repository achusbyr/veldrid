// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid
{
    /// <summary>
    ///     A structure describing the format expected by indirect dispatch commands contained in an indirect
    ///     <see cref="DeviceBuffer" />.
    /// </summary>
    public struct IndirectDispatchArguments
    {
        /// <summary>
        ///     The X group count, as if passed to the <see cref="CommandList.Dispatch(uint, uint, uint)" /> method.
        /// </summary>
        public uint GroupCountX;

        /// <summary>
        ///     The Y group count, as if passed to the <see cref="CommandList.Dispatch(uint, uint, uint)" /> method.
        /// </summary>
        public uint GroupCountY;

        /// <summary>
        ///     The Z group count, as if passed to the <see cref="CommandList.Dispatch(uint, uint, uint)" /> method.
        /// </summary>
        public uint GroupCountZ;
    }
}

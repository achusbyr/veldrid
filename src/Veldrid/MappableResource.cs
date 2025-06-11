// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid
{
    /// <summary>
    ///     A marker interface designating a device resource which can be mapped into CPU-visible memory with
    ///     <see cref="GraphicsDevice.Map(IMappableResource, MapMode, uint)" />
    /// </summary>
    public interface IMappableResource
    {
    }
}

// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid.MetalBindings
{
    public struct MTLRegion(MTLOrigin origin, MTLSize size)
    {
        public MTLOrigin Origin = origin;
        public MTLSize Size = size;
    }
}

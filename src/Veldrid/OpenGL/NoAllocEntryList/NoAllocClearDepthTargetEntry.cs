// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid.OpenGL.NoAllocEntryList
{
    internal struct NoAllocClearDepthTargetEntry
    {
        public readonly float Depth;
        public readonly byte Stencil;

        public NoAllocClearDepthTargetEntry(float depth, byte stencil)
        {
            Depth = depth;
            Stencil = stencil;
        }
    }
}

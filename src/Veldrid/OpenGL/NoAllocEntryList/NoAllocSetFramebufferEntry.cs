// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid.OpenGL.NoAllocEntryList
{
    internal struct NoAllocSetFramebufferEntry
    {
        public readonly Tracked<Framebuffer> Framebuffer;

        public NoAllocSetFramebufferEntry(Tracked<Framebuffer> fb)
        {
            Framebuffer = fb;
        }
    }
}

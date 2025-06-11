// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid.OpenGL.NoAllocEntryList
{
    internal struct NoAllocResolveTextureEntry
    {
        public readonly Tracked<Texture> Source;
        public readonly Tracked<Texture> Destination;

        public NoAllocResolveTextureEntry(Tracked<Texture> source, Tracked<Texture> destination)
        {
            Source = source;
            Destination = destination;
        }
    }
}

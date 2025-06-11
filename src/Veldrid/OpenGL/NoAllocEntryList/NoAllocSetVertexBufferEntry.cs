// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid.OpenGL.NoAllocEntryList
{
    internal struct NoAllocSetVertexBufferEntry
    {
        public readonly uint Index;
        public readonly Tracked<DeviceBuffer> Buffer;
        public uint Offset;

        public NoAllocSetVertexBufferEntry(uint index, Tracked<DeviceBuffer> buffer, uint offset)
        {
            Index = index;
            Buffer = buffer;
            Offset = offset;
        }
    }
}

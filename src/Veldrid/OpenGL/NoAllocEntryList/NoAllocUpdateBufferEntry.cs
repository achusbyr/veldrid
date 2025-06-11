// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid.OpenGL.NoAllocEntryList
{
    internal struct NoAllocUpdateBufferEntry
    {
        public readonly Tracked<DeviceBuffer> Buffer;
        public readonly uint BufferOffsetInBytes;
        public readonly StagingBlock StagingBlock;
        public readonly uint StagingBlockSize;

        public NoAllocUpdateBufferEntry(Tracked<DeviceBuffer> buffer, uint bufferOffsetInBytes, StagingBlock stagingBlock, uint stagingBlockSize)
        {
            Buffer = buffer;
            BufferOffsetInBytes = bufferOffsetInBytes;
            StagingBlock = stagingBlock;
            StagingBlockSize = stagingBlockSize;
        }
    }
}

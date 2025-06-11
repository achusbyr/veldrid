// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid.OpenGL.NoAllocEntryList
{
    internal struct NoAllocCopyBufferEntry
    {
        public readonly Tracked<DeviceBuffer> Source;
        public readonly uint SourceOffset;
        public readonly Tracked<DeviceBuffer> Destination;
        public readonly uint DestinationOffset;
        public readonly uint SizeInBytes;

        public NoAllocCopyBufferEntry(Tracked<DeviceBuffer> source, uint sourceOffset, Tracked<DeviceBuffer> destination, uint destinationOffset, uint sizeInBytes)
        {
            Source = source;
            SourceOffset = sourceOffset;
            Destination = destination;
            DestinationOffset = destinationOffset;
            SizeInBytes = sizeInBytes;
        }
    }
}

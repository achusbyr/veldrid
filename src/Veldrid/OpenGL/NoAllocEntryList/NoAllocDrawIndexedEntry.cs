// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid.OpenGL.NoAllocEntryList
{
    internal struct NoAllocDrawIndexedEntry
    {
        public readonly uint IndexCount;
        public readonly uint InstanceCount;
        public readonly uint IndexStart;
        public readonly int VertexOffset;
        public readonly uint InstanceStart;

        public NoAllocDrawIndexedEntry(uint indexCount, uint instanceCount, uint indexStart, int vertexOffset, uint instanceStart)
        {
            IndexCount = indexCount;
            InstanceCount = instanceCount;
            IndexStart = indexStart;
            VertexOffset = vertexOffset;
            InstanceStart = instanceStart;
        }
    }
}

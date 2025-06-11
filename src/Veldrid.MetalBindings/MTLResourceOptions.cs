// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid.MetalBindings
{
    public enum MTLResourceOptions : ulong
    {
        CPUCacheModeDefaultCache = MtlcpuCacheMode.DefaultCache,
        CPUCacheModeWriteCombined = MtlcpuCacheMode.WriteCombined,

        StorageModeShared = MTLStorageMode.Shared << 4,
        StorageModeManaged = MTLStorageMode.Managed << 4,
        StorageModePrivate = MTLStorageMode.Private << 4,
        StorageModeMemoryless = MTLStorageMode.Memoryless << 4,

        HazardTrackingModeUntracked = (uint)(0x1UL << 8),
    }
}

// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid.MetalBindings
{
    public enum MTLStorageMode : ulong
    {
        Shared = 0,
        Managed = 1,
        Private = 2,
        Memoryless = 3,
    }
}

// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid.MetalBindings
{
    public enum MTLLanguageVersion : uint
    {
        Version1_0 = 1 << 16,
        Version1_1 = (1 << 16) + 1,
        Version1_2 = (1 << 16) + 2,
    }
}

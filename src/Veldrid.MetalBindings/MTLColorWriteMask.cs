// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace Veldrid.MetalBindings
{
    [Flags]
    public enum MTLColorWriteMask
    {
        None = 0,
        Red = 1 << 3,
        Green = 1 << 2,
        Blue = 1 << 1,
        Alpha = 1 << 0,
        All = Red | Green | Blue | Alpha,
    }
}

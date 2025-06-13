// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.InteropServices;

namespace Veldrid.MetalBindings
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MTLSize(uint width, uint height, uint depth)
    {
        public UIntPtr Width = width;
        public UIntPtr Height = height;
        public UIntPtr Depth = depth;
    }
}

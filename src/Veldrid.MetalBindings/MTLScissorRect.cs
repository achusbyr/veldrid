// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace Veldrid.MetalBindings
{
    public struct MTLScissorRect(uint x, uint y, uint width, uint height)
        : IEquatable<MTLScissorRect>
    {
        public UIntPtr X = x;
        public UIntPtr Y = y;
        public UIntPtr Width = width;
        public UIntPtr Height = height;

        public bool Equals(MTLScissorRect other)
        {
            return X == other.X
                   && Y == other.Y
                   && Width == other.Width
                   && Height == other.Height;
        }
    }
}

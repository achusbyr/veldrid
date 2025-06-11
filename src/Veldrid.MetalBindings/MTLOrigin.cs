// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace Veldrid.MetalBindings
{
    public struct MTLOrigin
    {
        public UIntPtr X;
        public UIntPtr Y;
        public UIntPtr Z;

        public MTLOrigin(uint x, uint y, uint z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}

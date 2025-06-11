// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace Veldrid.MetalBindings
{
    public struct NSRange
    {
        public UIntPtr Location;
        public UIntPtr Length;

        public NSRange(UIntPtr location, UIntPtr length)
        {
            Location = location;
            Length = length;
        }

        public NSRange(uint location, uint length)
        {
            Location = location;
            Length = length;
        }
    }
}

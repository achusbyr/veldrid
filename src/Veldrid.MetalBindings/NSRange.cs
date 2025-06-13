// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace Veldrid.MetalBindings
{
    public struct NSRange(UIntPtr location, UIntPtr length)
    {
        public UIntPtr Location = location;
        public UIntPtr Length = length;

        public NSRange(uint location, uint length)
            : this(location, (UIntPtr)length)
        {
        }
    }
}

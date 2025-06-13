// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid.MetalBindings
{
    public struct Bool8(byte value)
    {
        public readonly byte Value = value;

        public Bool8(bool value)
            : this(value ? (byte)1 : (byte)0)
        {
        }

        public static implicit operator bool(Bool8 b)
        {
            return b.Value != 0;
        }

        public static implicit operator byte(Bool8 b)
        {
            return b.Value;
        }

        public static implicit operator Bool8(bool b)
        {
            return new Bool8(b);
        }
    }
}

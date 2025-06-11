// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid.MetalBindings
{
    // TODO: Technically this should be "pointer-sized",
    // but there are no non-64-bit platforms that anyone cares about.
    public struct CGFloat
    {
        private readonly double _value;

        public CGFloat(double value)
        {
            _value = value;
        }

        public double Value => _value;

        public static implicit operator CGFloat(double value)
        {
            return new CGFloat(value);
        }

        public static implicit operator double(CGFloat cgf)
        {
            return cgf.Value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}

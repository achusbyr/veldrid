// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid.MetalBindings
{
    public struct CGPoint(double x, double y)
    {
        public CGFloat X = x;
        public CGFloat Y = y;

        public override string ToString()
        {
            return string.Format("({0},{1})", X, Y);
        }
    }
}

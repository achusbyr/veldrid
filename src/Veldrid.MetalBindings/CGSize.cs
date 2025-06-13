// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid.MetalBindings
{
    public struct CGSize(double width, double height)
    {
        public double Width = width;
        public double Height = height;

        public override string ToString()
        {
            return string.Format("{0} x {1}", Width, Height);
        }
    }
}

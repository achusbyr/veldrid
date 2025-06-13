// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid.MetalBindings
{
    public struct MTLViewport(double originX, double originY, double width, double height, double znear, double zfar)
    {
        public double OriginX = originX;
        public double OriginY = originY;
        public double Width = width;
        public double Height = height;
        public double Znear = znear;
        public double Zfar = zfar;
    }
}

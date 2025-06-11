// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid.MetalBindings
{
    public struct MTLViewport
    {
        public double OriginX;
        public double OriginY;
        public double Width;
        public double Height;
        public double Znear;
        public double Zfar;

        public MTLViewport(double originX, double originY, double width, double height, double znear, double zfar)
        {
            OriginX = originX;
            OriginY = originY;
            Width = width;
            Height = height;
            Znear = znear;
            Zfar = zfar;
        }
    }
}

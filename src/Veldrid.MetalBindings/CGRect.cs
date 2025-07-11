// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid.MetalBindings
{
    public struct CGRect(CGPoint origin, CGSize size)
    {
        public CGPoint Origin = origin;
        public CGSize Size = size;

        public override string ToString()
        {
            return string.Format("{0}, {1}", Origin, Size);
        }
    }
}

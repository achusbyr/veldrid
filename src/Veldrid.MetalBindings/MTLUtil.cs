// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Text;

namespace Veldrid.MetalBindings
{
    public static class MTLUtil
    {
        public static unsafe string GetUtf8String(byte* stringStart)
        {
            int characters = 0;

            while (stringStart[characters] != 0)
            {
                characters++;
            }

            return Encoding.UTF8.GetString(stringStart, characters);
        }

        public static T AllocInit<T>(string typeName) where T : struct
        {
            var cls = new ObjCClass(typeName);
            return cls.AllocInit<T>();
        }
    }
}

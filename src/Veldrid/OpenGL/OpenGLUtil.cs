// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Diagnostics;
using System.Text;
using Veldrid.OpenGLBindings;
using static Veldrid.OpenGLBindings.OpenGLNative;

namespace Veldrid.OpenGL
{
    internal static class OpenGLUtil
    {
        private static int? maxLabelLength;

        [Conditional("DEBUG")]
        [DebuggerNonUserCode]
        internal static void CheckLastError()
        {
            uint error = GLGetError();

            if (error != 0)
            {
                if (Debugger.IsAttached) Debugger.Break();

                throw new VeldridException("glGetError indicated an error: " + (ErrorCode)error);
            }
        }

        internal static unsafe void SetObjectLabel(ObjectLabelIdentifier identifier, uint target, string name)
        {
            if (HasGlObjectLabel)
            {
                int byteCount = Encoding.UTF8.GetByteCount(name);

                if (maxLabelLength == null)
                {
                    int localMaxLabelLength = -1;
                    GLGetIntegerv(GetPName.MaxLabelLength, &localMaxLabelLength);
                    CheckLastError();
                    maxLabelLength = localMaxLabelLength;
                }

                if (byteCount >= maxLabelLength)
                {
                    name = name.Substring(0, maxLabelLength.Value - 4) + "...";
                    byteCount = Encoding.UTF8.GetByteCount(name);
                }

                Span<byte> utf8Bytes = stackalloc byte[128];
                if (byteCount + 1 > 128) utf8Bytes = new byte[byteCount + 1];

                fixed (char* namePtr = name)
                fixed (byte* utf8BytePtr = utf8Bytes)
                {
                    int written = Encoding.UTF8.GetBytes(namePtr, name.Length, utf8BytePtr, byteCount);
                    utf8BytePtr[written] = 0;
                    GLObjectLabel(identifier, target, (uint)byteCount, utf8BytePtr);
                    CheckLastError();
                }
            }
        }

        internal static TextureTarget GetTextureTarget(OpenGLTexture glTex, uint arrayLayer)
        {
            if ((glTex.Usage & TextureUsage.Cubemap) == TextureUsage.Cubemap)
            {
                switch (arrayLayer % 6)
                {
                    case 0:
                        return TextureTarget.TextureCubeMapPositiveX;

                    case 1:
                        return TextureTarget.TextureCubeMapNegativeX;

                    case 2:
                        return TextureTarget.TextureCubeMapPositiveY;

                    case 3:
                        return TextureTarget.TextureCubeMapNegativeY;

                    case 4:
                        return TextureTarget.TextureCubeMapPositiveZ;

                    case 5:
                        return TextureTarget.TextureCubeMapNegativeZ;
                }
            }

            return glTex.TextureTarget;
        }
    }
}

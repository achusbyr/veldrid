// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid.Vk
{
    internal struct VkVersion
    {
        private readonly uint value;

        public VkVersion(uint major, uint minor, uint patch)
        {
            value = (major << 22) | (minor << 12) | patch;
        }

        public uint Major => value >> 22;

        public uint Minor => (value >> 12) & 0x3ff;

        public uint Patch => (value >> 22) & 0xfff;

        public static implicit operator uint(VkVersion version)
        {
            return version.value;
        }
    }
}

// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid.MetalBindings
{
    public struct CVTimeStamp
    {
        public ulong Flags;
        public ulong HostTime;
        public double RateScalar;
        public ulong Reserved;
        public CvsmpteTime SmpteTime;
        public uint Version;
        public long VideoRefreshPeriod;
        public long VideoTime;
        public int VideoTimeScale;
    }
}

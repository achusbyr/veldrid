// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.InteropServices;

namespace Veldrid.MetalBindings
{
    public struct CVDisplayLink
    {
        private const string cv_framework = "/System/Library/Frameworks/CoreVideo.framework/CoreVideo";
        private const string cg_framework = "/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics";

        public readonly IntPtr NativePtr;

        public static implicit operator IntPtr(CVDisplayLink c)
        {
            return c.NativePtr;
        }

        public CVDisplayLink(IntPtr ptr)
        {
            NativePtr = ptr;
        }

        public static CVDisplayLink CreateWithActiveCGDisplays()
        {
            CVDisplayLinkCreateWithActiveCGDisplays(out CVDisplayLink link);
            return link;
        }

        public void SetOutputCallback(CVDisplayLinkOutputCallbackDelegate callback, IntPtr userData)
        {
            CVDisplayLinkSetOutputCallback(this, callback, userData);
        }

        public void Start()
        {
            CVDisplayLinkStart(this);
        }

        public void UpdateActiveMonitor(int x, int y, int w, int h)
        {
            uint[] displays = new uint[1];
            uint displayCount = 0;
            CGRect rect = new CGRect(new CGPoint(x, y), new CGSize(w, h));
            int err = CGGetDisplaysWithRect(rect, 1, displays, ref displayCount);

            if (err != 0)
            {
                return;
            }

            if (displayCount > 0)
            {
                CVDisplayLinkSetCurrentCGDisplay(this, displays[0]);
            }
        }

        [DllImport(cg_framework)]
        private static extern int CGGetDisplaysWithRect(CGRect rect, int maxDisplays, uint[] displays, ref uint displayCount);

        public double GetActualOutputVideoRefreshPeriod()
        {
            return CVDisplayLinkGetActualOutputVideoRefreshPeriod(this);
        }

        public void Stop()
        {
            CVDisplayLinkStop(this);
        }

        public void Release()
        {
            CVDisplayLinkRelease(this);
        }

        [DllImport(cv_framework)]
        private static extern int CVDisplayLinkCreateWithActiveCGDisplays(out CVDisplayLink displayLink);

        [DllImport(cv_framework)]
        private static extern double CVDisplayLinkGetActualOutputVideoRefreshPeriod(CVDisplayLink displayLink);

        [DllImport(cv_framework)]
        private static extern int CVDisplayLinkSetOutputCallback(CVDisplayLink displayLink, CVDisplayLinkOutputCallbackDelegate callback, IntPtr userData);

        [DllImport(cv_framework)]
        private static extern int CVDisplayLinkSetCurrentCGDisplay(CVDisplayLink displayLink, uint displayId);

        [DllImport(cv_framework)]
        private static extern int CVDisplayLinkStart(CVDisplayLink displayLink);

        [DllImport(cv_framework)]
        private static extern int CVDisplayLinkStop(CVDisplayLink displayLink);

        [DllImport(cv_framework)]
        private static extern int CVDisplayLinkRelease(CVDisplayLink displayLink);
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int CVDisplayLinkOutputCallbackDelegate(CVDisplayLink displayLink, CVTimeStamp* inNow, CVTimeStamp* inOutputTime, long flagsIn, long flagsOut, IntPtr userData);
}

// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MTLCommandQueue
    {
        public readonly IntPtr NativePtr;

        [Pure]
        public MTLCommandBuffer CommandBuffer()
        {
            return objc_msgSend<MTLCommandBuffer>(NativePtr, sel_commandBuffer);
        }

        public void InsertDebugCaptureBoundary()
        {
            objc_msgSend(NativePtr, sel_insertDebugCaptureBoundary);
        }

        private static readonly Selector sel_commandBuffer = "commandBuffer";
        private static readonly Selector sel_insertDebugCaptureBoundary = "insertDebugCaptureBoundary";
    }
}

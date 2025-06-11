// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.InteropServices;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MTLCommandBuffer : IEquatable<MTLCommandBuffer>
    {
        public readonly IntPtr NativePtr;

        public MTLRenderCommandEncoder RenderCommandEncoderWithDescriptor(MTLRenderPassDescriptor desc)
        {
            return new MTLRenderCommandEncoder(
                IntPtr_objc_msgSend(NativePtr, sel_renderCommandEncoderWithDescriptor, desc.NativePtr));
        }

        public void PresentDrawable(IntPtr drawable)
        {
            objc_msgSend(NativePtr, sel_presentDrawable, drawable);
        }

        public void Commit()
        {
            objc_msgSend(NativePtr, sel_commit);
        }

        public MTLBlitCommandEncoder BlitCommandEncoder()
        {
            return objc_msgSend<MTLBlitCommandEncoder>(NativePtr, sel_blitCommandEncoder);
        }

        public MTLComputeCommandEncoder ComputeCommandEncoder()
        {
            return objc_msgSend<MTLComputeCommandEncoder>(NativePtr, sel_computeCommandEncoder);
        }

        public void WaitUntilCompleted()
        {
            objc_msgSend(NativePtr, sel_waitUntilCompleted);
        }

        public void AddCompletedHandler(MTLCommandBufferHandler block)
        {
            objc_msgSend(NativePtr, sel_addCompletedHandler, block);
        }

        public void AddCompletedHandler(IntPtr block)
        {
            objc_msgSend(NativePtr, sel_addCompletedHandler, block);
        }

        public MTLCommandBufferStatus Status => (MTLCommandBufferStatus)uint_objc_msgSend(NativePtr, sel_status);

        private static readonly Selector sel_renderCommandEncoderWithDescriptor = "renderCommandEncoderWithDescriptor:";
        private static readonly Selector sel_presentDrawable = "presentDrawable:";
        private static readonly Selector sel_commit = "commit";
        private static readonly Selector sel_blitCommandEncoder = "blitCommandEncoder";
        private static readonly Selector sel_computeCommandEncoder = "computeCommandEncoder";
        private static readonly Selector sel_waitUntilCompleted = "waitUntilCompleted";
        private static readonly Selector sel_addCompletedHandler = "addCompletedHandler:";
        private static readonly Selector sel_status = "status";

        public bool Equals(MTLCommandBuffer other)
        {
            return NativePtr == other.NativePtr;
        }

        public override bool Equals(object obj)
        {
            return obj is MTLCommandBuffer other && Equals(other);
        }

        public override int GetHashCode()
        {
            return NativePtr.GetHashCode();
        }
    }
}

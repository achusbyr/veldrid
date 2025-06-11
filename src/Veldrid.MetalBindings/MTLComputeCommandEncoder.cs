// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    public struct MTLComputeCommandEncoder
    {
        public readonly IntPtr NativePtr;
        public bool IsNull => NativePtr == IntPtr.Zero;

        private static readonly Selector sel_set_compute_pipeline_state = "setComputePipelineState:";
        private static readonly Selector sel_set_buffer = "setBuffer:offset:atIndex:";
        private static readonly Selector sel_dispatch_threadgroups0 = "dispatchThreadgroups:threadsPerThreadgroup:";
        private static readonly Selector sel_dispatch_threadgroups1 = "dispatchThreadgroupsWithIndirectBuffer:indirectBufferOffset:threadsPerThreadgroup:";
        private static readonly Selector sel_end_encoding = "endEncoding";
        private static readonly Selector sel_set_texture = "setTexture:atIndex:";
        private static readonly Selector sel_set_sampler_state = "setSamplerState:atIndex:";
        private static readonly Selector sel_set_bytes = "setBytes:length:atIndex:";

        public void SetComputePipelineState(MTLComputePipelineState state)
        {
            objc_msgSend(NativePtr, sel_set_compute_pipeline_state, state.NativePtr);
        }

        public void SetBuffer(MTLBuffer buffer, UIntPtr offset, UIntPtr index)
        {
            objc_msgSend(NativePtr, sel_set_buffer,
                buffer.NativePtr,
                offset,
                index);
        }

        public unsafe void SetBytes(void* bytes, UIntPtr length, UIntPtr index)
        {
            objc_msgSend(NativePtr, sel_set_bytes, bytes, length, index);
        }

        public void DispatchThreadGroups(MTLSize threadgroupsPerGrid, MTLSize threadsPerThreadgroup)
        {
            objc_msgSend(NativePtr, sel_dispatch_threadgroups0, threadgroupsPerGrid, threadsPerThreadgroup);
        }

        public void DispatchThreadgroupsWithIndirectBuffer(
            MTLBuffer indirectBuffer,
            UIntPtr indirectBufferOffset,
            MTLSize threadsPerThreadgroup)
        {
            objc_msgSend(NativePtr, sel_dispatch_threadgroups1,
                indirectBuffer.NativePtr,
                indirectBufferOffset,
                threadsPerThreadgroup);
        }

        public void EndEncoding()
        {
            objc_msgSend(NativePtr, sel_end_encoding);
        }

        public void SetTexture(MTLTexture texture, UIntPtr index)
        {
            objc_msgSend(NativePtr, sel_set_texture, texture.NativePtr, index);
        }

        public void SetSamplerState(MTLSamplerState sampler, UIntPtr index)
        {
            objc_msgSend(NativePtr, sel_set_sampler_state, sampler.NativePtr, index);
        }

        public void PushDebugGroup(NSString @string)
        {
            objc_msgSend(NativePtr, Selectors.PUSH_DEBUG_GROUP, @string.NativePtr);
        }

        public void PopDebugGroup()
        {
            objc_msgSend(NativePtr, Selectors.POP_DEBUG_GROUP);
        }

        public void InsertDebugSignpost(NSString @string)
        {
            objc_msgSend(NativePtr, Selectors.INSERT_DEBUG_SIGNPOST, @string.NativePtr);
        }
    }
}

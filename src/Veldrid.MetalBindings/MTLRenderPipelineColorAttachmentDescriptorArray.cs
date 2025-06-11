// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.InteropServices;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MTLRenderPipelineColorAttachmentDescriptorArray
    {
        public readonly IntPtr NativePtr;

        public MTLRenderPipelineColorAttachmentDescriptor this[uint index]
        {
            get
            {
                IntPtr ptr = IntPtr_objc_msgSend(NativePtr, Selectors.OBJECT_AT_INDEXED_SUBSCRIPT, index);
                return new MTLRenderPipelineColorAttachmentDescriptor(ptr);
            }
            set => objc_msgSend(NativePtr, Selectors.SET_OBJECT_AT_INDEXED_SUBSCRIPT, value.NativePtr, index);
        }
    }
}

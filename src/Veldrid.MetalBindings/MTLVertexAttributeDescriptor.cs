// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    public struct MTLVertexAttributeDescriptor(IntPtr ptr)
    {
        public readonly IntPtr NativePtr = ptr;

        public MTLVertexFormat Format
        {
            get => (MTLVertexFormat)uint_objc_msgSend(NativePtr, sel_format);
            set => objc_msgSend(NativePtr, sel_set_format, (uint)value);
        }

        public UIntPtr Offset
        {
            get => UIntPtr_objc_msgSend(NativePtr, sel_offset);
            set => objc_msgSend(NativePtr, sel_set_offset, value);
        }

        public UIntPtr BufferIndex
        {
            get => UIntPtr_objc_msgSend(NativePtr, sel_buffer_index);
            set => objc_msgSend(NativePtr, sel_set_buffer_index, value);
        }

        private static readonly Selector sel_format = "format";
        private static readonly Selector sel_set_format = "setFormat:";
        private static readonly Selector sel_offset = "offset";
        private static readonly Selector sel_set_offset = "setOffset:";
        private static readonly Selector sel_buffer_index = "bufferIndex";
        private static readonly Selector sel_set_buffer_index = "setBufferIndex:";
    }
}

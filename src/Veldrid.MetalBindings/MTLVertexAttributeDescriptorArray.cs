// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    public struct MTLVertexAttributeDescriptorArray
    {
        public readonly IntPtr NativePtr;

        public MTLVertexAttributeDescriptor this[uint index]
        {
            get
            {
                IntPtr value = IntPtr_objc_msgSend(NativePtr, Selectors.OBJECT_AT_INDEXED_SUBSCRIPT, index);
                return new MTLVertexAttributeDescriptor(value);
            }
            set => objc_msgSend(NativePtr, Selectors.SET_OBJECT_AT_INDEXED_SUBSCRIPT, value.NativePtr, index);
        }
    }
}

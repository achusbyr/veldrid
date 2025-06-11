// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.InteropServices;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MTLLibrary
    {
        public readonly IntPtr NativePtr;

        public MTLLibrary(IntPtr ptr)
        {
            NativePtr = ptr;
        }

        public MTLFunction NewFunctionWithName(string name)
        {
            NSString nameNss = NSString.New(name);
            IntPtr function = IntPtr_objc_msgSend(NativePtr, sel_newFunctionWithName, nameNss);
            Release(nameNss.NativePtr);
            return new MTLFunction(function);
        }

        public MTLFunction NewFunctionWithNameConstantValues(string name, MTLFunctionConstantValues constantValues)
        {
            NSString nameNss = NSString.New(name);
            IntPtr function = IntPtr_objc_msgSend(
                NativePtr,
                sel_newFunctionWithNameConstantValues,
                nameNss.NativePtr,
                constantValues.NativePtr,
                out NSError error);
            Release(nameNss.NativePtr);

            if (function == IntPtr.Zero)
            {
                throw new Exception($"Failed to create MTLFunction: {error.LocalizedDescription}");
            }

            return new MTLFunction(function);
        }

        private static readonly Selector sel_newFunctionWithName = "newFunctionWithName:";
        private static readonly Selector sel_newFunctionWithNameConstantValues = "newFunctionWithName:constantValues:error:";
    }
}

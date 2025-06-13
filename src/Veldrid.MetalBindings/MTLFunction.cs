// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    public struct MTLFunction(IntPtr ptr)
    {
        public readonly IntPtr NativePtr = ptr;

        public NSDictionary FunctionConstantsDictionary => objc_msgSend<NSDictionary>(NativePtr, sel_function_constants_dictionary);

        private static readonly Selector sel_function_constants_dictionary = "functionConstantsDictionary";
    }
}

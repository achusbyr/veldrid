// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    public struct NSError
    {
        public readonly IntPtr NativePtr;
        public string Domain => string_objc_msgSend(NativePtr, sel_domain);
        public string LocalizedDescription => string_objc_msgSend(NativePtr, sel_localized_description);

        private static readonly Selector sel_domain = "domain";
        private static readonly Selector sel_localized_description = "localizedDescription";
    }
}

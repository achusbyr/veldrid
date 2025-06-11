// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid.MetalBindings
{
    internal static class Selectors
    {
        internal static readonly Selector TEXTURE = "texture";
        internal static readonly Selector SET_TEXTURE = "setTexture:";
        internal static readonly Selector LOAD_ACTION = "loadAction";
        internal static readonly Selector SET_LOAD_ACTION = "setLoadAction:";
        internal static readonly Selector STORE_ACTION = "storeAction";
        internal static readonly Selector SET_STORE_ACTION = "setStoreAction:";
        internal static readonly Selector RESOLVE_TEXTURE = "resolveTexture";
        internal static readonly Selector SET_RESOLVE_TEXTURE = "setResolveTexture:";
        internal static readonly Selector SLICE = "slice";
        internal static readonly Selector SET_SLICE = "setSlice:";
        internal static readonly Selector LEVEL = "level";
        internal static readonly Selector SET_LEVEL = "setLevel:";
        internal static readonly Selector OBJECT_AT_INDEXED_SUBSCRIPT = "objectAtIndexedSubscript:";
        internal static readonly Selector SET_OBJECT_AT_INDEXED_SUBSCRIPT = "setObject:atIndexedSubscript:";
        internal static readonly Selector PIXEL_FORMAT = "pixelFormat";
        internal static readonly Selector SET_PIXEL_FORMAT = "setPixelFormat:";
        internal static readonly Selector ALLOC = "alloc";
        internal static readonly Selector INIT = "init";
        internal static readonly Selector PUSH_DEBUG_GROUP = "pushDebugGroup:";
        internal static readonly Selector POP_DEBUG_GROUP = "popDebugGroup";
        internal static readonly Selector INSERT_DEBUG_SIGNPOST = "insertDebugSignpost:";
    }
}

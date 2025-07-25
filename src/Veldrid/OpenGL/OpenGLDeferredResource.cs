﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid.OpenGL
{
    internal interface IOpenGLDeferredResource
    {
        bool Created { get; }
        void EnsureResourcesCreated();
        void DestroyGLResources();
    }
}

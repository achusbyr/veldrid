// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid.OpenGL
{
    internal class OpenGLResourceSet : ResourceSet
    {
        public new OpenGLResourceLayout Layout { get; }
        public new IBindableResource[] Resources { get; }

        public override bool IsDisposed => disposed;
        public override string Name { get; set; }
        private bool disposed;

        public OpenGLResourceSet(ref ResourceSetDescription description)
            : base(ref description)
        {
            Layout = Util.AssertSubtype<ResourceLayout, OpenGLResourceLayout>(description.Layout);
            Resources = Util.ShallowClone(description.BoundResources);
        }

        #region Disposal

        public override void Dispose()
        {
            disposed = true;
        }

        #endregion
    }
}

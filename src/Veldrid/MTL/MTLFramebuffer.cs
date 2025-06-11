// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Veldrid.MetalBindings;

namespace Veldrid.MTL
{
    internal class MtlFramebuffer : Framebuffer
    {
        public override bool IsDisposed => disposed;

        public override string Name { get; set; }
        private bool disposed;

        public MtlFramebuffer(MtlGraphicsDevice gd, ref FramebufferDescription description)
            : base(description.DepthTarget, description.ColorTargets)
        {
        }

        public MtlFramebuffer()
        {
        }

        #region Disposal

        public override void Dispose()
        {
            disposed = true;
        }

        #endregion

        public MTLRenderPassDescriptor CreateRenderPassDescriptor()
        {
            var ret = MTLRenderPassDescriptor.New();

            for (int i = 0; i < ColorTargets.Count; i++)
            {
                var colorTarget = ColorTargets[i];
                var mtlTarget = Util.AssertSubtype<Texture, MtlTexture>(colorTarget.Target);
                var colorDescriptor = ret.ColorAttachments[(uint)i];
                colorDescriptor.Texture = mtlTarget.DeviceTexture;
                colorDescriptor.LoadAction = MTLLoadAction.Load;
                colorDescriptor.Slice = colorTarget.ArrayLayer;
                colorDescriptor.Level = colorTarget.MipLevel;
            }

            if (DepthTarget != null)
            {
                var mtlDepthTarget = Util.AssertSubtype<Texture, MtlTexture>(DepthTarget.Value.Target);
                var depthDescriptor = ret.DepthAttachment;
                depthDescriptor.LoadAction = mtlDepthTarget.MtlStorageMode == MTLStorageMode.Memoryless ? MTLLoadAction.DontCare : MTLLoadAction.Load;
                depthDescriptor.StoreAction = mtlDepthTarget.MtlStorageMode == MTLStorageMode.Memoryless ? MTLStoreAction.DontCare : MTLStoreAction.Store;
                depthDescriptor.Texture = mtlDepthTarget.DeviceTexture;
                depthDescriptor.Slice = DepthTarget.Value.ArrayLayer;
                depthDescriptor.Level = DepthTarget.Value.MipLevel;

                if (FormatHelpers.IsStencilFormat(mtlDepthTarget.Format))
                {
                    var stencilDescriptor = ret.StencilAttachment;
                    stencilDescriptor.LoadAction = mtlDepthTarget.MtlStorageMode == MTLStorageMode.Memoryless ? MTLLoadAction.DontCare : MTLLoadAction.Load;
                    stencilDescriptor.StoreAction = mtlDepthTarget.MtlStorageMode == MTLStorageMode.Memoryless ? MTLStoreAction.DontCare : MTLStoreAction.Store;
                    stencilDescriptor.Texture = mtlDepthTarget.DeviceTexture;
                    stencilDescriptor.Slice = DepthTarget.Value.ArrayLayer;
                }
            }

            return ret;
        }
    }
}

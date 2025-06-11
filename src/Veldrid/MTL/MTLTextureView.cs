// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Veldrid.MetalBindings;

namespace Veldrid.MTL
{
    internal class MtlTextureView : TextureView
    {
        public MTLTexture TargetDeviceTexture { get; }

        public override bool IsDisposed => disposed;

        public override string Name { get; set; }
        private readonly bool hasTextureView;
        private bool disposed;

        public MtlTextureView(ref TextureViewDescription description, MtlGraphicsDevice gd)
            : base(ref description)
        {
            var targetMtlTexture = Util.AssertSubtype<Texture, MtlTexture>(description.Target);

            if (BaseMipLevel != 0 || MipLevels != Target.MipLevels
                                  || BaseArrayLayer != 0 || ArrayLayers != Target.ArrayLayers
                                  || Format != Target.Format)
            {
                hasTextureView = true;
                uint effectiveArrayLayers = Target.Usage.HasFlag(TextureUsage.Cubemap) ? ArrayLayers * 6 : ArrayLayers;
                TargetDeviceTexture = targetMtlTexture.DeviceTexture.NewTextureView(
                    MtlFormats.VdToMtlPixelFormat(Format, (description.Target.Usage & TextureUsage.DepthStencil) != 0),
                    targetMtlTexture.MtlTextureType,
                    new NSRange(BaseMipLevel, MipLevels),
                    new NSRange(BaseArrayLayer, effectiveArrayLayers));
            }
            else
                TargetDeviceTexture = targetMtlTexture.DeviceTexture;
        }

        #region Disposal

        public override void Dispose()
        {
            if (hasTextureView && !disposed)
            {
                disposed = true;
                ObjectiveCRuntime.Release(TargetDeviceTexture.NativePtr);
            }
        }

        #endregion
    }
}

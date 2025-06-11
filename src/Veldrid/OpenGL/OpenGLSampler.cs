// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Veldrid.OpenGLBindings;
using static Veldrid.OpenGLBindings.OpenGLNative;
using static Veldrid.OpenGL.OpenGLUtil;

namespace Veldrid.OpenGL
{
    internal unsafe class OpenGLSampler : Sampler, IOpenGLDeferredResource
    {
        public override bool IsDisposed => disposeRequested;

        public uint NoMipmapSampler => noMipmapState.Sampler;
        public uint MipmapSampler => mipmapState.Sampler;

        public override string Name
        {
            get => name;
            set
            {
                name = value;
                nameChanged = true;
            }
        }

        public bool Created { get; private set; }
        private readonly OpenGLGraphicsDevice gd;
        private readonly SamplerDescription description;
        private readonly InternalSamplerState noMipmapState;
        private readonly InternalSamplerState mipmapState;
        private bool disposeRequested;

        private string name;
        private bool nameChanged;

        public OpenGLSampler(OpenGLGraphicsDevice gd, ref SamplerDescription description)
        {
            this.gd = gd;
            this.description = description;

            mipmapState = new InternalSamplerState();
            noMipmapState = new InternalSamplerState();
        }

        #region Disposal

        public override void Dispose()
        {
            if (!disposeRequested)
            {
                disposeRequested = true;
                gd.EnqueueDisposal(this);
            }
        }

        #endregion

        public void EnsureResourcesCreated()
        {
            if (!Created) createGLResources();

            if (nameChanged)
            {
                nameChanged = false;

                if (gd.Extensions.KhrDebug)
                {
                    SetObjectLabel(ObjectLabelIdentifier.Sampler, noMipmapState.Sampler, $"{name}_WithoutMipmapping");
                    SetObjectLabel(ObjectLabelIdentifier.Sampler, mipmapState.Sampler, $"{name}_WithMipmapping");
                }
            }
        }

        public void DestroyGLResources()
        {
            mipmapState.DestroyGLResources();
            noMipmapState.DestroyGLResources();
        }

        private void createGLResources()
        {
            var backendType = gd.BackendType;
            noMipmapState.CreateGLResources(description, false, backendType);
            mipmapState.CreateGLResources(description, true, backendType);
            Created = true;
        }

        private class InternalSamplerState
        {
            public uint Sampler => sampler;
            private uint sampler;

            public void CreateGLResources(SamplerDescription description, bool mipmapped, GraphicsBackend backend)
            {
                GLGenSamplers(1, out sampler);
                CheckLastError();

                GLSamplerParameteri(sampler, SamplerParameterName.TextureWrapS, (int)OpenGLFormats.VdToGLTextureWrapMode(description.AddressModeU));
                CheckLastError();
                GLSamplerParameteri(sampler, SamplerParameterName.TextureWrapT, (int)OpenGLFormats.VdToGLTextureWrapMode(description.AddressModeV));
                CheckLastError();
                GLSamplerParameteri(sampler, SamplerParameterName.TextureWrapR, (int)OpenGLFormats.VdToGLTextureWrapMode(description.AddressModeW));
                CheckLastError();

                if (description.AddressModeU == SamplerAddressMode.Border
                    || description.AddressModeV == SamplerAddressMode.Border
                    || description.AddressModeW == SamplerAddressMode.Border)
                {
                    var borderColor = toRgbaFloat(description.BorderColor);
                    GLSamplerParameterfv(sampler, SamplerParameterName.TextureBorderColor, (float*)&borderColor);
                    CheckLastError();
                }

                GLSamplerParameterf(sampler, SamplerParameterName.TextureMinLod, description.MinimumLod);
                CheckLastError();
                GLSamplerParameterf(sampler, SamplerParameterName.TextureMaxLod, description.MaximumLod);
                CheckLastError();

                if (backend == GraphicsBackend.OpenGL && description.LodBias != 0)
                {
                    GLSamplerParameterf(sampler, SamplerParameterName.TextureLodBias, description.LodBias);
                    CheckLastError();
                }

                if (description.Filter == SamplerFilter.Anisotropic)
                {
                    GLSamplerParameterf(sampler, SamplerParameterName.TextureMaxAnisotropyExt, description.MaximumAnisotropy);
                    CheckLastError();
                    GLSamplerParameteri(sampler, SamplerParameterName.TextureMinFilter, mipmapped ? (int)TextureMinFilter.LinearMipmapLinear : (int)TextureMinFilter.Linear);
                    CheckLastError();
                    GLSamplerParameteri(sampler, SamplerParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                    CheckLastError();
                }
                else
                {
                    OpenGLFormats.VdToGLTextureMinMagFilter(description.Filter, mipmapped, out var min, out var mag);
                    GLSamplerParameteri(sampler, SamplerParameterName.TextureMinFilter, (int)min);
                    CheckLastError();
                    GLSamplerParameteri(sampler, SamplerParameterName.TextureMagFilter, (int)mag);
                    CheckLastError();
                }

                if (description.ComparisonKind != null)
                {
                    GLSamplerParameteri(sampler, SamplerParameterName.TextureCompareMode, (int)TextureCompareMode.CompareRefToTexture);
                    CheckLastError();
                    GLSamplerParameteri(sampler, SamplerParameterName.TextureCompareFunc, (int)OpenGLFormats.VdToGLDepthFunction(description.ComparisonKind.Value));
                    CheckLastError();
                }
            }

            public void DestroyGLResources()
            {
                GLDeleteSamplers(1, ref sampler);
                CheckLastError();
            }

            private RgbaFloat toRgbaFloat(SamplerBorderColor borderColor)
            {
                switch (borderColor)
                {
                    case SamplerBorderColor.TransparentBlack:
                        return new RgbaFloat(0, 0, 0, 0);

                    case SamplerBorderColor.OpaqueBlack:
                        return new RgbaFloat(0, 0, 0, 1);

                    case SamplerBorderColor.OpaqueWhite:
                        return new RgbaFloat(1, 1, 1, 1);

                    default:
                        throw Illegal.Value<SamplerBorderColor>();
                }
            }
        }
    }
}

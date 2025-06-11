﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using Veldrid.OpenGLBindings;
using static Veldrid.OpenGLBindings.OpenGLNative;
using static Veldrid.OpenGL.OpenGLUtil;

namespace Veldrid.OpenGL
{
    /// <summary>
    ///     A utility class managing the relationships between textures, samplers, and their binding locations.
    /// </summary>
    internal unsafe class OpenGLTextureSamplerManager
    {
        private readonly bool dsaAvailable;
        private readonly uint lastTextureUnit;
        private readonly OpenGLTextureView[] textureUnitTextures;
        private readonly BoundSamplerStateInfo[] textureUnitSamplers;
        private uint currentActiveUnit;

        public OpenGLTextureSamplerManager(OpenGLExtensions extensions)
        {
            dsaAvailable = extensions.ArbDirectStateAccess;

            int maxTexUnits;
            GLGetIntegerv(GetPName.MaxCombinedTextureImageUnits, &maxTexUnits);
            CheckLastError();
            maxTexUnits = Math.Max(maxTexUnits, 8); // OpenGL spec indicates that implementations must support at least 8.

            textureUnitTextures = new OpenGLTextureView[maxTexUnits];
            textureUnitSamplers = new BoundSamplerStateInfo[maxTexUnits];
            lastTextureUnit = (uint)(maxTexUnits - 1);
        }

        public void SetTexture(uint textureUnit, OpenGLTextureView textureView)
        {
            uint textureID = textureView.GLTargetTexture;

            if (textureUnitTextures[textureUnit] != textureView)
            {
                if (dsaAvailable)
                {
                    GLBindTextureUnit(textureUnit, textureID);
                    CheckLastError();
                }
                else
                {
                    setActiveTextureUnit(textureUnit);
                    GLBindTexture(textureView.TextureTarget, textureID);
                    CheckLastError();
                }

                ensureSamplerMipmapState(textureUnit, textureView.MipLevels > 1);
                textureUnitTextures[textureUnit] = textureView;
            }
        }

        public void SetTextureTransient(TextureTarget target, uint texture)
        {
            textureUnitTextures[lastTextureUnit] = null;
            setActiveTextureUnit(lastTextureUnit);
            GLBindTexture(target, texture);
            CheckLastError();
        }

        public void SetSampler(uint textureUnit, OpenGLSampler sampler)
        {
            if (textureUnitSamplers[textureUnit].Sampler != sampler)
            {
                bool mipmapped = false;
                var texBinding = textureUnitTextures[textureUnit];
                if (texBinding != null) mipmapped = texBinding.MipLevels > 1;

                uint samplerID = mipmapped ? sampler.MipmapSampler : sampler.NoMipmapSampler;
                GLBindSampler(textureUnit, samplerID);
                CheckLastError();

                textureUnitSamplers[textureUnit] = new BoundSamplerStateInfo(sampler, mipmapped);
            }
            else if (textureUnitTextures[textureUnit] != null) ensureSamplerMipmapState(textureUnit, textureUnitTextures[textureUnit].MipLevels > 1);
        }

        private void setActiveTextureUnit(uint textureUnit)
        {
            if (currentActiveUnit != textureUnit)
            {
                GLActiveTexture(TextureUnit.Texture0 + (int)textureUnit);
                CheckLastError();
                currentActiveUnit = textureUnit;
            }
        }

        private void ensureSamplerMipmapState(uint textureUnit, bool mipmapped)
        {
            if (textureUnitSamplers[textureUnit].Sampler != null && textureUnitSamplers[textureUnit].Mipmapped != mipmapped)
            {
                var sampler = textureUnitSamplers[textureUnit].Sampler;
                uint samplerID = mipmapped ? sampler.MipmapSampler : sampler.NoMipmapSampler;
                GLBindSampler(textureUnit, samplerID);
                CheckLastError();

                textureUnitSamplers[textureUnit].Mipmapped = mipmapped;
            }
        }

        private struct BoundSamplerStateInfo
        {
            public readonly OpenGLSampler Sampler;
            public bool Mipmapped;

            public BoundSamplerStateInfo(OpenGLSampler sampler, bool mipmapped)
            {
                Sampler = sampler;
                Mipmapped = mipmapped;
            }
        }
    }
}

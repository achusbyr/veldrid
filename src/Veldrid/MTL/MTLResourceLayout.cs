// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Veldrid.MTL
{
    internal class MtlResourceLayout : ResourceLayout
    {
        private readonly ResourceBindingInfo[] bindingInfosByVdIndex;
        private bool disposed;
        public uint BufferCount { get; }
        public uint TextureCount { get; }
        public uint SamplerCount { get; }
#if !VALIDATE_USAGE
        public ResourceKind[] ResourceKinds { get; }
#endif
        public ResourceBindingInfo GetBindingInfo(int index)
        {
            return bindingInfosByVdIndex[index];
        }

#if !VALIDATE_USAGE
        public ResourceLayoutDescription Description { get; }
#endif

        public MtlResourceLayout(ref ResourceLayoutDescription description, MtlGraphicsDevice gd)
            : base(ref description)
        {
#if !VALIDATE_USAGE
            Description = description;
#endif

            var elements = description.Elements;
#if !VALIDATE_USAGE
            ResourceKinds = new ResourceKind[elements.Length];
            for (int i = 0; i < elements.Length; i++)
            {
                ResourceKinds[i] = elements[i].Kind;
            }
#endif

            bindingInfosByVdIndex = new ResourceBindingInfo[elements.Length];

            uint bufferIndex = 0;
            uint texIndex = 0;
            uint samplerIndex = 0;

            for (int i = 0; i < bindingInfosByVdIndex.Length; i++)
            {
                uint slot;

                switch (elements[i].Kind)
                {
                    case ResourceKind.UniformBuffer:
                        slot = bufferIndex++;
                        break;

                    case ResourceKind.StructuredBufferReadOnly:
                        slot = bufferIndex++;
                        break;

                    case ResourceKind.StructuredBufferReadWrite:
                        slot = bufferIndex++;
                        break;

                    case ResourceKind.TextureReadOnly:
                        slot = texIndex++;
                        break;

                    case ResourceKind.TextureReadWrite:
                        slot = texIndex++;
                        break;

                    case ResourceKind.Sampler:
                        slot = samplerIndex++;
                        break;

                    default: throw Illegal.Value<ResourceKind>();
                }

                bindingInfosByVdIndex[i] = new ResourceBindingInfo(
                    slot,
                    elements[i].Stages,
                    elements[i].Kind,
                    (elements[i].Options & ResourceLayoutElementOptions.DynamicBinding) != 0);
            }

            BufferCount = bufferIndex;
            TextureCount = texIndex;
            SamplerCount = samplerIndex;
        }

        public override string Name { get; set; }

        public override bool IsDisposed => disposed;

        public override void Dispose()
        {
            disposed = true;
        }

        internal struct ResourceBindingInfo
        {
            public uint Slot;
            public ShaderStages Stages;
            public ResourceKind Kind;
            public bool DynamicBuffer;

            public ResourceBindingInfo(uint slot, ShaderStages stages, ResourceKind kind, bool dynamicBuffer)
            {
                Slot = slot;
                Stages = stages;
                Kind = kind;
                DynamicBuffer = dynamicBuffer;
            }
        }
    }
}

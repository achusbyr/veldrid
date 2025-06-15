// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Veldrid.MetalBindings;

namespace Veldrid.MTL
{
    internal class MtlPipeline : Pipeline
    {
        public MTLRenderPipelineState RenderPipelineState { get; }
        public MTLComputePipelineState ComputePipelineState { get; }
        public MTLPrimitiveType PrimitiveType { get; }
        public new MtlResourceLayout[] ResourceLayouts { get; }
        public ResourceBindingModel ResourceBindingModel { get; }
        public uint VertexBufferCount { get; }
        public uint NonVertexBufferCount { get; }
        public MTLCullMode CullMode { get; }
        public MTLWinding FrontFace { get; }
        public MTLTriangleFillMode FillMode { get; }
        public MTLDepthStencilState DepthStencilState { get; }
        public MTLDepthClipMode DepthClipMode { get; }
        public override bool IsComputePipeline { get; }
        public bool ScissorTestEnabled { get; }
        public MTLSize ThreadsPerThreadgroup { get; } = new MTLSize(1, 1, 1);
        public bool HasStencil { get; }
        public uint StencilReference { get; }
        public RgbaFloat BlendColor { get; }
        public override bool IsDisposed => disposed;
        public override string Name { get; set; }

        private bool disposed;
        private List<MTLFunction> specializedFunctions;

        public MtlPipeline(ref GraphicsPipelineDescription description, MtlGraphicsDevice gd)
            : base(ref description)
        {
            PrimitiveType = MtlFormats.VdToMtlPrimitiveTopology(description.PrimitiveTopology);
            ResourceLayouts = new MtlResourceLayout[description.ResourceLayouts.Length];
            NonVertexBufferCount = 0;

            for (int i = 0; i < ResourceLayouts.Length; i++)
            {
                ResourceLayouts[i] = Util.AssertSubtype<ResourceLayout, MtlResourceLayout>(description.ResourceLayouts[i]);
                NonVertexBufferCount += ResourceLayouts[i].BufferCount;
            }

            ResourceBindingModel = description.ResourceBindingModel ?? gd.ResourceBindingModel;

            CullMode = MtlFormats.VdToMtlCullMode(description.RasterizerState.CullMode);
            FrontFace = MtlFormats.VdVoMtlFrontFace(description.RasterizerState.FrontFace);
            FillMode = MtlFormats.VdToMtlFillMode(description.RasterizerState.FillMode);
            ScissorTestEnabled = description.RasterizerState.ScissorTestEnabled;

            var mtlDesc = MTLRenderPipelineDescriptor.New();

            foreach (var shader in description.ShaderSet.Shaders)
            {
                var mtlShader = Util.AssertSubtype<Shader, MtlShader>(shader);
                MTLFunction specializedFunction;

                if (mtlShader.HasFunctionConstants)
                {
                    // Need to create specialized MTLFunction.
                    var constantValues = createConstantValues(description.ShaderSet.Specializations);
                    specializedFunction = mtlShader.Library.NewFunctionWithNameConstantValues(mtlShader.EntryPoint, constantValues);
                    addSpecializedFunction(specializedFunction);
                    ObjectiveCRuntime.Release(constantValues.NativePtr);

                    Debug.Assert(specializedFunction.NativePtr != IntPtr.Zero, "Failed to create specialized MTLFunction");
                }
                else
                    specializedFunction = mtlShader.Function;

                if (shader.Stage == ShaderStages.Vertex)
                    mtlDesc.VertexFunction = specializedFunction;
                else if (shader.Stage == ShaderStages.Fragment) mtlDesc.FragmentFunction = specializedFunction;
            }

            // Vertex layouts
            VertexLayoutDescription[] vdVertexLayouts = description.ShaderSet.VertexLayouts;
            var vertexDescriptor = mtlDesc.VertexDescriptor;

            for (uint i = 0; i < vdVertexLayouts.Length; i++)
            {
                uint layoutIndex = ResourceBindingModel == ResourceBindingModel.Improved
                    ? NonVertexBufferCount + i
                    : i;
                var mtlLayout = vertexDescriptor.Layouts[layoutIndex];
                mtlLayout.Stride = vdVertexLayouts[i].Stride;
                uint stepRate = vdVertexLayouts[i].InstanceStepRate;
                mtlLayout.StepFunction = stepRate == 0 ? MTLVertexStepFunction.PerVertex : MTLVertexStepFunction.PerInstance;
                mtlLayout.StepRate = Math.Max(1, stepRate);
            }

            uint element = 0;

            for (uint i = 0; i < vdVertexLayouts.Length; i++)
            {
                uint offset = 0;
                var vdDesc = vdVertexLayouts[i];

                for (uint j = 0; j < vdDesc.Elements.Length; j++)
                {
                    var elementDesc = vdDesc.Elements[j];
                    var mtlAttribute = vertexDescriptor.Attributes[element];
                    mtlAttribute.BufferIndex = ResourceBindingModel == ResourceBindingModel.Improved
                        ? NonVertexBufferCount + i
                        : i;
                    mtlAttribute.Format = MtlFormats.VdToMtlVertexFormat(elementDesc.Format);
                    mtlAttribute.Offset = elementDesc.Offset != 0 ? elementDesc.Offset : (UIntPtr)offset;
                    offset += FormatSizeHelpers.GetSizeInBytes(elementDesc.Format);
                    element += 1;
                }
            }

            VertexBufferCount = (uint)vdVertexLayouts.Length;

            // Outputs
            var outputs = description.Outputs;
            var blendStateDesc = description.BlendState;
            BlendColor = blendStateDesc.BlendFactor;

            if (outputs.SampleCount != TextureSampleCount.Count1) mtlDesc.SampleCount = FormatHelpers.GetSampleCountUInt32(outputs.SampleCount);

            if (outputs.DepthAttachment != null)
            {
                var depthFormat = outputs.DepthAttachment.Value.Format;
                var mtlDepthFormat = MtlFormats.VdToMtlPixelFormat(depthFormat, true);
                mtlDesc.DepthAttachmentPixelFormat = mtlDepthFormat;

                if (FormatHelpers.IsStencilFormat(depthFormat))
                {
                    HasStencil = true;
                    mtlDesc.StencilAttachmentPixelFormat = mtlDepthFormat;
                }
            }

            for (uint i = 0; i < outputs.ColorAttachments.Length; i++)
            {
                var attachmentBlendDesc = blendStateDesc.AttachmentStates[i];
                var colorDesc = mtlDesc.ColorAttachments[i];
                colorDesc.PixelFormat = MtlFormats.VdToMtlPixelFormat(outputs.ColorAttachments[i].Format, false);
                colorDesc.BlendingEnabled = attachmentBlendDesc.BlendEnabled;
                colorDesc.WriteMask = MtlFormats.VdToMtlColorWriteMask(attachmentBlendDesc.ColorWriteMask.GetOrDefault());
                colorDesc.AlphaBlendOperation = MtlFormats.VdToMtlBlendOp(attachmentBlendDesc.AlphaFunction);
                colorDesc.SourceAlphaBlendFactor = MtlFormats.VdToMtlBlendFactor(attachmentBlendDesc.SourceAlphaFactor);
                colorDesc.DestinationAlphaBlendFactor = MtlFormats.VdToMtlBlendFactor(attachmentBlendDesc.DestinationAlphaFactor);

                colorDesc.RGBBlendOperation = MtlFormats.VdToMtlBlendOp(attachmentBlendDesc.ColorFunction);
                colorDesc.SourceRGBBlendFactor = MtlFormats.VdToMtlBlendFactor(attachmentBlendDesc.SourceColorFactor);
                colorDesc.DestinationRGBBlendFactor = MtlFormats.VdToMtlBlendFactor(attachmentBlendDesc.DestinationColorFactor);
            }

            mtlDesc.AlphaToCoverageEnabled = blendStateDesc.AlphaToCoverageEnabled;

            RenderPipelineState = gd.Device.NewRenderPipelineStateWithDescriptor(mtlDesc);
            ObjectiveCRuntime.Release(mtlDesc.NativePtr);

            if (description.Outputs.DepthAttachment != null)
            {
                var depthDescriptor = MTLUtil.AllocInit<MTLDepthStencilDescriptor>(
                    nameof(MTLDepthStencilDescriptor));
                depthDescriptor.DepthCompareFunction = MtlFormats.VdToMtlCompareFunction(
                    description.DepthStencilState.DepthComparison);
                depthDescriptor.DepthWriteEnabled = description.DepthStencilState.DepthWriteEnabled;

                bool stencilEnabled = description.DepthStencilState.StencilTestEnabled;

                if (stencilEnabled)
                {
                    StencilReference = description.DepthStencilState.StencilReference;

                    var vdFrontDesc = description.DepthStencilState.StencilFront;
                    var front = MTLUtil.AllocInit<MTLStencilDescriptor>(nameof(MTLStencilDescriptor));
                    front.ReadMask = description.DepthStencilState.StencilReadMask;
                    front.WriteMask = description.DepthStencilState.StencilWriteMask;
                    front.DepthFailureOperation = MtlFormats.VdToMtlStencilOperation(vdFrontDesc.DepthFail);
                    front.StencilFailureOperation = MtlFormats.VdToMtlStencilOperation(vdFrontDesc.Fail);
                    front.DepthStencilPassOperation = MtlFormats.VdToMtlStencilOperation(vdFrontDesc.Pass);
                    front.StencilCompareFunction = MtlFormats.VdToMtlCompareFunction(vdFrontDesc.Comparison);
                    depthDescriptor.FrontFaceStencil = front;

                    var vdBackDesc = description.DepthStencilState.StencilBack;
                    var back = MTLUtil.AllocInit<MTLStencilDescriptor>(nameof(MTLStencilDescriptor));
                    back.ReadMask = description.DepthStencilState.StencilReadMask;
                    back.WriteMask = description.DepthStencilState.StencilWriteMask;
                    back.DepthFailureOperation = MtlFormats.VdToMtlStencilOperation(vdBackDesc.DepthFail);
                    back.StencilFailureOperation = MtlFormats.VdToMtlStencilOperation(vdBackDesc.Fail);
                    back.DepthStencilPassOperation = MtlFormats.VdToMtlStencilOperation(vdBackDesc.Pass);
                    back.StencilCompareFunction = MtlFormats.VdToMtlCompareFunction(vdBackDesc.Comparison);
                    depthDescriptor.BackFaceStencil = back;

                    ObjectiveCRuntime.Release(front.NativePtr);
                    ObjectiveCRuntime.Release(back.NativePtr);
                }

                DepthStencilState = gd.Device.NewDepthStencilStateWithDescriptor(depthDescriptor);
                ObjectiveCRuntime.Release(depthDescriptor.NativePtr);
            }

            DepthClipMode = description.DepthStencilState.DepthTestEnabled ? MTLDepthClipMode.Clip : MTLDepthClipMode.Clamp;
        }

        public MtlPipeline(ref ComputePipelineDescription description, MtlGraphicsDevice gd)
            : base(ref description)
        {
            IsComputePipeline = true;
            ResourceLayouts = new MtlResourceLayout[description.ResourceLayouts.Length];

            for (int i = 0; i < ResourceLayouts.Length; i++) ResourceLayouts[i] = Util.AssertSubtype<ResourceLayout, MtlResourceLayout>(description.ResourceLayouts[i]);

            ThreadsPerThreadgroup = new MTLSize(
                description.ThreadGroupSizeX,
                description.ThreadGroupSizeY,
                description.ThreadGroupSizeZ);

            var mtlDesc = MTLUtil.AllocInit<MTLComputePipelineDescriptor>(
                nameof(MTLComputePipelineDescriptor));
            var mtlShader = Util.AssertSubtype<Shader, MtlShader>(description.ComputeShader);
            MTLFunction specializedFunction;

            if (mtlShader.HasFunctionConstants)
            {
                // Need to create specialized MTLFunction.
                var constantValues = createConstantValues(description.Specializations);
                specializedFunction = mtlShader.Library.NewFunctionWithNameConstantValues(mtlShader.EntryPoint, constantValues);
                addSpecializedFunction(specializedFunction);
                ObjectiveCRuntime.Release(constantValues.NativePtr);

                Debug.Assert(specializedFunction.NativePtr != IntPtr.Zero, "Failed to create specialized MTLFunction");
            }
            else
                specializedFunction = mtlShader.Function;

            mtlDesc.ComputeFunction = specializedFunction;
            var buffers = mtlDesc.Buffers;
            uint bufferIndex = 0;

            foreach (var layout in ResourceLayouts)
            {
                foreach (var rle in layout.Description.Elements)
                {
                    var kind = rle.Kind;

                    if (kind == ResourceKind.UniformBuffer
                        || kind == ResourceKind.StructuredBufferReadOnly)
                    {
                        var bufferDesc = buffers[bufferIndex];
                        bufferDesc.Mutability = MTLMutability.Immutable;
                        bufferIndex += 1;
                    }
                    else if (kind == ResourceKind.StructuredBufferReadWrite)
                    {
                        var bufferDesc = buffers[bufferIndex];
                        bufferDesc.Mutability = MTLMutability.Mutable;
                        bufferIndex += 1;
                    }
                }
            }

            ComputePipelineState = gd.Device.NewComputePipelineStateWithDescriptor(mtlDesc);
            ObjectiveCRuntime.Release(mtlDesc.NativePtr);
        }

        #region Disposal

        public override void Dispose()
        {
            if (!disposed)
            {
                if (RenderPipelineState.NativePtr != IntPtr.Zero)
                    ObjectiveCRuntime.Release(RenderPipelineState.NativePtr);

                if (DepthStencilState.NativePtr != IntPtr.Zero)
                    ObjectiveCRuntime.Release(DepthStencilState.NativePtr);

                if (ComputePipelineState.NativePtr != IntPtr.Zero)
                    ObjectiveCRuntime.Release(ComputePipelineState.NativePtr);

                if (specializedFunctions != null)
                {
                    foreach (var function in specializedFunctions) ObjectiveCRuntime.Release(function.NativePtr);

                    specializedFunctions.Clear();
                }

                disposed = true;
            }
        }

        #endregion

        private unsafe MTLFunctionConstantValues createConstantValues(SpecializationConstant[] specializations)
        {
            var ret = MTLFunctionConstantValues.New();

            if (specializations != null)
            {
                foreach (var sc in specializations)
                {
                    var mtlType = MtlFormats.VdVoMtlShaderConstantType(sc.Type);
                    ret.SetConstantValuetypeatIndex(&sc.Data, mtlType, sc.ID);
                }
            }

            return ret;
        }

        private void addSpecializedFunction(MTLFunction function)
        {
            specializedFunctions ??= [];
            specializedFunctions.Add(function);
        }
    }
}

﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Text;
using Vortice.D3DCompiler;
using Vortice.Direct3D;
using Vortice.Direct3D11;

namespace Veldrid.D3D11
{
    internal class D3D11Shader : Shader
    {
        public ID3D11DeviceChild DeviceShader { get; }

        public override bool IsDisposed => DeviceShader.NativePointer == IntPtr.Zero;
        public byte[] Bytecode { get; internal set; }

        public override string Name
        {
            get => name;
            set
            {
                name = value;
                DeviceShader.DebugName = value;
            }
        }

        private readonly ID3D11Device device;
        private string name;

        public D3D11Shader(ID3D11Device device, ShaderDescription description)
            : base(description.Stage, description.EntryPoint)
        {
            this.device = device;

            if (description.ShaderBytes.Length > 4
                && description.ShaderBytes[0] == 0x44
                && description.ShaderBytes[1] == 0x58
                && description.ShaderBytes[2] == 0x42
                && description.ShaderBytes[3] == 0x43)
                Bytecode = Util.ShallowClone(description.ShaderBytes);
            else
                Bytecode = compileCode(description);

            switch (description.Stage)
            {
                case ShaderStages.Vertex:
                    DeviceShader = device.CreateVertexShader(Bytecode);
                    break;

                case ShaderStages.Geometry:
                    DeviceShader = device.CreateGeometryShader(Bytecode);
                    break;

                case ShaderStages.TessellationControl:
                    DeviceShader = device.CreateHullShader(Bytecode);
                    break;

                case ShaderStages.TessellationEvaluation:
                    DeviceShader = device.CreateDomainShader(Bytecode);
                    break;

                case ShaderStages.Fragment:
                    DeviceShader = device.CreatePixelShader(Bytecode);
                    break;

                case ShaderStages.Compute:
                    DeviceShader = device.CreateComputeShader(Bytecode);
                    break;

                default:
                    throw Illegal.Value<ShaderStages>();
            }
        }

        #region Disposal

        public override void Dispose()
        {
            DeviceShader.Dispose();
        }

        #endregion

        private byte[] compileCode(ShaderDescription description)
        {
            string profile;

            switch (description.Stage)
            {
                case ShaderStages.Vertex:
                    profile = device.FeatureLevel >= FeatureLevel.Level_11_0 ? "vs_5_0" : "vs_4_0";
                    break;

                case ShaderStages.Geometry:
                    profile = device.FeatureLevel >= FeatureLevel.Level_11_0 ? "gs_5_0" : "gs_4_0";
                    break;

                case ShaderStages.TessellationControl:
                    profile = device.FeatureLevel >= FeatureLevel.Level_11_0 ? "hs_5_0" : "hs_4_0";
                    break;

                case ShaderStages.TessellationEvaluation:
                    profile = device.FeatureLevel >= FeatureLevel.Level_11_0 ? "ds_5_0" : "ds_4_0";
                    break;

                case ShaderStages.Fragment:
                    profile = device.FeatureLevel >= FeatureLevel.Level_11_0 ? "ps_5_0" : "ps_4_0";
                    break;

                case ShaderStages.Compute:
                    profile = device.FeatureLevel >= FeatureLevel.Level_11_0 ? "cs_5_0" : "cs_4_0";
                    break;

                default:
                    throw Illegal.Value<ShaderStages>();
            }

            var flags = description.Debug ? ShaderFlags.Debug : ShaderFlags.OptimizationLevel3;
            Compiler.Compile(description.ShaderBytes, null!, null!,
                description.EntryPoint, null!,
                profile, flags, out var result, out var error);

            if (result == null) throw new VeldridException($"Failed to compile HLSL code: {Encoding.ASCII.GetString(error.AsBytes())}");

            return result.AsBytes();
        }
    }
}

// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using static Veldrid.MetalBindings.ObjectiveCRuntime;

namespace Veldrid.MetalBindings
{
    public unsafe struct MTLDevice(IntPtr nativePtr)
    {
        private const string metal_framework = "/System/Library/Frameworks/Metal.framework/Metal";

        public readonly IntPtr NativePtr = nativePtr;

        public static implicit operator IntPtr(MTLDevice device)
        {
            return device.NativePtr;
        }

        public string Name => string_objc_msgSend(NativePtr, sel_name);

        public MTLSize MaxThreadsPerThreadgroup
        {
            get
            {
                if (UseStret<MTLSize>())
                {
                    return objc_msgSend_stret<MTLSize>(this, sel_max_threads_per_threadgroup);
                }

                return MTLSize_objc_msgSend(this, sel_max_threads_per_threadgroup);
            }
        }

        public MTLLibrary NewLibraryWithSource(string source, MTLCompileOptions options)
        {
            NSString sourceNss = NSString.New(source);

            IntPtr library = IntPtr_objc_msgSend(NativePtr, sel_new_library_with_source,
                sourceNss,
                options,
                out NSError error);

            Release(sourceNss.NativePtr);

            if (library == IntPtr.Zero)
            {
                throw new Exception("Shader compilation failed: " + error.LocalizedDescription);
            }

            return new MTLLibrary(library);
        }

        public MTLLibrary NewLibraryWithData(DispatchData data)
        {
            IntPtr library = IntPtr_objc_msgSend(NativePtr, sel_new_library_with_data, data.NativePtr, out NSError error);

            if (library == IntPtr.Zero)
            {
                throw new Exception("Unable to load Metal library: " + error.LocalizedDescription);
            }

            return new MTLLibrary(library);
        }

        public MTLRenderPipelineState NewRenderPipelineStateWithDescriptor(MTLRenderPipelineDescriptor desc)
        {
            IntPtr ret = IntPtr_objc_msgSend(NativePtr, sel_new_render_pipeline_state_with_descriptor,
                desc.NativePtr,
                out NSError error);

            if (error.NativePtr != IntPtr.Zero)
            {
                throw new Exception("Failed to create new MTLRenderPipelineState: " + error.LocalizedDescription);
            }

            return new MTLRenderPipelineState(ret);
        }

        [Pure]
        public MTLComputePipelineState NewComputePipelineStateWithDescriptor(
            MTLComputePipelineDescriptor descriptor)
        {
            IntPtr ret = IntPtr_objc_msgSend(NativePtr, sel_new_compute_pipeline_state_with_descriptor,
                descriptor,
                0,
                IntPtr.Zero,
                out NSError error);

            if (error.NativePtr != IntPtr.Zero)
            {
                throw new Exception("Failed to create new MTLRenderPipelineState: " + error.LocalizedDescription);
            }

            return new MTLComputePipelineState(ret);
        }

        public MTLCommandQueue NewCommandQueue()
        {
            return objc_msgSend<MTLCommandQueue>(NativePtr, sel_new_command_queue);
        }

        public MTLBuffer NewBuffer(void* pointer, UIntPtr length, MTLResourceOptions options)
        {
            IntPtr buffer = IntPtr_objc_msgSend(NativePtr, sel_new_buffer_with_bytes,
                pointer,
                length,
                options);
            return new MTLBuffer(buffer);
        }

        public MTLBuffer NewBufferWithLengthOptions(UIntPtr length, MTLResourceOptions options)
        {
            IntPtr buffer = IntPtr_objc_msgSend(NativePtr, sel_new_buffer_with_length, length, options);
            return new MTLBuffer(buffer);
        }

        public MTLTexture NewTextureWithDescriptor(MTLTextureDescriptor descriptor)
        {
            return objc_msgSend<MTLTexture>(NativePtr, sel_new_texture_with_descriptor, descriptor.NativePtr);
        }

        public MTLSamplerState NewSamplerStateWithDescriptor(MTLSamplerDescriptor descriptor)
        {
            return objc_msgSend<MTLSamplerState>(NativePtr, sel_new_sampler_state_with_descriptor, descriptor.NativePtr);
        }

        public MTLDepthStencilState NewDepthStencilStateWithDescriptor(MTLDepthStencilDescriptor descriptor)
        {
            return objc_msgSend<MTLDepthStencilState>(NativePtr, sel_new_depth_stencil_state_with_descriptor, descriptor.NativePtr);
        }

        public Bool8 SupportsTextureSampleCount(UIntPtr sampleCount)
        {
            return bool8_objc_msgSend(NativePtr, sel_supports_texture_sample_count, sampleCount);
        }

        public Bool8 SupportsFeatureSet(MTLFeatureSet featureSet)
        {
            return bool8_objc_msgSend(NativePtr, sel_supports_feature_set, (uint)featureSet);
        }

        public Bool8 IsDepth24Stencil8PixelFormatSupported
            => bool8_objc_msgSend(NativePtr, sel_is_depth24_stencil8_pixel_format_supported);

        [DllImport(metal_framework)]
        public static extern MTLDevice MTLCreateSystemDefaultDevice();

        [DllImport(metal_framework)]
        public static extern NSArray MTLCopyAllDevices();

        private static readonly Selector sel_name = "name";
        private static readonly Selector sel_max_threads_per_threadgroup = "maxThreadsPerThreadgroup";
        private static readonly Selector sel_new_library_with_source = "newLibraryWithSource:options:error:";
        private static readonly Selector sel_new_library_with_data = "newLibraryWithData:error:";
        private static readonly Selector sel_new_render_pipeline_state_with_descriptor = "newRenderPipelineStateWithDescriptor:error:";
        private static readonly Selector sel_new_compute_pipeline_state_with_descriptor = "newComputePipelineStateWithDescriptor:options:reflection:error:";
        private static readonly Selector sel_new_command_queue = "newCommandQueue";
        private static readonly Selector sel_new_buffer_with_bytes = "newBufferWithBytes:length:options:";
        private static readonly Selector sel_new_buffer_with_length = "newBufferWithLength:options:";
        private static readonly Selector sel_new_texture_with_descriptor = "newTextureWithDescriptor:";
        private static readonly Selector sel_new_sampler_state_with_descriptor = "newSamplerStateWithDescriptor:";
        private static readonly Selector sel_new_depth_stencil_state_with_descriptor = "newDepthStencilStateWithDescriptor:";
        private static readonly Selector sel_supports_texture_sample_count = "supportsTextureSampleCount:";
        private static readonly Selector sel_supports_feature_set = "supportsFeatureSet:";
        private static readonly Selector sel_is_depth24_stencil8_pixel_format_supported = "isDepth24Stencil8PixelFormatSupported";
    }
}

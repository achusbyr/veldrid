﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace Veldrid
{
    /// <summary>
    ///     A device resource encapsulating all state in a graphics pipeline. Used in
    ///     <see cref="CommandList.SetPipeline(Pipeline)" /> to prepare a <see cref="CommandList" /> for draw commands.
    ///     See <see cref="GraphicsPipelineDescription" />.
    /// </summary>
    public abstract class Pipeline : IDeviceResource, IDisposable
    {
        /// <summary>
        ///     Gets a value indicating whether this instance represents a compute Pipeline.
        ///     If false, this instance is a graphics pipeline.
        /// </summary>
        public abstract bool IsComputePipeline { get; }

        /// <summary>
        ///     A bool indicating whether this instance has been disposed.
        /// </summary>
        public abstract bool IsDisposed { get; }

        /// <summary>
        ///     A string identifying this instance. Can be used to differentiate between objects in graphics debuggers and other
        ///     tools.
        /// </summary>
        public abstract string Name { get; set; }

        internal Pipeline(ref GraphicsPipelineDescription graphicsDescription)
            : this(graphicsDescription.ResourceLayouts)
        {
#if VALIDATE_USAGE
            GraphicsOutputDescription = graphicsDescription.Outputs;
#endif
        }

        internal Pipeline(ref ComputePipelineDescription computeDescription)
            : this(computeDescription.ResourceLayouts)
        {
        }

        internal Pipeline(ResourceLayout[] resourceLayouts)
        {
#if VALIDATE_USAGE
            ResourceLayouts = Util.ShallowClone(resourceLayouts);
#endif
        }

        #region Disposal

        /// <summary>
        ///     Frees unmanaged device resources controlled by this instance.
        /// </summary>
        public abstract void Dispose();

        #endregion

#if VALIDATE_USAGE
        internal OutputDescription GraphicsOutputDescription { get; }
        internal ResourceLayout[] ResourceLayouts { get; }
#endif
    }
}

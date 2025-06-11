﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace Veldrid
{
    // A GPU-CPU sync point
    /// <summary>
    ///     A synchronization primitive which allows the GPU to communicate when submitted work items have finished executing.
    /// </summary>
    public abstract class Fence : IDeviceResource, IDisposable
    {
        /// <summary>
        ///     Gets a value indicating whether the Fence is currently signaled. A Fence is signaled after a CommandList finishes
        ///     execution after it was submitted with a Fence instance.
        /// </summary>
        public abstract bool Signaled { get; }

        /// <summary>
        ///     A bool indicating whether this instance has been disposed.
        /// </summary>
        public abstract bool IsDisposed { get; }

        /// <summary>
        ///     A string identifying this instance. Can be used to differentiate between objects in graphics debuggers and other
        ///     tools.
        /// </summary>
        public abstract string Name { get; set; }

        #region Disposal

        /// <summary>
        ///     Frees unmanaged device resources controlled by this instance.
        /// </summary>
        public abstract void Dispose();

        #endregion

        /// <summary>
        ///     Sets this instance to the unsignaled state.
        /// </summary>
        public abstract void Reset();
    }
}

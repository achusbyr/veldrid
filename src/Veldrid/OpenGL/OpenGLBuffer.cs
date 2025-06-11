// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Diagnostics;
using Veldrid.OpenGLBindings;
using static Veldrid.OpenGLBindings.OpenGLNative;
using static Veldrid.OpenGL.OpenGLUtil;

namespace Veldrid.OpenGL
{
    internal unsafe class OpenGLBuffer : DeviceBuffer, IOpenGLDeferredResource
    {
        public override uint SizeInBytes { get; }
        public override BufferUsage Usage { get; }

        public uint Buffer => buffer;

        public override bool IsDisposed => disposeRequested;

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
        private uint buffer;
        private readonly bool dynamic;
        private bool disposeRequested;

        private string name;
        private bool nameChanged;

        public OpenGLBuffer(OpenGLGraphicsDevice gd, uint sizeInBytes, BufferUsage usage)
        {
            this.gd = gd;
            SizeInBytes = sizeInBytes;
            dynamic = (usage & BufferUsage.Dynamic) == BufferUsage.Dynamic;
            Usage = usage;
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
            if (!Created) CreateGLResources();

            if (nameChanged)
            {
                nameChanged = false;
                if (gd.Extensions.KhrDebug) SetObjectLabel(ObjectLabelIdentifier.Buffer, buffer, name);
            }
        }

        public void CreateGLResources()
        {
            Debug.Assert(!Created);

            if (gd.Extensions.ArbDirectStateAccess)
            {
                uint b;
                GLCreateBuffers(1, &b);
                CheckLastError();

                buffer = b;

                GLNamedBufferData(
                    buffer,
                    SizeInBytes,
                    null,
                    dynamic ? BufferUsageHint.DynamicDraw : BufferUsageHint.StaticDraw);
                CheckLastError();
            }
            else
            {
                GLGenBuffers(1, out buffer);
                CheckLastError();

                GLBindBuffer(BufferTarget.CopyReadBuffer, buffer);
                CheckLastError();

                GLBufferData(
                    BufferTarget.CopyReadBuffer,
                    SizeInBytes,
                    null,
                    dynamic ? BufferUsageHint.DynamicDraw : BufferUsageHint.StaticDraw);
                CheckLastError();
            }

            Created = true;
        }

        public void DestroyGLResources()
        {
            uint b = buffer;
            GLDeleteBuffers(1, ref b);
            CheckLastError();
        }
    }
}

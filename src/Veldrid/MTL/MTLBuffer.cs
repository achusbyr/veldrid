// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Veldrid.MetalBindings;

namespace Veldrid.MTL
{
    internal class MtlBuffer : DeviceBuffer
    {
        public override uint SizeInBytes { get; }
        public override BufferUsage Usage { get; }

        public uint ActualCapacity { get; }

        public override bool IsDisposed => disposed;

        public override string Name
        {
            get => name;
            set
            {
                var nameNss = NSString.New(value);
                DeviceBuffer.AddDebugMarker(nameNss, new NSRange(0, SizeInBytes));
                ObjectiveCRuntime.Release(nameNss.NativePtr);
                name = value;
            }
        }

        public MTLBuffer DeviceBuffer { get; }

        public unsafe void* Pointer { get; private set; }
        private string name;
        private bool disposed;

        public MtlBuffer(ref BufferDescription bd, MtlGraphicsDevice gd)
        {
            SizeInBytes = bd.SizeInBytes;
            uint roundFactor = (4 - SizeInBytes % 4) % 4;
            ActualCapacity = SizeInBytes + roundFactor;
            Usage = bd.Usage;

            bool sharedMemory = Usage == BufferUsage.Staging || (Usage & BufferUsage.Dynamic) == BufferUsage.Dynamic;
            var bufferOptions = sharedMemory ? MTLResourceOptions.StorageModeShared : MTLResourceOptions.StorageModePrivate;

            DeviceBuffer = gd.Device.NewBufferWithLengthOptions(
                ActualCapacity,
                bufferOptions);

            unsafe
            {
                if (sharedMemory)
                    Pointer = DeviceBuffer.Contents();
            }
        }

        #region Disposal

        public override void Dispose()
        {
            if (!disposed)
            {
                disposed = true;
                ObjectiveCRuntime.Release(DeviceBuffer.NativePtr);
            }
        }

        #endregion
    }
}

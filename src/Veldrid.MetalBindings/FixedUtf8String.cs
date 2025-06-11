// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Veldrid.MetalBindings
{
    internal unsafe class FixedUtf8String : IDisposable
    {
        private GCHandle _handle;
        private uint _numBytes;

        public byte* StringPtr => (byte*)_handle.AddrOfPinnedObject().ToPointer();

        public FixedUtf8String(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            byte[] text = Encoding.UTF8.GetBytes(s);
            _handle = GCHandle.Alloc(text, GCHandleType.Pinned);
            _numBytes = (uint)text.Length;
        }

        public void SetText(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            _handle.Free();
            byte[] text = Encoding.UTF8.GetBytes(s);
            _handle = GCHandle.Alloc(text, GCHandleType.Pinned);
            _numBytes = (uint)text.Length;
        }

        private string getString()
        {
            return Encoding.UTF8.GetString(StringPtr, (int)_numBytes);
        }

        public void Dispose()
        {
            _handle.Free();
        }

        public static implicit operator byte*(FixedUtf8String utf8String)
        {
            return utf8String.StringPtr;
        }

        public static implicit operator IntPtr(FixedUtf8String utf8String)
        {
            return new IntPtr(utf8String.StringPtr);
        }

        public static implicit operator FixedUtf8String(string s)
        {
            return new FixedUtf8String(s);
        }

        public static implicit operator string(FixedUtf8String utf8String)
        {
            return utf8String.getString();
        }
    }
}

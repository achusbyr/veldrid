﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Veldrid.Vk
{
    internal unsafe class FixedUtf8String : IDisposable
    {
        public byte* StringPtr => (byte*)handle.AddrOfPinnedObject().ToPointer();
        private GCHandle handle;
        private readonly uint numBytes;

        public FixedUtf8String(string s)
        {
            if (s == null) throw new ArgumentNullException(nameof(s));

            int byteCount = Encoding.UTF8.GetByteCount(s);
            byte[] text = new byte[byteCount + 1];
            handle = GCHandle.Alloc(text, GCHandleType.Pinned);
            numBytes = (uint)text.Length - 1; // Includes null terminator
            int encodedCount = Encoding.UTF8.GetBytes(s, 0, s.Length, text, 0);
            Debug.Assert(encodedCount == byteCount);
        }

        #region Disposal

        public void Dispose()
        {
            handle.Free();
        }

        #endregion

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

        public override string ToString()
        {
            return getString();
        }

        private string getString()
        {
            return Encoding.UTF8.GetString(StringPtr, (int)numBytes);
        }
    }
}

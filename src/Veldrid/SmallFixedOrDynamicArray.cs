﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Veldrid
{
    internal unsafe struct SmallFixedOrDynamicArray : IDisposable
    {
        private const int max_fixed_values = 5;

        public readonly uint Count;
        private fixed uint fixedData[max_fixed_values];
        public readonly uint[] Data;

        public uint Get(uint i)
        {
            return Count > max_fixed_values ? Data[i] : fixedData[i];
        }

        public SmallFixedOrDynamicArray(uint count, ref uint data)
        {
            if (count > max_fixed_values)
                Data = ArrayPool<uint>.Shared.Rent((int)count);
            else
            {
                for (int i = 0; i < count; i++) fixedData[i] = Unsafe.Add(ref data, i);

                Data = null;
            }

            Count = count;
        }

        public void Dispose()
        {
            if (Data != null) ArrayPool<uint>.Shared.Return(Data);
        }
    }
}

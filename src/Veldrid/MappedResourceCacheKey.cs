﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace Veldrid
{
    internal struct MappedResourceCacheKey : IEquatable<MappedResourceCacheKey>
    {
        public readonly IMappableResource Resource;
        public readonly uint Subresource;

        public MappedResourceCacheKey(IMappableResource resource, uint subresource)
        {
            Resource = resource;
            Subresource = subresource;
        }

        public bool Equals(MappedResourceCacheKey other)
        {
            return Resource.Equals(other.Resource)
                   && Subresource.Equals(other.Subresource);
        }

        public override int GetHashCode()
        {
            return HashHelper.Combine(Resource.GetHashCode(), Subresource.GetHashCode());
        }
    }
}

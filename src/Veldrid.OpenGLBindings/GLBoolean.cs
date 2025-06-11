// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace Veldrid.OpenGLBindings
{
    /// <summary>
    /// A boolean value stored in an unsigned byte.
    /// </summary>
    public struct GLBoolean : IEquatable<GLBoolean>
    {
        /// <summary>
        /// The raw value of the <see cref="GLBoolean"/>. A value of 0 represents "false", all other values represent "true".
        /// </summary>
        public byte Value;

        /// <summary>
        /// Constructs a new <see cref="GLBoolean"/> with the given raw value.
        /// </summary>
        /// <param name="value"></param>
        public GLBoolean(byte value)
        {
            Value = value;
        }

        /// <summary>
        /// Represents the boolean "true" value. Has a raw value of 1.
        /// </summary>
        public static readonly GLBoolean TRUE = new GLBoolean(1);

        /// <summary>
        /// Represents the boolean "true" value. Has a raw value of 0.
        /// </summary>
        public static readonly GLBoolean FALSE = new GLBoolean(0);

        /// <summary>
        /// Returns whether another <see cref="GLBoolean"/> value is considered equal to this one.
        /// Two <see cref="GLBoolean"/>s are considered equal when their raw values are equal.
        /// </summary>
        /// <param name="other">The value to compare to.</param>
        /// <returns>True if the other value's underlying raw value is equal to this instance's. False otherwise.</returns>
        public bool Equals(GLBoolean other)
        {
            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is GLBoolean b && Equals(b);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{(this ? "True" : "False")} ({Value})";
        }

        public static implicit operator bool(GLBoolean b)
        {
            return b.Value != 0;
        }

        public static implicit operator uint(GLBoolean b)
        {
            return b.Value;
        }

        public static implicit operator GLBoolean(bool b)
        {
            return b ? TRUE : FALSE;
        }

        public static implicit operator GLBoolean(byte value)
        {
            return new GLBoolean(value);
        }

        public static bool operator ==(GLBoolean left, GLBoolean right)
        {
            return left.Value == right.Value;
        }

        public static bool operator !=(GLBoolean left, GLBoolean right)
        {
            return left.Value != right.Value;
        }
    }
}

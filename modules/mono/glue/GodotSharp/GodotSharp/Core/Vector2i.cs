using System;
using System.Runtime.InteropServices;

namespace Godot
{
    /// <summary>
    /// 2-element structure that can be used to represent 2D grid coordinates or pairs of integers.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2i : IEquatable<Vector2i>
    {
        /// <summary>
        /// Enumerated index values for the axes.
        /// Returned by <see cref="MaxAxisIndex"/> and <see cref="MinAxisIndex"/>.
        /// </summary>
        public enum Axis
        {
            /// <summary>
            /// The vector's X axis.
            /// </summary>
            X = 0,
            /// <summary>
            /// The vector's Y axis.
            /// </summary>
            Y
        }

        /// <summary>
        /// The vector's X component. Also accessible by using the index position <c>[0]</c>.
        /// </summary>
        public int x;

        /// <summary>
        /// The vector's Y component. Also accessible by using the index position <c>[1]</c>.
        /// </summary>
        public int y;

        /// <summary>
        /// Access vector components using their index.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is not 0 or 1.
        /// </exception>
        /// <value>
        /// <c>[0]</c> is equivalent to <see cref="x"/>,
        /// <c>[1]</c> is equivalent to <see cref="y"/>.
        /// </value>
        public int this[int index]
        {
            readonly get
            {
                switch (index)
                {
                    case 0:
                        return x;
                    case 1:
                        return y;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        x = value;
                        return;
                    case 1:
                        y = value;
                        return;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
        }

        /// <summary>
        /// Helper method for deconstruction into a tuple.
        /// </summary>
        public readonly void Deconstruct(out int x, out int y)
        {
            x = this.x;
            y = this.y;
        }

        /// <summary>
        /// Returns a new vector with all components in absolute values (i.e. positive).
        /// </summary>
        /// <returns>A vector with <see cref="Mathf.Abs(int)"/> called on each component.</returns>
        public readonly Vector2i Abs()
        {
            return new Vector2i(Mathf.Abs(x), Mathf.Abs(y));
        }

        /// <summary>
        /// Returns the aspect ratio of this vector, the ratio of <see cref="x"/> to <see cref="y"/>.
        /// </summary>
        /// <returns>The <see cref="x"/> component divided by the <see cref="y"/> component.</returns>
        public readonly real_t Aspect()
        {
            return x / (real_t)y;
        }

        /// <summary>
        /// Returns a new vector with all components clamped between the
        /// components of <paramref name="min"/> and <paramref name="max"/> using
        /// <see cref="Mathf.Clamp(int, int, int)"/>.
        /// </summary>
        /// <param name="min">The vector with minimum allowed values.</param>
        /// <param name="max">The vector with maximum allowed values.</param>
        /// <returns>The vector with all components clamped.</returns>
        public readonly Vector2i Clamp(Vector2i min, Vector2i max)
        {
            return new Vector2i
            (
                Mathf.Clamp(x, min.x, max.x),
                Mathf.Clamp(y, min.y, max.y)
            );
        }

        /// <summary>
        /// Returns the length (magnitude) of this vector.
        /// </summary>
        /// <seealso cref="LengthSquared"/>
        /// <returns>The length of this vector.</returns>
        public readonly real_t Length()
        {
            int x2 = x * x;
            int y2 = y * y;

            return Mathf.Sqrt(x2 + y2);
        }

        /// <summary>
        /// Returns the squared length (squared magnitude) of this vector.
        /// This method runs faster than <see cref="Length"/>, so prefer it if
        /// you need to compare vectors or need the squared length for some formula.
        /// </summary>
        /// <returns>The squared length of this vector.</returns>
        public readonly int LengthSquared()
        {
            int x2 = x * x;
            int y2 = y * y;

            return x2 + y2;
        }

        /// <summary>
        /// Returns the axis of the vector's highest value. See <see cref="Axis"/>.
        /// If both components are equal, this method returns <see cref="Axis.X"/>.
        /// </summary>
        /// <returns>The index of the highest axis.</returns>
        public readonly Axis MaxAxisIndex()
        {
            return x < y ? Axis.Y : Axis.X;
        }

        /// <summary>
        /// Returns the axis of the vector's lowest value. See <see cref="Axis"/>.
        /// If both components are equal, this method returns <see cref="Axis.Y"/>.
        /// </summary>
        /// <returns>The index of the lowest axis.</returns>
        public readonly Axis MinAxisIndex()
        {
            return x < y ? Axis.X : Axis.Y;
        }

        /// <summary>
        /// Returns a vector with each component set to one or negative one, depending
        /// on the signs of this vector's components, or zero if the component is zero,
        /// by calling <see cref="Mathf.Sign(int)"/> on each component.
        /// </summary>
        /// <returns>A vector with all components as either <c>1</c>, <c>-1</c>, or <c>0</c>.</returns>
        public readonly Vector2i Sign()
        {
            Vector2i v = this;
            v.x = Mathf.Sign(v.x);
            v.y = Mathf.Sign(v.y);
            return v;
        }

        // Constants
        private static readonly Vector2i _zero = new Vector2i(0, 0);
        private static readonly Vector2i _one = new Vector2i(1, 1);

        private static readonly Vector2i _up = new Vector2i(0, -1);
        private static readonly Vector2i _down = new Vector2i(0, 1);
        private static readonly Vector2i _right = new Vector2i(1, 0);
        private static readonly Vector2i _left = new Vector2i(-1, 0);

        /// <summary>
        /// Zero vector, a vector with all components set to <c>0</c>.
        /// </summary>
        /// <value>Equivalent to <c>new Vector2i(0, 0)</c>.</value>
        public static Vector2i Zero { get { return _zero; } }
        /// <summary>
        /// One vector, a vector with all components set to <c>1</c>.
        /// </summary>
        /// <value>Equivalent to <c>new Vector2i(1, 1)</c>.</value>
        public static Vector2i One { get { return _one; } }

        /// <summary>
        /// Up unit vector. Y is down in 2D, so this vector points -Y.
        /// </summary>
        /// <value>Equivalent to <c>new Vector2i(0, -1)</c>.</value>
        public static Vector2i Up { get { return _up; } }
        /// <summary>
        /// Down unit vector. Y is down in 2D, so this vector points +Y.
        /// </summary>
        /// <value>Equivalent to <c>new Vector2i(0, 1)</c>.</value>
        public static Vector2i Down { get { return _down; } }
        /// <summary>
        /// Right unit vector. Represents the direction of right.
        /// </summary>
        /// <value>Equivalent to <c>new Vector2i(1, 0)</c>.</value>
        public static Vector2i Right { get { return _right; } }
        /// <summary>
        /// Left unit vector. Represents the direction of left.
        /// </summary>
        /// <value>Equivalent to <c>new Vector2i(-1, 0)</c>.</value>
        public static Vector2i Left { get { return _left; } }

        /// <summary>
        /// Constructs a new <see cref="Vector2i"/> with the given components.
        /// </summary>
        /// <param name="x">The vector's X component.</param>
        /// <param name="y">The vector's Y component.</param>
        public Vector2i(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Adds each component of the <see cref="Vector2i"/>
        /// with the components of the given <see cref="Vector2i"/>.
        /// </summary>
        /// <param name="left">The left vector.</param>
        /// <param name="right">The right vector.</param>
        /// <returns>The added vector.</returns>
        public static Vector2i operator +(Vector2i left, Vector2i right)
        {
            left.x += right.x;
            left.y += right.y;
            return left;
        }

        /// <summary>
        /// Subtracts each component of the <see cref="Vector2i"/>
        /// by the components of the given <see cref="Vector2i"/>.
        /// </summary>
        /// <param name="left">The left vector.</param>
        /// <param name="right">The right vector.</param>
        /// <returns>The subtracted vector.</returns>
        public static Vector2i operator -(Vector2i left, Vector2i right)
        {
            left.x -= right.x;
            left.y -= right.y;
            return left;
        }

        /// <summary>
        /// Returns the negative value of the <see cref="Vector2i"/>.
        /// This is the same as writing <c>new Vector2i(-v.x, -v.y)</c>.
        /// This operation flips the direction of the vector while
        /// keeping the same magnitude.
        /// </summary>
        /// <param name="vec">The vector to negate/flip.</param>
        /// <returns>The negated/flipped vector.</returns>
        public static Vector2i operator -(Vector2i vec)
        {
            vec.x = -vec.x;
            vec.y = -vec.y;
            return vec;
        }

        /// <summary>
        /// Multiplies each component of the <see cref="Vector2i"/>
        /// by the given <see langword="int"/>.
        /// </summary>
        /// <param name="vec">The vector to multiply.</param>
        /// <param name="scale">The scale to multiply by.</param>
        /// <returns>The multiplied vector.</returns>
        public static Vector2i operator *(Vector2i vec, int scale)
        {
            vec.x *= scale;
            vec.y *= scale;
            return vec;
        }

        /// <summary>
        /// Multiplies each component of the <see cref="Vector2i"/>
        /// by the given <see langword="int"/>.
        /// </summary>
        /// <param name="scale">The scale to multiply by.</param>
        /// <param name="vec">The vector to multiply.</param>
        /// <returns>The multiplied vector.</returns>
        public static Vector2i operator *(int scale, Vector2i vec)
        {
            vec.x *= scale;
            vec.y *= scale;
            return vec;
        }

        /// <summary>
        /// Multiplies each component of the <see cref="Vector2i"/>
        /// by the components of the given <see cref="Vector2i"/>.
        /// </summary>
        /// <param name="left">The left vector.</param>
        /// <param name="right">The right vector.</param>
        /// <returns>The multiplied vector.</returns>
        public static Vector2i operator *(Vector2i left, Vector2i right)
        {
            left.x *= right.x;
            left.y *= right.y;
            return left;
        }

        /// <summary>
        /// Divides each component of the <see cref="Vector2i"/>
        /// by the given <see langword="int"/>.
        /// </summary>
        /// <param name="vec">The dividend vector.</param>
        /// <param name="divisor">The divisor value.</param>
        /// <returns>The divided vector.</returns>
        public static Vector2i operator /(Vector2i vec, int divisor)
        {
            vec.x /= divisor;
            vec.y /= divisor;
            return vec;
        }

        /// <summary>
        /// Divides each component of the <see cref="Vector2i"/>
        /// by the components of the given <see cref="Vector2i"/>.
        /// </summary>
        /// <param name="vec">The dividend vector.</param>
        /// <param name="divisorv">The divisor vector.</param>
        /// <returns>The divided vector.</returns>
        public static Vector2i operator /(Vector2i vec, Vector2i divisorv)
        {
            vec.x /= divisorv.x;
            vec.y /= divisorv.y;
            return vec;
        }

        /// <summary>
        /// Gets the remainder of each component of the <see cref="Vector2i"/>
        /// with the components of the given <see langword="int"/>.
        /// This operation uses truncated division, which is often not desired
        /// as it does not work well with negative numbers.
        /// Consider using <see cref="Mathf.PosMod(int, int)"/> instead
        /// if you want to handle negative numbers.
        /// </summary>
        /// <example>
        /// <code>
        /// GD.Print(new Vector2i(10, -20) % 7); // Prints "(3, -6)"
        /// </code>
        /// </example>
        /// <param name="vec">The dividend vector.</param>
        /// <param name="divisor">The divisor value.</param>
        /// <returns>The remainder vector.</returns>
        public static Vector2i operator %(Vector2i vec, int divisor)
        {
            vec.x %= divisor;
            vec.y %= divisor;
            return vec;
        }

        /// <summary>
        /// Gets the remainder of each component of the <see cref="Vector2i"/>
        /// with the components of the given <see cref="Vector2i"/>.
        /// This operation uses truncated division, which is often not desired
        /// as it does not work well with negative numbers.
        /// Consider using <see cref="Mathf.PosMod(int, int)"/> instead
        /// if you want to handle negative numbers.
        /// </summary>
        /// <example>
        /// <code>
        /// GD.Print(new Vector2i(10, -20) % new Vector2i(7, 8)); // Prints "(3, -4)"
        /// </code>
        /// </example>
        /// <param name="vec">The dividend vector.</param>
        /// <param name="divisorv">The divisor vector.</param>
        /// <returns>The remainder vector.</returns>
        public static Vector2i operator %(Vector2i vec, Vector2i divisorv)
        {
            vec.x %= divisorv.x;
            vec.y %= divisorv.y;
            return vec;
        }

        /// <summary>
        /// Returns <see langword="true"/> if the vectors are equal.
        /// </summary>
        /// <param name="left">The left vector.</param>
        /// <param name="right">The right vector.</param>
        /// <returns>Whether or not the vectors are equal.</returns>
        public static bool operator ==(Vector2i left, Vector2i right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Returns <see langword="true"/> if the vectors are not equal.
        /// </summary>
        /// <param name="left">The left vector.</param>
        /// <param name="right">The right vector.</param>
        /// <returns>Whether or not the vectors are not equal.</returns>
        public static bool operator !=(Vector2i left, Vector2i right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Compares two <see cref="Vector2i"/> vectors by first checking if
        /// the X value of the <paramref name="left"/> vector is less than
        /// the X value of the <paramref name="right"/> vector.
        /// If the X values are exactly equal, then it repeats this check
        /// with the Y values of the two vectors.
        /// This operator is useful for sorting vectors.
        /// </summary>
        /// <param name="left">The left vector.</param>
        /// <param name="right">The right vector.</param>
        /// <returns>Whether or not the left is less than the right.</returns>
        public static bool operator <(Vector2i left, Vector2i right)
        {
            if (left.x == right.x)
            {
                return left.y < right.y;
            }
            return left.x < right.x;
        }

        /// <summary>
        /// Compares two <see cref="Vector2i"/> vectors by first checking if
        /// the X value of the <paramref name="left"/> vector is greater than
        /// the X value of the <paramref name="right"/> vector.
        /// If the X values are exactly equal, then it repeats this check
        /// with the Y values of the two vectors.
        /// This operator is useful for sorting vectors.
        /// </summary>
        /// <param name="left">The left vector.</param>
        /// <param name="right">The right vector.</param>
        /// <returns>Whether or not the left is greater than the right.</returns>
        public static bool operator >(Vector2i left, Vector2i right)
        {
            if (left.x == right.x)
            {
                return left.y > right.y;
            }
            return left.x > right.x;
        }

        /// <summary>
        /// Compares two <see cref="Vector2i"/> vectors by first checking if
        /// the X value of the <paramref name="left"/> vector is less than
        /// or equal to the X value of the <paramref name="right"/> vector.
        /// If the X values are exactly equal, then it repeats this check
        /// with the Y values of the two vectors.
        /// This operator is useful for sorting vectors.
        /// </summary>
        /// <param name="left">The left vector.</param>
        /// <param name="right">The right vector.</param>
        /// <returns>Whether or not the left is less than or equal to the right.</returns>
        public static bool operator <=(Vector2i left, Vector2i right)
        {
            if (left.x == right.x)
            {
                return left.y <= right.y;
            }
            return left.x < right.x;
        }

        /// <summary>
        /// Compares two <see cref="Vector2i"/> vectors by first checking if
        /// the X value of the <paramref name="left"/> vector is greater than
        /// or equal to the X value of the <paramref name="right"/> vector.
        /// If the X values are exactly equal, then it repeats this check
        /// with the Y values of the two vectors.
        /// This operator is useful for sorting vectors.
        /// </summary>
        /// <param name="left">The left vector.</param>
        /// <param name="right">The right vector.</param>
        /// <returns>Whether or not the left is greater than or equal to the right.</returns>
        public static bool operator >=(Vector2i left, Vector2i right)
        {
            if (left.x == right.x)
            {
                return left.y >= right.y;
            }
            return left.x > right.x;
        }

        /// <summary>
        /// Converts this <see cref="Vector2i"/> to a <see cref="Vector2"/>.
        /// </summary>
        /// <param name="value">The vector to convert.</param>
        public static implicit operator Vector2(Vector2i value)
        {
            return new Vector2(value.x, value.y);
        }

        /// <summary>
        /// Converts a <see cref="Vector2"/> to a <see cref="Vector2i"/>.
        /// </summary>
        /// <param name="value">The vector to convert.</param>
        public static explicit operator Vector2i(Vector2 value)
        {
            return new Vector2i(
                Mathf.RoundToInt(value.x),
                Mathf.RoundToInt(value.y)
            );
        }

        /// <summary>
        /// Returns <see langword="true"/> if the vector is equal
        /// to the given object (<see paramref="obj"/>).
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>Whether or not the vector and the object are equal.</returns>
        public override readonly bool Equals(object obj)
        {
            return obj is Vector2i other && Equals(other);
        }

        /// <summary>
        /// Returns <see langword="true"/> if the vectors are equal.
        /// </summary>
        /// <param name="other">The other vector.</param>
        /// <returns>Whether or not the vectors are equal.</returns>
        public readonly bool Equals(Vector2i other)
        {
            return x == other.x && y == other.y;
        }

        /// <summary>
        /// Serves as the hash function for <see cref="Vector2i"/>.
        /// </summary>
        /// <returns>A hash code for this vector.</returns>
        public override readonly int GetHashCode()
        {
            return y.GetHashCode() ^ x.GetHashCode();
        }

        /// <summary>
        /// Converts this <see cref="Vector2i"/> to a string.
        /// </summary>
        /// <returns>A string representation of this vector.</returns>
        public override readonly string ToString()
        {
            return $"({x}, {y})";
        }

        /// <summary>
        /// Converts this <see cref="Vector2i"/> to a string with the given <paramref name="format"/>.
        /// </summary>
        /// <returns>A string representation of this vector.</returns>
        public readonly string ToString(string format)
        {
            return $"({x.ToString(format)}, {y.ToString(format)})";
        }
    }
}
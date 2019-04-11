using System;
using System.Runtime.InteropServices;

namespace BLITTEngine.Core.Common
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2 : IEquatable<Vector2>, IComparable<Vector2>
    {
        public static readonly Vector2 UnitX = new Vector2(1, 0);
        public static readonly Vector2 UnitY = new Vector2(0, 1);
        public static readonly Vector2 Zero = new Vector2(0, 0);
        public static readonly Vector2 One = new Vector2(1, 1);

        public bool IsEmpty => X == 0.0f && Y == 0.0f;

        public float X;

        public float Y;

        public Vector2(float value)
        {
            X = value;
            Y = value;
        }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float Length()
        {
            return Calc.Sqrt(X * X + Y * Y);
        }

        public float LengthSquared()
        {
            return X * X + Y * Y;
        }

        public float Angle()
        {
            return (float) ((Math.Atan2(Y, X) + Math.PI * 2.5) % (Math.PI * 2));
        }

        public Vector2 Normalized
        {
            get
            {
                if (IsEmpty)
                {
                    return Vector2.Zero;
                }
                
                float val = 1.0f / Length();

                return new Vector2(X * val, Y * val);
            }
        }

        public void Normalize()
        {
            if (IsEmpty)
            {
                return;
            }
            
            float val = 1.0f / (float) Math.Sqrt((X * X) + (Y * Y));
            X *= val;
            Y *= val;
        }

        public static void Add(in Vector2 a, in Vector2 b, out Vector2 result)
        {
            result = new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static void Subtract(in Vector2 a, in Vector2 b, out Vector2 result)
        {
            result = new Vector2(a.X - b.X, a.Y - b.Y);
        }

        public static void Multiply(in Vector2 vector, float scale, out Vector2 result)
        {
            result = new Vector2(vector.X * scale, vector.Y * scale);
        }

        public static void Multiply(in Vector2 vector, ref Vector2 scale, out Vector2 result)
        {
            result = new Vector2(vector.X * scale.X, vector.Y * scale.Y);
        }

        public static void Divide(in Vector2 vector, float scale, out Vector2 result)
        {
            Multiply(in vector, 1 / scale, out result);
        }

        public static void Divide(in Vector2 vector, ref Vector2 scale, out Vector2 result)
        {
            result = new Vector2(vector.X / scale.X, vector.Y / scale.Y);
        }

        public static Vector2 Min(Vector2 a, Vector2 b)
        {
            a.X = a.X < b.X ? a.X : b.X;
            a.Y = a.Y < b.Y ? a.Y : b.Y;
            return a;
        }

        public static void Min(in Vector2 a, in Vector2 b, out Vector2 result)
        {
            result.X = a.X < b.X ? a.X : b.X;
            result.Y = a.Y < b.Y ? a.Y : b.Y;
        }

        public static Vector2 Max(Vector2 a, Vector2 b)
        {
            a.X = a.X > b.X ? a.X : b.X;
            a.Y = a.Y > b.Y ? a.Y : b.Y;
            return a;
        }

        public static void Max(in Vector2 a, in Vector2 b, out Vector2 result)
        {
            result.X = a.X > b.X ? a.X : b.X;
            result.Y = a.Y > b.Y ? a.Y : b.Y;
        }

        public static float Dot(Vector2 left, Vector2 right)
        {
            return left.X * right.X + left.Y * right.Y;
        }

        public static void Dot(in Vector2 left, in Vector2 right, out float result)
        {
            result = left.X * right.X + left.Y * right.Y;
        }

        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(
                left.X + right.X,
                left.Y + right.Y);
        }

        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(
                left.X - right.X,
                left.Y - right.Y);
        }

        public static Vector2 operator -(Vector2 vec)
        {
            return new Vector2(
                -vec.X,
                -vec.Y);
        }

        public static Vector2 operator *(Vector2 vec, float scale)
        {
            return new Vector2(
                vec.X * scale,
                vec.Y * scale);
        }

        public static Vector2 operator *(float scale, Vector2 vec)
        {
            return vec * scale;
        }

        public static Vector2 operator *(Vector2 vec, Vector2 scale)
        {
            return new Vector2(
                vec.X * scale.X,
                vec.Y * scale.Y);
        }

        public static Vector2 operator /(Vector2 vec, float scale)
        {
            return vec * (1.0f / scale);
        }

        public static Vector2 operator /(Vector2 vec, Vector2 scale)
        {
            return new Vector2(
                vec.X / scale.X,
                vec.Y / scale.Y);
        }

        public static bool operator ==(Vector2 left, Vector2 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return $"({X:F}, {Y:F})";
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector2))
                return false;

            return Equals((Vector2) obj);
        }

        public bool Equals(Vector2 other)
        {
            return
                X == other.X &&
                Y == other.Y;
        }

        public int CompareTo(Vector2 other)
        {
            var xComparison = X.CompareTo(other.X);
            return xComparison != 0 ? xComparison : Y.CompareTo(other.Y);
        }
    }
}
using System;
using System.Runtime.InteropServices;

namespace BLITTEngine.Core.Numerics
{
    [StructLayout(LayoutKind.Sequential)]
    public struct IntRect : IEquatable<IntRect>
    {
        private static IntRect _empty = new IntRect(0, 0, 0, 0);
        public ref readonly IntRect Empty => ref _empty;

        public int X1;
        public int Y1;
        public int X2;
        public int Y2;

        public static IntRect FromBox(int x, int y, int w, int h)
        {
            return new IntRect(x, y, x + w, y + h);
        }

        public IntRect(int x1, int y1, int x2, int y2)
        {
            this.X1 = x1;
            this.Y1 = y1;
            this.X2 = x2;
            this.Y2 = y2;
        }

        public int Width => X2 - X1;

        public int Height => Y2 - Y1;

        public bool IsEmpty => (Width == 0 && Height == 0);

        public bool IsRegular => X2 > X1 && Y2 > Y1;

        public IntRect Normalized => new IntRect(Calc.Min(X1, X2), Calc.Min(Y1, Y2), Calc.Max(X1, X2), Calc.Max(Y1, Y2));

        public int Area => Math.Abs(Width * Height);

        public Point2 TopLeft => new Point2(X1, Y1);

        public Point2 TopRight => new Point2(X2, Y1);

        public Point2 BottomLeft => new Point2(X1, Y2);

        public Point2 BottomRight => new Point2(X2, Y2);

        public Point2 Center => new Point2(Calc.Abs(X2 - X1) / 2, Calc.Abs(Y2 - Y1) / 2);

        public override bool Equals(object obj)
        {
            return obj is IntRect i && Equals(i);
        }

        public bool Equals(IntRect other)
        {
            return Equals(ref other);
        }

        public bool Equals(ref IntRect other)
        {
            return X1 == other.X1 && Y1 == other.Y1 && X2 == other.X2 && Y2 == other.Y2;
        }

        public bool Contains(Point2 p)
        {
            return p.X >= X1 && p.X <= X2 && p.Y >= Y1 && p.Y <= Y2;
        }

        public bool Contains(ref IntRect rect)
        {
            return X1 <= rect.X1 && X2 >= rect.X2 && Y1 <= rect.Y1 && Y2 >= rect.Y2;
        }

        public bool Contains(IntRect rect)
        {
            return Contains(ref rect);
        }

        public bool Intersects(ref IntRect other)
        {
            return X1 <= other.X2 &&
                   Y1 <= other.Y2 &&
                   X2 >= other.X1 &&
                   Y2 >= other.Y1;
        }

        public bool Intersects(IntRect other)
        {
            return Intersects(ref other);
        }

        public void CopyTo(out IntRect other)
        {
            other.X1 = X1;
            other.Y1 = Y1;
            other.X2 = X2;
            other.Y2 = Y2;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + X1;
                hash = hash * 23 + Y1;
                hash = hash * 23 + X2;
                hash = hash * 23 + Y2;
                return hash;
            }
        }

        public IntRect Inflated(int x, int y)
        {
            var copy = this;

            copy.X1 -= x;
            copy.X2 += x;
            copy.Y1 -= y;
            copy.Y2 += y;

            return copy;
        }

        public IntRect Inflated(int amount)
        {
            return Inflated(amount, amount);
        }

        public override string ToString()
        {
            return $"{X1},{Y1},{X2},{Y2}";
        }

        public static bool operator == (IntRect a, IntRect b)
        {
            return a.Equals(ref b);
        }

        public static bool operator !=(IntRect a, IntRect b)
        {
            return !a.Equals(ref b);
        }
    }
}
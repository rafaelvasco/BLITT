using System;
using System.Runtime.InteropServices;

namespace BLITTEngine.Numerics
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct RectangleI : IEquatable<RectangleI>
    {
        public static readonly RectangleI Empty;

        public int X;
        public int Y;
        public int W;
        public int H;

        public RectangleI(int x, int y, int w, int h)
        {
            X = x;
            Y = y;
            W = w;
            H = h;
        }

        public RectangleI(int w, int h) : this(0, 0, w, h)
        {
        }

        public int Right => X + W;

        public int Bottom => Y + H;

        public int MinX => Math.Min(X, X + W);

        public int MinY => Math.Min(Y, Y + H);

        public int MaxX => Math.Max(X, X + W);

        public int MaxY => Math.Max(Y, Y + H);

        public int AbsW => Math.Abs(W);

        public int AbsH => Math.Abs(H);

        public int CenterX => X + W / 2;

        public int CenterY => Y + H / 2;

        public bool IsEmpty => W == 0f && H == 0f;

        public bool IsRegular => W >= 0f && H >= 0f;

        public RectangleI Normalized
        {
            get
            {
                var copy = this;
                if (copy.W < 0)
                {
                    copy.X += copy.W;
                    copy.W = -copy.W;
                }
                if (copy.H < 0)
                {
                    copy.Y += copy.H;
                    copy.H = -copy.H;
                }
                return copy;
            }
        }

        public int Area => Math.Abs(W * H);

        public Point2 TopLeft => new Point2(X, Y);

        public Point2 BottomLeft => new Point2(X, Bottom);

        public Point2 BottomRight => new Point2(Right, Bottom);

        public Point2 TopRight => new Point2(Right, Y);

        public Point2 Center => new Point2(CenterX, CenterY);

        public Point2 TopCenter => new Point2(CenterX, Y);

        public Point2 BottomCenter => new Point2(CenterX, Y + H);

        public Point2 LeftCenter => new Point2(X, CenterY);

        public Point2 RightCenter => new Point2(X + W, CenterY);

        public override bool Equals(object obj)
        {
            return obj is RectangleI i && Equals(i);
        }

        public bool Equals(RectangleI other)
        {
            return Equals(ref other);
        }

        public bool Equals(ref RectangleI other)
        {
            return X == other.X && Y == other.Y && W == other.W && H == other.H;
        }

        public bool Contains(Point2 p)
        {
            return p.X >= X && p.Y >= Y && p.X < Right && p.Y < Bottom;
        }

        public bool Contains(ref RectangleI rect)
        {
            return rect.X >= X && rect.Y >= Y && rect.Right <= Right && rect.Bottom <= Bottom;
        }

        public bool Contains(RectangleI rect)
        {
            return Contains(ref rect);
        }

        public void CopyTo(out RectangleI other)
        {
            other.X = X;
            other.Y = Y;
            other.W = W;
            other.H = H;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + X;
                hash = hash * 23 + Y;
                hash = hash * 23 + W;
                hash = hash * 23 + H;
                return hash;
            }
        }

        public RectangleI Inflated(int x, int y)
        {
            var rect = this;
            rect.X -= x;
            rect.W += x * 2;
            rect.Y -= y;
            rect.H += y * 2;
            return rect;
        }

        public RectangleI Inflated(int amount)
        {
            return Inflated(amount, amount);
        }

        public bool Intersects(ref RectangleI other)
        {
            return X < other.X + other.W && Y < other.Y + other.H && X + W > other.X && Y + H > other.Y;
        }

        public bool Intersects(RectangleI other)
        {
            return Intersects(ref other);
        }

        public override string ToString()
        {
            return $"{X},{Y},{W},{H}";
        }

        public static implicit operator Rectangle(RectangleI r)
        {
            return new Rectangle(r.X, r.Y, r.W, r.H);
        }

        public static bool operator ==(RectangleI a, RectangleI b)
        {
            return a.Equals(ref b);
        }

        public static bool operator !=(RectangleI a, RectangleI b)
        {
            return !a.Equals(ref b);
        }

        public static RectangleI operator *(RectangleI a, int b)
        {
            return new RectangleI(a.X * b, a.Y * b, a.W * b, a.H * b);
        }

        public static RectangleI operator /(RectangleI a, int b)
        {
            return new RectangleI(a.X / b, a.Y / b, a.W / b, a.H / b);
        }
    }
}
using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace BLITTEngine.Numerics
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Rectangle : IEquatable<Rectangle>
    {
        public static readonly Rectangle Empty;

        public float X;
        public float Y;
        public float W;
        public float H;

        public Rectangle(float x, float y, float w, float h)
        {
            X = x;
            Y = y;
            W = w;
            H = h;
        }

        public Rectangle(float w, float h)
        {
            X = 0f;
            Y = 0f;
            W = w;
            H = h;
        }

        public float Right => X + W;

        public float Bottom => Y + H;

        public float MinX => Math.Min(X, X + W);

        public float MinY => Math.Min(Y, Y + H);

        public float MaxX => Math.Max(X, X + W);

        public float MaxY => Math.Max(Y, Y + H);

        public float AbsW => Math.Abs(W);

        public float AbsH => Math.Abs(H);

        public float CenterX => X + W * 0.5f;

        public float CenterY => Y + H * 0.5f;

        public bool IsEmpty => W == 0f && H == 0f;

        public bool IsNormalized => W >= 0f && H >= 0f;

        public Rectangle Normalized
        {
            get
            {
                var copy = this;
                if (copy.W < 0f)
                {
                    copy.X += copy.W;
                    copy.W = -copy.W;
                }
                if (copy.H < 0f)
                {
                    copy.Y += copy.H;
                    copy.H = -copy.H;
                }
                return copy;
            }
        }

        public float Area => Math.Abs(W * H);

        public Vector2 TopLeft => new Vector2(X, Y);

        public Vector2 BottomLeft => new Vector2(X, Bottom);

        public Vector2 BottomRight => new Vector2(Right, Bottom);

        public Vector2 TopRight => new Vector2(Right, Y);

        public Vector2 Center => new Vector2(CenterX, CenterY);

        public Vector2 TopCenter => new Vector2(CenterX, Y);

        public Vector2 BottomCenter => new Vector2(CenterX, Y + H);

        public Vector2 LeftCenter => new Vector2(X, CenterY);

        public Vector2 RightCenter => new Vector2(X + W, CenterY);

        public void CopyTo(out Rectangle other)
        {
            other.X = X;
            other.Y = Y;
            other.W = W;
            other.H = H;
        }

        public override bool Equals(object obj)
        {
            return obj is Rectangle rectangle && Equals(rectangle);
        }

        public bool Equals(Rectangle other)
        {
            return Equals(ref other);
        }

        public bool Equals(ref Rectangle other)
        {
            return X == other.X && Y == other.Y && W == other.W && H == other.H;
        }

        public bool Contains(Vector2 point)
        {
            return point.X > X && point.Y > Y && point.X < Right && point.Y < Bottom;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                hash = hash * 23 + W.GetHashCode();
                hash = hash * 23 + H.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{X},{Y},{W},{H}";
        }

        public int CompareArea(ref Rectangle a, ref Rectangle b)
        {
            return Math.Sign(a.Area - b.Area);
        }

        public int CompareArea(Rectangle a, Rectangle b)
        {
            return CompareArea(ref a, ref b);
        }

        public static Rectangle Box(float centerX, float centerY, float size)
        {
            return new Rectangle(centerX - size * 0.5f, centerY - size * 0.5f, size, size);
        }

        public static Rectangle Box(Vector2 center, float size)
        {
            return new Rectangle(center.X - size * 0.5f, center.Y - size * 0.5f, size, size);
        }

        public static Rectangle Box(float size)
        {
            return new Rectangle(size * -0.5f, size * -0.5f, size, size);
        }

        public static Rectangle Box(float centerX, float centerY, float w, float h)
        {
            return new Rectangle(centerX - w * 0.5f, centerY - h * 0.5f, w, h);
        }

        public static Rectangle Box(Vector2 center, float w, float h)
        {
            return new Rectangle(center.X - w * 0.5f, center.Y - h * 0.5f, w, h);
        }

        public static Rectangle Box(float w, float h)
        {
            return new Rectangle(w * -0.5f, h * -0.5f, w, h);
        }

        public static Rectangle FromBounds(float minX, float minY, float maxX, float maxY)
        {
            return new Rectangle(minX, minY, maxX - minX, maxY - minY);
        }

        public Rectangle Inflated(float x, float y)
        {
            var rect = this;
            rect.X -= x;
            rect.W += x * 2f;
            rect.Y -= y;
            rect.H += y * 2f;
            return rect;
        }

        public Rectangle Inflated(float amount)
        {
            return Inflated(amount, amount);
        }

        public static bool operator ==(Rectangle a, Rectangle b)
        {
            return a.Equals(ref b);
        }

        public static bool operator !=(Rectangle a, Rectangle b)
        {
            return !a.Equals(ref b);
        }
    }
}
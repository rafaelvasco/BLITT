using System;
using System.Runtime.InteropServices;

namespace BLITTEngine.Core.Graphics
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Color : IEquatable<Color>
    {
        private static readonly Color _transparent = new Color(0);
        private static readonly Color _black = new Color(0xFF000000);
        private static readonly Color _white = new Color(0xFFFFFFFF);
        private static readonly Color _red = new Color(0xFF0000FF);
        private static readonly Color _green = new Color(0xFF00FF00);
        private static readonly Color _blue = new Color(0xFFFF0000);
        private static readonly Color _yellow = new Color(0xFF00FFFF);
        private static readonly Color _fuschia = new Color(0xFFFF00FF);
        private static readonly Color _cyan = new Color(0xFFFFFF00);


        public static ref readonly Color Transparent => ref _transparent;
        public static ref readonly Color Black => ref _black;
        public static ref readonly Color White => ref _white;
        public static ref readonly Color Red => ref _red;
        public static ref readonly Color Green => ref _green;
        public static ref readonly Color Blue => ref _blue;
        public static ref readonly Color Yellow => ref _yellow;
        public static ref readonly Color Fuschia => ref _fuschia;
        public static ref readonly Color Cyan => ref _cyan;


        public float R, G, B, A;

        public Color(float r, float g, float b, float a = 1.0f)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public Color(uint col)
        {
            R = (col & 0xFF) / 255.0f;
            G = ((col >> 8) & 0xFF) / 255.0f;
            B = ((col >> 16) & 0xFF) / 255.0f;
            A = 1.0f;
        }

        public static Color operator -(Color c1, Color c2)
        {
            return new Color(c1.R - c2.R, c1.G - c2.G, c1.B - c2.B, c1.A - c2.A);
        }

        public static Color operator +(Color c1, Color c2)
        {
            return new Color(c1.R + c2.R, c1.G + c2.G, c1.B + c2.B, c1.A + c2.A);
        }

        public static Color operator *(Color c1, Color c2)
        {
            return new Color(c1.R * c2.R, c1.G * c2.G, c1.B * c2.B, c1.A * c2.A);
        }

        public static Color operator *(Color c1, float sc)
        {
            return new Color(c1.R * sc, c1.G * sc, c1.B * sc, c1.A * sc);
        }

        public static bool operator ==(Color c1, Color c2)
        {
            return c1.R == c2.R &&
                   c1.G == c2.G &&
                   c1.B == c2.B &&
                   c1.A == c2.A;
        }

        public static bool operator !=(Color c1, Color c2)
        {
            return c1.R != c2.R ||
                   c1.G != c2.G ||
                   c1.B != c2.B ||
                   c1.A != c2.A;
        }

        public bool Equals(Color other)
        {
            return R.Equals(other.R) && G.Equals(other.G) && B.Equals(other.B) && A.Equals(other.A);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            return obj is Color other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = R.GetHashCode();
                hashCode = (hashCode * 397) ^ G.GetHashCode();
                hashCode = (hashCode * 397) ^ B.GetHashCode();
                hashCode = (hashCode * 397) ^ A.GetHashCode();
                return hashCode;
            }
        }

        public void SetPackedColor(uint col)
        {
            R = (col & 0xFF);
            G = ((col >> 8) & 0xFF);
            B = ((col >> 16) & 0xFF);
            A = col >= 0xFF000000 ? (col >> 24) : 255;
        }

        public uint GetPackedColor()
        {
            return ((uint) (A * 255.0f) << 24) | ((uint) (B * 255.0f) << 16) | ((uint) (G * 255.0f) << 8) |
                   ((uint) (R * 255.0f));
        }

        public int GetIntRgba()
        {
            return (int) (((uint) (R * 255) << 24) | ((uint) (G * 255) << 16) | ((uint) (B * 255) << 8) |
                          (uint) (A * 255));
        }

        public void Clamp()
        {
            if (R < 0.0f) R = 0.0f;
            else if (R > 1.0f) R = 1.0f;

            if (G < 0.0f) G = 0.0f;
            else if (G > 1.0f) G = 1.0f;

            if (B < 0.0f) B = 0.0f;
            else if (B > 1.0f) B = 1.0f;

            if (A < 0.0f) A = 0.0f;
            else if (A > 1.0f) A = 1.0f;
        }

        public Color WithAlpha(float a)
        {
            return new Color(R, G, B, a);
        }

        public static implicit operator uint(Color val)
        {
            return val.GetPackedColor();
        }

        public static implicit operator int(Color val)
        {
            return val.GetIntRgba();
        }

        public static implicit operator Color(uint val)
        {
            return new Color(
                val
            );
        }
    }
}
using BLITTEngine.Numerics;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace BLITTEngine.Core.Graphics
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Color : IEquatable<Color>
    {
        private static Color _transparent = new Color(0);
        private static Color _black = new Color(0xFF000000);
        private static Color _white = new Color(0xFFFFFFFF);
        private static Color _red = new Color(0xFF0000FF);
        private static Color _green = new Color(0xFF00FF00);
        private static Color _blue = new Color(0xFFFF0000);
        private static Color _yellow = new Color(0xFF00FFFF);
        private static Color _fuschia = new Color(0xFFFF00FF);

        public static ref readonly Color Transparent => ref _transparent;
        public static ref readonly Color Black => ref _black;
        public static ref readonly Color White => ref _white;
        public static ref readonly Color Red => ref _red;
        public static ref readonly Color Green => ref _green;
        public static ref readonly Color Blue => ref _blue;
        public static ref readonly Color Yellow => ref _yellow;
        public static ref readonly Color Fuschia => ref _fuschia;

        public int RGBA => (int)(((uint)R << 24) | ((uint)G << 16) | ((uint)B << 8) | A);

        // ABGR
        private uint packed_value;

        public Color(uint packedValue)
        {
            packed_value = packedValue;
        }

        public Color(int r, int g, int b)
        {
            packed_value = 0xFF000000; // A = 255

            if (((r | g | b) & 0xFFFFFF00) != 0)
            {
                var clampedR = (uint)Calc.Clamp(r, 0, 255);
                var clampedG = (uint)Calc.Clamp(g, 0, 255);
                var clampedB = (uint)Calc.Clamp(b, 0, 255);

                packed_value |= (clampedB << 16) | (clampedG << 8) | clampedR;
            }
            else
            {
                packed_value |= ((uint)b << 16) | ((uint)g << 8) | ((uint)r);
            }
        }

        public Color(int r, int g, int b, int alpha)
        {
            if (((r | g | b | alpha) & 0xFFFFFF00) != 0)
            {
                var clampedR = (uint)Calc.Clamp(r, 0, 255);
                var clampedG = (uint)Calc.Clamp(g, 0, 255);
                var clampedB = (uint)Calc.Clamp(b, 0, 255);
                var clampedA = (uint)Calc.Clamp(alpha, 0, 255);

                packed_value = (clampedA << 24) | (clampedB << 16) | (clampedG << 8) | clampedR;
            }
            else
            {
                packed_value = ((uint)alpha << 24) | ((uint)b << 16) | ((uint)g << 8) | ((uint)r);
            }
        }

        public Color(byte r, byte g, byte b, byte alpha)
        {
            packed_value = ((uint)alpha << 24) | ((uint)b << 16) | ((uint)g << 8) | r;
        }

        public Color(float r, float g, float b)
            : this((int)(r * 255), (int)(g * 255), (int)(b * 255))
        {
        }

        public Color(float r, float g, float b, float alpha)
            : this((int)(r * 255), (int)(g * 255), (int)(b * 255), (int)(alpha * 255))
        {
        }

        public byte R
        {
            get
            {
                unchecked
                {
                    return (byte)packed_value;
                }
            }

            set => this.packed_value = (this.packed_value & 0xffffff00) | value;
        }

        public byte G
        {
            get
            {
                unchecked
                {
                    return (byte)(packed_value >> 8);
                }
            }

            set => this.packed_value = (this.packed_value & 0xffff00ff) | ((uint) value << 8);
        }

        public byte B
        {
            get
            {
                unchecked
                {
                    return (byte)(packed_value >> 16);
                }
            }

            set => this.packed_value = (this.packed_value & 0xff00ffff) | ((uint) value << 16);
        }

        public byte A
        {
            get
            {
                unchecked
                {
                    return (byte)(packed_value >> 24);
                }
            }

            set => this.packed_value = (this.packed_value & 0x00ffffff) | ((uint) value << 24);
        }

        public static bool operator ==(Color a, Color b)
        {
            return a.packed_value == b.packed_value;
        }

        public static bool operator !=(Color a, Color b)
        {
            return a.packed_value != b.packed_value;
        }

        public bool Equals(Color other)
        {
            return packed_value == other.packed_value;
        }

        public override bool Equals(object obj)
        {
            return (obj is Color color) && this.Equals(color);
        }

        public override int GetHashCode()
        {
            return packed_value.GetHashCode();
        }

        public static Color Lerp(Color value1, Color value2, float amount)
        {
            amount = Calc.Normalize(amount);

            return new Color(
                (int)Calc.Lerp(value1.R, value2.R, amount),
                (int)Calc.Lerp(value1.G, value2.G, amount),
                (int)Calc.Lerp(value1.B, value2.B, amount),
                (int)Calc.Lerp(value1.A, value2.A, amount));
        }

        public static Color Multiply(Color value, float scale)
        {
            return new Color((int)(value.R * scale), (int)(value.G * scale), (int)(value.B * scale),
                (int)(value.A * scale));
        }

        public static Color operator *(Color value, float scale)
        {
            return new Color((int)(value.R * scale), (int)(value.G * scale), (int)(value.B * scale),
                (int)(value.A * scale));
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(25);
            sb.Append("{R:");
            sb.Append(R);
            sb.Append(" G:");
            sb.Append(G);
            sb.Append(" B:");
            sb.Append(B);
            sb.Append(" A:");
            sb.Append(A);
            sb.Append("}");
            return sb.ToString();
        }

        public void Deconstruct(out float r, out float g, out float b)
        {
            r = R;
            g = G;
            b = B;
        }

        public void Deconstruct(out float r, out float g, out float b, out float a)
        {
            r = R;
            g = G;
            b = B;
            a = A;
        }

        public static implicit operator uint(Color val)
        {
            return val.packed_value;
        }

        public static implicit operator int(Color val)
        {
            return val.RGBA;
        }

        public static implicit operator Color(uint val)
        {
            return new Color(
                val
            );
        }
    }
}
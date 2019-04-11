using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using BLITTEngine.Core.Graphics;

namespace BLITTEngine.Core.Common
{
    public readonly struct ColorHsl : IEquatable<ColorHsl>, IComparable<ColorHsl>
    {
        public readonly float H;

        public readonly float S;

        public readonly float L;
        
        public ColorHsl(float h, float s, float l) : this()
        {
            // normalize the hue
            H = NormalizeHue(h);
            S = Calc.Clamp(s, 0f, 1f);
            L = Calc.Clamp(l, 0f, 1f);
        }
        
        public void CopyTo(out ColorHsl destination)
        {
            destination = new ColorHsl(H, S, L);
        }
        
        public void Destructure(out float h, out float s, out float l)
        {
            h = H;
            s = S;
            l = L;
        }
        
        public static ColorHsl operator +(ColorHsl a, ColorHsl b)
        {
            return new ColorHsl(a.H + b.H, a.S + b.S, a.L + b.L);
        }

        public static implicit operator ColorHsl(string value)
        {
            return Parse(value);
        }
        
        public int CompareTo(ColorHsl other)
        {
            return H.CompareTo(other.H)*100 + S.CompareTo(other.S)*10 + L.CompareTo(L);
        }
        
        public override bool Equals(object obj)
        {
            if (obj is ColorHsl c)
            {
                return Equals(c);
            } 

            return base.Equals(obj);
        }
        
        public bool Equals(ColorHsl value)
        {
            return H.Equals(value.H) && S.Equals(value.S) && L.Equals(value.L);
        }
        
        public override int GetHashCode()
        {
            return H.GetHashCode() ^
                   S.GetHashCode() ^
                   L.GetHashCode();
        }
        
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "H:{0:N1}° S:{1:N1} L:{2:N1}",
                H, 100*S, 100*L);
        }
        
        public static ColorHsl Parse(string s)
        {
            var hsl = s.Split(',');
            var hue = float.Parse(hsl[0].TrimEnd('°'), CultureInfo.InvariantCulture.NumberFormat);
            var sat = float.Parse(hsl[1], CultureInfo.InvariantCulture.NumberFormat);
            var lig = float.Parse(hsl[2], CultureInfo.InvariantCulture.NumberFormat);

            return new ColorHsl(hue, sat, lig);
        }
        
        public static bool operator ==(ColorHsl x, ColorHsl y)
        {
            return x.Equals(y);
        }
        
        public static bool operator !=(ColorHsl x, ColorHsl y)
        {
            return !x.Equals(y);
        }
        
        public static ColorHsl operator -(ColorHsl a, ColorHsl b)
        {
            return new ColorHsl(a.H - b.H, a.S - b.S, a.L - b.L);
        }

        public static ColorHsl Lerp(ColorHsl c1, ColorHsl c2, float t)
        {
            // loop around if c2.H < c1.H
            var h2 = c2.H >= c1.H ? c2.H : c2.H + 360;
            return new ColorHsl(
                c1.H + t*(h2 - c1.H),
                c1.S + t*(c2.S - c1.S),
                c1.L + t*(c2.L - c2.L));
        }
        
        public static ColorHsl FromRgb(Color color)
        {
            // derived from http://www.geekymonkey.com/Programming/CSharp/RGB2HSL_HSL2RGB.htm
            var r = color.R / 255f;
            var g = color.G / 255f;
            var b = color.B / 255f;
            var h = 0f; // default to black
            var s = 0f;
            var l = 0f;
            var v = Math.Max(r, g);
            v = Math.Max(v, b);

            var m = Math.Min(r, g);
            m = Math.Min(m, b);
            l = (m + v) / 2.0f;

            if (l <= 0.0)
                return new ColorHsl(h, s, l);

            var vm = v - m;
            s = vm;

            if (s > 0.0)
                s /= l <= 0.5f ? v + m : 2.0f - v - m;
            else
                return new ColorHsl(h, s, l);

            var r2 = (v - r) / vm;
            var g2 = (v - g) / vm;
            var b2 = (v - b) / vm;

            if (Math.Abs(r - v) < float.Epsilon)
                h = Math.Abs(g - m) < float.Epsilon ? 5.0f + b2 : 1.0f - g2;
            else if (Math.Abs(g - v) < float.Epsilon)
                h = Math.Abs(b - m) < float.Epsilon ? 1.0f + r2 : 3.0f - b2;
            else
                h = Math.Abs(r - m) < float.Epsilon ? 3.0f + g2 : 5.0f - r2;

            h *= 60;
            h = NormalizeHue(h);

            return new ColorHsl(h, s, l);
        }

        public Color ToRGB()
        {
            var h = H;
            var s = S;
            var l = L;

            if (s == 0f)
                return new Color(l, l, l);

            h = h/360f;
            var max = l < 0.5f ? l*(1 + s) : l + s - l*s;
            var min = 2f*l - max;

            return new Color(
                ComponentFromHue(min, max, h + 1f/3f),
                ComponentFromHue(min, max, h),
                ComponentFromHue(min, max, h - 1f/3f));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float ComponentFromHue(float m1, float m2, float h)
        {
            h = (h + 1f)%1f;
            if (h*6f < 1)
                return m1 + (m2 - m1)*6f*h;
            if (h*2 < 1)
                return m2;
            if (h*3 < 2)
                return m1 + (m2 - m1)*(2f/3f - h)*6f;
            return m1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float NormalizeHue(float h)
        {
            if (h < 0) return h + 360*((int) (h/360) + 1);
            return h%360;
        }
    }
}
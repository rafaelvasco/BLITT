using System;
using BLITTEngine.Core.Graphics;

namespace BLITTEngine.Core.Common
{
    public static class RandomEx
    {
        private static int _seed = Environment.TickCount;
        
        public static void RefreshSeed()
        {
            _seed = Environment.TickCount;
        }

        public static float NextFloat()
        {
            return NextInt() / (float) short.MaxValue;
        }

        public static float NextFloat(float max)
        {
            return max * NextFloat();
        }

        public static int NextInt(int max)
        {
           return (int) (max * NextFloat() + 0.5f);
        }
        
        public static int NextInt()
        {
            _seed = 214013 * _seed + 2531011;
            return (_seed >> 16) & 0x7FFF;
        }

        public static float NextAngle()
        {
            return Range(-Calc.PI, Calc.PI);
        }

        public static Color NextColor()
        {
            return new Color( NextFloat(), NextFloat(), NextFloat() );
        }

        public static void NextColor(out Color color, Range<Color> range)
        {
            color = new Color(
                Range(range.Min.Rf, range.Max.Rf),
                Range(range.Min.Gf, range.Max.Gf),
                Range(range.Min.Bf, range.Max.Bf)
                
            );
        }

        public static int Range(int min, int max)
        {
            return (int) ((max - min)*NextFloat() + 0.5f) + min;
        }

        public static float Range(float min, float max)
        {
            return (max - min)*NextFloat() + min;
        }

        public static Vector2 Range(Vector2 min, Vector2 max)
        {
            return new Vector2(
                (max.X - min.X)*NextFloat() + min.X,
                (max.Y - min.Y)*NextFloat() + min.Y
            );
        }

        public static float Range(Range<float> range)
        {
            return Range(range.Min, range.Max);
        }
        
        public static int Range(Range<int> range)
        {
            return Range(range.Min, range.Max);
        }
        
        public static Vector2 Range(Range<Vector2> range)
        {
            return Range(range.Min, range.Max);
        }

        public static bool Chance(float percent)
        {
            return NextFloat() < percent;
        }

        public static bool Chance(int value)
        {
            return NextInt(100) < value;
        }

        public static T Choose<T>(T first, T second)
        {
            return NextInt(2) == 0 ? first : second;
        }

        public static T Choose<T>(T first, T second, T third)
        {
            switch (NextInt(3))
            {
                case 0: return first;
                case 1: return second;
                default: return third;
            }
        }
        
        public static T Choose<T>(T first, T second, T third, T fourth)
        {
            switch (NextInt(4))
            {
                case 0: return first;
                case 1: return second;
                case 2: return third;
                default: return fourth;
            }
        }
        
        public static void NextUnitVector(out Vector2 vector)
        {
            var angle = NextAngle();
            vector = new Vector2(Calc.Cos(angle), Calc.Sin(angle));
        }
        
    }
}
using BLITTEngine.Core.Numerics;

namespace BLITTEngine.GameToolkit
{
    public static class Easings
    {
        public static float Linear(float v)
        {
            return v;
        }

        public static float Quad(float v)
        {
            return v * v;
        }

        public static float Cubic(float v)
        {
            return v * v * v;
        }

        public static float Quart(float v)
        {
            return v * v * v * v;
        }

        public static float Quint(float v)
        {
            return v * v * v * v * v;
        }

        public static float Sine(float v)
        {
            return 1 - Calc.Cos(v * Calc.PI_OVER2);
        }

        public static float Expo(float v)
        {
            return Calc.Pow(2, 10 * (v - 1));
        }

        public static float Circ(float v)
        {
            return 1 - Calc.Sqrt(1 - v * v);
        }

        public static float Back(float v, float bounciness = 1.70158f)
        {
            return v * v * ((bounciness + 1) * v - bounciness);
        }

        public static float Bounce(float v)
        {
            var a = 7.5625f;
            var b = 1.0f / 2.75f;

            return Calc.Min(Calc.Pow(a * v, 2), Calc.Pow(a * (v - 1.5f * b), 2) + 0.75f,
                Calc.Pow(a * (v - 2.25f * b), 2) + 0.9375f, Calc.Pow(a * (v - 2.625f * b), 2) + 0.984375f);
        }

        public static float Elastic(float v, float amp = 0, float period = 0.3f)
        {
            if (amp < 1) amp = 1;

            return -amp * Calc.Sin(2 * Calc.PI / period * (v - 1) - Calc.ASin(1 / amp)) * Calc.Pow(2, 10 * (v - 1));
        }
    }
}
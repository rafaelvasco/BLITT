using System;
using BLITTEngine.Core.Graphics;

namespace BLITTEngine.Core.Numerics
{
    public class RandomEx
    {
        private const double REAL_UNIT_INT = 1.0 / (int.MaxValue + 1.0);
        private const double REAL_UNIT_UINT = 1.0 / (uint.MaxValue + 1.0);
        private const uint Y = 842502087, Z = 3579807591, W = 273326509;

        private uint bitBuffer;
        private uint bitMask = 1;

        private uint x, y, z, w;

        /// <summary>
        ///     Initialises a new instance using time dependent seed.
        /// </summary>
        public RandomEx()
        {
            // Initialise using the system tick count.
            Reinitialise(Environment.TickCount);
        }

        /// <summary>
        ///     Initialises a new instance using an int value as seed.
        ///     This constructor signature is provided to maintain compatibility with
        ///     System.Random
        /// </summary>
        public RandomEx(int seed)
        {
            Reinitialise(seed);
        }

        /// <summary>
        ///     Reinitialises using an int value as a seed.
        /// </summary>
        /// <param name="seed"></param>
        public void Reinitialise(int seed)
        {
            x = (uint) seed;
            y = Y;
            z = Z;
            w = W;
        }

        public int Next()
        {
            var t = x ^ (x << 11);
            x = y;
            y = z;
            z = w;
            w = w ^ (w >> 19) ^ t ^ (t >> 8);

            var rtn = w & 0x7FFFFFFF;
            if (rtn == 0x7FFFFFFF)
                return Next();
            return (int) rtn;
        }

        public int Next(int upperBound)
        {
            if (upperBound < 0)
                throw new ArgumentOutOfRangeException(nameof(upperBound), upperBound, "upperBound must be >=0");

            var t = x ^ (x << 11);
            x = y;
            y = z;
            z = w;

            // The explicit int cast before the first multiplication gives better performance.
            // See comments in NextDouble.
            return (int) (REAL_UNIT_INT * (int) (0x7FFFFFFF & (w = w ^ (w >> 19) ^ t ^ (t >> 8))) * upperBound);
        }

        public int Next(int lowerBound, int upperBound)
        {
            if (lowerBound > upperBound)
                throw new ArgumentOutOfRangeException(nameof(upperBound), upperBound,
                    "upperBound must be >=lowerBound");

            var t = x ^ (x << 11);
            x = y;
            y = z;
            z = w;

            // The explicit int cast before the first multiplication gives better performance.
            // See comments in NextDouble.
            var range = upperBound - lowerBound;
            if (range < 0)
                return lowerBound + (int) (REAL_UNIT_UINT * (w = w ^ (w >> 19) ^ t ^ (t >> 8)) *
                                           (upperBound - (long) lowerBound));

            // 31 bits of precision will suffice if range<=int.MaxValue. This allows us to cast to an int and gain
            // a little more performance.
            return lowerBound + (int) (REAL_UNIT_INT * (int) (0x7FFFFFFF & (w = w ^ (w >> 19) ^ t ^ (t >> 8))) * range);
        }

        public double NextDouble()
        {
            var t = x ^ (x << 11);
            x = y;
            y = z;
            z = w;

            return REAL_UNIT_INT * (int) (0x7FFFFFFF & (w = w ^ (w >> 19) ^ t ^ (t >> 8)));
        }

        public float NextFloat()
        {
            return (float) NextDouble();
        }

        public float NextFloat(float lowerBound, float upperBound)
        {
            return (float) (lowerBound + upperBound * NextDouble());
        }

        public void NextBytes(byte[] buffer)
        {
            uint _x = x, _y = y, _z = z, _w = w;
            var i = 0;
            uint t;
            for (var bound = buffer.Length - 3; i < bound;)
            {
                t = _x ^ (_x << 11);
                _x = _y;
                _y = _z;
                _z = _w;
                _w = _w ^ (_w >> 19) ^ t ^ (t >> 8);

                buffer[i++] = (byte) _w;
                buffer[i++] = (byte) (_w >> 8);
                buffer[i++] = (byte) (_w >> 16);
                buffer[i++] = (byte) (_w >> 24);
            }

            if (i < buffer.Length)
            {
                t = _x ^ (_x << 11);
                _x = _y;
                _y = _z;
                _z = _w;
                _w = _w ^ (_w >> 19) ^ t ^ (t >> 8);

                buffer[i++] = (byte) _w;
                if (i < buffer.Length)
                {
                    buffer[i++] = (byte) (_w >> 8);
                    if (i < buffer.Length)
                    {
                        buffer[i++] = (byte) (_w >> 16);
                        if (i < buffer.Length) buffer[i] = (byte) (_w >> 24);
                    }
                }
            }

            x = _x;
            y = _y;
            z = _z;
            w = _w;
        }

        public uint NextUInt()
        {
            var t = x ^ (x << 11);
            x = y;
            y = z;
            z = w;
            return w = w ^ (w >> 19) ^ t ^ (t >> 8);
        }

        public int NextInt()
        {
            var t = x ^ (x << 11);
            x = y;
            y = z;
            z = w;
            return (int) (0x7FFFFFFF & (w = w ^ (w >> 19) ^ t ^ (t >> 8)));
        }

        public bool NextBool()
        {
            if (bitMask == 1)
            {
                // Generate 32 more bits.
                var t = x ^ (x << 11);
                x = y;
                y = z;
                z = w;
                bitBuffer = w = w ^ (w >> 19) ^ t ^ (t >> 8);

                // Reset the bitMask that tells us which bit to read next.
                bitMask = 0x80000000;
                return (bitBuffer & bitMask) == 0;
            }

            return (bitBuffer & (bitMask >>= 1)) == 0;
        }

        public Color NextColor()
        {
            return new Color(Next(0, 255), Next(0, 255), Next(0, 255));
        }
    }
}
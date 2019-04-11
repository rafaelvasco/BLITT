using System;

namespace BLITTEngine.Core.Common
{
    public struct Range<T> : IEquatable<Range<T>> where T : struct, IComparable<T> 
    {
        public T Min { get; }
        public T Max { get; }
        
        public Range(T min, T max)
        {
            if (min.CompareTo(max) > 0 || max.CompareTo(min) < 0)
                throw new ArgumentException("Min has to be smaller than or equal to max.");

            Min = min;
            Max = max;
        }
        
        public Range(T value)
            : this(value, value)
        {
        }
        
        public bool IsDegenerate => Min.Equals(Max);
        
        public bool IsProper => !Min.Equals(Max);
        
        public bool Equals(Range<T> value) => Min.Equals(value.Min) && Max.Equals(value.Max);

        public override bool Equals(object obj) => obj is Range<T> a && Equals(a);
        
        public override int GetHashCode() => Min.GetHashCode() ^ Max.GetHashCode();

        public static bool operator ==(Range<T> value1, Range<T> value2) => value1.Equals(value2);

        public static bool operator !=(Range<T> value1, Range<T> value2) => !value1.Equals(value2);

        public static implicit operator Range<T>(T value) => new Range<T>(value, value);

        public override string ToString() => $"Range<{typeof(T).Name}> [{Min} {Max}]";
        
        public bool IsInBetween(T value, bool minValueExclusive = false, bool maxValueExclusive = false)
        {
            if (minValueExclusive)
            {
                if (value.CompareTo(Min) <= 0)
                    return false;
            }

            if (value.CompareTo(Min) < 0)
                return false;

            if (maxValueExclusive)
            {
                if (value.CompareTo(Max) >= 0)
                    return false;
            }

            return value.CompareTo(Max) <= 0;
        }

    }
}
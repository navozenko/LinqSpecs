using System;

namespace LinqSpecs.Utilities
{
    internal static class HashCodeHelpers
    {
        public static int Combine<T>(T value)
        {
#if NETSTANDARD2_1_OR_GREATER
            return HashCode.Combine(value);
#else
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + value?.GetHashCode() ?? 0;
                return hash;
            }
#endif
        }

        public static int Combine<T1, T2>(T1 value1, T2 value2)
        {
#if NETSTANDARD2_1_OR_GREATER
            return HashCode.Combine(value1, value2);
#else
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + value1?.GetHashCode() ?? 0;
                hash = hash * 23 + value2?.GetHashCode() ?? 0;
                return hash;
            }
#endif
        }

        public static int Combine<T1, T2, T3>(T1 value1, T2 value2, T3 value3)
        {
#if NETSTANDARD2_1_OR_GREATER
            return HashCode.Combine(value1, value2, value3);
#else
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + value1?.GetHashCode() ?? 0;
                hash = hash * 23 + value2?.GetHashCode() ?? 0;
                hash = hash * 23 + value3?.GetHashCode() ?? 0;
                return hash;
            }
#endif
        }
    }
}

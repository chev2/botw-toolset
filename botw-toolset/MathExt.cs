using System;

namespace BOTWToolset
{
    /// <summary>
    /// Extensions for Math.
    /// </summary>
    static class MathExt
    {
        /// <summary>
        /// Clamps a value into a minimum and maximum range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val">Value to clamp.</param>
        /// <param name="min">The minimum value to use.</param>
        /// <param name="max">The maximum value to use.</param>
        /// <returns></returns>
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
    }
}

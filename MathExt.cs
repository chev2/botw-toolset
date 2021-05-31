namespace BOTWToolset
{
    /// <summary>
    /// Extensions for Math.
    /// </summary>
    static class MathExt
    {
        /// <summary>
        /// Linearly interpolates a value.
        /// </summary>
        /// <param name="from">Value to interpolate.</param>
        /// <param name="to">Value to interpolate to.</param>
        /// <param name="delta">Amount to interpolate - should be between 0 and 1.</param>
        /// <returns></returns>
        public static float Lerp(float from, float to, float delta)
        {
            if (delta > 1)
                return to;
            if (delta < 0)
                return from;

            return from + (to - from) * delta;
        }
    }
}

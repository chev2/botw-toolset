namespace BOTWToolset.IO.TSCB
{
    /// <summary>
    /// Manages the TSCB tab's pixel map.
    /// </summary>
    static class GridConverter
    {
        /// <summary>
        /// Converts a Z-Curve position to (X, Y) coordinates.
        /// </summary>
        /// <param name="index">Z-Curve index</param>
        /// <returns>int array with X and Y coordinates.</returns>
        public static int[] ZCurveToXY(int index)
        {
            int x = 0;
            int y = 0;

            // Shift bits to the right to get untangled X and Y bytes
            for (int i = 0; i < 16; i++)
            {
                x = ((index & (1 << (i * 2))) >> i) | x;
                y = ((index & (2 << (i * 2))) >> i + 1) | y;
            }

            return new int[] { x, y };
        }
    }
}

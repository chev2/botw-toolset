using System.Drawing;

namespace BOTWToolset.IO
{
    /// <summary>
    /// Contains colors used by the TSCB grid.
    /// </summary>
    public class GridColors
    {
        public static readonly Color[] GrassColors = new Color[]
        {
            Color.FromArgb(255, 95, 142, 74), // Green grass
            Color.FromArgb(255, 255, 193, 75), // Yellow grass
            Color.FromArgb(255, 206, 122, 66), // Wood chips 00
            Color.FromArgb(255, 181, 107, 57), // Wood chips 01
            Color.FromArgb(255, 160, 95, 51), // Wood chips 02
            Color.FromArgb(255, 137, 81, 44), // Wood chips 03
            Color.FromArgb(255, 137, 106, 72), // Wood chips 04 reeds
            Color.FromArgb(255, 79, 112, 68) // Wood chips 05 leaves
        };

        public static readonly Color[] WaterColors = new Color[]
        {
            Color.FromArgb(255, 73, 137, 255), // Water
            Color.FromArgb(255, 114, 255, 210), // Hot water
            Color.FromArgb(255, 173, 73, 255), // Poison water
            Color.FromArgb(255, 255, 97, 0), // Lava
            Color.FromArgb(255, 142, 211, 255), // Cold water
            Color.FromArgb(255, 86, 44, 0), // Bog (mud)
            Color.FromArgb(255, 102, 158, 255), // Clear water 01
            Color.FromArgb(255, 52, 99, 181) // Ocean
        };

        public static readonly Color[] MaterialColors = new Color[]
        {
            Color.FromArgb(255, 45, 153, 0),    // 00 - Grass (basic)
            Color.FromArgb(255, 183, 183, 183), // 01 - Light stone - Rough rock A
            Color.FromArgb(255, 170, 68, 66),   // 02 - Red cube rock
            Color.FromArgb(255, 163, 163, 163), // 03 - Rough rock
            Color.FromArgb(255, 183, 183, 183), // 04 - White rock
            Color.FromArgb(255, 32, 86, 32),    // 05 - Brown Soil and Grass (consolidated green)
            Color.FromArgb(255, 253, 251, 163), // 06 - sand beige
            Color.FromArgb(255, 252, 255, 112), // 07 - sandy beach (for coast)
            Color.FromArgb(255, 250, 250, 250), // 08 - snow (flat)
            Color.FromArgb(255, 255, 255, 255), // 09 - rock gravel
            Color.FromArgb(255, 255, 255, 255), // 10 - earth and rock - hard soil
            Color.FromArgb(255, 255, 255, 255), // 11 - lawn & earth
            Color.FromArgb(255, 255, 255, 255), // 12 - futago rock
            Color.FromArgb(255, 255, 255, 255), // 13 - cobblestones & lawn
            Color.FromArgb(255, 255, 255, 255), // 14 - fallen leaves A
            Color.FromArgb(255, 255, 255, 255), // 15 - thin rock and grass
            Color.FromArgb(255, 255, 255, 255), // 16 - cliff (white) A & grass
            Color.FromArgb(255, 255, 255, 255), // 17 - cliff (white) A
            Color.FromArgb(255, 255, 255, 255), // 18 - cliff (white) B
            Color.FromArgb(255, 255, 255, 255), // 19 - rock (red) A
            Color.FromArgb(255, 255, 255, 255), // 20 - maruwa & sand
            Color.FromArgb(255, 255, 255, 255), // 21 - rectangular rock - orange
            Color.FromArgb(255, 255, 255, 255), // 22 - rough rock B
            Color.FromArgb(255, 255, 255, 255), // 23 - soil (hardened red
            Color.FromArgb(255, 255, 255, 255), // 24 - sat - farm (for fields)
            Color.FromArgb(255, 255, 255, 255), // 25 - soil (compacted pebble)
            Color.FromArgb(255, 255, 255, 255), // 26 - green grass and stone
            Color.FromArgb(255, 255, 255, 255), // 27 - stormy rocks
            Color.FromArgb(255, 255, 255, 255), // 28 - earth and sand A
            Color.FromArgb(255, 255, 255, 255), // 29 - grass (dead) dried grass field
            Color.FromArgb(255, 255, 255, 255), // 30 - world end - the end of the world
            Color.FromArgb(255, 255, 255, 255), // 31 - death mountain rock (volcano) B
            Color.FromArgb(255, 255, 255, 255), // 32 - sat (mass / volcano)
            Color.FromArgb(255, 255, 255, 255), // 33 - snow bumpy
            Color.FromArgb(255, 255, 255, 255), // 34 - sat (brown)
            Color.FromArgb(255, 255, 255, 255), // 35 - earth (mass)
            Color.FromArgb(255, 255, 255, 255), // 36 - fallen leaves B
            Color.FromArgb(255, 255, 255, 255), // 37 - fallen leaves C
            Color.FromArgb(255, 255, 255, 255), // 38 - moss field A
            Color.FromArgb(255, 255, 255, 255), // 39 - moss field B
            Color.FromArgb(255, 255, 255, 255), // 40 - cracked soil
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255)
        };
    }
}

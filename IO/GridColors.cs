using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BOTWToolset.IO
{
    /// <summary>
    /// Contains colors used by the TSCB grid.
    /// </summary>
    public class GridColors
    {
        public static readonly IList<Color> GrassColors = new Color[]
        {
            Color.FromArgb(255,  95, 142,  74), // Green grass
            Color.FromArgb(255, 255, 193,  75), // Yellow grass
            Color.FromArgb(255, 206, 122,  66), // Wood chips 00
            Color.FromArgb(255, 181, 107,  57), // Wood chips 01
            Color.FromArgb(255, 160,  95,  51), // Wood chips 02
            Color.FromArgb(255, 137,  81,  44), // Wood chips 03
            Color.FromArgb(255, 137, 106,  72), // Wood chips 04 reeds
            Color.FromArgb(255,  79, 112,  68) // Wood chips 05 leaves
        };

        public static readonly IList<Color> WaterColors = new Color[]
        {
            Color.FromArgb(255,  53,  64,  70), // Water
            Color.FromArgb(255,  60,  94,  78), // Hot water
            Color.FromArgb(255, 173,  73, 255), // Poison water
            Color.FromArgb(255, 211, 115,  38), // Lava
            Color.FromArgb(255, 142, 211, 255), // Cold water
            Color.FromArgb(255,  86,  44,   0), // Bog (mud)
            Color.FromArgb(255, 102, 158, 255), // Clear water 01
            Color.FromArgb(255,  32,  43,  45) // Ocean
        };

        public static readonly IList<Color> MaterialColors = new Color[]
        {
            Color.FromArgb(255,  77, 101,  55), // 00 - Plant_GreenGrassField_A
            Color.FromArgb(255,  96,  95,  85), // 01 - Rock_NoisyRock_A
            Color.FromArgb(255, 127,  97,  45), // 02 - Rock_RedCubeRock_A
            Color.FromArgb(255,  82,  81,  80), // 03 - Rock_RoughRock_A
            Color.FromArgb(255, 106, 105, 105), // 04 - Rock_WhiteRock_A
            Color.FromArgb(255,  54,  63,  50), // 05 - Sand_BrownSoilAndGrass_A
            Color.FromArgb(255, 122, 105,  90), // 06 - Sand_SandBeige_A
            Color.FromArgb(255, 129, 125, 109), // 07 - Sand_SandBeach_A
            Color.FromArgb(255, 148, 147, 147), // 08 - Snow_SnowPowder_A
            Color.FromArgb(255,  81,  78,  69), // 09 - Rock_GravelStone_A
            Color.FromArgb(255, 118, 101,  80), // 10 - Sand_HardMad_A
            Color.FromArgb(255,  76,  87,  60), // 11 - Plant_GreenGrassAndMad_A
            Color.FromArgb(255,  67,  68,  70), // 12 - Rock_FutagoRock_A
            Color.FromArgb(255, 105, 102,  82), // 13 - Floor_StoneTilesAndMoss_A
            Color.FromArgb(255,  65,  54,  37), // 14 - Plant_FallenLeafAndGrass_A
            Color.FromArgb(255,  65,  64,  55), // 15 - Rock_RockAndGrass_A
            Color.FromArgb(255,  85,  96,  72), // 16 - Rock_LargeCliffAndGrass_A
            Color.FromArgb(255,  82,  82,  82), // 17 - Rock_LargeCliff_A
            Color.FromArgb(255, 106, 108,  98), // 18 - Rock_LargeCliff_B
            Color.FromArgb(255, 124, 101,  94), // 19 - Rock_RedRockSoft_A
            Color.FromArgb(255, 130, 120, 104), // 20 - Rock_RoundRockAndSand_A
            Color.FromArgb(255, 133, 103,  59), // 21 - Rock_OrangeCubeCliff_A
            Color.FromArgb(255,  84,  83,  81), // 22 - Rock_NoisyRock_B
            Color.FromArgb(255, 124,  95,  86), // 23 - Sand_HardSoilRed_A
            Color.FromArgb(255,  36,  24,  18), // 24 - Sand_FarmMad_A
            Color.FromArgb(255,  62,  63,  56), // 25 - Sand_HardMadAndStone_A
            Color.FromArgb(255,  83, 101,  65), // 26 - Plant_GreenGrassAndStone_A
            Color.FromArgb(255,  78,  55,  48), // 27 - Rock_RoughRock_B
            Color.FromArgb(255, 130, 111,  79), // 28 - Sand_LandSlide_A
            Color.FromArgb(255, 108,  98,  48), // 29 - Plant_DriedGrassField_A
            Color.FromArgb(255,  59,  52,  49), // 30 - Rock_WorldEnd_A
            Color.FromArgb(255,  94,  67,  59), // 31 - Rock_DeathMountain_B
            Color.FromArgb(255,  49,  33,  26), // 32 - Rock_DeathMountain_C
            Color.FromArgb(255, 163, 159, 159), // 33 - Snow_SnowBumpy_A
            Color.FromArgb(255,  95,  78,  35), // 34 - Sand_BrownSoil_A
            Color.FromArgb(255,  65,  57,  49), // 35 - Sand_SolidSoil_A
            Color.FromArgb(255,  33,  40,  23), // 36 - Plant_FallenLeafAndGrass_B
            Color.FromArgb(255,  26,  33,  16), // 37 - Plant_FallenLeafAndGrass_C
            Color.FromArgb(255,  37,  54,  20), // 38 - Plant_MossField_A
            Color.FromArgb(255,  54,  66,  15), // 39 - Plant_MossField_B
            Color.FromArgb(255,  89,  79,  69), // 40 - Sand_CrackSoil_A
            Color.FromArgb(255,  35,  81, 105), // 41 - Wall_TerraZoraBridge_A
            Color.FromArgb(255,  82,  77,  63), // 42 - Sand_BrownSoilAndStone_A
            Color.FromArgb(255,  93,  85,  78), // 43 - Rock_GravelStone_B
            Color.FromArgb(255, 102, 101, 104), // 44 - Sand_GraySoilAndGrass_A
            Color.FromArgb(255, 108,  68,  54), // 45 - Rock_HorizontallyCliff_A
            Color.FromArgb(255,  96,  96,  96), // 46 - Rock_LargeCliffSnow_A
            Color.FromArgb(255,  43,  43,  42), // 47 - Rock_RedRockDark_A
            Color.FromArgb(255,  47,  64,  77), // 48 - Rock_RockZora_A
            Color.FromArgb(255, 174, 155, 124), // 49 - Sand_PebblySoil_A
            Color.FromArgb(255, 186, 189, 190), // 50 - Rock_RockSnow_A
            Color.FromArgb(255,  46,  65,  76), // 51 - Rock_RockSnow_B
            Color.FromArgb(255,  59,  60,  61), // 52 - Rock_LargeCliff_C
            Color.FromArgb(255, 131, 124, 106), // 53 - Rock_BeigeRock_A
            Color.FromArgb(255, 143, 121, 105), // 54 - Sand_RedPebbly_A
            Color.FromArgb(255,  50,  84,  45), // 55 - Plant_GreenGrassField_B
            Color.FromArgb(255, 135, 117, 100), // 56 - Sand_SandWindPattern_A
            Color.FromArgb(255,  78,  79,  70), // 57 - Rock_CliffCheese_A
            Color.FromArgb(255,  62,  62,  49), // 58 - Rock_ColorfulRock_A
            Color.FromArgb(255, 150, 141, 106), // 59 - Rock_HardBrownStone_A
            Color.FromArgb(255,  54,  84, 107), // 60 - Wall_TerraZoraWall_A
            Color.FromArgb(255,  87,  89,  89), // 61 - Snow_SnowAndStone_A
            Color.FromArgb(255, 101,  96,  84), // 62 - Rock_MountainSheiker_A
            Color.FromArgb(255,  44,  60,  34), // 63 - Plant_MountainSheiker_A
            Color.FromArgb(255, 104,  64,  62), // 64 - Rock_TropicalCliff_A
            Color.FromArgb(255,  48,  53,  32), // 65 - Plant_TropicalGrass_A
            Color.FromArgb(255, 130,  86,  79), // 66 - Rock_RedCubeCliff_A
            Color.FromArgb(255, 175, 147, 102), // 67 - Sand_PebblySoil_B
            Color.FromArgb(255, 115,  99,  87), // 68 - Rock_SeasideRock_A
            Color.FromArgb(255, 110, 118, 111), // 69 - Rock_GravelStone_C
            Color.FromArgb(255, 109,  72,  67), // 70 - Rock_TropicalLumpRock_A
            Color.FromArgb(255, 101,  98,  97), // 71 - Wall_HyliaStoneRoad_A
            Color.FromArgb(255,  52,  66,  30), // 72 - Plant_LakeHylia_A
            Color.FromArgb(255, 123, 112,  96), // 73 - Sand_CrackSoil_B
            Color.FromArgb(255,  91, 105,  70), // 74 - Sand_SandBeachAndGrass_A
            Color.FromArgb(255,  47,  49,  45), // 75 - Plant_BlackGrassField_A
            Color.FromArgb(255, 119, 116, 108), // 76 - Sand_WhiteSoilAndStone_A
            Color.FromArgb(255,  67,  61,  55), // 77 - Sand_DebriWood_A
            Color.FromArgb(255,  97,  93,  76), // 78 - Sand_DebriStone_A
            Color.FromArgb(255,  60,  73,  42), // 79 - Rock_RoughRockMoss_A
            Color.FromArgb(255,  89,  81,  46), // 80 - Plant_FallenLeafAndGrass_D
            Color.FromArgb(255, 248, 200, 217), // 81 - Plant_FallenLeafCherry_A
            Color.FromArgb(255,  35,  32,  29) // 82 - Rock_RockBeachCoral_A
        };

        public static readonly BitmapPalette GrassPalette;
        public static readonly BitmapPalette WaterPalette;
        public static readonly BitmapPalette MaterialPalette;

        static GridColors()
        {
            GrassPalette = new BitmapPalette(GrassColors);
            WaterPalette = new BitmapPalette(WaterColors);
            MaterialPalette = new BitmapPalette(MaterialColors);
        }
    }
}

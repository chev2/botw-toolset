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
            Color.FromArgb(255, 95, 142, 74), // Green grass
            Color.FromArgb(255, 255, 193, 75), // Yellow grass
            Color.FromArgb(255, 206, 122, 66), // Wood chips 00
            Color.FromArgb(255, 181, 107, 57), // Wood chips 01
            Color.FromArgb(255, 160, 95, 51), // Wood chips 02
            Color.FromArgb(255, 137, 81, 44), // Wood chips 03
            Color.FromArgb(255, 137, 106, 72), // Wood chips 04 reeds
            Color.FromArgb(255, 79, 112, 68) // Wood chips 05 leaves
        };

        public static readonly IList<Color> WaterColors = new Color[]
        {
            Color.FromArgb(255, 53, 64, 70), // Water
            Color.FromArgb(255, 60, 94, 78), // Hot water
            Color.FromArgb(255, 173, 73, 255), // Poison water
            Color.FromArgb(255, 211, 115, 38), // Lava
            Color.FromArgb(255, 142, 211, 255), // Cold water
            Color.FromArgb(255, 86, 44, 0), // Bog (mud)
            Color.FromArgb(255, 102, 158, 255), // Clear water 01
            Color.FromArgb(255, 32, 43, 45) // Ocean
        };

        public static readonly IList<Color> MaterialColors = new Color[]
        {
            Color.FromArgb(255, 76, 100, 55),   // 00 - Plant_GreenGrassField_A
            Color.FromArgb(255, 183, 183, 183), // 01 - Rock_NoisyRock_A
            Color.FromArgb(255, 170, 68, 66),   // 02 - Rock_RedCubeRock_A
            Color.FromArgb(255, 163, 163, 163), // 03 - Rock_RoughRock_A
            Color.FromArgb(255, 183, 183, 183), // 04 - Rock_WhiteRock_A
            Color.FromArgb(255, 32, 86, 32),    // 05 - Sand_BrownSoilAndGrass_A
            Color.FromArgb(255, 253, 251, 163), // 06 - Sand_SandBeige_A
            Color.FromArgb(255, 252, 255, 112), // 07 - Sand_SandBeach_A
            Color.FromArgb(255, 250, 250, 250), // 08 - Snow_SnowPowder_A
            Color.FromArgb(255, 192, 192, 192), // 09 - Rock_GravelStone_A
            Color.FromArgb(255, 252, 255, 112), // 10 - Sand_HardMad_A
            Color.FromArgb(255, 76, 100, 55), // 11 - Plant_GreenGrassAndMad_A
            Color.FromArgb(255, 183, 183, 183), // 12 - Rock_FutagoRock_A
            Color.FromArgb(255, 150, 150, 150), // 13 - Floor_StoneTilesAndMoss_A
            Color.FromArgb(255, 82, 137, 30), // 14 - Plant_FallenLeafAndGrass_A
            Color.FromArgb(255, 112, 137, 89), // 15 - Rock_RockAndGrass_A
            Color.FromArgb(255, 183, 183, 183), // 16 - Rock_LargeCliffAndGrass_A
            Color.FromArgb(255, 183, 183, 183), // 17 - Rock_LargeCliff_A
            Color.FromArgb(255, 183, 183, 183), // 18 - Rock_LargeCliff_B
            Color.FromArgb(255, 184, 104, 104), // 19 - Rock_RedRockSoft_A
            Color.FromArgb(255, 183, 183, 183), // 20 - Rock_RoundRockAndSand_A
            Color.FromArgb(255, 184, 122, 104), // 21 - Rock_OrangeCubeCliff_A
            Color.FromArgb(255, 183, 183, 183), // 22 - Rock_NoisyRock_B
            Color.FromArgb(255, 255, 224, 112), // 23 - Sand_HardSoilRed_A
            Color.FromArgb(255, 56, 39, 26), // 24 - Sand_FarmMad_A
            Color.FromArgb(255, 56, 39, 26), // 25 - Sand_HardMadAndStone_A
            Color.FromArgb(255, 76, 100, 55), // 26 - Plant_GreenGrassAndStone_A
            Color.FromArgb(255, 183, 183, 183), // 27 - Rock_RoughRock_B
            Color.FromArgb(255, 56, 39, 26), // 28 - Sand_LandSlide_A
            Color.FromArgb(255, 146, 155, 85), // 29 - Plant_DriedGrassField_A
            Color.FromArgb(255, 36, 25, 17), // 30 - Rock_WorldEnd_A
            Color.FromArgb(255, 93, 67, 59), // 31 - Rock_DeathMountain_B
            Color.FromArgb(255, 93, 67, 59), // 32 - Rock_DeathMountain_C
            Color.FromArgb(255, 250, 250, 250), // 33 - Snow_SnowBumpy_A
            Color.FromArgb(255, 56, 39, 26), // 34 - Sand_BrownSoil_A
            Color.FromArgb(255, 56, 39, 26), // 35 - Sand_SolidSoil_A
            Color.FromArgb(255, 76, 100, 55), // 36 - Plant_FallenLeafAndGrass_B
            Color.FromArgb(255, 76, 100, 55), // 37 - Plant_FallenLeafAndGrass_C
            Color.FromArgb(255, 81, 104, 65), // 38 - Plant_MossField_A
            Color.FromArgb(255, 81, 104, 65), // 39 - Plant_MossField_B
            Color.FromArgb(255, 107, 81, 73), // 40 - Sand_CrackSoil_A
            Color.FromArgb(255, 82, 149, 159), // 41 - Wall_TerraZoraBridge_A
            Color.FromArgb(255, 56, 39, 26), // 42 - Sand_BrownSoilAndStone_A - Soil - Pebbles mixed with pebbles
            Color.FromArgb(255, 183, 183, 183), // 43 - Rock_GravelStone_B - Stone_Light - Gravel B 
            Color.FromArgb(255, 68, 104, 44), // 44 - Sand_GraySoilAndGrass_A - Gravel mixed with weeds
            Color.FromArgb(255, 183, 183, 183), // 45 - Rock_HorizontallyCliff_A
            Color.FromArgb(255, 183, 183, 183), // 46 - Rock_LargeCliffSnow_A
            Color.FromArgb(255, 134, 75, 75), // 47 - Rock_RedRockDark_A
            Color.FromArgb(255, 82, 149, 159), // 48 - Rock_RockZora_A
            Color.FromArgb(255, 252, 255, 112), // 49 - Sand_PebblySoil_A
            Color.FromArgb(255, 250, 250, 250), // 50 - Rock_RockSnow_A
            Color.FromArgb(255, 250, 250, 250), // 51 - Rock_RockSnow_B
            Color.FromArgb(255, 183, 183, 183), // 52 - Rock_LargeCliff_C
            Color.FromArgb(255, 183, 183, 183), // 53 - Rock_BeigeRock_A
            Color.FromArgb(255, 255, 159, 127), // 54 - Sand_RedPebbly_A
            Color.FromArgb(255, 76, 100, 55), // 55 - Plant_GreenGrassField_B
            Color.FromArgb(255, 252, 255, 112), // 56 - Sand_SandWindPattern_A
            Color.FromArgb(255, 182, 170, 135), // 57 - Rock_CliffCheese_A
            Color.FromArgb(255, 183, 183, 183), // 58 - Rock_ColorfulRock_A
            Color.FromArgb(255, 118, 93, 73), // 59 - Rock_HardBrownStone_A
            Color.FromArgb(255, 82, 149, 159), // 60 - Wall_TerraZoraWall_A
            Color.FromArgb(255, 250, 250, 250), // 61 - Snow_SnowAndStone_A
            Color.FromArgb(255, 183, 183, 183), // 62 - Rock_MountainSheiker_A
            Color.FromArgb(255, 99, 131, 72), // 63 - Plant_MountainSheiker_A
            Color.FromArgb(255, 159, 135, 93), // 64 - Rock_TropicalCliff_A
            Color.FromArgb(255, 76, 100, 55), // 65 - Plant_TropicalGrass_A
            Color.FromArgb(255, 184, 104, 104), // 66 - Rock_RedCubeCliff_A
            Color.FromArgb(255, 252, 255, 112), // 67 - Sand_PebblySoil_B
            Color.FromArgb(255, 183, 183, 183), // 68 - Rock_SeasideRock_A
            Color.FromArgb(255, 183, 183, 183), // 69 - Rock_GravelStone_C
            Color.FromArgb(255, 183, 183, 183), // 70 - Rock_TropicalLumpRock_A
            Color.FromArgb(255, 120, 120, 120), // 71 - Wall_HyliaStoneRoad_A
            Color.FromArgb(255, 99, 131, 72), // 72 - Plant_LakeHylia_A
            Color.FromArgb(255, 107, 81, 73), // 73 - Sand_CrackSoil_B
            Color.FromArgb(255, 252, 255, 112), // 74 - Sand_SandBeachAndGrass_A
            Color.FromArgb(255, 99, 131, 72), // 75 - Plant_BlackGrassField_A
            Color.FromArgb(255, 221, 221, 221), // 76 - Sand_WhiteSoilAndStone_A
            Color.FromArgb(255, 126, 98, 51), // 77 - Sand_DebriWood_A
            Color.FromArgb(255, 141, 141, 141), // 78 - Sand_DebriStone_A
            Color.FromArgb(255, 150, 150, 150), // 79 - Rock_RoughRockMoss_A
            Color.FromArgb(255, 99, 131, 72), // 80 - Plant_FallenLeafAndGrass_D
            Color.FromArgb(255, 99, 131, 72), // 81 - Plant_FallenLeafCherry_A
            Color.FromArgb(255, 183, 183, 183) // 82 - Rock_RockBeachCoral_A
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

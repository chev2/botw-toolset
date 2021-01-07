namespace BOTWToolset.IO.TSCB
{
    /// <summary>
    /// Stores area info for area data declared in a .tscb file.
    /// </summary>
    public class AreaInfo
    {
        public float PositionX { get => _positionX; set => _positionX = value; }
        private float _positionX;

        public float PositionZ { get => _positionZ; set => _positionZ = value; }
        private float _positionZ;

        public float AreaSize { get => _areaSize; set => _areaSize = value; }
        private float _areaSize;

        public float MinTerrainHeight { get => _minTerrainHeight; set => _minTerrainHeight = value; }
        private float _minTerrainHeight;

        public float MaxTerrainHeight { get => _maxTerrainHeight; set => _maxTerrainHeight = value; }
        private float _maxTerrainHeight;

        public float MinWaterHeight { get => _minWaterHeight; set => _minWaterHeight = value; }
        private float _minWaterHeight;

        public float MaxWaterHeight { get => _maxWaterHeight; set => _maxWaterHeight = value; }
        private float _maxWaterHeight;

        public uint Unknown1 { get => _unknown1; set => _unknown1 = value; }
        private uint _unknown1;

        public uint FileBase { get => _fileBase; set => _fileBase = value; }
        private uint _fileBase;

        public uint Unknown2 { get => _unknown2; set => _unknown2 = value; }
        private uint _unknown2;

        public uint Unknown3 { get => _unknown3; set => _unknown3 = value; }
        private uint _unknown3;

        public uint ReferenceExtra { get => _refExtra; set => _refExtra = value; }
        private uint _refExtra;

        public bool HasWater { get => _hasWater; set => _hasWater = value; }
        private bool _hasWater = false;

        public bool HasGrass { get => _hasGrass; set => _hasGrass = value; }
        private bool _hasGrass = false;

        public uint Offset;

        public AreaInfo(float x_pos, float z_pos, float area_size, float min_terrain_height, float max_terrain_height,
            float min_water_height, float max_water_height, uint unknown_1, uint file_base, uint unknown_2, uint unknown_3, uint ref_extra)
        {
            PositionX = x_pos;
            PositionZ = z_pos;
            AreaSize = area_size;
            MinTerrainHeight = min_terrain_height;
            MaxTerrainHeight = max_terrain_height;
            MinWaterHeight = min_water_height;
            MaxWaterHeight = max_water_height;
            Unknown1 = unknown_1;
            FileBase = file_base;
            Unknown2 = unknown_2;
            Unknown3 = unknown_3;
            ReferenceExtra = ref_extra;
        }
    }
}

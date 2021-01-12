using BOTWToolset.Debugging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BOTWToolset.IO.TSCB
{
    /// <summary>
    /// Interacts with .tcsb (terrain scene binary) files.
    /// More info found on the <see href="https://zeldamods.org/wiki/TSCB">ZeldaMods wiki</see>.
    /// </summary>
    public class TSCB
    {
        public const byte HeaderLength = 48; // Length of the header, in bytes

        public string Signature { get => _signature; set => _signature = value; }
        private string _signature;

        public uint Version { get => _version; set => _version = value; }
        private uint _version;

        public uint FileBaseOffset { get => _fileBaseOffset; set => _fileBaseOffset = value; }
        private uint _fileBaseOffset;

        public float WorldScale { get => _worldScale; set => _worldScale = value.Clamp(0f, 800.0f); }
        private float _worldScale;

        public float TerrainMaxHeight { get => _terrainMaxHeight; set => _terrainMaxHeight = value.Clamp(0f, 800.0f); }
        private float _terrainMaxHeight;

        public byte[] MaterialInfoOffsets;

        public uint MaterialInfoLength { get => _materialInfoLength; set => _materialInfoLength = value; }
        private uint _materialInfoLength;

        public byte[] AreaArrayOffsets;

        public uint AreaArrayLength { get => _areaArrayLength; set => _areaArrayLength = value; }
        private uint _areaArrayLength;

        public MaterialInfo[] MaterialInfo;

        public AreaInfo[] AreaInfo;

        public float TileSize;

        public string[] FileNames;

        /// <summary>
        /// Reads a .tscb file and returns a TSCBInfo containing its data.
        /// </summary>
        /// <param name="file">The .tscb file.</param>
        /// <returns></returns>
        public static TSCB ReadFile(string file)
        {
            if (File.Exists(file))
            {
                TSCB t = new TSCB();

                // Use big-endian
                using (var r = new BinaryReaderBig(File.Open(file, FileMode.Open)))
                {
                    // Set header info from file on the new TSCBInfo
                    t.Signature = new string(r.ReadChars(4));
                    t.Version = r.ReadByte();

                    // Skip the 3 extra version bytes
                    r.BaseStream.Seek(3, SeekOrigin.Current);

                    // Skip 4 bytes of "00 00 00 01"
                    r.BaseStream.Seek(4, SeekOrigin.Current);

                    t.FileBaseOffset = r.ReadUInt32();
                    t.WorldScale = r.ReadSingle();
                    t.TerrainMaxHeight = r.ReadSingle();
                    t.MaterialInfoLength = r.ReadUInt32();
                    t.AreaArrayLength = r.ReadUInt32();

                    // Skip 8 bytes of padding
                    r.Advance(8);

                    t.TileSize = r.ReadSingle();

                    // Skip 4 bytes of "00 00 00 08"
                    r.Advance(4);

                    // Read mat info offsets
                    t.MaterialInfoOffsets = r.ReadBytes((int)((t.MaterialInfoLength * 4) + 4));

                    BOTWConsole.Log($"Offset before material iteration: {r.BaseStream.Position}");

                    // Initialize mat info array with provided length
                    t.MaterialInfo = new MaterialInfo[t.MaterialInfoLength];

                    // Initialize every mat info, then add to the array
                    for (int i = 0; i < t.MaterialInfoLength; i++)
                    {
                        uint index = r.ReadUInt32();
                        float tex_u = r.ReadSingle();
                        float tex_v = r.ReadSingle();
                        float unk_1 = r.ReadSingle();
                        float unk_2 = r.ReadSingle();

                        MaterialInfo matInfo = new MaterialInfo(index, tex_u, tex_v, unk_1, unk_2);

                        t.MaterialInfo[i] = matInfo;
                    }

                    BOTWConsole.Log($"Offset before area offset iteration: {r.BaseStream.Position}");

                    // Read area offsets
                    t.AreaArrayOffsets = r.ReadBytes((int)(t.AreaArrayLength * 4));

                    BOTWConsole.Log($"Offset before area iteration: {r.BaseStream.Position}");

                    t.AreaInfo = new AreaInfo[t.AreaArrayLength];

                    // Read every area info entry
                    for (int i = 0; i < t.AreaArrayLength; i++)
                    {
                        uint offset = (uint)r.BaseStream.Position;

                        float xpos = r.ReadSingle();
                        float zpos = r.ReadSingle();
                        float area_size = r.ReadSingle();
                        float min_terrain_height = r.ReadSingle();
                        float max_terrain_height = r.ReadSingle();
                        float min_water_height = r.ReadSingle();
                        float max_water_height = r.ReadSingle();
                        uint unk_1 = r.ReadUInt32();

                        if (unk_1 == 0)
                        { // If this unknown is equal to 0, skip the extra byte coming after it
                            uint next_val = r.ReadUInt32();

                            if (next_val != 1) // If the next value isn't extra unneeded info
                            {
                                r.Advance(-4);
                            }
                        }

                        uint file_base = r.ReadUInt32();
                        uint unk_2 = r.ReadUInt32();
                        uint unk_3 = r.ReadUInt32();
                        uint ref_extra = r.ReadUInt32();

                        AreaInfo areaInfo = new AreaInfo
                        {
                            PositionX = xpos,
                            PositionZ = zpos,
                            AreaSize = area_size,
                            MinTerrainHeight = min_terrain_height,
                            MaxTerrainHeight = max_terrain_height,
                            MinWaterHeight = min_water_height,
                            MaxWaterHeight = max_water_height,
                            Unknown1 = unk_1,
                            FileBase = file_base,
                            Unknown2 = unk_2,
                            Unknown3 = unk_3,
                            ReferenceExtra = ref_extra,
                            Offset = offset
                        };

                        areaInfo.ExtraInfoLength = r.ReadUInt32(); //Usually 0, 4, or 8

                        if (ref_extra == 4)
                        {
                            if (areaInfo.ExtraInfoLength == 8)
                            { //Skip the extra "20" after the 8, as well as the extra info
                                areaInfo.HasGrass = true;
                                areaInfo.HasWater = true;
                                r.Advance(36);
                            }
                            else //If the length is 4
                            {
                                var bytes = r.ReadBytes(16).ToArray();
                                if (bytes[7] == 0) //If byte 7 equals 0
                                    areaInfo.HasGrass = true;
                                else //Else if the 2nd byte should be anything else (should always be 1)
                                    areaInfo.HasWater = true;
                            }
                        }
                        else //If the extra info flags aren't set, go back 4
                        {
                            r.Advance(-4);
                        }

                        t.AreaInfo[i] = areaInfo;
                    }

                    BOTWConsole.Log($"Offset after area iteration: {r.BaseStream.Position} (should be {t.FileBaseOffset + 16})");

                    //Get the number of filenames by getting how many bytes they take up out of the entire file size
                    var filenames_count = (r.BaseStream.Length - (t.FileBaseOffset + 16)) / 12;

                    BOTWConsole.Log($"Filename count: {filenames_count} (should be {t.AreaArrayLength})");

                    t.FileNames = new string[filenames_count];

                    r.BaseStream.Seek(t.FileBaseOffset + 16, SeekOrigin.Begin); // TODO: change this to 'current' later, or maybe even remove

                    for (int i = 0; i < filenames_count; i++)
                    {
                        string filename = new string(r.ReadChars(12));

                        t.FileNames[i] = filename;
                    }
                }

                return t;
            }
            else
            {
                throw new FileNotFoundException("Cannot find .tscb file to read.");
            }
        }

        /// <summary>
        /// Writes TSCB data to a byte array.
        /// </summary>
        /// <param name="tscb"><see cref="TSCB"/> that contains data to write.</param>
        /// <returns>Byte array containing the TSCB data.</returns>
        public static byte[] GetBytes(TSCB tscb)
        {
            List<byte> b = new List<byte>();

            //TSCB header
            b.AddRange(new byte[] {
                0x54, 0x53, 0x43, 0x42,
                0x0A, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x01
            });

            b.AddRange(BitConverter.GetBytes(tscb.FileBaseOffset).Reverse());
            b.AddRange(BitConverter.GetBytes(tscb.WorldScale).Reverse());
            b.AddRange(BitConverter.GetBytes(tscb.TerrainMaxHeight).Reverse());
            b.AddRange(BitConverter.GetBytes(tscb.MaterialInfoLength).Reverse());
            b.AddRange(BitConverter.GetBytes(tscb.AreaArrayLength).Reverse());

            // Padding
            b.AddRange(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });

            b.AddRange(BitConverter.GetBytes(tscb.TileSize).Reverse());

            b.AddRange(new byte[] { 0x00, 0x00, 0x00, 0x08 });

            // Add material info offsets
            b.AddRange(tscb.MaterialInfoOffsets);

            // Write material infos
            foreach (var mat in tscb.MaterialInfo)
            {
                b.AddRange(BitConverter.GetBytes(mat.MaterialIndex).Reverse());
                b.AddRange(BitConverter.GetBytes(mat.TextureU).Reverse());
                b.AddRange(BitConverter.GetBytes(mat.TextureV).Reverse());
                b.AddRange(BitConverter.GetBytes(mat.Unknown1).Reverse());
                b.AddRange(BitConverter.GetBytes(mat.Unknown2).Reverse());
            }

            b.AddRange(tscb.AreaArrayOffsets);

            foreach (var area in tscb.AreaInfo)
            {
                // Add area bytes
                b.AddRange(BitConverter.GetBytes(area.PositionX).Reverse());
                b.AddRange(BitConverter.GetBytes(area.PositionZ).Reverse());

                b.AddRange(BitConverter.GetBytes(area.AreaSize).Reverse());
                b.AddRange(BitConverter.GetBytes(area.MinTerrainHeight).Reverse());
                b.AddRange(BitConverter.GetBytes(area.MaxTerrainHeight).Reverse());
                b.AddRange(BitConverter.GetBytes(area.MinWaterHeight).Reverse());
                b.AddRange(BitConverter.GetBytes(area.MaxWaterHeight).Reverse());
                b.AddRange(BitConverter.GetBytes(area.Unknown1).Reverse());
                b.AddRange(BitConverter.GetBytes(area.FileBase).Reverse());
                b.AddRange(BitConverter.GetBytes(area.Unknown2).Reverse());
                b.AddRange(BitConverter.GetBytes(area.Unknown3).Reverse());
                b.AddRange(BitConverter.GetBytes(area.ReferenceExtra).Reverse());

                if (area.ReferenceExtra == 4)
                {
                    b.AddRange(BitConverter.GetBytes(area.ExtraInfoLength).Reverse());

                    if (area.ExtraInfoLength == 8)
                    {
                        b.AddRange(new byte[] {
                        0x00, 0x00, 0x00, 0x14,
                        0x00, 0x00, 0x00, 0x03,
                        0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x01,
                        0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x03,
                        0x00, 0x00, 0x00, 0x01,
                        0x00, 0x00, 0x00, 0x01,
                        0x00, 0x00, 0x00, 0x00
                    });
                    }
                    else
                    {
                        byte[] grass = new byte[] { 0x00, 0x00, 0x00, 0x00 };
                        byte[] water = new byte[] { 0x00, 0x00, 0x00, 0x01 };

                        b.AddRange(new byte[] { 0x00, 0x00, 0x00, 0x03 });
                        b.AddRange(area.HasGrass == true ? grass : water);
                        b.AddRange(new byte[] { 0x00, 0x00, 0x00, 0x01 });
                        b.AddRange(new byte[] { 0x00, 0x00, 0x00, 0x00 });
                    }
                }
            }

            foreach (string filename in tscb.FileNames)
            {
                b.AddRange(System.Text.Encoding.ASCII.GetBytes(filename));
            }

            return b.ToArray();
        }
    }
}

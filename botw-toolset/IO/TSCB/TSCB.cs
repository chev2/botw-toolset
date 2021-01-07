using BOTWToolset.Debugging;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace BOTWToolset.IO.TSCB
{
    /// <summary>
    /// Interacts with .tcsb files.
    /// </summary>
    static class TSCB
    {
        public static TSCBInfo ReadFile(string file)
        {
            if (File.Exists(file))
            {
                TSCBInfo t = new TSCBInfo();

                using (var r = new BinaryReader(File.Open(file, FileMode.Open)))
                {
                    // Set header info from file on the new TSCBInfo
                    t.SetHeaderInfo(r);

                    // Skip over mat info offsets
                    r.BaseStream.Seek((t.MaterialInfoLength * 4) + 4, SeekOrigin.Current);

                    BOTWConsole.Log($"Offset before material iteration: {r.BaseStream.Position}");

                    // Initialize mat info array with provided length
                    t.MaterialInfo = new MaterialInfo[t.MaterialInfoLength];

                    // Initialize every mat info, then add to the array
                    for (int i = 0; i < t.MaterialInfoLength; i++)
                    {
                        uint index = BitConverter.ToUInt32(r.ReadBytes(4).Reverse().ToArray(), 0); // Reverse byte order - use big endian
                        float tex_u = BitConverter.ToSingle(r.ReadBytes(4).Reverse().ToArray(), 0);
                        float tex_v = BitConverter.ToSingle(r.ReadBytes(4).Reverse().ToArray(), 0);
                        float unk_1 = BitConverter.ToSingle(r.ReadBytes(4).Reverse().ToArray(), 0);
                        float unk_2 = BitConverter.ToSingle(r.ReadBytes(4).Reverse().ToArray(), 0);

                        MaterialInfo matInfo = new MaterialInfo(index, tex_u, tex_v, unk_1, unk_2);

                        t.MaterialInfo[i] = matInfo;
                    }

                    BOTWConsole.Log($"Offset before area offset iteration: {r.BaseStream.Position}");

                    // Skip over area offsets (current position plus area array bytes)
                    r.BaseStream.Seek(t.AreaArrayLength * 4, SeekOrigin.Current);

                    BOTWConsole.Log($"Offset before area iteration: {r.BaseStream.Position}");

                    t.AreaInfo = new AreaInfo[t.AreaArrayLength];

                    for (int i = 0; i < t.AreaArrayLength; i++)
                    {
                        uint offset = (uint)r.BaseStream.Position;

                        float xpos = BitConverter.ToSingle(r.ReadBytes(4).Reverse().ToArray(), 0); // Reverse byte order - use big endian
                        float zpos = BitConverter.ToSingle(r.ReadBytes(4).Reverse().ToArray(), 0);
                        float area_size = BitConverter.ToSingle(r.ReadBytes(4).Reverse().ToArray(), 0);
                        float min_terrain_height = BitConverter.ToSingle(r.ReadBytes(4).Reverse().ToArray(), 0);
                        float max_terrain_height = BitConverter.ToSingle(r.ReadBytes(4).Reverse().ToArray(), 0);
                        float min_water_height = BitConverter.ToSingle(r.ReadBytes(4).Reverse().ToArray(), 0);
                        float max_water_height = BitConverter.ToSingle(r.ReadBytes(4).Reverse().ToArray(), 0);
                        uint unk_1 = BitConverter.ToUInt32(r.ReadBytes(4).Reverse().ToArray(), 0);

                        if (unk_1 == 0)
                        { // If this unknown isn't equal to 2, skip the extra byte coming after it
                            uint next_val = BitConverter.ToUInt32(r.ReadBytes(4).Reverse().ToArray(), 0);

                            if (next_val == 1) //if the next value is extra unneeded info
                            {
                                
                            } else //else, if the value is valid
                            {
                                r.BaseStream.Seek(-4, SeekOrigin.Current);
                            }
                        }

                        uint file_base = BitConverter.ToUInt32(r.ReadBytes(4).Reverse().ToArray(), 0);
                        uint unk_2 = BitConverter.ToUInt32(r.ReadBytes(4).Reverse().ToArray(), 0);
                        uint unk_3 = BitConverter.ToUInt32(r.ReadBytes(4).Reverse().ToArray(), 0);
                        uint ref_extra = BitConverter.ToUInt32(r.ReadBytes(4).Reverse().ToArray(), 0);

                        AreaInfo areaInfo = new AreaInfo(xpos, zpos, area_size, min_terrain_height, max_terrain_height, min_water_height,
                            max_water_height, unk_1, file_base, unk_2, unk_3, ref_extra);

                        areaInfo.Offset = offset;

                        uint extra_info_len = BitConverter.ToUInt32(r.ReadBytes(4).Reverse().ToArray(), 0); //Usually 0, 4, or 8

                        if (ref_extra == 4) {
                            if (extra_info_len == 8)
                            { //Skip the extra "20" after the 8, as well as the extra info
                                areaInfo.HasGrass = true;
                                areaInfo.HasWater = true;
                                r.BaseStream.Seek(36, SeekOrigin.Current);
                            }
                            else //If the length is 4
                            {
                                var bytes = r.ReadBytes(16).Reverse().ToArray();
                                if (bytes[1] == 0) //If the 2nd byte is 0
                                    areaInfo.HasGrass = true;
                                else //Else if the 2nd byte should be anything else (should always be 1)
                                    areaInfo.HasWater = true;
                            }
                        } else //If the extra info flags aren't set, go back 4
                        {
                            r.BaseStream.Seek(-4, SeekOrigin.Current);
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
    }
}

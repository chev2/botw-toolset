using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BOTWToolset.IO.SARC
{
    /// <summary>
    /// Stores info on a SARC archive file.
    /// More info found on the <see href="https://zeldamods.org/wiki/SARC">ZeldaMods wiki</see>.
    /// </summary>
    public class SARC
    {
        //SARC header
        public string Magic { get => _magic; set => _magic = value; }
        private string _magic;

        public ushort HeaderLength { get => _headerLength; set => _headerLength = value; }
        private ushort _headerLength;

        public bool IsBigEndian { get => _isBigEndian; set => _isBigEndian = value; }
        private bool _isBigEndian;

        public uint FileSize { get => _fileSize; set => _fileSize = value; }
        private uint _fileSize;

        public uint DataOffset { get => _dataOffset; set => _dataOffset = value; }
        private uint _dataOffset;

        public ushort Version { get => _version; set => _version = value; }
        private ushort _version;

        public SFAT SFAT;

        public SFNT SFNT;

        public byte[][] Files;

        /// <summary>
        /// Gets a <see cref="SARC"/> from an array of bytes.
        /// </summary>
        /// <param name="bytes">Array of bytes to get <see cref="SARC"/> info from.</param>
        /// <returns><see cref="SARC"/> with the stream's data.</returns>
        public static SARC FromBytes(byte[] bytes)
        {
            SARC s = new SARC();

            using (var r = new BinaryReaderBig(new MemoryStream(bytes)))
            {
                // SARC header
                s.Magic = new string(r.ReadChars(4));
                if (s.Magic != "SARC")
                    // TODO: raise an exception here instead of returning null
                    return null;

                s.HeaderLength = r.ReadUInt16();
                s.IsBigEndian = r.ReadUInt16() == 0xFEFF;
                s.FileSize = r.ReadUInt32();
                s.DataOffset = r.ReadUInt32();
                s.Version = r.ReadUInt16();

                // Skip 2 reserved bytes
                r.Advance(2);

                // SFAT header
                string sfat_magic = new string(r.ReadChars(4));
                if (sfat_magic != "SFAT")
                    // TODO: raise an exception here instead of returning null
                    return null;

                ushort sfat_headerlen = r.ReadUInt16();
                ushort sfat_nodecount = r.ReadUInt16();
                uint sfat_hashkey = r.ReadUInt32();

                // SFAT nodes
                SFATNode[] sfat_nodes = new SFATNode[sfat_nodecount];

                for (int i = 0; i < sfat_nodecount; i++)
                {
                    SFATNode sfan = new SFATNode(
                        r.ReadUInt32(), //hash
                        r.ReadUInt32(), //attributes
                        r.ReadUInt32(), //node file beginning
                        r.ReadUInt32() //node file end
                    );

                    sfat_nodes[i] = sfan;
                }

                s.SFAT = new SFAT
                {
                    Magic = sfat_magic,
                    HeaderLength = sfat_headerlen,
                    NodeCount = sfat_nodecount,
                    HashKey = sfat_hashkey,
                    Nodes = sfat_nodes
                };

                // SFNT header
                var sfnt_magic = new string(r.ReadChars(4));
                if (sfnt_magic != "SFNT")
                    // TODO: raise an exception here instead of returning null
                    return null;
                var sfnt_headerlen = r.ReadUInt16();

                r.Advance(2); // Skip 2 reserved bytes

                // Read every filename (null-terminated strings)
                List<string> file_names = new List<string>();
                List<byte> cur_string = new List<byte>();

                int bytes_to_iterate = (int)((s.DataOffset - r.BaseStream.Position) / 4);

                for (int i = 0; i < bytes_to_iterate; i++)
                {
                    byte[] four_bytes = r.ReadBytes(4);

                    if (Array.Exists(four_bytes, e => e == 0)) // If this four-byte array contains zeroes
                    {
                        // Remove the zeroes
                        byte[] bytes_filter = four_bytes.Where(x => x != 0).ToArray();

                        // Add the valid characters
                        foreach (var byte_s in bytes_filter)
                        {
                            cur_string.Add(byte_s);
                        }

                        file_names.Add(System.Text.Encoding.UTF8.GetString(cur_string.ToArray()));
                        cur_string.Clear();
                    }
                    else
                    {
                        cur_string.Add(four_bytes[0]);
                        cur_string.Add(four_bytes[1]);
                        cur_string.Add(four_bytes[2]);
                        cur_string.Add(four_bytes[3]);
                    }
                }

                s.SFNT = new SFNT
                {
                    Magic = sfnt_magic,
                    HeaderLength = sfnt_headerlen,
                    FileNames = file_names.ToArray()
                };

                s.Files = new byte[s.SFAT.Nodes.Length][]; // An array with byte[] arrays representing every file

                // Use the offsets of every file to read the file
                for (int i = 0; i < s.SFAT.Nodes.Length; i++)
                {
                    SFATNode f = s.SFAT.Nodes[i];

                    // Seek to the beginning of the node file
                    r.BaseStream.Seek(s.DataOffset + f.NodeFileDataBegin, SeekOrigin.Begin);

                    // Read the bytes from beginning to end
                    s.Files[i] = r.ReadBytes((int)(f.NodeFileDataEnd - f.NodeFileDataBegin));
                }
            }

            return s;
        }

        /// <summary>
        /// Gets an array of bytes from a <see cref="SARC"/>.
        /// </summary>
        /// <param name="sarc"><see cref="SARC"/> to convert to bytes.</param>
        /// <returns>byte[] containg the <see cref="SARC"/> data.</returns>
        public static byte[] ToBytes(SARC sarc)
        {
            List<byte> bytes = new List<byte>();

            // SARC header
            bytes.AddRange(new byte[] { 0x53, 0x41, 0x52, 0x43 }); // SARC magic header
            bytes.AddRange(new byte[] { 0x00, 0x14 }); // Header length, always 0x14
            bytes.AddRange(new byte[] { 0xFE, 0xFF }); // 0xFEFF - big endian, 0xFFFE - little endian
            bytes.AddRange(BitConverter.GetBytes(sarc.FileSize).Reverse()); // File size of the entire SARC, including headers
            bytes.AddRange(BitConverter.GetBytes(sarc.DataOffset).Reverse()); // Beginning of data offset
            bytes.AddRange(new byte[] { 0x01, 0x00 }); // Version number, always 0x0100
            bytes.AddRange(new byte[] { 0x00, 0x00 }); // Reserved

            // SFAT header
            bytes.AddRange(new byte[] { 0x53, 0x46, 0x41, 0x54 }); // SFAT magic header
            bytes.AddRange(new byte[] { 0x00, 0x0C }); // Header length, always 0xC
            bytes.AddRange(BitConverter.GetBytes(sarc.SFAT.NodeCount).Reverse());
            bytes.AddRange(new byte[] { 0x00, 0x00, 0x00, 0x65 }); // Hash key, always 0x65

            // SFAT node structures
            foreach (SFATNode node in sarc.SFAT.Nodes)
            {
                bytes.AddRange(BitConverter.GetBytes(node.FileNameHash).Reverse());
                bytes.AddRange(BitConverter.GetBytes(node.FileAttributes).Reverse());
                bytes.AddRange(BitConverter.GetBytes(node.NodeFileDataBegin).Reverse());
                bytes.AddRange(BitConverter.GetBytes(node.NodeFileDataEnd).Reverse());
            }

            // SFNT header
            bytes.AddRange(new byte[] { 0x53, 0x46, 0x4E, 0x54 }); // SFNT magic header
            bytes.AddRange(new byte[] { 0x00, 0x08 }); // Header length, always 0x8
            bytes.AddRange(new byte[] { 0x00, 0x00 }); // Reserved

            foreach (var filename in sarc.SFNT.FileNames)
            {
                var extra = filename.Length % 4; // Number of characters not in a 4-byte-aligned segment

                for (int i = 0; i < filename.Length + (4 - extra); i++)
                {
                    if (i < filename.Length) // If this is a valid char
                        bytes.Add((byte)filename[i]);
                    else // If not, add null
                        bytes.Add(0x00);
                }
            }

            foreach (var file_bytes in sarc.Files)
                bytes.AddRange(file_bytes);

            return bytes.ToArray();
        }

        /// <summary>
        /// Writes a <see cref="SARC"/>'s unpackaged files to a folder.
        /// </summary>
        /// <param name="sarc"><see cref="SARC"/> to get file data from.</param>
        /// <param name="folder">Folder to write the unpackaged files to.</param>
        public static void WriteFiles(SARC sarc, string folder)
        {
            //TODO: Finish this function
            byte[][] files = (byte[][])sarc.Files.Clone();

            for (int i = 0; i < files.Length; i++)
            {
                var file_name = sarc.SFNT.FileNames[i];

                // Get combined path (folder + file name)
                var file_path = Path.Combine(folder, file_name);

                // Write bytes to file
                File.WriteAllBytes(file_path, files[i]);
            }
        }
    }
}

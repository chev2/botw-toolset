using BOTWToolset.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BOTWToolset.IO.Yaz0
{
    public class Yaz0
    {
        public string Magic { get => _magic; set => _magic = value; }
        private string _magic;

        public uint UncompressedDataSize { get => _uncompressedDataSize; set => _uncompressedDataSize = value; }
        private uint _uncompressedDataSize;

        public uint DataAlignment { get => _dataAlignment; set => _dataAlignment = value; }
        private uint _dataAlignment;

        public byte[] Bytes;

        private static int s_num_bytes1, s_match_pos;
        private static bool s_prev_flag = false;

        public static Yaz0 ReadFile(string file)
        {
            if (File.Exists(file))
            {
                Yaz0 y = new Yaz0();

                // Use big-endian
                using (var r = new BinaryReaderBig(File.Open(file, FileMode.Open)))
                {
                    y.Magic = new string(r.ReadChars(4));
                    if (y.Magic != "Yaz0")
                        throw new InvalidMagicException("This file is not Yaz0-encoded.");

                    y.UncompressedDataSize = r.ReadUInt32();
                    y.DataAlignment = r.ReadUInt32();

                    // Seek back to beginning of file to capture all bytes
                    r.BaseStream.Seek(0, SeekOrigin.Begin);

                    // Capture all bytes
                    y.Bytes = r.ReadBytes((int)r.BaseStream.Length);
                }

                return y;
            }
            else
            {
                throw new FileNotFoundException("Cannot find Yaz0 file to read.");
            }
        }

        /// <summary>
        /// Decompresses a Yaz0-encoded array of bytes.
        /// </summary>
        /// <param name="bytes">The array of bytes to decompress.</param>
        /// <returns>byte[] containing the decompressed data.</returns>
        public static byte[] Decompress(byte[] bytes)
        {
            List<byte> de_bytes = new List<byte>();

            using (var r = new BinaryReaderBig(new MemoryStream(bytes)))
            {
                string magic = new string(r.ReadChars(4));
                if (magic != "Yaz0")
                    throw new InvalidMagicException("This file is not Yaz0-encoded.");

                uint de_size = r.ReadUInt32();
                /*var data_alignment = r.ReadUInt32();*/
                r.Advance(4);

                // Skip 4 empty bytes
                r.Advance(4);

                while (de_bytes.Count < de_size)
                {
                    byte bits = r.ReadByte();

                    BitArray ba = new BitArray(new byte[] { bits });

                    for (int i = 7; i > -1 && (de_bytes.Count < de_size); i--)
                    {
                        if (ba[i])
                        {
                            de_bytes.Add(r.ReadByte());
                        }
                        else
                        {
                            byte byte1 = r.ReadByte();
                            byte byte2 = r.ReadByte();

                            byte byte1_upper = (byte)(byte1 & 0x0F);
                            byte byte1_lower = (byte)(byte1 & 0xF0);
                            byte1_lower = (byte)(byte1_lower >> 4);

                            int final_offs = ((byte1_upper << 8) | byte2) + 1;
                            int final_len = (byte1_lower == 0) ? r.ReadByte() + 0x12 : byte1_lower + 2;

                            for (int j = 0; j < final_len; j++)
                            {
                                de_bytes.Add(de_bytes[de_bytes.Count - final_offs]);
                            }
                        }
                    }

                }
            }

            return de_bytes.ToArray();
        }

        /// <summary>
        /// Compresses a byte array using Yaz0-encoding.
        /// </summary>
        /// <param name="bytes">The bytes to encode.</param>
        /// <returns>Yaz0-encoded byte array.</returns>
        public static byte[] Compress(byte[] bytes)
        {
            if (System.Text.Encoding.ASCII.GetString(new byte[] { bytes[0], bytes[1], bytes[2], bytes[3] }) == "Yaz0")
                throw new InvalidMagicException("This file is already Yaz0-encoded.");

            List<byte> de_bytes = new List<byte>();

            uint uncompressed_size = (uint)bytes.Length;

            de_bytes.AddRange(new byte[] { 0x59, 0x61, 0x7A, 0x30 }); // Yaz0 magic
            de_bytes.AddRange(BitConverter.GetBytes(uncompressed_size).Reverse()); // Uncompressed size - reverse for endianness
            de_bytes.AddRange(new byte[] { 0x00, 0x00, 0x00, 0x00 }); // Data alignment
            de_bytes.AddRange(new byte[] { 0x00, 0x00, 0x00, 0x00 }); // End of header
            de_bytes.AddRange(Encode(bytes)); // Compressed data

            return de_bytes.ToArray();
        }

        private static byte[] Encode(byte[] bytes)
        {
            int src_pos = 0;

            byte[] dest = new byte[24]; // 8, 16, 24
            int dest_pos = 0;

            int valid_bit_cnt = 0;
            byte cur_code_byte = 0;

            List<byte> output = new List<byte>(); // Output bytes

            while (src_pos < bytes.Length)
            {
                CompressInner(bytes, src_pos, out int num_bytes, out int match_pos);

                if (num_bytes < 3)
                {
                    // Straight copy
                    dest[dest_pos] = bytes[src_pos];
                    src_pos++;
                    dest_pos++;

                    // Set flag for straight copy
                    cur_code_byte |= (byte)(0x80 >> valid_bit_cnt);
                }
                else
                {
                    // RLE
                    uint dist = (uint)(src_pos - match_pos - 1);
                    byte byte1, byte2, byte3;

                    // Requires 3 byte encoding
                    if (num_bytes >= 0x12)
                    {
                        byte1 = (byte)(0 | (dist >> 8));
                        byte2 = (byte)(dist & 0xFF);

                        dest[dest_pos++] = byte1;
                        dest[dest_pos++] = byte2;

                        if (num_bytes > 0xFF + 0x12)
                            num_bytes = 0xFF + 0x12;
                        byte3 = (byte)(num_bytes - 0x12);
                        dest[dest_pos++] = byte3;
                    }
                    else // 2 byte encoding
                    {
                        byte1 = (byte)((uint)((num_bytes - 2) << 4) | (dist >> 8));
                        byte2 = (byte)(dist & 0xFF);
                        dest[dest_pos++] = byte1;
                        dest[dest_pos++] = byte2;
                    }
                    src_pos += num_bytes;
                }

                valid_bit_cnt++;

                // If the block is filled
                if (valid_bit_cnt == 8)
                {
                    // Write the code byte
                    output.Add(cur_code_byte);

                    // Write any bytes in the dest buffer
                    for (int i = 0; i < dest_pos; i++)
                    {
                        output.Add(dest[i]);
                    }

                    cur_code_byte = 0;
                    valid_bit_cnt = 0;
                    dest_pos = 0;
                }
            }

            // If it didn't finish on a whole byte, add the last code byte.
            if (valid_bit_cnt > 0)
            {
                // Write the code byte
                output.Add(cur_code_byte);

                // Write any bytes in the dest buffer
                for (int i = 0; i < dest_pos; i++)
                {
                    output.Add(dest[i]);
                }
            }

            return output.ToArray();
        }

        private static void CompressInner(byte[] src, int src_pos, out int out_num_bytes, out int out_match_pos)
        {
            if (s_prev_flag)
            {
                out_match_pos = s_match_pos;
                s_prev_flag = false;
                out_num_bytes = s_num_bytes1;
                return;
            }

            s_prev_flag = false;
            SimpleRLEEncode(src, src_pos, out int num_bytes, out s_match_pos);
            out_match_pos = s_match_pos;

            if (num_bytes >= 3)
            {
                SimpleRLEEncode(src, src_pos + 1, out s_num_bytes1, out s_match_pos);

                if (s_num_bytes1 >= num_bytes + 2)
                {
                    num_bytes = 1;
                    s_prev_flag = true;
                }
            }

            out_num_bytes = num_bytes;
        }

        private static void SimpleRLEEncode(byte[] src, int src_pos, out int out_num_bytes, out int out_match_pos)
        {
            int start_pos = src_pos - 0x400;
            int num_bytes = 1;
            int match_pos = 0;

            if (start_pos < 0)
                start_pos = 0;

            // Search backwards for an already-encoded bit
            for (int i = start_pos; i < src_pos; i++)
            {
                int j;

                for (j = 0; j < src.Length - src_pos; j++)
                {
                    if (src[i + j] != src[j + src_pos])
                        break;
                }

                if (j > num_bytes)
                {
                    num_bytes = j;
                    match_pos = i;
                }
            }

            out_match_pos = match_pos;

            if (num_bytes == 2)
                num_bytes = 1;

            out_num_bytes = num_bytes;
        }
    }
}

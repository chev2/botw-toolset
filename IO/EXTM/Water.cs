﻿using System.IO;

namespace BOTWToolset.IO.EXTM
{
    /// <summary>
    /// Interacts with water data in an .extm file, used in conjunction with <see cref="TSCB.TSCB"/>.
    /// More info found on the <see href="https://zeldamods.org/wiki/Water.extm">ZeldaMods wiki</see>.
    /// </summary>
    class Water
    {
        public ushort Height { get => _height; set => _height = value; }
        private ushort _height;

        public ushort XAxisFlowRate { get => _xAxisFlowRate; set => _xAxisFlowRate = value; }
        private ushort _xAxisFlowRate;

        public ushort ZAxisFlowRate { get => _zAxisFlowRate; set => _zAxisFlowRate = value; }
        private ushort _zAxisFlowRate;

        public byte MaterialIndex { get => _matIndex; set => _matIndex = value; }
        private byte _matIndex;

        public byte MaterialIndexChecksum
        {
            get
            {
                if (_matIndex != 0)
                    return (byte)(_matIndex + 3);
                return _matIndex;
            }
        }

        /// <summary>
        /// Gets an array of <see cref="Water"/> from an array of bytes.
        /// </summary>
        /// <param name="bytes">The array of bytes to retrieve <see cref="Water"/> data from.</param>
        /// <returns><see cref="Water"/>[] data.</returns>
        public static Water[] FromBytes(byte[] bytes)
        {
            using (var r = new BinaryReaderBig(new MemoryStream(bytes)))
            {
                Water[] waters = new Water[r.BaseStream.Length / 8];

                for (int i = 0; i < waters.Length; i++)
                {
                    Water w = new Water
                    {
                        Height = r.ReadUInt16(),
                        XAxisFlowRate = r.ReadUInt16(),
                        ZAxisFlowRate = r.ReadUInt16()
                    };

                    // Skip material index checksum
                    r.BaseStream.Seek(1, SeekOrigin.Current);

                    w.MaterialIndex = r.ReadByte();

                    waters[i] = w;
                }

                return waters;
            }
        }
    }
}

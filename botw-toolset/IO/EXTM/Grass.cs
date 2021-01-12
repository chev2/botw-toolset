using System.IO;

namespace BOTWToolset.IO.EXTM
{
    /// <summary>
    /// Interacts with grass data in an .extm file, used in conjunction with <see cref="TSCB.TSCB"/>.
    /// More info found on the <see href="https://zeldamods.org/wiki/Grass.extm">ZeldaMods wiki</see>.
    /// </summary>
    class Grass
    {
        public byte Height { get => _height; set => _height = value; }
        private byte _height;

        public byte R { get => _r; set => _r = value; }
        private byte _r;

        public byte G { get => _g; set => _g = value; }
        private byte _g;

        public byte B { get => _b; set => _b = value; }
        private byte _b;

        /// <summary>
        /// Gets an array of <see cref="Grass"/> from an array of bytes.
        /// </summary>
        /// <param name="bytes">The array of bytes to retrieve <see cref="Grass"/> data from.</param>
        /// <returns><see cref="Grass"/>[] data.</returns>
        public static Grass[] FromBytes(byte[] bytes)
        {
            using (var r = new BinaryReaderBig(new MemoryStream(bytes)))
            {
                Grass[] grasses = new Grass[r.BaseStream.Length / 4];

                for (int i = 0; i < grasses.Length; i++)
                {
                    Grass g = new Grass
                    {
                        Height = r.ReadByte(),
                        R = r.ReadByte(),
                        G = r.ReadByte(),
                        B = r.ReadByte()
                    };

                    grasses[i] = g;
                }

                return grasses;
            }
        }
    }
}

using System.IO;

namespace BOTWToolset.IO.EXTM
{
    /// <summary>
    /// Stores info on grass data in an .extm file
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

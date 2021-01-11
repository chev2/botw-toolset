using System.IO;

namespace BOTWToolset.IO
{
    public class MATE
    {
        public byte Material0 { get => _material0; set => _material0 = value; }
        private byte _material0;

        public byte Material1 { get => _material1; set => _material1 = value; }
        private byte _material1;

        public byte BlendWeight { get => _blendWeight; set => _blendWeight = value; }
        private byte _blendWeight;

        public static MATE[] FromBytes(byte[] bytes)
        {
            using (var r = new BinaryReader(new MemoryStream(bytes)))
            {
                MATE[] mats = new MATE[r.BaseStream.Length / 4];

                for (int i = 0; i < mats.Length; i++)
                {
                    MATE m = new MATE
                    {
                        Material0 = r.ReadByte(),
                        Material1 = r.ReadByte(),
                        BlendWeight = r.ReadByte()
                    };

                    // Skip unknown byte
                    r.BaseStream.Seek(1, SeekOrigin.Current);

                    mats[i] = m;
                }

                return mats;
            }
        }
    }
}

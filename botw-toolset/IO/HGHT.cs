using System.IO;

namespace BOTWToolset.IO
{
    /// <summary>
    /// Contains data for .hght files.
    /// </summary>
    public class HGHT
    {
        public ushort[] Heights;

        public static HGHT FromBytes(byte[] bytes)
        {
            using (var r = new BinaryReader(new MemoryStream(bytes)))
            {
                HGHT h = new HGHT
                {
                    Heights = new ushort[r.BaseStream.Length / 2]
                };

                for (int i = 0; i < h.Heights.Length; i++)
                {
                    h.Heights[i] = r.ReadUInt16();
                }

                return h;
            }
        }
    }
}

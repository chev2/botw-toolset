using System.IO;

namespace BOTWToolset.IO
{
    /// <summary>
    /// Contains data for .hght files.
    /// </summary>
    public class HGHT
    {
        public ushort[] Heights;

        /// <summary>
        /// Retrieves a <see cref="HGHT"/> array from a set of bytes.
        /// </summary>
        /// <param name="bytes">The array of bytes to read.</param>
        /// <returns><see cref="HGHT"/> array.</returns>
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

using System;
using System.IO;
using System.Linq;
using System.Text;

namespace BOTWToolset.IO
{
    /// <summary>
    /// Big-endian variant of BinaryReader
    /// </summary>
    public class BinaryReaderBig : BinaryReader
    {
        public BinaryReaderBig(Stream stream) : base(stream) { }
        public BinaryReaderBig(Stream stream, Encoding encoding) : base(stream, encoding) { }
        public BinaryReaderBig(Stream stream, Encoding encoding, bool leaveOpen) : base(stream, encoding, leaveOpen) { }

        /// <summary>
        /// Advances the stream by the specified number of bytes.
        /// </summary>
        /// <param name="bytes">The number of bytes to advance by.</param>
        public void Advance(long bytes)
        {
            BaseStream.Seek(bytes, SeekOrigin.Current);
        }

        public override short ReadInt16()
        {
            return BitConverter.ToInt16(base.ReadBytes(2).Reverse().ToArray(), 0);
        }

        public override ushort ReadUInt16()
        {
            return BitConverter.ToUInt16(base.ReadBytes(2).Reverse().ToArray(), 0);
        }

        public override int ReadInt32()
        {
            return BitConverter.ToInt32(base.ReadBytes(4).Reverse().ToArray(), 0);
        }

        public override uint ReadUInt32()
        {
            return BitConverter.ToUInt32(base.ReadBytes(4).Reverse().ToArray(), 0);
        }

        public override long ReadInt64()
        {
            return BitConverter.ToInt64(base.ReadBytes(8).Reverse().ToArray(), 0);
        }
        public override ulong ReadUInt64()
        {
            return BitConverter.ToUInt64(base.ReadBytes(8).Reverse().ToArray(), 0);
        }

        public override float ReadSingle()
        {
            return BitConverter.ToSingle(base.ReadBytes(4).Reverse().ToArray(), 0);
        }

        public override double ReadDouble()
        {
            return BitConverter.ToDouble(base.ReadBytes(8).Reverse().ToArray(), 0);
        }
    }
}

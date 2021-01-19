using System.IO;
using System.Text;

//TODO: reverse bytes on each function

namespace BOTWToolset.IO
{
    /// <summary>
    /// Big-endian variant of BinaryWriter.
    /// </summary>
    public class BinaryWriterBig : BinaryWriter
    {
        public BinaryWriterBig(Stream output) : base(output) { }
        public BinaryWriterBig(Stream output, Encoding encoding) : base(output, encoding) { }
        public BinaryWriterBig(Stream output, Encoding encoding, bool leaveOpen) : base(output, encoding, leaveOpen) { }

        public override void Write(short value)
        {
            base.Write(value);
        }

        public override void Write(ushort value)
        {
            base.Write(value);
        }

        public override void Write(int value)
        {
            base.Write(value);
        }

        public override void Write(uint value)
        {
            base.Write(value);
        }

        public override void Write(long value)
        {
            base.Write(value);
        }

        public override void Write(ulong value)
        {
            base.Write(value);
        }

        public override void Write(float value)
        {
            base.Write(value);
        }

        public override void Write(double value)
        {
            base.Write(value);
        }
    }
}

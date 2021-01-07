using System;
using System.IO;
using System.Linq;

namespace BOTWToolset.IO.TSCB
{
    /// <summary>
    /// Contains data for a TCSB file.
    /// </summary>
    public class TSCBInfo
    {
        public const byte HeaderLength = 48; // Length of the header, in bytes

        public string Signature { get => _signature; set => _signature = value; }
        private string _signature;

        public uint Version { get => _version; set => _version = value; }
        private uint _version;

        public uint FileBaseOffset { get => _fileBaseOffset; set => _fileBaseOffset = value; }
        private uint _fileBaseOffset;

        public float WorldScale { get => _worldScale; set => _worldScale = value.Clamp(0f, 800.0f); }
        private float _worldScale;

        public float TerrainMaxHeight { get => _terrainMaxHeight; set => _terrainMaxHeight = value.Clamp(0f, 800.0f); }
        private float _terrainMaxHeight;

        public uint MaterialInfoLength { get => _materialInfoLength; set => _materialInfoLength = value; }
        private uint _materialInfoLength;

        public MaterialInfo[] MaterialInfo;

        public uint AreaArrayLength { get => _areaArrayLength; set => _areaArrayLength = value; }
        private uint _areaArrayLength;

        public AreaInfo[] AreaInfo;

        public float TileSize;

        public string[] FileNames;

        public void SetHeaderInfo(BinaryReader r)
        {
            Signature = new string(r.ReadChars(4));
            Version = r.ReadUInt32();

            r.BaseStream.Seek(4, SeekOrigin.Current); // Skip 4 bytes of "00 00 00 01"

            FileBaseOffset = BitConverter.ToUInt32(r.ReadBytes(4).Reverse().ToArray(), 0); // Reverse byte order - use big endian
            WorldScale = BitConverter.ToSingle(r.ReadBytes(4).Reverse().ToArray(), 0);
            TerrainMaxHeight = BitConverter.ToSingle(r.ReadBytes(4).Reverse().ToArray(), 0);
            MaterialInfoLength = BitConverter.ToUInt32(r.ReadBytes(4).Reverse().ToArray(), 0);
            AreaArrayLength = BitConverter.ToUInt32(r.ReadBytes(4).Reverse().ToArray(), 0);

            r.BaseStream.Seek(8, SeekOrigin.Current);// Skip 8 bytes of padding

            TileSize = BitConverter.ToSingle(r.ReadBytes(4).Reverse().ToArray(), 0);

            r.BaseStream.Seek(4, SeekOrigin.Current); // Skip 4 bytes of "00 00 00 08"
        }
    }
}

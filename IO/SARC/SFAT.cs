namespace BOTWToolset.IO.SARC
{
    /// <summary>
    /// Stores info on the SFAT header (SARC file allocation table), used in conjunction with <see cref="SARC"/>.
    /// </summary>
    public struct SFAT
    {
        public string Magic { get => _magic; set => _magic = value; }
        private string _magic;

        public ushort HeaderLength { get => _headerLength; set => _headerLength = value; }
        private ushort _headerLength;

        public ushort NodeCount { get => _nodeCount; set => _nodeCount = value; }
        private ushort _nodeCount;

        public uint HashKey { get => _hashKey; set => _hashKey = value; }
        private uint _hashKey;

        public SFATNode[] Nodes;
    }
}

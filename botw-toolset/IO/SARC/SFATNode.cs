namespace BOTWToolset.IO.SARC
{
    /// <summary>
    /// Contains information on stored archive files in the SARC, used in conjunction with <see cref="SFAT"/> and <see cref="SARC"/>.
    /// </summary>
    public struct SFATNode
    {
        public uint FileNameHash { get => _fileNameHash; set => _fileNameHash = value; }
        private uint _fileNameHash;

        public uint FileAttributes { get => _fileAttributes; set => _fileAttributes = value; }
        private uint _fileAttributes;

        public uint NodeFileDataBegin { get => _nodeFileDataBegin; set => _nodeFileDataBegin = value; }
        private uint _nodeFileDataBegin;

        public uint NodeFileDataEnd { get => _nodeFileDataEnd; set => _nodeFileDataEnd = value; }
        private uint _nodeFileDataEnd;

        public SFATNode(uint file_name_hash, uint file_attrs, uint node_file_begin, uint node_file_end)
        {
            _fileNameHash = file_name_hash;
            _fileAttributes = file_attrs;
            _nodeFileDataBegin = node_file_begin;
            _nodeFileDataEnd = node_file_end;
        }
    }
}

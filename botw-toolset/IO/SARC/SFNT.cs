namespace BOTWToolset.IO.SARC
{
    public struct SFNT
    {
        public string Magic { get => _magic; set => _magic = value; }
        private string _magic;

        public ushort HeaderLength { get => _headerLength; set => _headerLength = value; }
        private ushort _headerLength;

        public string[] FileNames;
    }
}

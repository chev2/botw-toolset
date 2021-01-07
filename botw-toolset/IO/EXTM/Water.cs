namespace BOTWToolset.IO.EXTM
{
    /// <summary>
    /// Stores info on water data in an .extm file
    /// </summary>
    class Water
    {
        public ushort Height { get => _height; set => _height = value; }
        private ushort _height;

        public ushort XAxisFlowRate { get => _xAxisFlowRate; set => _xAxisFlowRate = value; }
        private ushort _xAxisFlowRate;

        public ushort ZAxisFlowRate { get => _zAxisFlowRate; set => _zAxisFlowRate = value; }
        private ushort _zAxisFlowRate;

        public byte MaterialIndex { get => _matIndex; set => _matIndex = value; }
        private byte _matIndex;

        public byte MaterialIndexChecksum
        {
            get
            {
                if (_matIndex != 0)
                    return (byte)(_matIndex + 3);
                return _matIndex;
            }
        }
    }
}

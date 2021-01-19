namespace BOTWToolset.IO.TSCB
{
    /// <summary>
    /// Stores material info for a material declared in a .tscb file.
    /// </summary>
    public class MaterialInfo
    {
        public uint MaterialIndex { get => _materialIndex; set => _materialIndex = value; }
        private uint _materialIndex;

        public float TextureU { get => _textureU; set => _textureU = value; }
        private float _textureU;

        public float TextureV { get => _textureV; set => _textureV = value; }
        private float _textureV;

        public float Unknown1 { get => _unknown1; set => _unknown1 = value; }
        private float _unknown1;

        public float Unknown2 { get => _unknown2; set => _unknown2 = value; }
        private float _unknown2;

        public MaterialInfo(uint mat_index, float tex_u, float tex_v, float unk_1, float unk_2)
        {
            MaterialIndex = mat_index;
            TextureU = tex_u;
            TextureV = tex_v;
            Unknown1 = unk_1;
            Unknown2 = unk_2;
        }
    }
}

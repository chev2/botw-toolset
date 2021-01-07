using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOTWToolset.IO.EXTM
{
    /// <summary>
    /// Stores info on grass data in an .extm file
    /// </summary>
    class Grass
    {
        public byte Height { get => _height; set => _height = value; }
        private byte _height;

        public byte R { get => _r; set => _r = value; }
        private byte _r;

        public byte G { get => _g; set => _g = value; }
        private byte _g;

        public byte B { get => _b; set => _b = value; }
        private byte _b;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Digger
{
    class Map
    {
        public const int WIDTH = 20;
        public const int HEIGHT = 12;
        private int xSize;
        private int ySize;
        private Field[,] fields;

        private static int level;
        public static Map getInstance(int level)
        {
            return new Map();
        }
    }
}

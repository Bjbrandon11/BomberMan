using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BomberMan
{
    class Tile1
    {
        public string TileSheetName;
        public int TileSheetIndex;

        public const int Width = 32;
        public const int Height = 32;
        public static readonly Vector2 size = new Vector2(Width, Height);

        public Tile1(string tileSheetName, int tileSheetIndex)
        {
            TileSheetName = tileSheetName;
            TileSheetIndex = tileSheetIndex;
        }
    }
}

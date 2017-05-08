using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BomberMan
{
    class GameHolder
    {
        public static Game game { get; set; }
        public static SpriteBatch spritebatch { get; set; }
        public static Layout level { get; set; }
    }
}

<<<<<<< HEAD
﻿using Microsoft.Xna.Framework;
using System;
=======
﻿using System;
>>>>>>> refs/remotes/origin/master
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BomberMan
{
    class Block
    {
<<<<<<< HEAD
        public const int SIZE= 108;
        public readonly bool breakable;
        public bool broken;
        Tile text;
        Rectangle hitBox;
        public Block(int x,int y):this(x,y, new Tile(0, 0, 32, 32, Tile.TextureList["Tiles/Block_Invin"]),false){}
        public Block(int x, int y,Tile text,bool breakable)
        {
            this.text = text;
            hitBox = new Rectangle(x, y, SIZE, SIZE);
            this.breakable = breakable;
            broken = false;
        }
        public bool Intersects(Rectangle rec) { return rec.Intersects(hitBox); }
        public void Draw()
        {
            text.Draw(hitBox);
        }

=======
        public void placeBomb(Bomb receive)
        {
            this.item = receive;
        }
>>>>>>> refs/remotes/origin/master
    }
}

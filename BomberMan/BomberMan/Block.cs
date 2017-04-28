using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BomberMan
{
    class Block
    {
        Entity item;
        public const int SIZE = 108;
        //public readonly bool breakable;
        //public bool broken;

        public Tile text;
        public Rectangle hitBox;
        public BlockState currentState;

        public Block(int x, int y) : this(x, y, new Tile(0, 0, 32, 32, Tile.TextureList["Tiles/Block_Invin"]), BlockState.Passable) { }
        public Block(int x, int y, Tile text, BlockState state)

        {
            this.text = text;
            hitBox = new Rectangle(x, y, SIZE, SIZE);
            //this.breakable = breakable;
            //broken = false;
            this.currentState = state;
        }

        public void placeBomb(Bomb receive)
        {
            this.item = receive;
        }
        
        public bool Intersects(Rectangle rec) { return rec.Intersects(hitBox); }
        public void Draw()
        {
            text.Draw(hitBox);
        }

    }
}

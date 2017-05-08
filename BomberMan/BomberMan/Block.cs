using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BomberMan
{
    public class Block
    {
        Entity item;
        public const int SIZE = 32;
        //public readonly bool breakable;
        //public bool broken;

        public Tile text;
        public Rectangle hitBox;
        public BlockState currentState;

        public Block(int x, int y) : this(x, y, new Tile(0, 0, 32, 32, Tile.TextureList["Block_Invin"]), BlockState.Passable) { }
        public Block(int x,int y,BlockState state):this(x,y,null,state)
        {
            currentState = state;
            switch(state)
            {
                case BlockState.Impassable: text = new Tile(0, 0, 32, 32, Tile.TextureList["Block_Invin"]);break;
                case BlockState.Breakable: text = new Tile(0, 0, 32, 32, Tile.TextureList["break"]); break;
                case BlockState.Spawn: text = new Tile(0, 0, 32, 32, Tile.TextureList["Block_Invin"]); break;
            }
        }
        public Block(int x, int y, Tile text, BlockState state)

        {
            this.text = text;
            hitBox = new Rectangle(x, y, (int)(SIZE*Game1.scaleFrom32),(int)(SIZE*Game1.scaleFrom32));
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
            if(currentState!=BlockState.Spawn)
            text.Draw(hitBox);
        }

    }
}

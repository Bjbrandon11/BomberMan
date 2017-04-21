using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BomberMan
{
    class Animation
    {
        private Tile[] tiles;
        private int fpt;
        private int currentFrames;
        public Animation(Tile[] tiles,int FramesPerTile)
        {
            this.tiles = tiles;
            fpt=FramesPerTile;
            currentFrames = 0;
        }
        public void Update()
        {
            currentFrames++;
            if (currentFrames+1 >= fpt * tiles.Length)
                currentFrames = 0;
        }
        public void Draw(Rectangle rect,Color color)
        {
            tiles[currentFrames / fpt].Draw(rect, color);
        }
    }
}

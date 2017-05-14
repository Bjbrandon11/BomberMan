using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BomberMan
{
    
    public class Animation
    {
        public static Animation Bomb; 
        public static void LoadContent()
        {
            Bomb = new Animation(((Tile[])Tile.bombAnimation.Clone()), 3);
        }
        public Tile[] tiles;
        public int fpt;
        public int completed;
        public int currentFrames;
        public Animation(Tile[] tileList,int FramesPerTile)
        {
            this.tiles = tileList;
            fpt=FramesPerTile;
            currentFrames = 0;
            completed = 0;
        }
        public void Update()
        {
            currentFrames++;
            if (currentFrames >= fpt * tiles.Length)
            {
                currentFrames = 0;
                completed++;
            }
        }
        public Animation Clone() { return new Animation((Tile[])tiles.Clone(), fpt); }
        public Animation Clone(int frames) { return new Animation((Tile[])tiles.Clone(), frames); }
        public void Draw(Rectangle rect,Color color)
        {
            tiles[currentFrames / fpt].Draw(rect, color);
        }
    }
}

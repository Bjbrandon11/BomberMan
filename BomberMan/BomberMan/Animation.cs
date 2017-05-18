using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BomberMan
{
    
    public class Animation
    {
        public static Animation Bomb,Walk_Side,Walk_Up,Walk_Down; 
        public static void LoadContent()
        {
            Tile[] temp1 = new Tile[4];
            Tile[] temp2 = new Tile[4];
            Tile[] temp3 = new Tile[4];
            Tile[] temp = new Tile[30];
            for (int i = 0; i < temp.Length; i++)
                temp[i] = new Tile(i * 16, 0, 16, 16, Tile.TextureList["Explosion-sheet"]);
            Bomb = new Animation(temp, 3);
            temp = new Tile[4];
            for (int i = 0; i < temp.Length; i++)
                temp[i] = new Tile(i * 32, 0, 32, 32, Tile.TextureList["Walk_Side"]);
            Walk_Side = new Animation(temp,7);

            for (int i = 0; i < temp.Length; i++)
                temp[i] = new Tile(i * 32, 0, 32, 32, Tile.TextureList["Walk_Up"]);
            Walk_Up = new Animation(temp, 7);

            for (int i = 0; i < temp.Length; i++)
                temp[i] = new Tile(i * 32, 0, 32, 32, Tile.TextureList["Walk_Down"]);
            Walk_Down = new Animation(temp, 7);
        }
        public Tile[] tiles;
        public int fpt;
        public int completed;
        public int currentFrames;
        public enum AnimPlayState { Pause,Play,Reverse}
        public bool Flipped;
        public AnimPlayState pState;
        public Animation(Tile[] tileList,int FramesPerTile)
        {
            tiles = (Tile[])tileList.Clone();
            fpt=FramesPerTile;
            currentFrames = 0;
            completed = 0;
            pState = AnimPlayState.Play;
            Flipped = false;
        }
        public void Update()
        {
            if(pState != AnimPlayState.Pause)
            currentFrames++;
            if (currentFrames >= fpt * tiles.Length)
            {
                currentFrames = 0;
                completed++;
            }
        }
        public void Restart() { currentFrames = 0; }
        public void PRestart()
        {
            currentFrames = 0;
            pState = AnimPlayState.Pause;
        }
        public Animation Clone() { return new Animation((Tile[])tiles.Clone(), fpt); }
        public Animation Clone(int frames) { return new Animation((Tile[])tiles.Clone(), frames); }
        public bool Equals(Animation other) { return tiles[0].Equals(other.tiles[0]); }
        public void Draw(Rectangle rect,Color color)
        {
            if(pState!=AnimPlayState.Reverse)
                tiles[currentFrames / fpt].Draw(rect, color,Flipped);
            if (pState == AnimPlayState.Reverse)
                tiles[tiles.Length-1-(currentFrames / fpt)].Draw(rect, color,Flipped);
        }
    }
}

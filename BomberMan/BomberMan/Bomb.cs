using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BomberMan
{
    public class Bomb : Entity
    {
        /// <summary>
        /// instanceVariables
        /// </summary>
        Timer explode;
        readonly int Range;
        //public bool isExplosionFinished;

        int SIZE = (int)(16*Game1.scaleFrom32*1.5);
        Animation anim;

        public Block[,] Maze;


        public Bomb(Point p, Timer time, int range) : base(p)
        {
            Range = range;
            //isExplosionFinished = false;
            explode = time;
            base.hitBox = new Rectangle(p.X - SIZE / 2, p.Y - SIZE / 2, SIZE, SIZE);
            anim = Animation.Bomb.Clone(4);
        }
        public override void Update()
        {
            anim.Update();
            explode.Update();
            if (explode.isDone())
            {
                
            }
            if (anim.completed > 0)
            {
                Explosion();
                Game1.EntityList.Remove(this);
            }
            
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            anim.Draw(base.hitBox, Color.White);
        }

        public void Explosion()
        {
            ///<remarks>
            ///Based on what the level is called change the name as needed.
            /// 
            ///</remarks>
            //Maze[MazeIndex.X, MazeIndex.Y]
            //bool isBlocked = true;
            Maze = GameHolder.level.tiles;
            
            
            for (int i = 0; i <= Range; i++)
            {
                int x = (int)((double)hitBox.Center.X / (32 * Game1.scaleFrom32)) + i;
                int y = (int)((double)hitBox.Center.Y / (32 * Game1.scaleFrom32));
                if (x >= Maze.GetLength(0))
                    break;
                Block b = Maze[x, y];
                if (b.currentState == BlockState.Breakable )
                {
                    //isBlocked = true;
                    b.newState(new Tile(0, 0, 32, 32, Tile.TextureList["EXP_END_2"]), BlockState.Explosion);
                    break;
                }
                else if(Maze[x + 1, y].currentState == BlockState.Impassable)
                {
                    b.newState(new Tile(0, 0, 32, 32, Tile.TextureList["EXP_END_2"]), BlockState.Explosion);
                    break;
                }
                else if (b.currentState != BlockState.Impassable)
                    if(i+1 > Range && x+1 >= Maze.GetLength(1))
                        b.newState(new Tile(0, 0, 32, 32, Tile.TextureList["EXP_END_2"]), BlockState.Explosion);
                    else
                        b.newState(new Tile(0, 0, 32, 32, Tile.TextureList["EXP_CONNECT_2"]), BlockState.Explosion);
            }
            for (int i = 0; i >= -Range; i--)
            {
                int x = (int)((double)hitBox.Center.X / (32 * Game1.scaleFrom32)) + i;
                int y = (int)((double)hitBox.Center.Y / (32 * Game1.scaleFrom32));
                if (x <0)
                    break;
                Block b = Maze[x, y];
                if (b.currentState == BlockState.Breakable)
                {
                    //isBlocked = true;
                    b.newState(new Tile(0,0,32,32,Tile.TextureList["EXP_END_4"]),BlockState.Explosion);
                    break;
                }
                else if (Maze[x -1, y].currentState == BlockState.Impassable)
                {
                    b.newState(new Tile(0, 0, 32, 32, Tile.TextureList["EXP_END_4"]), BlockState.Explosion);
                    break;
                }
                else if (b.currentState != BlockState.Impassable)
                    if (i + 1 > Range && x + 1 >= Maze.GetLength(1))
                        b.newState(new Tile(0, 0, 32, 32, Tile.TextureList["EXP_END_4"]), BlockState.Explosion);
                    else
                        b.newState(new Tile(0, 0, 32, 32, Tile.TextureList["EXP_CONNECT_2"]), BlockState.Explosion);
            }
            for (int i = 0; i <= Range ; i++)
            {
                int x = (int)((double)hitBox.Center.X / (32 * Game1.scaleFrom32));
                int y = (int)((double)hitBox.Center.Y / (32 * Game1.scaleFrom32))+i;
                if (y >= Maze.GetLength(1))
                    break;
                Block b = Maze[x, y];
                if (b.currentState == BlockState.Breakable)
                {
                    //isBlocked = true;
                    b.newState(new Tile(0, 0, 32, 32, Tile.TextureList["EXP_END_3"]), BlockState.Explosion);
                    break;
                }
                else if (Maze[x, y+1].currentState == BlockState.Impassable)
                {
                    b.newState(new Tile(0, 0, 32, 32, Tile.TextureList["EXP_END_3"]), BlockState.Explosion);
                    break;
                }
                else if (b.currentState != BlockState.Impassable)
                    if (i + 1 > Range && x + 1 >= Maze.GetLength(1))
                        b.newState(new Tile(0, 0, 32, 32, Tile.TextureList["EXP_END_3"]), BlockState.Explosion);
                    else
                        b.newState(new Tile(0, 0, 32, 32, Tile.TextureList["EXP_CONNECT_1"]), BlockState.Explosion);
            }
            for (int i = 0; i >= -Range ; i--)
            {
                int x = (int)((double)hitBox.Center.X / (32 * Game1.scaleFrom32));
                int y = (int)((double)hitBox.Center.Y / (32 * Game1.scaleFrom32))+i;
                if (y <0)
                    break;
                Block b = Maze[x, y];
                if (b.currentState == BlockState.Breakable)
                {
                    //isBlocked = true;
                    b.newState(new Tile(0, 0, 32, 32, Tile.TextureList["EXP_END_1"]), BlockState.Explosion);
                    break;
                }
                else if (Maze[x, y-1].currentState == BlockState.Impassable)
                {
                    b.newState(new Tile(0, 0, 32, 32, Tile.TextureList["EXP_END_1"]), BlockState.Explosion);
                    break;
                }
                else if (b.currentState != BlockState.Impassable)
                    if (i+1 > Range && x + 1 >= Maze.GetLength(1))
                        b.newState(new Tile(0, 0, 32, 32, Tile.TextureList["EXP_END_1"]), BlockState.Explosion);
                    else
                        b.newState(new Tile(0, 0, 32, 32, Tile.TextureList["EXP_CONNECT_1"]), BlockState.Explosion);
            }
            Maze[(int)(hitBox.Center.X / (32 * Game1.scaleFrom32)), (int)(hitBox.Center.Y / (32 * Game1.scaleFrom32))].newState(new Tile(0, 0, 32, 32, Tile.TextureList["EXP_CENTER"]), BlockState.Explosion);

        }

    }
}

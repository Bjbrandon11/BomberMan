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

        const int SIZE = 48;
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
            if (explode.Update())
            {
                Explosion();
            }
            if (anim.completed > 0)
            {
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
            for (int i = 1; i <= Range; i++)
            {
                if (Maze[MazeIndex.X + i, MazeIndex.Y].currentState == BlockState.Breakable)
                {
                    //isBlocked = true;
                    Maze[MazeIndex.X + i, MazeIndex.Y].currentState = BlockState.Explosion;
                    break;
                }
                else
                    Maze[MazeIndex.X + i, MazeIndex.Y].currentState = BlockState.Explosion;
            }
            for (int i = -1; i >= -Range; i--)
            {
                if (Maze[MazeIndex.X + i, MazeIndex.Y].currentState == BlockState.Breakable)
                {
                    //isBlocked = true;
                    Maze[MazeIndex.X + i, MazeIndex.Y].currentState = BlockState.Explosion;
                    break;
                }
                else
                    Maze[MazeIndex.X + i, MazeIndex.Y].currentState = BlockState.Explosion;
            }
            for (int i = 1; i <= Range; i++)
            {
                if (Maze[MazeIndex.X, MazeIndex.Y + i].currentState == BlockState.Breakable)
                {
                    //isBlocked = true;
                    Maze[MazeIndex.X, MazeIndex.Y + i].currentState = BlockState.Explosion;
                    break;
                }
                else
                    Maze[MazeIndex.X, MazeIndex.Y + i].currentState = BlockState.Explosion;
            }
            for (int i = -1; i >= -Range; i--)
            {
                if (Maze[MazeIndex.X, MazeIndex.Y + i].currentState == BlockState.Breakable)
                {
                    //isBlocked = true;
                    Maze[MazeIndex.X, MazeIndex.Y + i].currentState = BlockState.Explosion;
                    break;
                }
                else
                    Maze[MazeIndex.X, MazeIndex.Y + i].currentState = BlockState.Explosion;
            }


        }

    }
}

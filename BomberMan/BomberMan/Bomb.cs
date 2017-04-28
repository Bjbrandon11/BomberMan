using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BomberMan
{
    class Bomb : Entity
    {
        /// <summary>
        /// instanceVariables
        /// </summary>
        readonly int HorizontalRange;
        readonly int VerticalRange;

        const int SIZE = 48;
        Animation anim;

        public Block[,] Maze;


        public Bomb(Point p, int x, int y) : base(p)
        {
            HorizontalRange = x;
            VerticalRange = y;
            base.hitBox = new Rectangle(p.X-SIZE/2,p.Y-SIZE/2,SIZE,SIZE);
            anim = Animation.Bomb.Clone(4);
        }
        public override void Update()
        {
            anim.Update();
            
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            anim.Draw(base.hitBox, Color.White);
        }

        public void Explode()
        {
            ///<remarks>
            ///Based on what the level is called change the name as needed.
            /// 
            ///</remarks>
            //Maze[MazeIndex.X, MazeIndex.Y]
            //bool isBlocked = true;
            for (int i = 1; i <= HorizontalRange; i++)
            {
                if (Maze[MazeIndex.X + i, MazeIndex.Y].currentState == Block.BlockState.Breakable)
                {
                    //isBlocked = true;
                    Maze[MazeIndex.X + i, MazeIndex.Y].currentState = Block.BlockState.Explosion;
                    break;
                }
                else
                    Maze[MazeIndex.X + i, MazeIndex.Y].currentState = Block.BlockState.Explosion;
            }
            for (int i = -1; i >= -HorizontalRange; i--)
            {
                if (Maze[MazeIndex.X + i, MazeIndex.Y].currentState == Block.BlockState.Breakable)
                {
                    //isBlocked = true;
                    Maze[MazeIndex.X + i, MazeIndex.Y].currentState = Block.BlockState.Explosion;
                    break;
                }
                else
                    Maze[MazeIndex.X + i, MazeIndex.Y].currentState = Block.BlockState.Explosion;
            }
            for (int i = 1; i <= VerticalRange; i++)
            {
                if (Maze[MazeIndex.X, MazeIndex.Y + i].currentState == Block.BlockState.Breakable)
                {
                    //isBlocked = true;
                    Maze[MazeIndex.X, MazeIndex.Y + i].currentState = Block.BlockState.Explosion;
                    break;
                }
                else
                    Maze[MazeIndex.X, MazeIndex.Y + i].currentState = Block.BlockState.Explosion;
            }
            for (int i = -1; i >= -VerticalRange; i--)
            {
                if (Maze[MazeIndex.X, MazeIndex.Y + i].currentState == Block.BlockState.Breakable)
                {
                    //isBlocked = true;
                    Maze[MazeIndex.X, MazeIndex.Y + i].currentState = Block.BlockState.Explosion;
                    break;
                }
                else
                    Maze[MazeIndex.X, MazeIndex.Y + i].currentState = Block.BlockState.Explosion;

        }

    }
}

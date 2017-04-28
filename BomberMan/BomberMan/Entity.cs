using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BomberMan
{
    abstract class Entity
    {
        
        private int Height;
        private int Width;
        private Dictionary<String,Animation> animations;

        public Rectangle hitBox;

        protected Point MazeIndex;

        public Entity(Point p)
        {
            MazeIndex = p;
        }
        public Entity(Vector2 v) : this(new Point((int)v.X, (int)v.Y)) { }
        public Entity(int x, int y) : this(new Point(x, y)) { }
        public void setAnimations(Dictionary<String, Animation> anims){animations = anims;}

        public bool Intersects(Rectangle rect) { return hitBox.Intersects(rect); }
        public abstract void Update();
        public abstract void Draw(SpriteBatch spritebatch);
    }
}

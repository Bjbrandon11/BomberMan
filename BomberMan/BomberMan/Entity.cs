using Microsoft.Xna.Framework;
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
        private Rectangle hitBox;
        //Animation
        public Entity(Point p)
        {
            
        }
        public bool Intersects(Rectangle rect) { return hitBox.Intersects(hitBox); }
        public abstract void Update();
        public void Draw(Spritebatch spritebatch)
        {
            //draw Animation
        }
    }
}

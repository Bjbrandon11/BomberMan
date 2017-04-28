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
        readonly int HorizontalRange;
        readonly int VerticalRange;
        const int SIZE = 48;
        Animation anim;
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


    }
}

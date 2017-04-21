using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BomberMan
{
    class Bomb : Entity
    {
        readonly int HorizontalRange;
        readonly int VerticalRange;
        public Bomb(Point p, int x, int y) : base(p)
        {
            HorizontalRange = x;
            VerticalRange = y;
        }
        

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BomberMan
{
    class Player
    {
        Rectangle location;
        public void placeBomb()
        {
            int bottom = this.location.Bottom;
            int center = this.location.Center.X;
            this.gameBoard.receiveBomb(new Bomb(), bottom, center);
        }
    }
}

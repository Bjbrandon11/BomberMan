using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace BomberMan
{
    class Bomb: Entity
    {
        int radius;
        Vector2 location;
        Timer animTimer;

        public override void Update()
        {
            //TODO- write update logic
            animTimer.Update();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BomberMan
{
    class Maze
    {
        public void receiveBomb(Bomb receive, int top, int center)
        {
            blockLocs[top, center].placeBomb(receive);
        }
    }
}

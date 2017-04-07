using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BomberMan
{
    class BombTimer: Timer
    {
        int changeRate;//This is the amount to change the frequency by;
        public BombTimer(int initialFreq,int changeFreq): base(initialFreq)
        {
            changeRate = changeFreq;
        }
        public new bool Update()
        {
            bool result = false;
            if (isRunning)
            {
                base.count++;
                if (base.count >= base.frequency)
                {
                    result = true;
                    base.frequency /= changeRate;
                    if (frequency == 0)
                    {
                        base.End();
                    }
                    Reset();
                }
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BomberMan
{
    class ExplosionTimer : Timer
    {
        readonly int offset;
        readonly int original;
        public ExplosionTimer(int initial, int offset) : base(initial)
        {
            this.offset = offset;
            this.original = initial;
        }
        public void TotalReset()
        {
            base.Reset();
            frequency = original;
        }
        public bool OverallUpdate()
        {
            bool result = this.isDone();
            if (result)
            {
                frequency /= offset;
                if (frequency == 0)
                    result = true;
                else
                    result = false;
            }
            return result;
        }
 
    }
}
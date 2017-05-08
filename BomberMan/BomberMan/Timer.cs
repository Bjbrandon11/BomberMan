using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BomberMan
{
    public class Timer
    {
        protected int count;//this is the number of frames that have gone by since it started
        protected int frequency;//this is how often the action occurs
        protected bool isRunning;
        const float FRAMES_PER_SECOND = 60.0f;
        public Timer(int frames)
        {
            frequency = frames;
            Reset();
        }
        public Timer(double seconds) : this((int)(seconds * FRAMES_PER_SECOND))
        { }
        public bool canUse() { return count >= frequency; }
        public void Play()
        {
            isRunning = true;
        }
        public void Reset()
        {
            count = 0;
        }
        public virtual bool Update()
        {
            bool result = false;
            if (isRunning)
            {
                count++;
            }
            return result;
        }
        public void End()
        {
            isRunning = false;
            Reset();
        }
        public void Pause()
        {
            isRunning = false;
        }
    }
}
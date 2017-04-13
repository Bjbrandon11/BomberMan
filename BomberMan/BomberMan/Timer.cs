using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BomberMan
{
    class Timer
    {

        public int count;//this is the number of frames that have gone by since it started
        public int frequency;//this is how often the action occurs
        public bool isRunning;
        public const float FRAMES_PER_SECOND = 60.0f;
        public Timer(int frames)
        {
            frequency = frames;
            Reset();
        }
        public Timer(double seconds) : this((int)(seconds * FRAMES_PER_SECOND))
        { }
        public void Play()
        {
            isRunning = true;
        }
        public void Reset()
        {
            count = 0;
        }
        public bool Update()
        {
            bool result = false;
            if (isRunning)
            {
                count++;
                if (count >= frequency)
                {
                    result = true;
                    Reset();
                }
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
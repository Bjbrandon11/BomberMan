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
        public bool Running { get { return isRunning; } }
        const float FRAMES_PER_SECOND = 60.0f;
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
        public bool isDone() { return count >= frequency; }
        public void Update()
        {
            if (isRunning)
            {
                count++;
            }
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
        public double getPercent()
        {
            return (double)count / (double)frequency;// * 100.0d;
        }
    }
}
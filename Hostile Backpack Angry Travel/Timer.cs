using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostile_Backpack_Angry_Travel
{
    class Timer
    {
        private double delayTarget; //heading towards 
        private double delayTime; // current delay time

        public Timer(double delayTarget)
        {
            this.delayTarget = delayTarget;
        }

        public bool UpdateAndCheckTimer(GameTime gt)
        {

            delayTime += gt.ElapsedGameTime.TotalMilliseconds;

            if (delayTime.CompareTo(delayTarget) > 0)
            {
                delayTime = 0.0;
                return true;
            }

            return false;
        }

        public void changeTimer(double delayTarget)
        {
            this.delayTarget = delayTarget;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Hostile_Backpack_Angry_Travel
{
    class BarrierList
    {
        List<Barriers> listBarriers = new List<Barriers>();
        Timer timer;
        int mainSpeed;
        int minHight, maxhight;
        int time = 2000;

        public BarrierList()
        {

        }

        public void initBarrierList()
        {
            timer = new Timer(time);
        }

        public void createBarrierList(ContentManager cm, int speed, int miH, int mxH)
        {
            Barriers temp = new Barriers(0, 0, 0);
            mainSpeed = speed;
            minHight = miH;
            maxhight = mxH-100;
            listBarriers.Add(new Barriers(minHight, maxhight, mainSpeed));
            listBarriers[0].LoadContent(cm);
        }

        public Boolean updateBarriers(GameTime gt, Rectangle playerRec, ContentManager cm)
        {
            int x = 0;
            Boolean isHit = false;
            while (x < listBarriers.Count)
            {
                Barriers barriers = listBarriers[x];
                barriers.Update(gt);
                // drop off the pipe if it is off the left side of screen
                if ((barriers.getRect().X + barriers.getRect().Width < 5)) // negative makes sure to get completely off left
                {
                    listBarriers.RemoveAt(x--); // MUST DO -- so we account for the shift in indexes that follow
                    // otherwise we skip the next list element
                    //pipeCount++; // increment pipe count since we successfully navigated it
                }

                if (barriers.playerIntersection(playerRec))
                {
                    listBarriers.RemoveAt(x--);
                    isHit = true;

                }

                x++;
            }

            if (timer.UpdateAndCheckTimer(gt))
            {
                // create the new pipe off screen...
                listBarriers.Add(new Barriers(minHight, maxhight, mainSpeed));
                // ...and call the loadcontent method
                // we know the last pipe is the one we just added...
                listBarriers[listBarriers.Count - 1].LoadContent(cm);
            }

            return isHit;
        }

        public Boolean hitBarrier(Rectangle playerRec)
        {
            int x = 0;
            while (x < listBarriers.Count)
            {
                Barriers barriers = listBarriers[x];

                if (barriers.playerIntersection(playerRec))
                {
                    return true;
                }

                x++;
            }
            return false;

        }

        public void purgeAll()
        {
            listBarriers.Clear();
        }

        public void draw(SpriteBatch sb)
        {
            foreach (Barriers barrier in listBarriers)
                barrier.Draw(sb);
        }
    }
}

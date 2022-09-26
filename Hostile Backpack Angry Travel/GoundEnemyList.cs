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
    class GoundEnemyList
    {
        List<Ground_Enemy> listGroundEnemy = new List<Ground_Enemy>();
        Texture2D spriteSheet;
        Timer timer;
        int mainSpeed;
        int hight;
        int minTime = 1000;
        int maxTime = 4000;
        int currTime;
        Random rand = new Random();

        public GoundEnemyList(Texture2D ss)
        {
            spriteSheet = ss;
        }

        public void iniEnemyList()
        {
            currTime = rand.Next(minTime, maxTime);
            timer = new Timer(currTime);
        }

        public void createEnemyList(ContentManager cm, int speed, int hight)
        {
            Ground_Enemy temp = new Ground_Enemy(mainSpeed, hight, spriteSheet);
            mainSpeed = speed;
            this.hight = hight;
            listGroundEnemy.Add(new Ground_Enemy(mainSpeed, hight, spriteSheet));

        }

        public Boolean updateEnemies(GameTime gt, Rectangle playerRec, ContentManager cm)
        {
            int x = 0;
            Boolean isHit = false;
            while (x < listGroundEnemy.Count)
            {
                Ground_Enemy ground_Enemy = listGroundEnemy[x];
                ground_Enemy.update(gt);
                // drop off the pipe if it is off the left side of screen
                if ((ground_Enemy.getRect().X + ground_Enemy.getRect().Width < 5)) // negative makes sure to get completely off left
                {
                    listGroundEnemy.RemoveAt(x--); // MUST DO -- so we account for the shift in indexes that follow
                    // otherwise we skip the next list element
                    //pipeCount++; // increment pipe count since we successfully navigated it
                }

                if (ground_Enemy.playerIntersection(playerRec))
                {
                    listGroundEnemy.RemoveAt(x--);
                    isHit = true;

                }

                x++;
            }

            if (timer.UpdateAndCheckTimer(gt))
            {
                // create the new pipe off screen...
                currTime = rand.Next(minTime, maxTime);
                timer.changeTimer(currTime);
                listGroundEnemy.Add(new Ground_Enemy(mainSpeed, hight, spriteSheet));
                // ...and call the loadcontent method
                // we know the last pipe is the one we just added...

            }

            return isHit;
        }

        public void purgeAll()
        {
            listGroundEnemy.Clear();
        }

        public void draw(SpriteBatch sb)
        {
            foreach (Ground_Enemy ground_Enemy in listGroundEnemy)
                ground_Enemy.draw(sb);
        }
    }
}

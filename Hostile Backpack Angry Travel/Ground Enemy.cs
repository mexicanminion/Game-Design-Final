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
    class Ground_Enemy
    {

        int rank = 0;
        int loop = 0;
        static int hight;
        Rectangle mainEnemy;
        Texture2D spriteSheet;
        static int speed;
        const int MAX_ENEMY_SPRITES = 3;
        static Rectangle[] ssEnemyRects = new Rectangle[MAX_ENEMY_SPRITES];

        public Ground_Enemy(int s, int h, Texture2D ss)
        {
            speed = s;
            hight = h;
            spriteSheet = ss;
            mainEnemy = new Rectangle(HostileAngryBackpack.SPRITE_EXPECTED_WIDTH, hight- 63, 43, 64);
            if (ssEnemyRects[0].IsEmpty)
            {
                for (int i = 0; i < MAX_ENEMY_SPRITES; i++)
                {
                    ssEnemyRects[i] = new Rectangle(0 + (i * 40), 0, 40, 64);
                }
            }
        }

        public void update(GameTime gt)
        {
            mainEnemy.X = (int)(mainEnemy.X - speed * gt.ElapsedGameTime.TotalMilliseconds / 60);
            loop++;
            if (loop % 10 == 0)
                rank++;
            if(rank >= 3)
                rank = 0;
        }

        public Boolean playerIntersection(Rectangle playerRec)
        {
            if (mainEnemy.Intersects(playerRec))
            {
                return true;
            }
            return false;
        }


        public void draw(SpriteBatch sb)
        {
            sb.Draw(spriteSheet, mainEnemy, ssEnemyRects[rank], Color.White);
        }

        public Rectangle getRect() { return mainEnemy; }

    }
}

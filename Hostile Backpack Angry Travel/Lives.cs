using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostile_Backpack_Angry_Travel
{
    class Lives
    {
        int rank = 0;
        Rectangle mainHeart;
        Texture2D spriteSheet;
        const int MAX_HEART_OPTIONS = 2;
        static Rectangle[] ssHeartRects = new Rectangle[MAX_HEART_OPTIONS];

        public Lives(Texture2D ss, Rectangle recDraw)
        {
            spriteSheet = ss;
            mainHeart = recDraw;

            new Rectangle(10, 0, 10, 9);

            if (ssHeartRects[0].IsEmpty)
            {
                for (int i = 0; i < MAX_HEART_OPTIONS; i++)
                {
                    ssHeartRects[i] = new Rectangle(0 + (i * 10), 0, 10, 9);
                }
            }

        }

        public void decreaseLives()
        {
            rank++;
        }

        public void resetLives()
        {
            rank = 0;
        }

        public void draw(SpriteBatch sb)
        {
            sb.Draw(spriteSheet, mainHeart, ssHeartRects[rank], Color.White);
        }
    }
}

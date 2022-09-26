using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostile_Backpack_Angry_Travel
{
    class Background_and_Floor
    {
        Rectangle bgRect, gdRect;
        Texture2D background, ground;

        const int stichPoint = 948;
        const int bgSpeed = 10;
        const int gdSpeed = 20;


        public Background_and_Floor(Texture2D background, Texture2D floor)
        {

            this.background = background;
            ground = floor;

            bgRect = new Rectangle(0, 0, (int)(background.Width), (int)(background.Height));
            gdRect = new Rectangle(0, (int)(HostileAngryBackpack.SPRITE_EXPECTED_HEIGHT - floor.Height), (int)(floor.Width), (int)(floor.Height));
        }

        public void updatePos(GameTime gt)
        {
            bgRect.X = (int)(bgRect.X - bgSpeed * gt.ElapsedGameTime.TotalMilliseconds / 60) % (int)(stichPoint);
            gdRect.X = (int)(gdRect.X - gdSpeed * gt.ElapsedGameTime.TotalMilliseconds / 60) % (int)(stichPoint);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(background, bgRect, Color.White);
            //sb.Draw(ground, gdRect, Color.White);

        }

    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ParticleIntro;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostile_Backpack_Angry_Travel
{
    class JetpackFailure
    {
        double maxValue;
        int decreseValue;
        double riseValue;
        double currentValue;
        Texture2D emptyPixel;
        Boolean outOfFuel;

        public JetpackFailure(int total, int decrese, double rise)
        {
            maxValue = total;
            decreseValue = decrese;
            riseValue = rise;
            currentValue = total;
            outOfFuel = false;
        }

        public void LoadContent(ContentManager cm)
        {
            emptyPixel = cm.Load<Texture2D>("pixel");
        }

        public void updateFuel(KeyboardState kb, GamePadState pad)
        {
            if (!outOfFuel && currentValue > 0)
            {
                if (kb.IsKeyDown(Keys.Space) || pad.IsButtonDown(Buttons.B))
                    currentValue -= decreseValue;
                else if (!kb.IsKeyDown(Keys.Space) && currentValue <= maxValue)
                    currentValue += riseValue;

                if (currentValue <= 0)
                    outOfFuel = true;
            }
            else
            {
                currentValue += riseValue;
                if(currentValue > maxValue * .15)
                {
                    outOfFuel = false;
                }
            }
            
        }

        public Boolean fuelOut()
        {
            if (outOfFuel)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void reset()
        {
            currentValue = maxValue;
        }

        public void draw(SpriteBatch sb)
        {
            sb.Draw(emptyPixel, new Rectangle(HostileAngryBackpack.SPRITE_EXPECTED_WIDTH - 10, 100, 20, 100), Color.Gray);
            if(outOfFuel)
                sb.Draw(emptyPixel, new Rectangle(HostileAngryBackpack.SPRITE_EXPECTED_WIDTH - 10, 100, 20, (int)((currentValue/maxValue)*100)), Color.Red);
            else
                sb.Draw(emptyPixel, new Rectangle(HostileAngryBackpack.SPRITE_EXPECTED_WIDTH - 10, 100, 20, (int)((currentValue / maxValue) * 100)), Color.Green);
        }
    }
}

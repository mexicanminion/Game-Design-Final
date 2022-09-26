using Microsoft.Xna.Framework;
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
    class Player
    {
        static Texture2D spriteSheet, t;
        static Rectangle mainrec;
        static double gravity = 9.8;
        double currGravity = gravity;
        const int MAX_PLAYER_ANIMATIONS = 2;
        int animationRank = 0;
        Rectangle picRec;
        ParticleSystem partSystem;
        List<Texture2D> particles = new List<Texture2D>();
        static Rectangle[] ssPlayerRects = new Rectangle[MAX_PLAYER_ANIMATIONS];
        int speed;
   


        //public Player(Texture2D ss, Rectangle drawRect, Rectangle srcRect, int speed)
        public Player(Texture2D ss,Texture2D particle,  Rectangle drawRect, int speed)
        {
            spriteSheet = ss;
            mainrec = drawRect;
            //picRec = srcRect;
            this.speed = speed;
            t = particle;

            if (ssPlayerRects[0].IsEmpty)
            {
                for (int i = 0; i < MAX_PLAYER_ANIMATIONS; i++)
                {
                    ssPlayerRects[i] = new Rectangle(0 + (i * 53), 0, 53, 106);
                }
            }

        }

        public void createparticles(GraphicsDevice gd)
        {
            particles.Add(t);
            // create the particle system
            partSystem = new ParticleSystem(particles, new Vector2((int)(gd.Viewport.Width / 2),
                                                                    (int)(gd.Viewport.Height / 2)));
            // set colors        
            partSystem.setColor(Color.White);
        }

        public void Draw(SpriteBatch sp)
        {
            //sp.Draw(spriteSheet, mainrec, picRec, Color.White);
            partSystem.Draw(sp);
            //sp.Draw(spriteSheet, mainrec, Color.White);
            sp.Draw(spriteSheet, mainrec, ssPlayerRects[animationRank], Color.White);
        }

        public void processInput(KeyboardState kb, GamePadState pad1, Boolean outOfFuel)
        {
            
            if ((kb.IsKeyDown(Keys.Space) || pad1.IsButtonDown(Buttons.B)) && !outOfFuel)
            {
                gravityEffect(false, kb, pad1);
                partSystem.EmitterLocation = new Vector2(mainrec.X + (mainrec.Width/5), mainrec.Y + mainrec.Height/2);
                partSystem.emit(3, 10, 20);
                animationRank = 1;
                //mainrec.Y -= speed;
            }
            else
            {
                gravityEffect(true, kb, pad1);
                animationRank = 0;
                //mainrec.Y += speed;

            }

            partSystem.Update();

        }


        private void gravityEffect(bool isFalling, KeyboardState kb, GamePadState pad)
        {

            mainrec.Y += (int)currGravity;

            if (isFalling)
            {
                if(currGravity < gravity)
                {
                    currGravity += .5;
                }
                else
                {
                    currGravity = gravity;
                }
            }
            else
            {
                currGravity -= .5;   
            }

            if (currGravity > gravity)
                currGravity = gravity;

            if (Math.Abs(currGravity) > gravity)
                currGravity = -gravity;

            if (mainrec.Y + mainrec.Height >= HostileAngryBackpack.SPRITE_EXPECTED_HEIGHT && (!kb.IsKeyDown(Keys.Space) && !pad.IsButtonDown(Buttons.B)))
            {
                mainrec.Y = HostileAngryBackpack.SPRITE_EXPECTED_HEIGHT - mainrec.Height;
                currGravity = 0;
            }else if(mainrec.Y + mainrec.Height >= HostileAngryBackpack.SPRITE_EXPECTED_HEIGHT && (kb.IsKeyDown(Keys.Space) || pad.IsButtonDown(Buttons.B)))
            {
                mainrec.Y = HostileAngryBackpack.SPRITE_EXPECTED_HEIGHT - mainrec.Height - 1;
                currGravity = 0;
            }

            if (mainrec.Y <= 0 && (kb.IsKeyDown(Keys.Space) || pad.IsButtonDown(Buttons.B)) && !isFalling)
            {
                mainrec.Y = 0;
                currGravity = 0;

            } else if (mainrec.Y <= 0 && (!kb.IsKeyDown(Keys.Space) || !pad.IsButtonDown(Buttons.B)))
            {
                mainrec.Y = 1;
                currGravity = 0;
            }


        }

        public void reset()
        {
            mainrec.Y = HostileAngryBackpack.SPRITE_EXPECTED_HEIGHT;
            particles.Clear();
            particles.Add(t);
        }

        public Rectangle getRect() { return mainrec; }
    }
}

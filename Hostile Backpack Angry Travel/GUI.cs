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
    class GUI
    {
        Texture2D beginPic, backPic, infoPic, exitPic;
        Color beginColor, backColor, infoColor, exitColor;
        Rectangle beginButton, backButton, infoButton, exitButton;
        int GUIPlaceCount = 0;
        Boolean controllerPressed = false;

        float scaleX, scaleY;

        public GUI(Texture2D beginB, Texture2D backB, Texture2D infoB, Texture2D exitB)
        {
            beginPic = beginB;
            backPic = backB;
            infoPic = infoB;
            exitPic = exitB;

            beginButton = new Rectangle((HostileAngryBackpack.SPRITE_EXPECTED_WIDTH / 2) - 101, 215, 100, 50);
            infoButton = new Rectangle((HostileAngryBackpack.SPRITE_EXPECTED_WIDTH / 2) + 1, 215, 100, 50);
            exitButton = new Rectangle((HostileAngryBackpack.SPRITE_EXPECTED_WIDTH / 2) - 100 / 2, 266, 100, 50);
            backButton = new Rectangle(10, 15, 50, 25);
        }

        public void updateScale(float x, float y)
        {
            scaleX = x;
            scaleY = y;
        }

        public void updateGamePad(GamePadState pad, GamePadState prevPad, Boolean isController)
        {
            if (isController)
            {
                if (!pad.IsButtonDown(Buttons.DPadDown) && prevPad.IsButtonDown(Buttons.DPadDown))
                {
                    GUIPlaceCount++;
                    if (GUIPlaceCount > 2)
                        GUIPlaceCount = 0;
                }

                if (!pad.IsButtonDown(Buttons.DPadUp) && prevPad.IsButtonDown(Buttons.DPadUp))
                {
                    GUIPlaceCount--;
                    if (GUIPlaceCount < 0)
                        GUIPlaceCount = 2;
                }

                if(!pad.IsButtonDown(Buttons.B) && prevPad.IsButtonDown(Buttons.B))
                {
                    controllerPressed = true;
                }
                else
                {
                    controllerPressed = false;
                }
            }
            else
            {
                GUIPlaceCount = 5;
            }
            
        }

        public Boolean updateInfo(MouseState mouse, MouseState prevMouse)
        {
            if ((infoButton.Contains(new Point((int)(mouse.X/scaleX), (int)(mouse.Y/scaleY))) && (!(mouse.LeftButton == ButtonState.Pressed) && prevMouse.LeftButton == ButtonState.Pressed)) || GUIPlaceCount == 1 && controllerPressed)
            {
                infoColor = Color.Red;
                return true;
 
            }
            else if ((infoButton.Contains(new Point((int)(mouse.X / scaleX), (int)(mouse.Y / scaleY))) && mouse.LeftButton == ButtonState.Pressed ) || GUIPlaceCount == 1)
            {
                infoColor = Color.Red;
            }
            else if (infoButton.Contains(new Point((int)(mouse.X / scaleX), (int)(mouse.Y / scaleY))))
            {
                infoColor = Color.Yellow;
            }
            else
            {
                infoColor = Color.White;
            }

            return false;
        }

        public Boolean updatebegin(MouseState mouse, MouseState prevMouse)
        {
            if ((beginButton.Contains(new Point((int)(mouse.X / scaleX), (int)(mouse.Y / scaleY))) && (!(mouse.LeftButton == ButtonState.Pressed) && prevMouse.LeftButton == ButtonState.Pressed)) || GUIPlaceCount == 0 && controllerPressed)
            {
                beginColor = Color.Red;
                return true;

            }
            else if ((beginButton.Contains(new Point((int)(mouse.X / scaleX), (int)(mouse.Y / scaleY))) && mouse.LeftButton == ButtonState.Pressed) || GUIPlaceCount == 0)
            {
                beginColor = Color.Red;
            }
            else if (beginButton.Contains(new Point((int)(mouse.X / scaleX), (int)(mouse.Y / scaleY))))
            {
                beginColor = Color.Yellow;
            }
            else
            {
                beginColor = Color.White;
            }

            return false;
        }

        public Boolean updateExit(MouseState mouse, MouseState prevMouse)
        {
            if ((exitButton.Contains(new Point((int)(mouse.X / scaleX), (int)(mouse.Y / scaleY))) && (!(mouse.LeftButton == ButtonState.Pressed) && prevMouse.LeftButton == ButtonState.Pressed)) || GUIPlaceCount == 2 && controllerPressed)
            {
                exitColor = Color.Red;
                return true;

            }
            else if ((exitButton.Contains(new Point((int)(mouse.X / scaleX), (int)(mouse.Y / scaleY))) && mouse.LeftButton == ButtonState.Pressed) || GUIPlaceCount == 2)
            {
                exitColor = Color.Red;
            }
            else if (exitButton.Contains(new Point((int)(mouse.X / scaleX), (int)(mouse.Y / scaleY))))
            {
                exitColor = Color.Yellow;
            }
            else
            {
                exitColor = Color.White;
            }

            return false;
        }

        public Boolean updateBack(MouseState mouse, MouseState prevMouse)
        {
            if ((backButton.Contains(new Point((int)(mouse.X / scaleX), (int)(mouse.Y / scaleY))) && (!(mouse.LeftButton == ButtonState.Pressed) && prevMouse.LeftButton == ButtonState.Pressed)) || controllerPressed)
            {
                backColor = Color.Red;
                return true;

            }
            else if (backButton.Contains(new Point((int)(mouse.X / scaleX), (int)(mouse.Y / scaleY))) && mouse.LeftButton == ButtonState.Pressed)
            {
                backColor = Color.Red;
            }
            else if (backButton.Contains(new Point((int)(mouse.X / scaleX), (int)(mouse.Y / scaleY))))
            {
                backColor = Color.Yellow;
            }
            else
            {
                backColor = Color.White;
            }

            return false;
        }

        public void drawBegin(SpriteBatch sb)
        {
            sb.Draw(beginPic, beginButton, beginColor);
        }

        public void drawExit(SpriteBatch sb)
        {
            sb.Draw(exitPic, exitButton, exitColor);
        }

        public void drawBack(SpriteBatch sb)
        {
            sb.Draw(backPic, backButton, backColor);
        }

        public void drawInfo(SpriteBatch sb)
        {
            sb.Draw(infoPic, infoButton, infoColor);
        }
    }
}

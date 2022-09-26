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
    class Barriers
    {
        static Texture2D barrierPic;
        Rectangle barrierRec;
        Random rand = new Random();
        static int minHight, maxHight;
        static int barrierSpeed;

        public Barriers(int miH, int mxH,int speed)
        {
            minHight = miH;
            maxHight = mxH;
            barrierSpeed = speed;
        }

        private int randombarrierY()
        {
            return rand.Next(minHight, maxHight);
        }

        public void LoadContent(ContentManager cm)
        {
            barrierPic = cm.Load<Texture2D>("pencil");
            barrierRec = new Rectangle(HostileAngryBackpack.SPRITE_EXPECTED_WIDTH,
                                      randombarrierY(),
                                      (int)(barrierPic.Width/2),
                                      (int)(barrierPic.Height/2));
        }

        public Boolean playerIntersection(Rectangle playerRec)
        {
            if (barrierRec.Intersects(playerRec)){
                return true;
            }
            return false;
        }

        public void Update(GameTime gt)
        {
            barrierRec.X = (int)(barrierRec.X - barrierSpeed * gt.ElapsedGameTime.TotalMilliseconds / 60);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(barrierPic, barrierRec, Color.White);
        }

        public Rectangle getRect() { return barrierRec; }
        public void setSpeed(int speed) { barrierSpeed = speed; }
    }
}

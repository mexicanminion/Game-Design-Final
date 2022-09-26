using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleIntro
{
    public class Particle
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Gravity { get; set; }
        public float Angle { get; set; }
        public float AngularVelocity { get; set; }
        Color color;
        public float Size { get; set; }
        public int TTL { get; set; }
        byte alphaX, rX, gX, bX;

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, Vector2 Gravity,
            float angle, float angularVelocity, Color color, float size, int ttl)
        {
            Texture = texture;  // texture to draw
            Position = position; // where
            Velocity = velocity;  // 
            Angle = angle;
            AngularVelocity = angularVelocity;
            this.Gravity = Gravity;
            this.color = color;
            alphaX = (byte)(Math.Floor(this.color.A / (double)ttl));
            Size = size;
            TTL = ttl;
            rX = 0;
            gX = 0;
            bX = 0;
        }

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, Vector2 Gravity,
                    float angle, float angularVelocity, Color colorStart, Color colorEnd, float size, int ttl)
        {
            Texture = texture;
            Position = position;
            Velocity = velocity;
            Angle = angle;
            AngularVelocity = angularVelocity;
            this.Gravity = Gravity;
            this.color = colorStart;
            alphaX = (byte)(Math.Floor((this.color.A - colorEnd.A) / (double)ttl));
            rX = (byte)(Math.Ceiling((this.color.R - colorEnd.R) / (double)ttl));
            gX = (byte)(Math.Ceiling((this.color.G - colorEnd.G) / (double)ttl));
            bX = (byte)(Math.Ceiling((this.color.B - colorEnd.B) / (double)ttl));

            Size = size;
            TTL = ttl;
        }

        public void Update()
        {
            // shorten life
            TTL--;

            // move position
            Position += Velocity + Gravity;

            // change rotational angle (used in updating position)
            Angle += AngularVelocity;

            // decay color
            color.A -= alphaX;
            color.R -= rX;
            color.G -= gX;
            color.B -= bX;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            spriteBatch.Draw(Texture, Position, sourceRectangle, color,
                Angle, origin, Size, SpriteEffects.None, 0f);
        }
    }
}
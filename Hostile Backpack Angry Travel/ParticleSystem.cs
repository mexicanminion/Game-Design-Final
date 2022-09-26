using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleIntro
{
    public class ParticleSystem
    {
        private Random random;  // random num generator
        public Vector2 EmitterLocation { get; set; } // where the particles emit (start) from
        private List<Particle> particles; // list of active particles
        private List<Texture2D> textures; // list of textures we can use to generate particles 
        private Color startColor, endColor; // simple start/end color blending

        public ParticleSystem(List<Texture2D> textures, Vector2 location)
        {
            EmitterLocation = location;
            this.textures = textures;
            this.particles = new List<Particle>();
            random = new Random();
        }

        public void emit(int count, int lowDuration, int highDuration)
        {
            // for the number of particles we're supposed to add to the system (specified in count variable)
            for (int i = 0; i < count; i++)
            {
                // generate a new particle and add it to the list of active particles.
                particles.Add(GenerateNewParticle(lowDuration, highDuration));
            }
        }

        // 2 overloads for setColor 
        // this one goes from a color to alphablend 0 of that color
        public void setColor(Color s) { startColor = s; }

        // if there is an end color also, then the color will transition from start to end color
        public void setColor(Color s, Color e) { startColor = s; endColor = e; }


        private Particle GenerateNewParticle(int ttlShort, int ttlLong)
        {
            // which texture
            Texture2D texture = textures[random.Next(textures.Count)];

            // where to start
            Vector2 position = EmitterLocation;

            // in which direction and speed to move 
            Vector2 velocity = new Vector2(
                                    1f * (float)(random.NextDouble() * 2 - 1),
                                    5f * (float)(random.NextDouble() * 2 - 1));

            // the rotational angle of the image
            float angle = 0;

            // the rate of rotational change
            float angularVelocity = 1f * (float)(random.NextDouble() * 2 - 1);

            // how big
            float size = (float)random.NextDouble();

            // how long to live (between short and long life)
            int ttl = random.Next(ttlShort, ttlLong + 1);

            // create the particle and return it 
            //      particle will get stored in the particle list...
            return new Particle(texture, position, velocity, new Vector2(-2f, 6f), angle, angularVelocity, startColor, endColor, size, ttl);
        }

        public void Update()
        {
            // for every particle
            for (int particle = 0; particle < particles.Count; particle++)
            {
                // update it
                particles[particle].Update();

                // check time to live so we can remove if necessary
                if (particles[particle].TTL <= 0)
                {
                    // yep, time to go away...
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // for each particle
            for (int index = 0; index < particles.Count; index++)
            {
                // ask it to draw itself
                particles[index].Draw(spriteBatch);
            }
        }
    }
}
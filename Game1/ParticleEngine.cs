using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class ParticleEngine
    {
        private Random random;
        private List<Particle> particles;
        private List<Texture2D> textures;

        public Vector2 EmitterLocation { get; set; }

        public int NumberOfParticles { get; private set; }
        
        public ParticleEngine(List<Texture2D> textures, Vector2 location)
        {
            EmitterLocation = location;
            this.textures = textures;
            this.particles = new List<Particle>();
            random = new Random();
        }

        private Particle GenerateNewParticle(GameTime gameTime)
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = EmitterLocation;

            Vector2 velocity = new Vector2(90f, (float)random.NextDouble(-210D, 800D));
            Vector2 acceleration = new Vector2(10f, 10f);
            float angle = 0;
            float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            Color color = new Color((float)random.NextDouble(0D, 255D), (float)random.NextDouble(0D,255D),0);
            float size = (float)random.NextDouble();
            int ttl = 1200 + random.Next(40);
            NumberOfParticles++;

            return new Particle(texture, position, velocity, acceleration, angle, angularVelocity, new Color(0, 102, 204), size, ttl);
        }

        public void Update(GameTime gameTime)
        {
            int total = 10;
            for(int i = 0; i < total; i++)
            {
                particles.Add(GenerateNewParticle(gameTime));
            }

            for(int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update(gameTime);
                if(particles[particle].TTL <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                    NumberOfParticles--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
        }
    }
}

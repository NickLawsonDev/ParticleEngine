using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Particle
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Mass = 1.0f;
        public Vector2 Acceleration { get; set; }
        public float Angle { get; set; }
        public float AngularVelocity { get; set; }
        public Color Color { get; set; }
        public float Size { get; set; }
        public int TTL { get; set; } //Time to live
        public bool IsInUse { get; set; }

        private ParticleEmitter _Emitter;
        private Random _Random;

        public Particle(ParticleEmitter emitter, Texture2D texture, Vector2 position, Vector2 velocity, Vector2 acceleration, float angle, float angularVelocity, Color color, float size, int ttl)
        {
            _Emitter = emitter;
            _Random = new Random();

            IsInUse = true;
            Texture = texture;
            Position = position;
            Velocity = velocity;
            Acceleration = acceleration;
            Angle = angle;
            AngularVelocity = angularVelocity;
            Color = color;
            Size = size;
            TTL = ttl;
        }

        public void Update(GameTime gameTime)
        {
            var dt = gameTime.ElapsedGameTime.TotalSeconds;

            TTL--;
            var t = Vector2.Multiply(Velocity, (float)dt);
            var r = Vector2.Multiply(Acceleration, 0.5f);
            var q = Vector2.Multiply(r, (float)dt);
            var f = Vector2.Multiply(q, (float)dt);

            Position += (t + f);
            Velocity += Vector2.Multiply(Acceleration, (float)dt);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            spriteBatch.Draw(Texture, Position, sourceRectangle, Color, Angle, origin, Size, SpriteEffects.None, 0f);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Game1
{
    public class ParticleEmitter
    {
        public enum EmitterTypes
        {
            None = 0,
            Burst,
            Sequential
        }

        public Vector2 Position { get; set; }
        public EmitterTypes EmitterType { get; set; }

        private bool _IsInUse { get; set; } = false;
        private LinkedList<Particle> _UsedParticles;
        private LinkedList<Particle> _FreeParticles;
        private Random _Random;
        private int _NumberOfParticles { get; set; }
        private List<Texture2D> _Textures;

        public int TotalNumberOfParticles
        {
            get { return _UsedParticles.Count + _FreeParticles.Count; }
            private set { TotalNumberOfParticles = value; }
        }

        public ParticleEmitter(int numberOfParticles, List<Texture2D> textures, Vector2 position, EmitterTypes emitterType = EmitterTypes.None)
        {
            _Random = new Random();

            EmitterType = emitterType;
            Position = position;

            _NumberOfParticles = numberOfParticles;
            _IsInUse = true;
            _Textures = textures;
            _FreeParticles = new LinkedList<Particle>();
            _UsedParticles = new LinkedList<Particle>();

            if(_FreeParticles.Count > 0 && _UsedParticles.Count < _NumberOfParticles)
            {
                _NumberOfParticles -= _FreeParticles.Count;

                foreach (var particle in _FreeParticles)
                {
                    if (_NumberOfParticles > 0)
                    {
                        _FreeParticles.Remove(particle);
                        particle.IsInUse = false;
                        _UsedParticles.AddLast(particle);

                        _NumberOfParticles--;
                    }
                    else
                        break;
                }
            }

            if(numberOfParticles > 0)
                GenerateParticleList(_NumberOfParticles);
        }

        private void GenerateParticleList(int numberOfParticles)
        {
            Texture2D texture = _Textures[_Random.Next(_Textures.Count)];

            for(var i = 0; i < _NumberOfParticles; i++)
            {
                var velocity = new Vector2(300f * (float)(_Random.NextDouble() * 2 - 1), 500f * (float)(_Random.NextDouble() * 2 - 1));
                var acceleration = new Vector2(0.1f, 0.1f);
                float angularVelocity = 0.1f * (float)(_Random.NextDouble() * 2 - 1);
                float angle = 0f;
                Color color = new Color((float)_Random.NextDouble(), (float)_Random.NextDouble(), (float)_Random.NextDouble());
                float size = (float)_Random.NextDouble() + (float)_Random.NextDouble();
                int ttl = 30 + _Random.Next(40);

                _UsedParticles.AddLast(new Particle(this, texture, Position, velocity, acceleration, angle, angularVelocity, color, size, ttl));
            }
        }

        public void Update(GameTime gameTime)
        {
            var dirtyParticles = new LinkedList<Particle>();
            foreach (var particle in _UsedParticles)
            {
                particle.Update(gameTime);
                if (particle.TTL <= 0)
                {
                    dirtyParticles.AddLast(particle);
                    particle.IsInUse = false;
                    _FreeParticles.AddLast(particle);
                }
            }

            foreach(var particle in dirtyParticles)
            {
                _UsedParticles.Remove(particle);
            }

            if(EmitterType == EmitterTypes.Sequential)
            {
                if(_FreeParticles.Count > _UsedParticles.Count)
                {
                    var freeParticles = new LinkedList<Particle>();
                    foreach (var particle in _FreeParticles)
                    {
                        freeParticles.AddLast(particle);
                        particle.Position = Position;
                        particle.Velocity = new Vector2(100f * (float)(_Random.NextDouble() * 2 - 1), 200f * (float)(_Random.NextDouble() * 2 - 1));
                        particle.Acceleration = new Vector2(0.1f, 0.1f);
                        particle.AngularVelocity = 0.1f * (float)(_Random.NextDouble() * 2 - 1);
                        particle.Angle = 0f;
                        particle.Color = new Color((float)_Random.NextDouble(), (float)_Random.NextDouble(), (float)_Random.NextDouble());
                        particle.Size = (float)_Random.NextDouble() + (float)_Random.NextDouble();
                        particle.TTL = 100 + _Random.Next(40);
                        particle.IsInUse = true;
                        _UsedParticles.AddLast(particle);
                    }

                    foreach (var particle in freeParticles)
                    {
                        _FreeParticles.Remove(particle);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var particle in _UsedParticles)
            {
                particle.Draw(spriteBatch);
            }
        }
    }
}

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
        public int TotalNumberOfParticles
        {
            get
            {
                var temp = 0;
                _UsedEmitters.ToList().ForEach(a => temp += a.TotalNumberOfParticles);
                _FreeEmitters.ToList().ForEach(a => temp += a.TotalNumberOfParticles);
                return temp;
            }
            private set { TotalNumberOfParticles = value; }
        }

        private LinkedList<ParticleEmitter> _UsedEmitters { get; set; }
        private LinkedList<ParticleEmitter> _FreeEmitters { get; set; }
        
        public ParticleEngine()
        {
            _UsedEmitters = new LinkedList<ParticleEmitter>();
            _FreeEmitters = new LinkedList<ParticleEmitter>();
        }

        public void GenerateNewEmitter(int numberOfParticles, List<Texture2D> textures, Vector2 position, ParticleEmitter.EmitterTypes emitterType = ParticleEmitter.EmitterTypes.None)
        {
            _UsedEmitters.AddLast(new ParticleEmitter(numberOfParticles, textures, position, emitterType));
        }

        public void Update(GameTime gameTime)
        {
            foreach(var emitter in _UsedEmitters)
            {
                emitter.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var emitter in _UsedEmitters)
            {
                emitter.Draw(spriteBatch);
            }
        }
    }
}

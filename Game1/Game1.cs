using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using VelcroPhysics.Utilities;

namespace Game1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _Graphics;
        private SpriteBatch _SpriteBatch;
        private ParticleEngine _ParticleEngine;
        private FrameCounter _FrameCounter;
        private Random _Rand;


        public Game1()
        {
            _Graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = 800,
                PreferredBackBufferWidth = 800
            };
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            _Graphics.SynchronizeWithVerticalRetrace = false;
            _Graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _SpriteBatch = new SpriteBatch(GraphicsDevice);

            List<Texture2D> textures = new List<Texture2D>();
            //textures.Add(Content.Load<Texture2D>("Sprites/blackSmoke00"));
            //textures.Add(Content.Load<Texture2D>("Sprites/blackSmoke01"));
            //textures.Add(Content.Load<Texture2D>("Sprites/blackSmoke02"));
            //textures.Add(Content.Load<Texture2D>("Sprites/blackSmoke03"));
            //textures.Add(Content.Load<Texture2D>("Sprites/blackSmoke04"));
            //textures.Add(Content.Load<Texture2D>("Sprites/blackSmoke05"));
            //textures.Add(Content.Load<Texture2D>("Sprites/blackSmoke06"));
            //textures.Add(Content.Load<Texture2D>("Sprites/blackSmoke07"));
            //textures.Add(Content.Load<Texture2D>("Sprites/blackSmoke08"));
            //textures.Add(Content.Load<Texture2D>("Sprites/blackSmoke09"));
            //textures.Add(Content.Load<Texture2D>("Sprites/blackSmoke10"));
            //textures.Add(Content.Load<Texture2D>("Sprites/blackSmoke11"));
            //textures.Add(Content.Load<Texture2D>("Sprites/blackSmoke12"));
            //textures.Add(Content.Load<Texture2D>("Sprites/blackSmoke13"));
            //textures.Add(Content.Load<Texture2D>("Sprites/blackSmoke14"));
            //textures.Add(Content.Load<Texture2D>("Sprites/blackSmoke15"));
            //textures.Add(Content.Load<Texture2D>("Sprites/blackSmoke16"));
            //textures.Add(Content.Load<Texture2D>("Sprites/blackSmoke17"));
            //textures.Add(Content.Load<Texture2D>("Sprites/blackSmoke18"));
            //textures.Add(Content.Load<Texture2D>("Sprites/blackSmoke19"));
            //textures.Add(Content.Load<Texture2D>("Sprites/blackSmoke20"));
            //textures.Add(Content.Load<Texture2D>("Sprites/blackSmoke21"));
            //textures.Add(Content.Load<Texture2D>("Sprites/blackSmoke22"));
            //textures.Add(Content.Load<Texture2D>("Sprites/blackSmoke23"));
            //textures.Add(Content.Load<Texture2D>("Sprites/blackSmoke24"));
            textures.Add(Content.Load<Texture2D>("Sprites/IDR_GIF2"));
            _ParticleEngine = new ParticleEngine(textures, new Vector2(400, 240));
            _Rand = new Random();

            _FrameCounter = new FrameCounter();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _ParticleEngine.EmitterLocation = new Vector2(Mouse.GetState().X,Mouse.GetState().Y);
            _ParticleEngine.Update(gameTime);

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _FrameCounter.Update(deltaTime);
            var fps = $"FPS: {_FrameCounter.AverageFramesPerSecond}, Particles: {_ParticleEngine.NumberOfParticles}";
            this.Window.Title = fps;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null);

            _ParticleEngine.Draw(_SpriteBatch);

            _SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

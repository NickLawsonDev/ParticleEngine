using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _Graphics;
        private SpriteBatch _SpriteBatch;
        private ParticleEngine _ParticleEngine;
        private FrameCounter _FrameCounter;
        private Random _Random;
        private Quadtree quad;

        private const int SCREEN_WIDTH = 800;
        private const int SCREEN_HEIGHT = 800;


        public Game1()
        {
            _Graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = SCREEN_HEIGHT,
                PreferredBackBufferWidth = SCREEN_WIDTH
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
            textures.Add(Content.Load<Texture2D>("Sprites/red_square"));

            _Random = new Random();
            _SpriteBatch = new SpriteBatch(GraphicsDevice);
            quad = new Quadtree(0, new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT));
            quad.CreateTest();

            _ParticleEngine = new ParticleEngine();
            _ParticleEngine.GenerateNewEmitter(500, textures, new Vector2(SCREEN_WIDTH/2,SCREEN_HEIGHT/2), ParticleEmitter.EmitterTypes.Sequential);
            _ParticleEngine.GenerateNewEmitter(_Random.Next(0, 500), textures, new Vector2(SCREEN_WIDTH / 2, SCREEN_HEIGHT/2 + 200), ParticleEmitter.EmitterTypes.Sequential);

            _FrameCounter = new FrameCounter();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _ParticleEngine.Update(gameTime);

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _FrameCounter.Update(deltaTime);
            var fps = $"FPS: {_FrameCounter.AverageFramesPerSecond}, Particles: {_ParticleEngine.TotalNumberOfParticles}";
            this.Window.Title = fps;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Cornsilk);
            _SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null);

//#if DEBUG
//            foreach(var node in quad._Nodes)
//            {
//                if(node != null)
//                    _SpriteBatch.Draw(Content.Load<Texture2D>("Sprites/red_square"), new Rectangle(node._Bounds.X, node._Bounds.Y, node._Bounds.Width, node._Bounds.Height), new Color(255, 255, 255));
//            }
//#endif

            _ParticleEngine.Draw(_SpriteBatch);

            _SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BomberMan
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public static double scaleFrom32 = 1.5;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static List<Bomb> EntityList;
        private Layout level;
        private const int TargetFrameRate = 60;
        private const int BackBufferWidth = 1280;
        private const int BackBuffeHeight = 720;
        public static Block[] spawns;
        List<Player> players;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = BackBufferWidth;
            graphics.PreferredBackBufferHeight = BackBuffeHeight;
            graphics.PreferMultiSampling = true;
            Content.RootDirectory = "Content";
            TargetElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / TargetFrameRate);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            EntityList = new List<Bomb>();
            players = new List<Player>();
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameHolder.spritebatch = spriteBatch;
            GameHolder.game = this;
            Tile.LoadContent();
            LoadLevel();
            spawns = level.getSpawnBlocks();
            players.Add(new Player(spawns[0].hitBox.Center));
            Animation.LoadContent();
            // TODO: use this.Content to load your game content here
        }
        private void LoadLevel()
        {
            level = new Layout(Services, @"Content/Level/Level01.txt");
        }
        public List<Bomb> getEntityList() { return EntityList; }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
                this.Exit();

            foreach (Entity thing in EntityList)
            {
                thing.Update();
            }
            // TODO: Add your update logic here
            foreach (Player p in players)
            {
                p.Update();
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, new RasterizerState { MultiSampleAntiAlias = true  });
            foreach (Player p in players)
                p.Draw(spriteBatch);
            foreach (Bomb b in EntityList)
                b.Draw(spriteBatch);
            level.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

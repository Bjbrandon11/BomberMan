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

        protected override void Initialize()
        {
            EntityList = new List<Bomb>();
            players = new List<Player>();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameHolder.spritebatch = spriteBatch;
            GameHolder.game = this;
            Tile.LoadContent();
            LoadLevel();
            spawns = level.getSpawnBlocks();
            players.Add(new Player(PlayerIndex.One, spawns[0].hitBox.Center));
            Animation.LoadContent();
        }

        private void LoadLevel()
        {
            level = new Layout(Services, @"Content/Level/Level01.txt");
            GameHolder.level = level;
        }

        public List<Bomb> getEntityList() { return EntityList; }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
                this.Exit();
            level.Update();
            for (int i = EntityList.Count - 1; i >= 0; i--)
            {
                EntityList[i].Update();
            }
            // TODO: Add your update logic here
            foreach (Player p in players)
            {
                p.Update();
            }
            // TODO: Add your update logic here
            foreach (Player p in players)
                p.Update();
            foreach (Entity e in EntityList)
                e.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
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

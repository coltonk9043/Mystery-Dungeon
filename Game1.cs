// Colton K
// Main Class for Dungeon Game

using Dungeon;
using Dungeon.Levels;
using Dungeon.Utilities;
using DungeonGame.Entities;
using DungeonGame.Entities.Enemies;
using DungeonGame.Entities.Player;
using DungeonGame.Item;
using DungeonGame.Levels;
using DungeonGame.UI;
using DungeonGame.UI.Menus;
using DungeonGame.UI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace DungeonGame
{
    enum EditorMode
    {
        TILES = 0,
        TILE_ENTITIES = 1,
        ENTITIES = 2
    }

    public class Game1 : GenericGame
    {
        private static Game1 instance;

        private WorldGenerator worldGenerator;
       
        public ClientPlayer player;

        private TitleMenu titleMenu;
        private PauseMenu pauseMenu;
        private IngameGui ingameGui;

        public static Effect _blur;

        public Game1() : base()
        {
            Game1.instance = this;
  
        }

        public Gui getCurrentScreen() => this.currentScreen;

        public void setCurrentScreen(Gui screen) => this.currentScreen = screen;

        protected override void LoadAssets()
        {
            base.LoadAssets();

            this.currentWorld = new DungeonGame.Levels.World(this, "dev");
            this.player = new ClientPlayer(this, this.currentWorld, this.Content.Load<Texture2D>("Textures/player"), new Vector3(20f, 50f, 0.0f));
            this.pauseMenu = new PauseMenu(this, null, this.font);
            this.pauseMenu.Load(this.contentManager);
            this.ingameGui = new IngameGui(this, null, this.font, this.player);
            this.ingameGui.Load(this.contentManager);

            this.currentWorld.addEntity(new EntityBat(this.currentWorld, new Vector3(35f, 40f, 0.0f)));
            this.currentWorld.addPlayer(this.player);


            this.mainCamera = new Camera(this, this.currentWorld, this.graphics, this.player);

            this.titleMenu = new TitleMenu(this, null, this.font);
            this.titleMenu.Load(this.contentManager);
            this.currentScreen = titleMenu;
        }

        protected override void Update(GameTime gameTime)
        {
            this.mouseHelper.Update();
            if (this.currentScreen != null)
            {
                this.currentScreen.Update(gameTime, this.mouseHelper);
            }
            
            elapsed_delta += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsed_delta >= millisecond_interval)
            {
                FixedUpdateGame(gameTime);
                elapsed_delta = 0.0f;
            }
            
            this.UpdateGame(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            if (this.currentScreen != null)
            {
                if (this.currentScreen.isIngame)
                    this.RenderGame(gameTime);
                this.spriteBatch.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, depthStencilState: DepthStencilState.None, rasterizerState: RasterizerState.CullCounterClockwise);
                this.currentScreen.Draw(this.spriteBatch, this.font);
                this.spriteBatch.End();
            }
            else
                this.RenderGame(gameTime);
            this.mouseHelper.Draw(this.spriteBatch);
            base.Draw(gameTime);
        }

        public void UpdateGame(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.currentScreen = this.pauseMenu;

            this.mainCamera.Update(this.graphics, gameTime);
            this.currentWorld.Update(gameTime);
            this.ingameGui.Update(gameTime, this.mouseHelper);
        }

        public void FixedUpdateGame(GameTime gameTime)
        {
            this.currentWorld.FixedUpdate(gameTime);
        }

        /**
         * Draws the game.
         */
        public void RenderGame(GameTime gameTime)
        {
            // Render World.
            this.currentWorld.Draw(this.spriteBatch, this.mainCamera);

            this.spriteBatch.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, depthStencilState: DepthStencilState.None, rasterizerState: RasterizerState.CullCounterClockwise);
            this.ingameGui.Draw(this.spriteBatch, this.font);
            this.spriteBatch.End();
        }

        public void exit() => this.Exit();

        public static Game1 getInstance() => Game1.instance;
    }
}

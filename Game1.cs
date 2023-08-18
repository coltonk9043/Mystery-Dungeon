﻿// Colton K
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

        // World Editor specific variables.
        private WorldEditGui worldEditGui;
        public bool worldEditor = false;
        public int currentTile = 0;
        public int currentLayer = 0;

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
            if (this.worldEditor)
            {
                this.worldEditGui.Update(gameTime, this.mouseHelper);
                float scrollOffset = this.mouseHelper.getScrollOffset();

                if (scrollOffset > 0.0f)
                    this.currentTile = Math.Min(this.currentWorld.getTileTextures().Length - 1, Math.Max(0, this.currentTile - 1));
                else if (scrollOffset < 0.0f)
                    this.currentTile = Math.Min(this.currentWorld.getTileTextures().Length - 1, Math.Max(0, this.currentTile + 1));

                if (!this.mouseHelper.getLeftDown())
                    return;
                Vector2 positionRelativeToWorld = this.mainCamera.getMousePositionRelativeToWorld(this.mouseHelper);
                this.currentWorld.setTile(0, (int)positionRelativeToWorld.X / 16, (int)(positionRelativeToWorld.Y / 16.0) + 1, this.currentTile);
            }
            else
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
            matrixStack.Push();
            matrixStack.Multiply(this.mainCamera.GetTransform());
            this.currentWorld.Draw(this.matrixStack, this.spriteBatch, this.mainCamera);
            matrixStack.Pop();

            this.spriteBatch.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, depthStencilState: DepthStencilState.None, rasterizerState: RasterizerState.CullCounterClockwise);
            if (this.worldEditor)
                this.worldEditGui.Draw(this.spriteBatch, this.font);
            else
                this.ingameGui.Draw(this.spriteBatch, this.font);
            this.spriteBatch.End();
            
        }

        public void exit() => this.Exit();

        public static Game1 getInstance() => Game1.instance;
    }
}

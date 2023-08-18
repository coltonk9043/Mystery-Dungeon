using Dungeon.Levels;
using Dungeon.Utilities;
using DungeonGame.Entities.Enemies;
using DungeonGame.Entities.Player;
using DungeonGame.Item;
using DungeonGame.Levels;
using DungeonGame.UI.Menus;
using DungeonGame.UI.Widgets;
using DungeonGame.UI;
using DungeonGame;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using DungeonGame.Entities;

namespace Dungeon
{
    public class WorldEditor : GenericGame
    {
        public ClientPlayer player;
        private PauseMenu pauseMenu;
        private IngameGui ingameGui;

        // World Editor specific variables.
        private WorldEditGui worldEditGui;
        public int currentTile = 0;
        public int currentLayer = 0;

        public WorldEditor() : base()
        {

        }

        public Gui getCurrentScreen() => this.currentScreen;

        public void setCurrentScreen(Gui screen) => this.currentScreen = screen;

        protected override void LoadContent()
        {
            this.pauseMenu = new PauseMenu(this, null, this.font);
            this.worldEditGui = new WorldEditGui(this, null, this.font);
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

            this.worldEditGui.Update(gameTime, this.mouseHelper);

            if (!this.mouseHelper.getLeftDown())
                return;
            Vector2 positionRelativeToWorld = this.mainCamera.getMousePositionRelativeToWorld(this.mouseHelper);
            this.currentWorld.setTile(0, (int)positionRelativeToWorld.X / 16, (int)(positionRelativeToWorld.Y / 16.0) + 1, this.currentTile);


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

            this.worldEditGui.Draw(this.spriteBatch, this.font);

            this.spriteBatch.End();

        }

        public void exit() => this.Exit();
    }
}

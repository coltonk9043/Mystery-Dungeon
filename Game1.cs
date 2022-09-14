// Colton K
// Main Class for Dungeon Game

using Dungeon.Levels;
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

namespace DungeonGame
{
    public class Game1 : Game
    {
        private static Game1 instance;
        public static int ScreenHeight;
        public static int ScreenWidth;
        public static Items Items;
        public ContentManager contentManager;
        public GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public MouseHelper mouseHelper;
        private WorldGenerator worldGenerator;
        public World currentWorld;
        public ClientPlayer player;
        public Camera mainCamera;
        private FrameCounter framerateCounter = new FrameCounter();
        public SpriteFont font;
        public Settings settings;
        private PauseMenu pauseMenu;
        private IngameGui ingameGui;

        // World Editor specific variables.
        private WorldEditGui worldEditGui;
        public bool worldEditor = false;
        public int currentTile = 0;
        public int currentLayer = 0;


        //
        private Gui currentScreen = (Gui)null;
        public static Effect _blur;

        public Game1()
        {
            Game1.instance = this;
            this._graphics = new GraphicsDeviceManager(this);
            //_graphics.PreparingDeviceSettings += (object s, PreparingDeviceSettingsEventArgs args) =>
            //{
            //    args.GraphicsDeviceInformation.PresentationParameters.RenderTargetUsage = RenderTargetUsage.PreserveContents;
            //};
            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = false;
            this.contentManager = this.Content;
        }

        public Gui getCurrentScreen() => this.currentScreen;

        public void setCurrentScreen(Gui screen) => this.currentScreen = screen;

        protected override void Initialize()
        {
            this._graphics.IsFullScreen = false;
            this._graphics.PreferredBackBufferWidth = 1200;
            this._graphics.PreferredBackBufferHeight = 800;
            Game1.ScreenHeight = this._graphics.PreferredBackBufferHeight;
            Game1.ScreenWidth = this._graphics.PreferredBackBufferWidth;
            this._graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this._spriteBatch = new SpriteBatch(this.GraphicsDevice);
            this.font = this.Content.Load<SpriteFont>("Arial");
            //_blur = this.Content.Load<Effect>("Blur");
            Game1.Items = new Items();
            //this.currentWorld = new DungeonGame.Levels.World("dev");
            worldGenerator = new WorldGenerator(50, 50);
            this.currentWorld = worldGenerator.Generate();
            this.currentWorld.addEntity(new EntityBat(new Vector3(35f, 40f, 0.0f)));
            this.player = new ClientPlayer(this.Content.Load<Texture2D>("Textures/player"), new Vector3(20f, 50f, 0.0f));
            this.currentWorld.addPlayer(this.player);
            this.mainCamera = new Camera(this._graphics, (Entity)this.player);
            this.settings = new Settings();
            this.mouseHelper = new MouseHelper(this);
            this.currentScreen = (Gui)new TitleMenu(null, this.font);
            this.pauseMenu = new PauseMenu(null, this.font);
            this.ingameGui = new IngameGui(null, this.font, this.player, this.framerateCounter);
            this.worldEditGui = new WorldEditGui(null, this.font, this.framerateCounter);
           
        }

        protected override void Update(GameTime gameTime)
        {
            this.mouseHelper.Update();
            if (this.currentScreen != null)
            {
                if (this.currentScreen.isIngame)
                    this.UpdateGame(gameTime);
                this.currentScreen.Update(gameTime, this.mouseHelper);
            }
            else
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
                this._spriteBatch.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, depthStencilState: DepthStencilState.None, rasterizerState: RasterizerState.CullCounterClockwise);
                this.currentScreen.Draw(this._spriteBatch, this.font);
                this._spriteBatch.End();
            }
            else
                this.RenderGame(gameTime);
            this.mouseHelper.Draw(this._spriteBatch);
            base.Draw(gameTime);
        }

        public void UpdateGame(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.currentScreen = this.pauseMenu;
            this.framerateCounter.Update(gameTime);
            this.mainCamera.Update(this._graphics, gameTime);
            this.currentWorld.Update(gameTime);
            if (this.worldEditor)
            {
                this.worldEditGui.Update(gameTime, this.mouseHelper);
                float scrollOffset = this.mouseHelper.getScrollOffset();
                if ((double)scrollOffset > 0.0)
                    this.currentTile = Math.Min(this.currentWorld.getTileTextures().Length - 1, Math.Max(0, this.currentTile - 1));
                else if ((double)scrollOffset < 0.0)
                    this.currentTile = Math.Min(this.currentWorld.getTileTextures().Length - 1, Math.Max(0, this.currentTile + 1));
                if (!this.mouseHelper.getLeftDown())
                    return;
                Vector2 positionRelativeToWorld = this.mainCamera.getMousePositionRelativeToWorld();
                this.currentWorld.setTile(0, (int)positionRelativeToWorld.X / 16, (int)((double)positionRelativeToWorld.Y / 16.0) + 1, this.currentTile);
            }
            else
                this.ingameGui.Update(gameTime, this.mouseHelper);
        }

        public void RenderGame(GameTime gameTime)
        {
            this.currentWorld.Draw(this._spriteBatch, this.mainCamera);
            this._spriteBatch.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, depthStencilState: DepthStencilState.None, rasterizerState: RasterizerState.CullCounterClockwise);
            if (this.worldEditor)
                this.worldEditGui.Draw(this._spriteBatch, this.font);
            else
                this.ingameGui.Draw(this._spriteBatch, this.font);
            this._spriteBatch.End();
        }

        public void exit() => this.Exit();

        public static Game1 getInstance() => Game1.instance;
    }
}

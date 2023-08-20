using Dungeon.Utilities;
using DungeonGame;
using DungeonGame.Entities;
using DungeonGame.Item;
using DungeonGame.Levels;
using DungeonGame.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dungeon
{
    public abstract class GenericGame : Game
    {
        public static Items Items;

        public int ScreenHeight { get; set; }
        public int ScreenWidth { get; set; }

        protected ContentManager contentManager;
        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;
        protected MouseHelper mouseHelper;

        protected World currentWorld;
        protected Camera mainCamera;
        protected Gui currentScreen;


        protected Entity currentPlayer;

        // Getters
        public Camera GetCamera() { return mainCamera; }
        public World GetWorld() { return currentWorld; }
        public MouseHelper GetMouseHelper() { return mouseHelper; }
        public ContentManager GetContentManager() { return contentManager; }
        public GraphicsDeviceManager GetGraphics() { return graphics; }
        public SpriteBatch GetSpriteBatch() { return spriteBatch; }
        public Gui GetCurrentScreen() { return currentScreen; }
        public Entity GetPlayer() { return currentPlayer; }
        public SpriteFont GetFont() { return font; }
        // Setters
        public void SetWorld(World world) { currentWorld = world; }
        public void SetPlayer(Entity entity) { currentPlayer = entity; }
        public void SetCurrentScreen(Gui screen) { currentScreen = screen; }

        // Timer Variables
        protected int timeSteps = 60;
        protected float millisecond_interval;
        protected float elapsed_delta = 0.0f;

        // Rendering Variables
        protected SpriteFont font;

        public Settings settings;

        public GenericGame()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = false;
            this.contentManager = this.Content;
            this.millisecond_interval = 1.0f / timeSteps;
        }

        protected override void Initialize()
        {
            this.graphics.IsFullScreen = false;
            this.graphics.PreferredBackBufferWidth = 1200;
            this.graphics.PreferredBackBufferHeight = 800;
            this.ScreenHeight = this.graphics.PreferredBackBufferHeight;
            this.ScreenWidth = this.graphics.PreferredBackBufferWidth;
            this.graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.LoadAssets();
        }

        protected virtual void LoadAssets()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            this.font = this.Content.Load<SpriteFont>("Arial");
            //_blur = this.Content.Load<Effect>("Blur");
            Game1.Items = new Items();

            //worldGenerator = new WorldGenerator(50, 50);
            //this.currentWorld = worldGenerator.Generate();

            this.settings = new Settings();
            this.mouseHelper = new MouseHelper(this);
        }
    }
}

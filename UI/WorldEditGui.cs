// Colton K
// The GUI for World Edit. 
using Dungeon;
using DungeonGame.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Reflection;

namespace DungeonGame.UI.Widgets
{
    internal class WorldEditGui : Gui
    {
        private int currentTile = 0;

        private ButtonWidget saveButton;
        private ButtonWidget tileGridButton;
        private ButtonWidget tileButton;
        private ButtonWidget tileEntityButton;

        private Texture2D currentTileOverlay;
        private Texture2D[] tileTextures; 

        private World world;
        private EditorMode currentEditor = EditorMode.TILES;

        private Texture2D black;

        public WorldEditGui(WorldEditor game, Gui parent, SpriteFont font, World world)
          : base(game, parent, font)
        {
            this.tileTextures = world.getTileTextures();
            this.saveButton = new ButtonWidget(game.GetContentManager(), new Rectangle(game.ScreenWidth - 224, game.ScreenHeight - 96, 192, 64), "Save", font, new Action(game.GetWorld().Save));
            this.tileGridButton = new ButtonWidget(game.GetContentManager(), new Rectangle(16, 96, 64, 64), "Textures/button_gridmap", "", font, new Action(game.GetWorld().toggleGridmap));
            this.tileButton = new ButtonWidget(game.GetContentManager(), new Rectangle(16, 16, 64, 64), "Textures/GUI/button_tiles", "", font, new Action(() => { this.currentEditor = EditorMode.TILES;  }));
            this.tileEntityButton = new ButtonWidget(game.GetContentManager(), new Rectangle(96, 16, 64, 64), "Textures/GUI/button_tileentity", "", font, new Action(() => { this.currentEditor = EditorMode.TILE_ENTITIES; }));

            black  = new Texture2D(game.GraphicsDevice, 1, 1);
            black.SetData<Color>(0, 0, null, new Color[] { new Color(0,0,0) }, 0, 1);

            this.widgets.Add(this.saveButton);
            this.widgets.Add(this.tileGridButton);
            this.widgets.Add(this.tileButton);
            this.widgets.Add(this.tileEntityButton);
            this.currentTileOverlay = game.GetContentManager().Load<Texture2D>("Textures/currentTileOverlay");
        }

        /// <summary>
        /// Draws the World Editor GUI onto the screen/
        /// </summary>
        /// <param name="spriteBatch">Spritebatch used for rendering.</param>
        /// <param name="font">The current global fon.t</param>
        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Draw(tileTextures[currentTile], new Vector2(game.ScreenWidth - 96, 32), new Rectangle?(new Rectangle(0, 0, 16, 16)), Color.White, 0.0f, Vector2.Zero, 4f, SpriteEffects.None, 0.0f);

            spriteBatch.DrawString(font, "Layer " + 0, new Vector2(16, game.ScreenHeight-32), Color.White);
            spriteBatch.Draw(this.currentTileOverlay, new Vector2(game.ScreenWidth - 112, 16), new Rectangle?(new Rectangle(0, 0, 24, 24)), Color.White, 0.0f, Vector2.Zero, 4f, SpriteEffects.None, 0.0f);

            foreach (Widget widget in this.widgets)
                widget.Draw(spriteBatch, font);
        }

        public override void Update(GameTime gameTime, MouseHelper mouseHelper)
        {
            foreach (Widget widget in this.widgets)
                widget.Update(gameTime, mouseHelper);
        }

        public void SetTile(int currentTile)
        {
            this.currentTile = currentTile;
            Debug.WriteLine(this.currentTile);
        }
    }
}

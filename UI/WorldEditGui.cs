// Colton K
// The GUI for World Edit. 
using Dungeon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DungeonGame.UI.Widgets
{
    internal class WorldEditGui : Gui
    {
        private int currentTile;

        private ButtonWidget saveButton;
        private ButtonWidget tileGridButton;
        private ButtonWidget tileButton;
        private ButtonWidget tileEntityButton;

        private Texture2D currentTileOverlay;

        private EditorMode currentEditor = EditorMode.TILES;

        public WorldEditGui(WorldEditor game, Gui parent, SpriteFont font)
          : base(game, parent, font)
        {

            this.saveButton = new ButtonWidget(game.GetContentManager(), new Rectangle(game.ScreenWidth - 224, game.ScreenHeight - 96, 192, 64), "Save", font, new Action(game.GetWorld().Save));
            this.tileGridButton = new ButtonWidget(game.GetContentManager(), new Rectangle(16, 96, 64, 64), "Textures/button_gridmap", "", font, new Action(game.GetWorld().toggleGridmap));
            this.tileButton = new ButtonWidget(game.GetContentManager(), new Rectangle(16, 16, 64, 64), "Textures/GUI/button_tiles", "", font, new Action(this.changeEditorToTile));
            this.tileEntityButton = new ButtonWidget(game.GetContentManager(), new Rectangle(96, 16, 64, 64), "Textures/GUI/button_tileentity", "", font, new Action(this.changeEditorToTileEntity));

            this.widgets.Add(this.saveButton);
            this.widgets.Add(this.tileGridButton);
            this.widgets.Add(this.tileButton);
            this.widgets.Add(this.tileEntityButton);
            this.currentTileOverlay = game.GetContentManager().Load<Texture2D>("Textures/currentTileOverlay");
        }

        public void changeEditorToTile()
        {
            this.currentEditor = EditorMode.TILES;
        }
       
        public void changeEditorToTileEntity()
        {
            this.currentEditor = EditorMode.TILE_ENTITIES;
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Draw(game.GetWorld().getTileTextures()[currentTile], new Vector2(game.ScreenWidth - 96, 32), new Rectangle?(new Rectangle(0, 0, 16, 16)), Color.White, 0.0f, Vector2.Zero, 4f, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(font, "Layer " + Game1.getInstance().currentLayer, new Vector2(16, game.ScreenHeight-32), Color.White);
            spriteBatch.Draw(this.currentTileOverlay, new Vector2(game.ScreenWidth - 112, 16), new Rectangle?(new Rectangle(0, 0, 24, 24)), Color.White, 0.0f, Vector2.Zero, 4f, SpriteEffects.None, 0.0f);
            foreach (Widget widget in this.widgets)
                widget.Draw(spriteBatch, font);
        }

        public override void Update(GameTime gameTime, MouseHelper mouseHelper)
        {
            float scrollOffset = mouseHelper.getScrollOffset();

            if (scrollOffset > 0.0f)
                this.currentTile = Math.Min(this.game.GetWorld().getTileTextures().Length - 1, Math.Max(0, this.currentTile - 1));
            else if (scrollOffset < 0.0f)
                this.currentTile = Math.Min(this.game.GetWorld().getTileTextures().Length - 1, Math.Max(0, this.currentTile + 1));


            foreach (Widget widget in this.widgets)
                widget.Update(gameTime, mouseHelper);
        }
    }
}

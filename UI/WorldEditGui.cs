// Colton K
// The GUI for World Edit. 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DungeonGame.UI.Widgets
{
    internal class WorldEditGui : Gui
    {
        private FrameCounter frameCounter;
        private ButtonWidget saveButton;
        private ButtonWidget tileGridButton;
        private ButtonWidget tileButton;
        private ButtonWidget tileEntityButton;

        private Texture2D currentTileOverlay;

        private int currentEditor = 0; // 0 = Blocks (Layers), 1 = Tile Entities, 2 = Entities

        public WorldEditGui(Gui parent, SpriteFont font, FrameCounter frameCounter)
          : base(parent, font)
        {
            this.frameCounter = frameCounter;
            this.saveButton = new ButtonWidget(new Rectangle(Game1.ScreenWidth - 224, Game1.ScreenHeight - 96, 192, 64), "Save", font, new Action(Game1.getInstance().currentWorld.Save));
            this.tileGridButton = new ButtonWidget(new Rectangle(16, 96, 64, 64), "Textures/button_gridmap", "", font, new Action(Game1.getInstance().currentWorld.toggleGridmap));
            this.tileButton = new ButtonWidget(new Rectangle(16, 16, 64, 64), "Textures/GUI/button_tiles", "", font, new Action(this.changeEditorToTile));
            this.tileEntityButton = new ButtonWidget(new Rectangle(96, 16, 64, 64), "Textures/GUI/button_tileentity", "", font, new Action(this.changeEditorToTileEntity));

            this.widgets.Add(this.saveButton);
            this.widgets.Add(this.tileGridButton);
            this.widgets.Add(this.tileButton);
            this.widgets.Add(this.tileEntityButton);
            this.currentTileOverlay = Game1.getInstance().contentManager.Load<Texture2D>("Textures/currentTileOverlay");
        }

        public void changeEditorToTile()
        {
            this.currentEditor = 0;
        }
       
        public void changeEditorToTileEntity()
        {
            this.currentEditor = 1;
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Draw(Game1.getInstance().currentWorld.getTileTextures()[Game1.getInstance().currentTile], new Vector2(Game1.ScreenWidth - 96, 32), new Rectangle?(new Rectangle(0, 0, 16, 16)), Color.White, 0.0f, Vector2.Zero, 4f, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(font, "Layer " + Game1.getInstance().currentLayer, new Vector2(16, Game1.ScreenHeight-32), Color.White);
            spriteBatch.Draw(this.currentTileOverlay, new Vector2(Game1.ScreenWidth - 112, 16), new Rectangle?(new Rectangle(0, 0, 24, 24)), Color.White, 0.0f, Vector2.Zero, 4f, SpriteEffects.None, 0.0f);
            foreach (Widget widget in this.widgets)
                widget.Draw(spriteBatch, font);
        }

        public override void Update(GameTime gameTime, MouseHelper mouseHelper)
        {
            foreach (Widget widget in this.widgets)
                widget.Update(gameTime, mouseHelper);
        }
    }
}

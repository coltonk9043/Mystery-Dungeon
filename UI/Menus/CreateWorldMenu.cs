using Dungeon;
using DungeonGame.Levels;
using DungeonGame.UI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Xml.Linq;

namespace DungeonGame.UI.Menus
{
    internal class CreateWorldMenu : Gui
    {
        private LabelWidget createWorldLabel;
        private LabelWidget xLabel;
        private LabelWidget yLabel;
        private LabelWidget layersLabel;
        private LabelWidget worldNameLabel;
        private TextFieldWidget xField;
        private TextFieldWidget yField;
        private TextFieldWidget worldNameField;
        private ButtonWidget backButton;
        private ButtonWidget createWorldButton;

        public CreateWorldMenu(GenericGame game, Gui parent, SpriteFont font)
          : base(game, parent, font)
        {
            this.isIngame = false;
            this.createWorldLabel = new LabelWidget(game.GetContentManager(), new Rectangle(game.ScreenWidth / 2 - 48, 80, 320, 48), "Create World", game.GetFont());
            this.worldNameLabel = new LabelWidget(game.GetContentManager(), new Rectangle(game.ScreenWidth / 2 - 210, 110, 320, 48), "Name:", game.GetFont());
            this.worldNameField = new TextFieldWidget(game.GetContentManager(), new Rectangle(game.ScreenWidth / 2 - 160, 100, 320, 48));
            this.xLabel = new LabelWidget(game.GetContentManager(), new Rectangle(game.ScreenWidth / 2 - 180, 170, 320, 48), "X:", game.GetFont());
            this.xField = new TextFieldWidget(game.GetContentManager(), new Rectangle(game.ScreenWidth / 2 - 160, 160, 320, 48));
            this.yLabel = new LabelWidget(game.GetContentManager(), new Rectangle(game.ScreenWidth / 2 - 180, 230, 320, 48), "Y:", game.GetFont());
            this.yField = new TextFieldWidget(game.GetContentManager(), new Rectangle(game.ScreenWidth / 2 - 160, 220, 320, 48));
            this.layersLabel = new LabelWidget(game.GetContentManager(), new Rectangle(game.ScreenWidth / 2 - 180, 290, 320, 48), "Layers:", game.GetFont());
            this.backButton = new ButtonWidget(game.GetContentManager(), new Rectangle(game.ScreenWidth / 2 - 200, game.ScreenHeight - 128, 192, 64), "Back", font, new Action(new Action(((Gui)this).returnToParent)));
            this.createWorldButton = new ButtonWidget(game.GetContentManager(), new Rectangle(game.ScreenWidth / 2 + 8, game.ScreenHeight - 128, 192, 64), "Create World", font, new Action(this.CreateNewWorld));
            this.widgets.Add((Widget)this.createWorldLabel);
            this.widgets.Add((Widget)this.worldNameLabel);
            this.widgets.Add((Widget)this.worldNameField);
            this.widgets.Add((Widget)this.xLabel);
            this.widgets.Add((Widget)this.xField);
            this.widgets.Add((Widget)this.yLabel);
            this.widgets.Add((Widget)this.yField);
            this.widgets.Add((Widget)this.layersLabel);
            this.widgets.Add((Widget)this.backButton);
            this.widgets.Add((Widget)this.createWorldButton);
        }

        public override void Update(GameTime gameTime, MouseHelper mouseHelper)
        {
            foreach (Widget widget in this.widgets)
                widget.Update(gameTime, mouseHelper);
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            foreach (Widget widget in this.widgets)
                widget.Draw(spriteBatch, font);
        }

        public void CreateNewWorld()
        {
            
            
            int width = Int32.Parse(this.xField.getText());
            int height = Int32.Parse(this.yField.getText());
            game.SetWorld(new World(this.worldNameField.getText(), width, height));
            game.SetCurrentScreen(null);
        }
    }
}

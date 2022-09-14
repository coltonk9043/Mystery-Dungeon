// Decompiled with JetBrains decompiler
// Type: DungeonGame.UI.Menus.CreateWorldMenu
// Assembly: DungeonGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E78E8B53-5180-47B9-9458-06A9AF653F10
// Assembly location: C:\Users\Colton's PC\Documents\Games\Dungeon\Dungeon\bin\Debug\netcoreapp3.1\DungeonGame.dll

using DungeonGame.Levels;
using DungeonGame.UI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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
        private TextFieldWidget layersField;
        private TextFieldWidget worldNameField;
        private ButtonWidget backButton;
        private ButtonWidget createWorldButton;

        public CreateWorldMenu(Gui parent, SpriteFont font)
          : base(parent, font)
        {
            this.isIngame = false;
            this.createWorldLabel = new LabelWidget(new Rectangle(Game1.ScreenWidth / 2 - 48, 80, 320, 48), "Create World", Game1.getInstance().font);
            this.worldNameLabel = new LabelWidget(new Rectangle(Game1.ScreenWidth / 2 - 210, 110, 320, 48), "Name:", Game1.getInstance().font);
            this.worldNameField = new TextFieldWidget(new Rectangle(Game1.ScreenWidth / 2 - 160, 100, 320, 48));
            this.xLabel = new LabelWidget(new Rectangle(Game1.ScreenWidth / 2 - 180, 170, 320, 48), "X:", Game1.getInstance().font);
            this.xField = new TextFieldWidget(new Rectangle(Game1.ScreenWidth / 2 - 160, 160, 320, 48));
            this.yLabel = new LabelWidget(new Rectangle(Game1.ScreenWidth / 2 - 180, 230, 320, 48), "Y:", Game1.getInstance().font);
            this.yField = new TextFieldWidget(new Rectangle(Game1.ScreenWidth / 2 - 160, 220, 320, 48));
            this.layersLabel = new LabelWidget(new Rectangle(Game1.ScreenWidth / 2 - 180, 290, 320, 48), "Layers:", Game1.getInstance().font);
            this.layersField = new TextFieldWidget(new Rectangle(Game1.ScreenWidth / 2 - 160, 280, 320, 48));
            this.backButton = new ButtonWidget(new Rectangle(Game1.ScreenWidth / 2 - 200, Game1.ScreenHeight - 128, 192, 64), "Back", font, new Action(new Action(((Gui)this).returnToParent)));
            this.createWorldButton = new ButtonWidget(new Rectangle(Game1.ScreenWidth / 2 + 8, Game1.ScreenHeight - 128, 192, 64), "Create World", font, new Action(this.CreateNewWorld));
            this.widgets.Add((Widget)this.createWorldLabel);
            this.widgets.Add((Widget)this.worldNameLabel);
            this.widgets.Add((Widget)this.worldNameField);
            this.widgets.Add((Widget)this.xLabel);
            this.widgets.Add((Widget)this.xField);
            this.widgets.Add((Widget)this.yLabel);
            this.widgets.Add((Widget)this.yField);
            this.widgets.Add((Widget)this.layersLabel);
            this.widgets.Add((Widget)this.layersField);
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
            Game1 instance = Game1.getInstance();
            instance.worldEditor = true;
            int width = Int32.Parse(this.xField.getText());
            int height = Int32.Parse(this.yField.getText());
            int layers = Int32.Parse(this.layersField.getText());
            instance.currentWorld = new World(this.worldNameField.getText(), width, height, layers);
            instance.setCurrentScreen(null);
        }
    }
}

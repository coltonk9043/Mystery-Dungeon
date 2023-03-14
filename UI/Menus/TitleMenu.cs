// Decompiled with JetBrains decompiler
// Type: DungeonGame.UI.Menus.TitleMenu
// Assembly: DungeonGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E78E8B53-5180-47B9-9458-06A9AF653F10
// Assembly location: C:\Users\Colton's PC\Documents\Games\Dungeon\Dungeon\bin\Debug\netcoreapp3.1\DungeonGame.dll

using DungeonGame.UI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DungeonGame.UI.Menus
{
  public class TitleMenu : Gui
  {
    private ButtonWidget newButton;
    private ButtonWidget loadButton;
    private ButtonWidget coopButton;
    private ButtonWidget settingsButton;
    private ButtonWidget worldEditButton;
    private ButtonWidget exitButton;
    private SliderWidget slider;
    private TextFieldWidget textfield;
    private SettingsMenu settingsMenu;
    private CreateWorldMenu worldEditMenu;

    public TitleMenu(Gui parent, SpriteFont font)
      : base(parent, font)
    {
      this.isIngame = false;
      this.newButton = new ButtonWidget(new Rectangle(Game1.ScreenWidth / 2 - 216 - 192, Game1.ScreenHeight - 128, 192, 64), "New Game", font, new Action(this.StartNewGame));
      this.loadButton = new ButtonWidget(new Rectangle(Game1.ScreenWidth / 2 - 8 - 192, Game1.ScreenHeight - 128, 192, 64), "Load Game", font, new Action(this.LoadGame));
      this.coopButton = new ButtonWidget(new Rectangle(Game1.ScreenWidth / 2 + 8, Game1.ScreenHeight - 128, 192, 64), "Co-op", font, new Action(((Game) Game1.getInstance()).Exit));
      this.exitButton = new ButtonWidget(new Rectangle(Game1.ScreenWidth / 2 + 216, Game1.ScreenHeight - 128, 192, 64), "Exit", font, new Action(((Game) Game1.getInstance()).Exit));
      this.settingsButton = new ButtonWidget(new Rectangle(Game1.ScreenWidth - 80, 16, 64, 64), "Textures/button_settings", "", font, new Action(this.OpenSettingsMenu));
      this.worldEditButton = new ButtonWidget(new Rectangle(Game1.ScreenWidth - 80, 96, 64, 64), "Textures/button_world_editor", "", font, new Action(this.OpenWorldEdit));
      this.slider = new SliderWidget(new Rectangle(Game1.ScreenWidth / 2, 96, 320, 32), 0, 100);
      this.textfield = new TextFieldWidget(new Rectangle(Game1.ScreenWidth / 2, 160, 320, 48));
      this.widgets.Add((Widget) this.newButton);
      this.widgets.Add((Widget) this.loadButton);
      this.widgets.Add((Widget) this.coopButton);
      this.widgets.Add((Widget) this.exitButton);
      this.widgets.Add((Widget) this.settingsButton);
      this.widgets.Add((Widget) this.worldEditButton);
      this.widgets.Add((Widget) this.slider);
      this.widgets.Add((Widget) this.textfield);
      this.settingsMenu = new SettingsMenu((Gui) this, font);
      this.worldEditMenu = new CreateWorldMenu((Gui) this, font);
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

    public void StartNewGame() => Game1.getInstance().setCurrentScreen(this.parent);

    public void LoadGame() => Game1.getInstance().setCurrentScreen(this.parent);

    public void OpenSettingsMenu() => Game1.getInstance().setCurrentScreen((Gui) this.settingsMenu);

    public void OpenWorldEdit() => Game1.getInstance().setCurrentScreen((Gui)this.worldEditMenu);
  }
}

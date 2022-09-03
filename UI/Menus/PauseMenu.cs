// Decompiled with JetBrains decompiler
// Type: DungeonGame.UI.Menus.PauseMenu
// Assembly: DungeonGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E78E8B53-5180-47B9-9458-06A9AF653F10
// Assembly location: C:\Users\Colton's PC\Documents\Games\Dungeon\Dungeon\bin\Debug\netcoreapp3.1\DungeonGame.dll

using DungeonGame.UI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DungeonGame.UI.Menus
{
  internal class PauseMenu : Gui
  {
    private ButtonWidget returnToGame;
    private ButtonWidget settings;
    private ButtonWidget returnToMainMenu;
    private Texture2D overlay;

    public PauseMenu(Gui parent, SpriteFont font)
      : base(parent, font)
    {
      this.overlay = new Texture2D(Game1.getInstance().GraphicsDevice, 1, 1);
      this.overlay.SetData<Color>(new Color[1]
      {
        new Color(50, 50, 50, 50)
      });
      this.isIngame = true;
      this.returnToGame = new ButtonWidget(new Rectangle(Game1.ScreenWidth / 2 - 86, Game1.ScreenHeight / 2 - 128, 192, 64), "Return To Game", font, new Action(((Gui) this).returnToParent));
      this.settings = new ButtonWidget(new Rectangle(Game1.ScreenWidth / 2 - 86, Game1.ScreenHeight / 2 - 48, 192, 64), "Settings", font, new Action(this.openSettings));
      this.returnToMainMenu = new ButtonWidget(new Rectangle(Game1.ScreenWidth / 2 - 86, Game1.ScreenHeight / 2 + 32, 192, 64), "Exit Game", font, new Action(this.exitGame));
      this.widgets.Add((Widget) this.returnToGame);
      this.widgets.Add((Widget) this.settings);
      this.widgets.Add((Widget) this.returnToMainMenu);
    }

    public override void Update(GameTime gameTime, MouseHelper mouseHelper)
    {
      foreach (Widget widget in this.widgets)
        widget.Update(gameTime, mouseHelper);
    }

    public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
    {
      spriteBatch.Draw(this.overlay, new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight), Color.Black);
      foreach (Widget widget in this.widgets)
        widget.Draw(spriteBatch, font);
    }

    public void openSettings() => Game1.getInstance().setCurrentScreen((Gui) new SettingsMenu((Gui) this, this.font));

    public void exitGame() => Game1.getInstance().setCurrentScreen((Gui) new TitleMenu((Gui) null, this.font));
  }
}

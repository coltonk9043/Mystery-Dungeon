using Dungeon;
using DungeonGame.UI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DungeonGame.UI.Menus
{
  internal class SettingsMenu : Gui
  {
    private ButtonWidget restoreButton;
    private ButtonWidget backButton;

    public SettingsMenu(GenericGame game, Gui parent, SpriteFont font)
      : base(game, parent, font)
    {
      this.isIngame = false;
      this.restoreButton = new ButtonWidget(game.GetContentManager(), new Rectangle(game.ScreenWidth / 2 - 8 - 192, game.ScreenHeight - 128, 192, 64), "Restore Defaults", font, new Action(((Gui) this).returnToParent));
      this.backButton = new ButtonWidget(game.GetContentManager(), new Rectangle(game.ScreenWidth / 2 + 8, game.ScreenHeight - 128, 192, 64), "Back", font, new Action(((Gui) this).returnToParent));
      this.widgets.Add((Widget) this.restoreButton);
      this.widgets.Add((Widget) this.backButton);
    }

    public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
    {
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

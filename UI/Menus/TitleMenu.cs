using Dungeon;
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

    public TitleMenu(GenericGame game, Gui parent, SpriteFont font)
      : base(game, parent, font)
    {
      this.isIngame = false;
      this.newButton = new ButtonWidget(game.GetContentManager(), new Rectangle(game.ScreenWidth / 2 - 216 - 192, game.ScreenHeight - 128, 192, 64), "New Game", font, new Action(this.StartNewGame));
      this.loadButton = new ButtonWidget(game.GetContentManager(), new Rectangle(game.ScreenWidth / 2 - 8 - 192, game.ScreenHeight - 128, 192, 64), "Load Game", font, new Action(this.LoadGame));
      this.coopButton = new ButtonWidget(game.GetContentManager(), new Rectangle(game.ScreenWidth / 2 + 8, game.ScreenHeight - 128, 192, 64), "Co-op", font, new Action(((Game) Game1.getInstance()).Exit));
      this.exitButton = new ButtonWidget(game.GetContentManager(), new Rectangle(game.ScreenWidth / 2 + 216, game.ScreenHeight - 128, 192, 64), "Exit", font, new Action(((Game) Game1.getInstance()).Exit));
      this.settingsButton = new ButtonWidget(game.GetContentManager(), new Rectangle(game.ScreenWidth - 80, 16, 64, 64), "Textures/button_settings", "", font, new Action(this.OpenSettingsMenu));
      this.worldEditButton = new ButtonWidget(game.GetContentManager(), new Rectangle(game.ScreenWidth - 80, 96, 64, 64), "Textures/button_world_editor", "", font, new Action(this.OpenWorldEdit));
      this.slider = new SliderWidget(game.GetContentManager(), new Rectangle(game.ScreenWidth / 2, 96, 320, 32), 0, 100);
      this.textfield = new TextFieldWidget(game.GetContentManager(), new Rectangle(game.ScreenWidth / 2, 160, 320, 48));
      this.widgets.Add(this.newButton);
      this.widgets.Add(this.loadButton);
      this.widgets.Add(this.coopButton);
      this.widgets.Add(this.exitButton);
      this.widgets.Add(this.settingsButton);
      this.widgets.Add(this.worldEditButton);
      this.widgets.Add(this.slider);
      this.widgets.Add(this.textfield);
      this.settingsMenu = new SettingsMenu(game, this, font);
      this.worldEditMenu = new CreateWorldMenu(game, this, font);
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

    public void OpenSettingsMenu() => Game1.getInstance().setCurrentScreen(this.settingsMenu);

    public void OpenWorldEdit() => Game1.getInstance().setCurrentScreen(this.worldEditMenu);
  }
}

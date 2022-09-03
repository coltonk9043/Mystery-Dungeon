// Decompiled with JetBrains decompiler
// Type: DungeonGame.UI.Widgets.SliderWidget
// Assembly: DungeonGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E78E8B53-5180-47B9-9458-06A9AF653F10
// Assembly location: C:\Users\Colton's PC\Documents\Games\Dungeon\Dungeon\bin\Debug\netcoreapp3.1\DungeonGame.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonGame.UI.Widgets
{
  internal class SliderWidget : Widget
  {
    private Texture2D notch;
    private int minSliderPosition;
    private int maxSliderPosition;
    private int sliderPosition;

    public SliderWidget(Rectangle position, int minSliderPosition, int maxSliderPosition)
      : base(position, Game1.getInstance().contentManager.Load<Texture2D>("Textures/slider_background"))
    {
      this.notch = Game1.getInstance().contentManager.Load<Texture2D>("Textures/slider_notch");
      this.minSliderPosition = minSliderPosition;
      this.maxSliderPosition = maxSliderPosition;
    }

    public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
    {
      spriteBatch.Draw(this.texture, this.position, Color.White);
      spriteBatch.Draw(this.notch, new Rectangle(this.position.X + this.sliderPosition, this.position.Y, 64, 32), Color.White);
    }

    public override void Update(GameTime gameTime, MouseHelper mouseHelper)
    {
      if (!mouseHelper.getLeftDown() || (double) mouseHelper.getPosition().X < (double) (this.position.X + 32) || (double) mouseHelper.getPosition().X > (double) (this.position.X + this.position.Width - 32) || (double) mouseHelper.getPosition().Y < (double) this.position.Y || (double) mouseHelper.getPosition().Y > (double) (this.position.Y + this.position.Height))
        return;
      this.sliderPosition = (int) ((double) mouseHelper.getPosition().X - 32.0 - (double) this.position.X);
    }

    public int getSliderPosInt() => (int) ((double) ((float) this.sliderPosition / 256f) * (double) this.maxSliderPosition) - this.minSliderPosition;
  }
}

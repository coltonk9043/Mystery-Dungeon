// Decompiled with JetBrains decompiler
// Type: DungeonGame.UI.Widgets.Widget
// Assembly: DungeonGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E78E8B53-5180-47B9-9458-06A9AF653F10
// Assembly location: C:\Users\Colton's PC\Documents\Games\Dungeon\Dungeon\bin\Debug\netcoreapp3.1\DungeonGame.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonGame.UI.Widgets
{
  public abstract class Widget
  {
    public Rectangle position;
    public Texture2D texture;

    public Widget(Rectangle position, Texture2D texture)
    {
      this.position = position;
      this.texture = texture;
    }

    public Widget(Rectangle position)
    {
      this.position = position;
      this.texture = (Texture2D) null;
    }

    public abstract void Update(GameTime gameTime, MouseHelper mouseHelper);

    public abstract void Draw(SpriteBatch spriteBatch, SpriteFont font);
  }
}

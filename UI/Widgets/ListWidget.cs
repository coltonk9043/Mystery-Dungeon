// Decompiled with JetBrains decompiler
// Type: DungeonGame.UI.Widgets.ListWidget
// Assembly: DungeonGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E78E8B53-5180-47B9-9458-06A9AF653F10
// Assembly location: C:\Users\Colton's PC\Documents\Games\Dungeon\Dungeon\bin\Debug\netcoreapp3.1\DungeonGame.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DungeonGame.UI.Widgets
{
  internal class ListWidget : Widget
  {
    public List<Widget> widgets = new List<Widget>();
    public int currentPosition = 0;

    public ListWidget(Rectangle position)
      : base(position, (Texture2D) null)
    {
    }

    public override void Draw(SpriteBatch spriteBatch, SpriteFont font) => throw new NotImplementedException();

    public override void Update(GameTime gameTime, MouseHelper mouseHelper)
    {
    }
  }
}

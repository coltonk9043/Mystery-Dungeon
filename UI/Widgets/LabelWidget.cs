// Decompiled with JetBrains decompiler
// Type: DungeonGame.UI.Widgets.LabelWidget
// Assembly: DungeonGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E78E8B53-5180-47B9-9458-06A9AF653F10
// Assembly location: C:\Users\Colton's PC\Documents\Games\Dungeon\Dungeon\bin\Debug\netcoreapp3.1\DungeonGame.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DungeonGame.UI.Widgets
{
    internal class LabelWidget : Widget
    {
        private string text;
        public bool mouseOver;
        private Action pressedAction;

        public LabelWidget(Rectangle position, string text, SpriteFont font, Action func)
          : base(position)
        {
            this.text = text;
            this.pressedAction = func;
        }

        public LabelWidget(Rectangle position, string text, SpriteFont font)
          : base(position)
        {
            this.text = text;
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font) => spriteBatch.DrawString(font, this.text, new Vector2((float)this.position.X, (float)this.position.Y), this.mouseOver ? Color.Yellow : Color.LightGray);

        public override void Update(GameTime gameTime, MouseHelper mouseHelper)
        {
            if ((double)mouseHelper.getPosition().X >= (double)this.position.X && (double)mouseHelper.getPosition().X <= (double)(this.position.X + this.position.Width))
            {
                if ((double)mouseHelper.getPosition().Y >= (double)this.position.Y && (double)mouseHelper.getPosition().Y <= (double)(this.position.Y + this.position.Height))
                {
                    this.mouseOver = true;
                    if (!mouseHelper.getLeftClicked())
                        return;
                    if(this.pressedAction != null) this.pressedAction();
                }
                else
                    this.mouseOver = false;
            }
            else
                this.mouseOver = false;
        }
    }
}

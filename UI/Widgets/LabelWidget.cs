﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DungeonGame.UI.Widgets
{
    internal class LabelWidget : Widget
    {
        private string text;
        public bool mouseOver;
        private Action pressedAction;

        public LabelWidget(ContentManager content, Rectangle position, string text, SpriteFont font, Action func)
          : base(content, position)
        {
            this.text = text;
            this.pressedAction = func;
        }

        public LabelWidget(ContentManager content, Rectangle position, string text, SpriteFont font)
          : base(content, position)
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

        public override void Load(ContentManager contentManager)
        {

        }
    }
}

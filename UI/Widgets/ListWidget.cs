using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DungeonGame.UI.Widgets
{
    internal class ListWidget : Widget
    {
        public List<Widget> widgets = new List<Widget>();
        public int currentPosition = 0;

        public ListWidget(ContentManager content, Rectangle position)
          : base(content,position)
        {
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font) => throw new NotImplementedException();

        public override void Update(GameTime gameTime, MouseHelper mouseHelper)
        {

        }

        public override void Load(ContentManager contentManager)
        {
        }
    }
}

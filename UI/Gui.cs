using Dungeon;
using DungeonGame.UI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DungeonGame.UI
{
    public abstract class Gui
    {
        protected GenericGame game;
        public Gui parent;
        public SpriteFont font;
        public List<Widget> widgets = new List<Widget>();
        public bool isIngame = true;

        public Gui(GenericGame game, Gui parent, SpriteFont font)
        {
            this.game = game;
            this.parent = parent;
            this.font = font;
        }

        public void Load(ContentManager contentManager)
        {
            foreach(Widget widget in widgets)
            {
                widget.Load(contentManager);
            }
        }

        public abstract void Update(GameTime gameTime, MouseHelper mouseHelper);

        public abstract void Draw(SpriteBatch spriteBatch, SpriteFont font);

        public void returnToParent() => Game1.getInstance().setCurrentScreen(this.parent);
    }
}

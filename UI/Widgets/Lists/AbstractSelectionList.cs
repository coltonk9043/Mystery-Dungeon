using DungeonGame;
using DungeonGame.UI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dungeon.UI.Widgets.Lists
{
    class AbstractSelectionList<T> : Widget
    {
        private List<T> children;

        private double scrollAmount;
        private T selected;
        private T hovered;

        public AbstractSelectionList(ContentManager content, Rectangle position) : base(content, position)
        {
            this.children = new List<T>();
        }

        public override void Update(GameTime gameTime, MouseHelper mouseHelper)
        {
            throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            throw new NotImplementedException();
        }


        public T getSelected(){ return this.selected; }

        public void setSelected(T t) { this.selected = t;}

        public void clearEntries() { children.Clear(); }

        public void setEntry(int index, T entry) { children[index] = entry; }

        public T getEntry(int index) { return this.children[index]; }

        private void scroll(int input) { this.scrollAmount = this.scrollAmount + (double)input; }

        public double getScrollAmount() { return this.scrollAmount; }

        public void setAmount(int amount) { this.scrollAmount = amount; }

        public override void Load(ContentManager contentManager)
        {
            throw new NotImplementedException();
        }
    }
 
}

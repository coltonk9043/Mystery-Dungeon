// Decompiled with JetBrains decompiler
// Type: DungeonGame.UI.Gui
// Assembly: DungeonGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E78E8B53-5180-47B9-9458-06A9AF653F10
// Assembly location: C:\Users\Colton's PC\Documents\Games\Dungeon\Dungeon\bin\Debug\netcoreapp3.1\DungeonGame.dll

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

using DungeonGame;
using DungeonGame.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dungeon.UI
{
    class ChatBoxGui : Gui
    {
        Stack<String> messages;
        private Texture2D texture;


        public ChatBoxGui(Gui parent, SpriteFont font) : base(parent, font)
        {
            this.texture = Game1.getInstance().contentManager.Load<Texture2D>("Textures/chatBox");
            messages = new Stack<String>();
            messages.Push("Cocolots: first!");
            messages.Push("Penji: second!");
            messages.Push("Snorty: third!");
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Draw(this.texture, new Rectangle(4, Game1.ScreenHeight / 2 - 100, 320, 256), Color.White);
            int offset = 0;
            for(int i = messages.Count - 1; i >= 0; i--) 
            {
                spriteBatch.DrawString(font, messages.ToArray()[i], new Vector2(24f, (Game1.ScreenHeight / 2) + 116 - offset), Color.White);
                offset += 24;   
            }
        }

        public override void Update(GameTime gameTime, MouseHelper mouseHelper)
        {
            if(messages.Count > 5)
            {
                messages.Pop();
            }
        }

        public String getMessage(int index)
        {
            return this.messages.ToArray()[index];
        }

        public void addMessage(String message)
        {
            this.messages.Push(message);
        }
    }
}

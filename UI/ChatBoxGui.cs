using DungeonGame;
using DungeonGame.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Dungeon.UI
{
    class ChatBoxGui : Gui
    {
        Stack<String> messages;
        private Texture2D texture;

        // Message specific features
        private double timeLastMessage = 0.0;
        private float alpha = 1.0f;
        private bool messageAddedRecently = false;

        public ChatBoxGui(GenericGame game, Gui parent, SpriteFont font) : base(game, parent, font)
        {
            this.texture = game.GetContentManager().Load<Texture2D>("Textures/chatBox");
            messages = new Stack<String>();
            messages.Push("Cocolots: first!");
            messages.Push("Penji: second!");
            messages.Push("Snorty: third!");
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Draw(this.texture, new Rectangle(4, game.ScreenHeight / 2 - 100, 320, 256), new Color(Color.White, alpha));
            int offset = 0;
            for(int i = messages.Count - 1; i >= 0; i--) 
            {
                spriteBatch.DrawString(font, messages.ToArray()[i], new Vector2(24f, (game.ScreenHeight / 2) + 116 - offset), Color.White);
                offset += 24;   
            }
        }

        public override void Update(GameTime gameTime, MouseHelper mouseHelper)
        {
            if (messageAddedRecently)
            {
                timeLastMessage = gameTime.TotalGameTime.TotalMilliseconds;
                messageAddedRecently = false;
            }

            if(alpha > 0.0 && gameTime.TotalGameTime.TotalMilliseconds - timeLastMessage > 5000)
            {
                alpha = Math.Clamp(alpha - 0.10f, 0.0f, 1.0f);
            }
        }

        public String getMessage(int index)
        {
            return this.messages.ToArray()[index];
        }

        public void addMessage(String message)
        {
            this.messages.Push(message);
            messageAddedRecently = true;
            
        }
    }
}

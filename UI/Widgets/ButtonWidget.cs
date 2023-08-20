using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using System;

namespace DungeonGame.UI.Widgets
{
    internal class ButtonWidget : Widget
    {
        private string text;
        private Vector2 textSize;
        private Action pressedAction;
        private Texture2D mouseOffTexture;
        private Texture2D mouseOverTexture;
        private SoundEffect effect;
        private string tooltip;

        public ButtonWidget(ContentManager content, Rectangle position, string text, SpriteFont font, Action func)
          : base(content, position)
        {
            this.text = text;
            this.textSize = font.MeasureString(text);
            this.pressedAction = func;
        }

        public ButtonWidget(ContentManager content,
          Rectangle position,
          string texture,
          string text,
          SpriteFont font,
          Action func)
          : base(content, position)
        {
            this.text = text;
            this.textSize = font.MeasureString(text);
            this.pressedAction = func;
            this.texture = content.Load<Texture2D>(texture);
            this.mouseOverTexture = content.Load<Texture2D>(texture + "_mouseOver");
        }

        public override void Load(ContentManager contentManager)
        {
            this.texture = contentManager.Load<Texture2D>("Textures/button_48x16");
            this.mouseOffTexture = contentManager.Load<Texture2D>("Textures/button_48x16");
            this.mouseOverTexture = contentManager.Load<Texture2D>("Textures/button_48x16_mouseOver");
            this.effect = contentManager.Load<SoundEffect>("Sounds/buttonClick");
        }

        public override void Update(GameTime gameTime, MouseHelper mouseHelper)
        {
            if (this.texture == this.mouseOverTexture)
                this.texture = this.mouseOffTexture;
            if ((double)mouseHelper.getPosition().X < (double)this.position.X || (double)mouseHelper.getPosition().X > (double)(this.position.X + this.position.Width) || (double)mouseHelper.getPosition().Y < (double)this.position.Y || (double)mouseHelper.getPosition().Y > (double)(this.position.Y + this.position.Height))
                return;
            this.texture = this.mouseOverTexture;
            if (mouseHelper.getLeftClicked())
            {
                this.effect.Play();
                this.pressedAction();
            }
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            // Position Rectangle

            // Texture Rectangles
/*            Rectangle topLeft = new Rectangle(0, 0, 4, 4);
            Rectangle left = new Rectangle(0, 4, 4, 24);
            Rectangle bottomLeft = new Rectangle(0, 28, 4, 4);

            Rectangle top = new Rectangle(4, 0, 24, 4);
            Rectangle middle = new Rectangle(4, 4, 24, 24);
            Rectangle bottom = new Rectangle(4, 28, 24, 4);

            Rectangle topRight = new Rectangle(28, 0, 4, 4);
            Rectangle right = new Rectangle(28, 4, 4, 24);
            Rectangle bottomRight = new Rectangle(28, 28, 4, 4);
  
            spriteBatch.Draw(this.texture, new Vector2(this.position.X, this.position.Y), topLeft, Color.White, 0.0f, Vector2.Zero, 4.0f, SpriteEffects.None, 0);
            spriteBatch.Draw(this.texture, new Vector2(this.position.X, this.position.Y + 16), left, Color.White, 0.0f, Vector2.Zero, 4.0f, SpriteEffects.None, 0);
            spriteBatch.Draw(this.texture, new Vector2(this.position.X, this.position.Y + this.position.Height - 16), bottomLeft, Color.White, 0.0f, Vector2.Zero, 4.0f, SpriteEffects.None, 0);

            spriteBatch.Draw(this.texture, new Vector2(this.position.X + 16, this.position.Y), top, Color.White, 0.0f, Vector2.Zero, 4.0f, SpriteEffects.None, 0);
            spriteBatch.Draw(this.texture, new Vector2(this.position.X + 16, this.position.Y + 16), middle, Color.White, 0.0f, Vector2.Zero, 4.0f, SpriteEffects.None, 0);
            spriteBatch.Draw(this.texture, new Vector2(this.position.X + 16, this.position.Y + this.position.Height - 16), bottom, Color.White, 0.0f, Vector2.Zero, 4.0f, SpriteEffects.None, 0);

            spriteBatch.Draw(this.texture, new Vector2(this.position.X + this.position.Width - 16, this.position.Y), topRight, Color.White, 0.0f, Vector2.Zero, 4.0f, SpriteEffects.None, 0);
            spriteBatch.Draw(this.texture, new Vector2(this.position.X + this.position.Width - 16, this.position.Y + 4), right, Color.White, 0.0f, Vector2.Zero, 4.0f, SpriteEffects.None, 0);
            spriteBatch.Draw(this.texture, new Vector2(this.position.X + this.position.Width - 16, this.position.Y + this.position.Height - 16), bottomRight, Color.White, 0.0f, Vector2.Zero, 4.0f, SpriteEffects.None, 0);
*/
            spriteBatch.Draw(this.texture, this.position, Color.White);
            spriteBatch.DrawString(font, this.text, new Vector2((float)(this.position.X + this.position.Width / 2) - this.textSize.X / 2f, (float)(this.position.Y + this.position.Height / 2) - this.textSize.Y / 2f), Color.White);
        }
    }
}

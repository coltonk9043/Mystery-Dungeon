using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
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

        public ButtonWidget(Rectangle position, string text, SpriteFont font, Action func)
          : base(position, Game1.getInstance().contentManager.Load<Texture2D>("Textures/button_48x16"))
        {
            this.mouseOffTexture = Game1.getInstance().contentManager.Load<Texture2D>("Textures/button_48x16");
            this.mouseOverTexture = Game1.getInstance().contentManager.Load<Texture2D>("Textures/button_48x16_mouseOver");
            this.text = text;
            this.textSize = font.MeasureString(text);
            this.pressedAction = func;
            this.effect = Game1.getInstance().contentManager.Load<SoundEffect>("Sounds/buttonClick");
        }

        public ButtonWidget(
          Rectangle position,
          string texture,
          string text,
          SpriteFont font,
          Action func)
          : base(position, Game1.getInstance().contentManager.Load<Texture2D>(texture))
        {
            this.mouseOffTexture = Game1.getInstance().contentManager.Load<Texture2D>(texture);
            this.mouseOverTexture = Game1.getInstance().contentManager.Load<Texture2D>(texture + "_mouseOver");
            this.text = text;
            this.textSize = font.MeasureString(text);
            this.pressedAction = func;
            this.effect = Game1.getInstance().contentManager.Load<SoundEffect>("Sounds/buttonClick");
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
            spriteBatch.Draw(this.texture, this.position, Color.White);
            spriteBatch.DrawString(font, this.text, new Vector2((float)(this.position.X + this.position.Width / 2) - this.textSize.X / 2f, (float)(this.position.Y + this.position.Height / 2) - this.textSize.Y / 2f), Color.White);
        }
    }
}

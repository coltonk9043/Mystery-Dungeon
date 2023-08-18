using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonGame.UI.Widgets
{
    internal class SliderWidget : Widget
    {
        private Texture2D notch;
        private int minSliderPosition;
        private int maxSliderPosition;
        private int sliderPosition;

        public SliderWidget(ContentManager content, Rectangle position, int minSliderPosition, int maxSliderPosition) : base(content, position)
        {
            this.minSliderPosition = minSliderPosition;
            this.maxSliderPosition = maxSliderPosition;

        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Draw(this.texture, this.position, Color.White);
            spriteBatch.Draw(this.notch, new Rectangle(this.position.X + this.sliderPosition, this.position.Y, 64, 32), Color.White);
        }

        public override void Update(GameTime gameTime, MouseHelper mouseHelper)
        {
            if (!mouseHelper.getLeftDown() || (double)mouseHelper.getPosition().X < (double)(this.position.X + 32) || (double)mouseHelper.getPosition().X > (double)(this.position.X + this.position.Width - 32) || (double)mouseHelper.getPosition().Y < (double)this.position.Y || (double)mouseHelper.getPosition().Y > (double)(this.position.Y + this.position.Height))
                return;
            this.sliderPosition = (int)((double)mouseHelper.getPosition().X - 32.0 - (double)this.position.X);
        }

        public override void Load(ContentManager contentManager)
        {
            this.texture = contentManager.Load<Texture2D>("Textures/slider_background");
            this.notch = contentManager.Load<Texture2D>("Textures/slider_notch");
        }

        public int getSliderPosInt() => (int)((double)((float)this.sliderPosition / 256f) * (double)this.maxSliderPosition) - this.minSliderPosition;
    }
}

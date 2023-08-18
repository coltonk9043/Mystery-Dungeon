using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonGame.UI.Widgets
{
    public abstract class Widget
    {
        public Rectangle position;
        public Texture2D texture;

        public Widget(ContentManager content, Rectangle position, Texture2D texture)
        {
            this.Load(content);
            this.position = position;
            this.texture = texture;
        }

        public Widget(ContentManager content, Rectangle position)
        {
            this.Load(content);
            this.position = position;
            this.texture = (Texture2D)null;
        }

        public abstract void Load(ContentManager contentManager);
        
        public abstract void Update(GameTime gameTime, MouseHelper mouseHelper);

        public abstract void Draw(SpriteBatch spriteBatch, SpriteFont font);
    }
}

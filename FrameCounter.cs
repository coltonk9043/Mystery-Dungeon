// Colton K
// A class that calculates the average FPS of the game.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonGame
{
    internal class FrameCounter
    {
        private double Frames = 0.0;
        private double Updates = 0.0;
        private double Elapsed = 0.0;
        private double Last = 0.0;
        private double Now = 0.0;
        private double Frequency = 1.0;

        /// <summary>
        /// Every update, recalculate the new framerate.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            this.Now = gameTime.TotalGameTime.TotalSeconds;
            this.Elapsed = this.Now - this.Last;
            if (this.Elapsed > this.Frequency)
            {
                this.Elapsed = 0.0;
                this.Frames = 0.0;
                this.Updates = 0.0;
                this.Last = this.Now;
            }
            ++this.Updates;
        }

        /// <summary>
        /// Draws the FPS on the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="font"></param>
        public void DrawFps(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, "FPS: " + ((int)(this.Frames / this.Elapsed)).ToString(), new Vector2(4f, 4f), Color.White);
            ++this.Frames;
        }
    }
}

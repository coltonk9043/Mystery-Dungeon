// Colton K
// A class representing a terrain entitiy (Something which is not a tile, but will not act as a true entity.
using DungeonGame.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using BoundingBox = DungeonGame.Entities.BoundingBox;

namespace DungeonGame.Levels.TerrainFeatures
{
    public class TerrainEntity : Entity
    {
        // Variables.
        protected BoundingBox transparencyTrigger;
        protected bool isTransparent;
        protected int alpha = (int)byte.MaxValue;
        protected double timeSinceLastTransition;

        public TerrainEntity(Vector3 position)
          : base(position)
        {
        }

        /// <summary>
        /// Renders the terrain entity.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, new Vector2(this.position.X - (float)(this.texture.Width / 2), this.position.Y - (float)this.texture.Height), new Rectangle?(new Rectangle(0, 0, this.texture.Width, this.texture.Height)), new Color(this.alpha, this.alpha, this.alpha, this.alpha), 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
        }

        public override void handleCollision(Entity entity)
        {
        }

        /// <summary>
        /// Updates the entity and allows the player to be seen when 'underneith'
        /// </summary>
        /// <param name="gameTime"></param>
        public override void UpdateEntity(GameTime gameTime)
        {
            this.timeSinceLastTransition += gameTime.ElapsedGameTime.TotalMilliseconds;
            this.isTransparent = Game1.getInstance().player.GetBoundingBox().Intersection(this.transparencyTrigger) != null;
            if (this.isTransparent)
            {
                if (this.alpha <= 30 || this.timeSinceLastTransition <= 0.25)
                    return;
                this.alpha -= 15;
                this.timeSinceLastTransition = 0.0;
            }
            else if (this.alpha < (int)byte.MaxValue && this.timeSinceLastTransition > 0.25)
            {
                this.alpha += 15;
                this.timeSinceLastTransition = 0.0;
            }
        }

        /// <summary>
        /// When the Terrain Entity is clicked (Dependent on the type of terrain entity.)
        /// </summary>
        public override void onMouseClicked() => throw new NotImplementedException();
    }
}

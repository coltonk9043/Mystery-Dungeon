// Colton K
// A class representing a Tree Entity
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonGame.Levels.TerrainFeatures
{
    public class TreeEntity : TerrainEntity
    {
        /// <summary>
        /// Constructor for a Tree entity.
        /// </summary>
        /// <param name="position"></param>
        public TreeEntity(World world, Vector3 position)
          : base(world, position)
        {
            this.texture = Game1.getInstance().GetContentManager().Load<Texture2D>("Textures/tree");
            this.entityShadow = Game1.getInstance().GetContentManager().Load<Texture2D>("Textures/tree_Shadow");
            this.boundingBox = new DungeonGame.Entities.BoundingBox(this.position.X - 8f, this.position.Y - 16f, 16f, 16f);
            this.transparencyTrigger = new DungeonGame.Entities.BoundingBox(this.position.X - 24f, this.position.Y - 80f, 48f, 64f);
        }
    }
}

// Colton K
// A class representing a Torch entity.
using DungeonGame;
using DungeonGame.World.TerrainFeatures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dungeon.World.TerrainFeatures
{
    public class TorchEntity : TerrainEntity
    {
        /// <summary>
        /// Constructor for a Torch Entity.
        /// </summary>
        /// <param name="position"></param>
        public TorchEntity(Vector3 position) : base(position)
        {
            this.texture = Game1.getInstance().contentManager.Load<Texture2D>("Textures/Items/Torch");
            this.entityShadow = null;
            this.boundingBox = null;
            this.transparencyTrigger = new DungeonGame.Entities.BoundingBox(this.position.X - 24f, this.position.Y - 80f, 48f, 64f);
        }
    }
}

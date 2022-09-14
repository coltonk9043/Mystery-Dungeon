// Colton K
// A class representing a Torch entity.
using Dungeon.Entities;
using DungeonGame;
using DungeonGame.Levels.TerrainFeatures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dungeon.Levels.TerrainFeatures
{
    public class TorchEntity : TerrainEntity, ILightSource
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

        public Color Color { get => new Color(246, 154, 84, 255); set => throw new NotImplementedException(); }
        public float Radius { get => 20000.0f; set => throw new NotImplementedException(); }
        public float Intensity { get => 1.0f; set => throw new NotImplementedException(); }
    }
}

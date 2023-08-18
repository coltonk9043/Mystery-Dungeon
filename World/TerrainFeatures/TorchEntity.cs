// Colton K
// A class representing a Torch entity.
using Dungeon.Entities;
using Dungeon.Utilities;
using DungeonGame;
using DungeonGame.Levels;
using DungeonGame.Levels.TerrainFeatures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Dungeon.Levels.TerrainFeatures
{
    public class TorchEntity : TerrainEntity, ILightSource
    {

        private Color _Color = new Color(246, 154, 84, 255);
        private float _Radius = 512.0f;
        private bool _LightOn = true;

        private NoiseGenerator noise;

        /// <summary>
        /// Constructor for a Torch Entity.
        /// </summary>
        /// <param name="position"></param>
        public TorchEntity(World world, Vector3 position) : base(world,position)
        {
            this.texture = Game1.getInstance().GetContentManager().Load<Texture2D>("Textures/Items/Torch");
            this.entityShadow = null;
            this.boundingBox = null;
            this.transparencyTrigger = null;
            this.noise = new NoiseGenerator();
        }

        public override void UpdateEntity(GameTime gameTime)
        {
            base.UpdateEntity(gameTime);



            double noise = this.noise.GenerateNoise((float)gameTime.TotalGameTime.TotalMilliseconds / 512.0f, (float)gameTime.TotalGameTime.Milliseconds / 354.0f);
            _Radius = (float)(600 + (50 * noise));
        }

        public Color LightColor { get => _Color; set => _Color = value; }
        public float LightRadius { get => _Radius; set => _Radius = value; }
        public float LightIntensity { get => 1.0f; set => throw new NotImplementedException(); }
        public bool LightOn { get => _LightOn; set => throw new NotImplementedException(); }
    }
}

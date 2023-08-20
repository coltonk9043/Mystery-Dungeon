using Dungeon.Entities;
using Dungeon.Utilities;
using DungeonGame.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DungeonGame.Entities.Enemies
{
    internal class EntityBat : EnemyEntity
    {
        public EntityBat(World world, Vector3 position)
          : base(world, position)
        {
            Texture2D[,] texture2DArray = TextureUtils.create2DTextureArrayFromFile(this.world.GetCurrentGame().Content, "Textures/Enemies/bat", 24, 16);

            this.SouthFacingAnim = new Animation(new Texture2D[2]
            {
                texture2DArray[0, 0],
                texture2DArray[1, 0]
            }, 160f, true, true);


            this.currentAnimation = this.SouthFacingAnim;
            this.texture = this.SouthFacingAnim.getCurrentTexture();
            this.animated = true;
            this.boundingBox = new BoundingBox(this.position.X, this.position.Y, 20f, 6f);
            this.MovementSpeed = 0.5f;
            this.attackDamage = 10;
            this.attackSpeed = 1f;
        }
    }
}

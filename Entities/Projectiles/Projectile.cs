using DungeonGame;
using DungeonGame.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dungeon.Projectiles
{
    public class Projectile : Entity
    {
        protected float rotation;
        protected float rotationVelocity;

        private Entity shooter;
        protected int damage;
        protected bool breaksWhenHit = false;

        public Projectile(Vector3 position, Vector3 velocity, float rotation, float rotationVelocity, Entity shooter) : base(position)
        {

            this.rotation = rotation;
            this.rotationVelocity = rotationVelocity;

            Texture2D[,] texture2DArray = TextureUtils.create2DTextureArrayFromFile("Textures/fireball", 16, 16);
            this.animated = true;
            this.currentAnimation = new Animation(new Texture2D[4]
            {
        texture2DArray[0, 0],
        texture2DArray[1, 0],
        texture2DArray[2, 0],
        texture2DArray[3, 0]
            }, 160f, true, false);

            this.velocity = velocity;
            this.shooter = shooter;
        }


        public override void handleCollision(Entity entity)
        {
            if (entity is LivingEntity)
            {
                ((LivingEntity)entity).HP -= this.damage;
            }
        }

        public override void onMouseClicked()
        {
            return;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, new Vector2(this.position.X - (float)(this.texture.Width / 2), this.position.Y - (float)this.texture.Height - this.position.Z), new Rectangle?(new Rectangle(0, 0, this.texture.Width, this.texture.Height)), Color.White, this.rotation, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
        }

        public override void RenderEntityShadow(SpriteBatch spriteBatch)
        {
            return;
        }

        public override void UpdateEntity(GameTime gameTime)
        {
            return;
        }
    }
}

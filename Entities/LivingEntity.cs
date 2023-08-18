using DungeonGame.Item;
using DungeonGame.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DungeonGame.Entities
{
    public class LivingEntity : Entity
    {
        // Animation Variables
        public Animation NorthFacingAnim;
        public Animation EastFacingAnim;
        public Animation SouthFacingAnim;
        public Animation WestFacingAnim;

        // Character Variables
        protected float movementSpeed = 1f;
        protected float maxHP = 100f;
        protected float hp = 100f;
        protected float timeSinceLastSwung;
        protected float attackSpeed = 1f;
        protected int attackDamage = 1;
        protected float recoverTime = 2000f;
        protected bool wasHit;
        protected float hitColor;
        protected float timeLastHit;
        protected ItemStack currentItem;
        protected bool currentSwinging;

        // Pathfinding Variables
        protected List<Vector2> path;


        public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }
        public float MaxHP { get { return maxHP; } set { maxHP = value; } }
        public float HP { get { return hp; } set { hp = value; } }
        public ItemStack CurrentItem { get { return currentItem; } set { currentItem = value; } }

        public LivingEntity(World world, Vector3 position)
          : base(world, position)
        {
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, new Vector2(this.position.X - (float)(this.texture.Width / 2), this.position.Y - (float)this.texture.Height - this.position.Z), new Rectangle?(new Rectangle(0, 0, this.texture.Width, this.texture.Height)), this.wasHit ? new Color(this.hitColor, 50f, 50f) : Color.White, 0.0f, Vector2.Zero, 1 + (this.position.Z / 160.0f), SpriteEffects.None, 0.0f);
            //Texture2D black = Game1.black;
            //spriteBatch.Draw(black, new Rectangle((int)this.boundingBox.X, (int)this.boundingBox.Y, (int)this.boundingBox.Width, (int)this.boundingBox.Height), Color.White);
        }

        public override void HandleCollision(Entity entity)
        {
        }

        public override void UpdateEntity(GameTime gameTime)
        {
            this.timeSinceLastSwung += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if ((double)this.HP <= 0.0)
                this.removed = true;
            if (!this.wasHit)
                return;
            this.hitColor = (float)((double)byte.MaxValue * ((double)byte.MaxValue * ((double)this.timeLastHit / gameTime.TotalGameTime.TotalMilliseconds)));
            if ((double)this.recoverTime <= gameTime.TotalGameTime.TotalMilliseconds - (double)this.timeLastHit)
            {
                this.wasHit = false;
                this.timeLastHit = 0.0f;
            }
        }

        public void jump()
        {
            if(this.position.Z == 0.0f)
            {
                this.velocity.Z = 1.2f;
                this.acceleration.Z = 5f;
            }
        }

        public void onHit(LivingEntity entity, int dmg, GameTime gameTime)
        {
            this.wasHit = true;
            this.timeLastHit = (float)gameTime.TotalGameTime.TotalMilliseconds;
            this.HP -= (float)dmg;
            Vector3 vector3 = (this.position - entity.position) * 2f;
            vector3.Normalize();
            this.velocity = vector3;
        }

        public override void OnMouseClicked() => throw new NotImplementedException();

        public void Death()
        {
        }
    }
}

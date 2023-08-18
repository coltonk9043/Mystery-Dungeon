// Colton K
// An abstract class for an Entity in the game.
using Dungeon.Projectiles;
using DungeonGame.Levels;
using DungeonGame.Levels.TerrainFeatures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DungeonGame.Entities
{
    public abstract class Entity
    {
        /**
         * Physics Variables
         */
        public Vector3 position;
        public Vector3 velocity;
        public Vector3 acceleration;
        public bool isKinematic = false;
        public Direction facing;

        public Entity Parent { get; set; }
        public List<Entity> Children { get; set; }

        protected World world;
        public World GetWorld() { return world; }

        public Queue<Vector2> path;

        public bool removed;

        protected Texture2D texture;
        protected Texture2D entityShadow;
        protected Animation currentAnimation;
        protected BoundingBox boundingBox;

        protected bool animated { get; set; }

        public enum Direction
        {
            NORTH,
            EAST,
            SOUTH,
            WEST,
        }

        /// <summary>
        /// Abstract Constructor for a basic entity.
        /// </summary>
        /// <param name="position"></param>
        public Entity(World world, Vector3 position)
        {
            this.world = world;
            this.position = position;
            this.velocity = new Vector3();
            this.acceleration = new Vector3();
            this.entityShadow = Game1.getInstance().Content.Load<Texture2D>("Textures/entity_shadow");
        }

        public void AddChild(Entity child)
        {
            child.Parent = this;
            Children.Add(child);
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="world"></param>
        public void Update(GameTime gameTime, Levels.World world)
        {
            // If the entity is animated, update the animation.
            if (this.animated)
            {
                this.currentAnimation.Update(gameTime);
                this.texture = this.currentAnimation.getCurrentTexture();
            }

            if (!this.isKinematic)
                this.acceleration.Z = -9.81f;

            // Runs an update method.
            this.UpdateEntity(gameTime);

            this.velocity += acceleration * (gameTime.ElapsedGameTime.Milliseconds / 1000.0f);

            // Precalculates the next position that the player would be.
            float nextPosX = this.position.X + this.velocity.X;
            float nextPosY = this.position.Y + this.velocity.Y;

            // Creates a new bounding box to check whether or not the next position is inside of a collidable tile/entity.
            if (this.boundingBox != null)
            {
                BoundingBox bb = new BoundingBox(nextPosX - (this.boundingBox.Width / 2f), nextPosY - this.boundingBox.Height, boundingBox.Width, boundingBox.Height);
                this.CheckWorldCollision(bb, world, nextPosX, nextPosY);
                this.CheckEntityCollisions(bb);
            }

            
            // Update player's position and bounding box accordingly.
            this.position.X += this.velocity.X;
            this.position.Y += this.velocity.Y;
            this.position.Z += this.velocity.Z;

            this.position.Z = MathF.Max(0, this.position.Z);
            if (this.boundingBox != null)
            {
                this.boundingBox.X = this.position.X - this.boundingBox.Width / 2f;
                this.boundingBox.Y = this.position.Y - this.boundingBox.Height;
            }
        }

        public abstract void Render(SpriteBatch spriteBatch);

        public abstract void OnMouseClicked();

        public virtual void RenderEntityShadow(SpriteBatch spriteBatch)
        {
            if (this.entityShadow == null) return;
            spriteBatch.Draw(this.entityShadow, new Vector2(this.position.X - (float)(this.entityShadow.Width / 2), this.position.Y - (float)(this.entityShadow.Height / 2)), new Rectangle?(new Rectangle(0, 0, this.entityShadow.Width, this.entityShadow.Height)), Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
        }

        public void CheckWorldCollision(BoundingBox currentBoundingBox, Levels.World world, float posX, float posY)
        {
            Vector2 entityPosAsTilePos = this.GetPositionAsTilePos(posX, posY);
            BoundingBox boundingBox1 = new BoundingBox(currentBoundingBox.X, this.boundingBox.Y, this.boundingBox.Width, this.boundingBox.Height);
            BoundingBox boundingBox2 = new BoundingBox(this.boundingBox.X, currentBoundingBox.Y, this.boundingBox.Width, this.boundingBox.Height);
            for (int layer = 0; layer < world.layers; layer++)
            {
                if (world.getLayer(layer).getCollidable())
                {
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            int offsetX = (int)entityPosAsTilePos.X - x;
                            int offsetY = (int)entityPosAsTilePos.Y - y;
                            int tile = world.getTile(layer, offsetX, offsetY);

                            if (tile != -1)
                            {
                                BoundingBox tileBB = new BoundingBox((float)offsetX * 16, (float)offsetY * 16, 16f, 16f);
                                BoundingBox boundingBox3 = boundingBox1.Intersection(tileBB);
                                BoundingBox boundingBox4 = boundingBox2.Intersection(tileBB);
                                if (boundingBox3 != null && (double)boundingBox3.Width > 0.0)
                                    this.velocity.X = 0.0f;
                                if (boundingBox4 != null && (double)boundingBox4.Height > 0.0)
                                    this.velocity.Y = 0.0f;
                               
                            }
                        }
                    }
                }
            }
        }

        public void CheckEntityCollisions(BoundingBox currentBoundingBox)
        {
            BoundingBox boundingBox1 = new BoundingBox(currentBoundingBox.X, this.boundingBox.Y, this.boundingBox.Width, this.boundingBox.Height);
            BoundingBox boundingBox2 = new BoundingBox(this.boundingBox.X, currentBoundingBox.Y, this.boundingBox.Width, this.boundingBox.Height);
            foreach (Entity entity in world.getEntities())
            {
                if (this != entity)
                {
                    if (entity is LivingEntity || entity is ItemEntity || entity is Projectile)
                    {
                        if (this.boundingBox.Intersection(entity.boundingBox) != null)
                            this.HandleCollision(entity);
                    }
                    else
                    {
                        TerrainEntity terrainEntity = (TerrainEntity)entity;
                        BoundingBox boundingBox3 = boundingBox1.Intersection(terrainEntity.boundingBox);
                        BoundingBox boundingBox4 = boundingBox2.Intersection(terrainEntity.boundingBox);
                        if (boundingBox3 != null && (double)boundingBox3.Width > 0.0)
                            this.velocity.X = 0.0f;
                        if (boundingBox4 != null && (double)boundingBox4.Height > 0.0)
                            this.velocity.Y = 0.0f;
                    }
                }
            }
        }

        public abstract void HandleCollision(Entity entity);

        public abstract void UpdateEntity(GameTime gameTime);

        public float DistanceFromEntity(Entity entity)
        {
            float num1 = this.position.X - entity.position.X;
            float num2 = this.position.Y - entity.position.Y;
            return (float)Math.Sqrt((double)num1 * (double)num1 + (double)num2 * (double)num2);
        }

        public float EuclidianDistanceFromEntity(Entity entity)
        {
            float num1 = this.position.X - entity.position.X;
            float num2 = this.position.Y - entity.position.Y;
            return num1 + num2;
        }

        public Vector2 GetEntityPosAsTilePos() => new Vector2((float)(int)(this.position.X / 16.0), (float)(int)(this.position.Y / 16.0));
        public Vector2 GetPositionAsTilePos(float x, float y) => new Vector2((float)(int)(x / 16.0), (float)(int)(y / 16.0));

        /// <summary>
        /// Sets the current Animation.
        /// </summary>
        /// <param name="animation"></param>
        public void SetAnimation(Animation animation)
        {
            if (this.currentAnimation == animation)
                return;
            this.currentAnimation.Stop();
            this.currentAnimation = animation;
            this.currentAnimation.Start();
        }

        /// <summary>
        /// Returns the current Texture2D used by the Entity.
        /// </summary>
        /// <returns>Textures currently being used by this Entity as a Texture2D.</returns>
        public Texture2D GetTexture() { return this.texture; }

        /// <summary>
        /// Returns the BoundingBox currently used by the Entity.
        /// </summary>
        /// <returns></returns>
        public BoundingBox GetBoundingBox() { return this.boundingBox; }

        public void playAnimationOnce()
        {
        }
    }
}

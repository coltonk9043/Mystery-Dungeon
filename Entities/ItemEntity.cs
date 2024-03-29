﻿using Dungeon.Entities;
using DungeonGame.Entities.Player;
using DungeonGame.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DungeonGame.Entities
{
    internal class ItemEntity : Entity
    {
        public DungeonGame.Item.AbstractItem item;
        private Random rand;

        private Color _color;


        public ItemEntity(World world, Vector3 position, DungeonGame.Item.AbstractItem item)
          : base(world, position)
        {
            this.item = item;
            this.isKinematic = true;
            this.texture = this.item.getTexture();
            this.boundingBox = new BoundingBox(this.position.X, this.position.Y, (float)this.texture.Width, (float)this.texture.Height);
            this.rand = new Random();

            _color = new Color(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255), 255);
        }

        public override void HandleCollision(Entity entity)
        {
            if (entity != Game1.getInstance().player)
                return;
            ((AbstractPlayer)entity).inventory.addItem(this.item);
            this.removed = true;
        }

        public override void OnMouseClicked()
        {
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, new Vector2(this.position.X - (float)(this.texture.Width / 2), this.position.Y - (float)this.texture.Height - this.position.Z), new Rectangle?(new Rectangle(0, 0, this.texture.Width, this.texture.Height)), Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
            //Texture2D black = Game1.black;
            //spriteBatch.Draw(black, new Rectangle((int) this.boundingBox.X, (int) this.boundingBox.Y, (int) this.boundingBox.Width, (int) this.boundingBox.Height), Color.White);
        }

        public override void RenderEntityShadow(SpriteBatch spriteBatch) => spriteBatch.Draw(this.entityShadow, new Vector2(this.position.X - (float)(this.entityShadow.Width / 4), this.position.Y - (float)(this.entityShadow.Height / 4)), new Rectangle?(new Rectangle(0, 0, this.entityShadow.Width / 2, this.entityShadow.Height / 2)), Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);

        public override void UpdateEntity(GameTime gameTime) => this.position.Z = (float)(Math.Sin(gameTime.TotalGameTime.TotalMilliseconds / 200.0) + 1.0);
    }
}

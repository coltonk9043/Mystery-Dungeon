// Decompiled with JetBrains decompiler
// Type: DungeonGame.Entities.Player.AbstractPlayer
// Assembly: DungeonGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E78E8B53-5180-47B9-9458-06A9AF653F10
// Assembly location: C:\Users\Colton's PC\Documents\Games\Dungeon\Dungeon\bin\Debug\netcoreapp3.1\DungeonGame.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonGame.Entities.Player
{
    public class AbstractPlayer : LivingEntity
    {
        public Inventory inventory;

        private float mana = 20;
        private float stamina = 100;

        public bool showingItem;
        private double timeShowedItem;
        private Texture2D holdingItemTexture;
        public Animation SwordSwingSouth;

        public float Mana { get { return mana; } set { mana = value; } }
        public float Stamina { get { return stamina; } set { stamina = value; } }

        public AbstractPlayer(Texture2D texture, Vector3 position)
          : base(position)
        {
            this.texture = texture;
            Texture2D[,] texture2DArray = TextureUtils.create2DTextureArrayFromFile("Textures/player_spritesheet", 16, 24);
            this.NorthFacingAnim = new Animation(new Texture2D[4]
            {
        texture2DArray[0, 2],
        texture2DArray[1, 2],
        texture2DArray[0, 2],
        texture2DArray[2, 2]
            }, 160f, true, false);
            this.EastFacingAnim = new Animation(new Texture2D[4]
            {
        texture2DArray[0, 3],
        texture2DArray[1, 3],
        texture2DArray[0, 3],
        texture2DArray[2, 3]
            }, 160f, true, false);
            this.SouthFacingAnim = new Animation(new Texture2D[4]
            {
        texture2DArray[0, 0],
        texture2DArray[1, 0],
        texture2DArray[0, 0],
        texture2DArray[2, 0]
            }, 160f, true, false);
            this.WestFacingAnim = new Animation(new Texture2D[4]
            {
        texture2DArray[0, 1],
        texture2DArray[1, 1],
        texture2DArray[0, 1],
        texture2DArray[2, 1]
            }, 160f, true, false);
            this.SwordSwingSouth = new Animation(new Texture2D[4]
            {
        texture2DArray[4, 4],
        texture2DArray[5, 4],
        texture2DArray[6, 4],
        texture2DArray[7, 4]
            }, 45f, false, false);
            this.holdingItemTexture = texture2DArray[0, 4];
            this.currentAnimation = this.SouthFacingAnim;
            this.texture = this.SouthFacingAnim.getCurrentTexture();
            this.boundingBox = new DungeonGame.Entities.BoundingBox(this.position.X, this.position.Y, 8f, 8f);
            this.animated = true;
            this.inventory = new Inventory();
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            if (this.showingItem)
            {
                spriteBatch.Draw(this.holdingItemTexture, new Vector2(this.position.X - (float)(this.texture.Width / 2), this.position.Y - (float)this.texture.Height), new Rectangle?(new Rectangle(0, 0, this.texture.Width, this.texture.Height)), Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
                Texture2D texture = this.CurrentItem.getItem().getTexture();
                spriteBatch.Draw(this.CurrentItem.getItem().getTexture(), new Vector2(this.position.X - (float)(texture.Width / 4), (float)((double)this.position.Y - (double)texture.Height - 1.0)), new Rectangle?(new Rectangle(0, 0, texture.Width, texture.Height)), Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.0f);
            }
            else
                base.Render(spriteBatch);
        }

        public override void handleCollision(Entity entity)
        {
        }

        public override void UpdateEntity(GameTime gameTime)
        {
            this.MovePlayer(gameTime);
            if (!this.showingItem)
                return;
            if (this.timeShowedItem == 0.0)
                this.timeShowedItem = gameTime.TotalGameTime.TotalSeconds;
            if (gameTime.TotalGameTime.TotalSeconds - this.timeShowedItem > 2.0)
            {
                this.showingItem = false;
                this.timeShowedItem = 0.0;
            }
        }

        public virtual void MovePlayer(GameTime gameTime)
        {
        }

        public void showItem() => this.showingItem = true;
    }
}

using DungeonGame.Entities;
using DungeonGame.Entities.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonGame.Item
{
    public class AbstractItem
    {
        private int maxStackSize;
        private bool usable;
        private Texture2D texture;
        private int rarity;

        public AbstractItem(string texture, bool usable, int rarity, int maxStackSize)
        {
            this.texture = Game1.getInstance().GetContentManager().Load<Texture2D>("Textures/Items/" + texture);
            this.usable = usable;
            this.rarity = rarity;
            this.maxStackSize = maxStackSize;
        }

        public virtual void Use(LivingEntity user, GameTime gameTime)
        {
            if (!(user is AbstractPlayer))
                return;
            ((AbstractPlayer)user).showItem();
        }

        public Texture2D getTexture() => this.texture;

        public int getMaxStackSize() => this.maxStackSize;
    }
}

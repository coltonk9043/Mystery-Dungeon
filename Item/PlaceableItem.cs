using DungeonGame;
using DungeonGame.Entities;
using DungeonGame.Entities.Player;
using DungeonGame.Item;
using Microsoft.Xna.Framework;
using static DungeonGame.Entities.Entity;


namespace Dungeon.Item
{
    public class PlaceableItem : AbstractItem
    {
        Entity entityType;
        public PlaceableItem(string texture, bool usable, int rarity, int maxStackSize) : base(texture, usable, rarity, maxStackSize)
        {
            
        }

        public override void Use(LivingEntity user, GameTime gameTime)
        {
            if (!(user is AbstractPlayer))
                return;
            Vector2 mousePosRelativeToWorld = Game1.getInstance().mainCamera.getMousePositionRelativeToWorld();
        }
    }
}

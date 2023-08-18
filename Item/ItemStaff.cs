using DungeonGame.Entities.Player;
using DungeonGame.Entities;
using DungeonGame.Item;
using DungeonGame;
using Microsoft.Xna.Framework;
using System;
using Dungeon.Projectiles;

namespace Dungeon.Item
{
    internal class ItemStaff : AbstractItem
    {
        public ItemStaff(
          string texture,
          bool usable,
          int rarity,
          int maxStackSize
)
          : base(texture, usable, rarity, maxStackSize)
        {
            
        }

        public override void Use(LivingEntity user, GameTime gameTime)
        {
            if (user is AbstractPlayer)
            {
                AbstractPlayer abstractPlayer = (AbstractPlayer)user;
                user.SetAnimation(abstractPlayer.SwordSwingSouth);
            }

            Game1 game = Game1.getInstance();
            Vector2 mousePos = game.GetCamera().getMousePositionRelativeToWorld(game.GetMouseHelper());
            Vector3 newVelocity = Vector3.Normalize(new Vector3(mousePos.X, mousePos.Y, 0) - game.player.position) * 10;
            float rotation = (float)(Math.Atan2(mousePos.Y - game.player.position.Y, mousePos.X - game.player.position.X));
            Projectile newProj = new Projectile(user.GetWorld(), game.player.position + new Vector3(game.player.GetTexture().Width / 2, game.player.GetTexture().Height / 2, 0), newVelocity, rotation, 0, game.player);
            game.GetWorld().addEntity(newProj);
        }
    }
}

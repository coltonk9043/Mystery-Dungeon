// Decompiled with JetBrains decompiler
// Type: DungeonGame.Item.ItemSword
// Assembly: DungeonGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E78E8B53-5180-47B9-9458-06A9AF653F10
// Assembly location: C:\Users\Colton's PC\Documents\Games\Dungeon\Dungeon\bin\Debug\netcoreapp3.1\DungeonGame.dll

using DungeonGame.Entities;
using DungeonGame.Entities.Player;
using Microsoft.Xna.Framework;
using static DungeonGame.Entities.Entity;
using BoundingBox = DungeonGame.Entities.BoundingBox;

namespace DungeonGame.Item
{
  public class ItemSword : AbstractItem
  {
    private SwordProperties swordProperties;

    public ItemSword(
      string texture,
      bool usable,
      int rarity,
      int maxStackSize,
      SwordProperties swordProperties)
      : base(texture, usable, rarity, maxStackSize)
    {
      this.swordProperties = swordProperties;
    }

    public SwordProperties GetSwordProperties() => this.swordProperties;

    public override void Use(LivingEntity user, GameTime gameTime)
    {
      if (user is AbstractPlayer)
      {
        AbstractPlayer abstractPlayer = (AbstractPlayer) user;
        user.SetAnimation(abstractPlayer.SwordSwingSouth);
      }
      BoundingBox areaOfEffect = this.GetAreaOfEffect((Entity) user);
      foreach (Entity entity in Game1.getInstance().GetWorld().getEntities())
      {
        if (entity is LivingEntity && entity != user)
        {
          LivingEntity livingEntity = (LivingEntity) entity;
          if (livingEntity.GetBoundingBox().Intersection(areaOfEffect) != null)
            livingEntity.onHit(user, this.swordProperties.getDamage(), gameTime);
        }
      }
    }

    public BoundingBox GetAreaOfEffect(Entity user)
    {
      Vector3 position = user.position;
      BoundingBox boundingBox = new BoundingBox(position.X - (float) (this.getTexture().Width / 2), position.Y - 64f, (float) this.getTexture().Width, (float) (this.getTexture().Height + 64));
      switch (user.facing)
      {
        case Direction.NORTH:
          boundingBox = new BoundingBox(position.X - (float) (this.getTexture().Width / 2), position.Y + (float) this.getTexture().Height, (float) this.getTexture().Width, (float) this.getTexture().Height);
          break;
        case Direction.EAST:
          boundingBox = new BoundingBox(position.X - 64f, position.Y + (float) (this.getTexture().Height / 2), (float) (this.getTexture().Width + 64), (float) this.getTexture().Height);
          break;
        case Direction.SOUTH:
          boundingBox = new BoundingBox(position.X - (float) (this.getTexture().Width / 2), position.Y - 64f, (float) this.getTexture().Width, (float) (this.getTexture().Height + 64));
          break;
        case Direction.WEST:
          boundingBox = new BoundingBox(position.X - (float) this.getTexture().Width, position.Y + (float) (this.getTexture().Height / 2), (float) this.getTexture().Width, (float) this.getTexture().Height);
          break;
      }
      return boundingBox;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: DungeonGame.Entities.Enemies.EnemyEntity
// Assembly: DungeonGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E78E8B53-5180-47B9-9458-06A9AF653F10
// Assembly location: C:\Users\Colton's PC\Documents\Games\Dungeon\Dungeon\bin\Debug\netcoreapp3.1\DungeonGame.dll

using DungeonGame.Entities.Player;
using DungeonGame.Levels;
using Microsoft.Xna.Framework;

namespace DungeonGame.Entities.Enemies
{
    internal class EnemyEntity : LivingEntity
    {
        public LivingEntity currentAgro;
        public float agroRange = 64f;
        public Vector3 spawnLocation;
        private bool findPathOnce = true;

        public EnemyEntity(World world, Vector3 position)
          : base(world, position)
        {
            this.spawnLocation = position;
        }

        public override void UpdateEntity(GameTime gameTime)
        {
            base.UpdateEntity(gameTime);
            if (this.currentAgro != null)
            {
                if (this.wasHit)
                    return;
                Vector3 vector3 = this.currentAgro.position - this.position;
                vector3.Normalize();
                this.velocity = vector3 * this.MovementSpeed;
                if (this.DistanceFromEntity((Entity)this.currentAgro) >this.agroRange)
                    this.currentAgro = null;
            }
            else
            {
                this.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                foreach (AbstractPlayer player in Game1.getInstance().GetWorld().getPlayers())
                {
                    if ((double)this.DistanceFromEntity((Entity)player) <= (double)this.agroRange)
                    {
                        this.currentAgro = player;
                    }
                }
            }
        }

        public override void HandleCollision(Entity entity)
        {
            if (!(entity is ClientPlayer))
                return;
            this.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            if ((double)this.timeSinceLastSwung > 1000.0 / (double)this.attackSpeed)
            {
                ((LivingEntity)entity).HP -= (float)this.attackDamage;
                this.timeSinceLastSwung = 0.0f;
            }
        }
    }
}

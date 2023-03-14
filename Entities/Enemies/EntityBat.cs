// Decompiled with JetBrains decompiler
// Type: DungeonGame.Entities.Enemies.EntityBat
// Assembly: DungeonGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E78E8B53-5180-47B9-9458-06A9AF653F10
// Assembly location: C:\Users\Colton's PC\Documents\Games\Dungeon\Dungeon\bin\Debug\netcoreapp3.1\DungeonGame.dll

using Dungeon.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DungeonGame.Entities.Enemies
{
    internal class EntityBat : EnemyEntity, ILightSource
    {
        public EntityBat(Vector3 position)
          : base(position)
        {
            Texture2D[,] texture2DArray = TextureUtils.create2DTextureArrayFromFile("Textures/Enemies/bat", 24, 16);
            this.SouthFacingAnim = new Animation(new Texture2D[2]
            {
        texture2DArray[0, 0],
        texture2DArray[1, 0]
            }, 160f, true, true);
            this.currentAnimation = this.SouthFacingAnim;
            this.texture = this.SouthFacingAnim.getCurrentTexture();
            this.animated = true;
            this.boundingBox = new BoundingBox(this.position.X, this.position.Y, 20f, 6f);
            this.MovementSpeed = 0.5f;
            this.attackDamage = 10;
            this.attackSpeed = 1f;
        }

        public Color Color { get => new Color(0, 166, 255, 255); set => throw new NotImplementedException(); }
        public float Radius { get => 20000.0f; set => throw new NotImplementedException(); }
        public float Intensity { get => 1.0f; set => throw new NotImplementedException(); }
    }
}

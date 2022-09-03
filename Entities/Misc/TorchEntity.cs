// Decompiled with JetBrains decompiler
// Type: DungeonGame.Entities.Misc.TorchEntity
// Assembly: DungeonGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E78E8B53-5180-47B9-9458-06A9AF653F10
// Assembly location: C:\Users\Colton's PC\Documents\Games\Dungeon\Dungeon\bin\Debug\netcoreapp3.1\DungeonGame.dll
using Dungeon.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonGame.Entities.Misc
{
    public class TorchEntity : Entity, ILightSource
    {
        private string _name;
        private bool isStatic;
        private bool isDynamic;

        public Color Color { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public float Radius { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public float Intensity { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public TorchEntity(Vector3 position): base(position)
        {
            this.position = position;
            this.velocity = new Vector3();
            this.acceleration = new Vector3();
            this.entityShadow = Game1.getInstance().Content.Load<Texture2D>("Textures/entity_shadow");
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            throw new System.NotImplementedException();
        }

        public override void onMouseClicked()
        {
            throw new System.NotImplementedException();
        }

        public override void handleCollision(Entity entity)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateEntity(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
    }
}

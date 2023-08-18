// Colton K
// A generic camera entity.
using Dungeon;
using DungeonGame.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DungeonGame.Entities
{
    public class Camera : Entity
    {
        private GenericGame game;
        private Entity entityToFollow;
        private float zoom = 4f;

        /// <summary>
        /// Constructor for the camera.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="entityToFollow"></param>
        public Camera(GenericGame game, World world, GraphicsDeviceManager graphics, Entity entityToFollow)
          : base(world, entityToFollow.position)
        {
            this.game = game;
            this.entityToFollow = entityToFollow;
            this.position = entityToFollow.position;
        }

        /// <summary>
        /// Updates the camera entity.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="gameTime"></param>
        public void Update(GraphicsDeviceManager graphics, GameTime gameTime)
        {
            this.position.X = this.entityToFollow.position.X;
            this.position.Y = this.entityToFollow.position.Y - 24f;
        }

        /// <summary>
        /// Gets the transformation matrix of the camera.
        /// </summary>
        /// <returns></returns>
        public Matrix GetTransform() => Matrix.CreateTranslation(-this.position.X, -this.position.Y - (this.entityToFollow.GetTexture().Height / 2), 0.0f) * Matrix.CreateScale(new Vector3(this.zoom, this.zoom, 1f)) * Matrix.CreateTranslation((float)(game.ScreenWidth / 2), (float)(game.ScreenHeight / 2), 0.0f);

        public Matrix GetLightingTransform() => Matrix.CreateTranslation(-this.position.X, -this.position.Y - (this.entityToFollow.GetTexture().Height / 2), 0.0f) * Matrix.CreateScale(new Vector3(this.zoom, -this.zoom, 1f)) * Matrix.CreateTranslation((float)(game.ScreenWidth / 2), (float)(game.ScreenHeight / 2), 0.0f);




        /// <summary>
        /// Gets the mouse position relative to the world.
        /// </summary>
        /// <returns></returns>
        public Vector2 getMousePositionRelativeToWorld(MouseHelper mouseHelper)
        {
            Vector2 position = mouseHelper.getPosition();
            return new Vector2((this.position.X * this.zoom + position.X - (float)(game.ScreenWidth / 2) + this.entityToFollow.GetTexture().Width) / this.zoom, (this.position.Y * this.zoom + position.Y - (float)(game.ScreenHeight / 2) + this.entityToFollow.GetTexture().Height) / this.zoom);
        }

        public void setZoom(float zoom) => this.zoom = zoom;

        public void zoomIn() => this.zoom -= 0.1f;

        public void zoomOut() => this.zoom += 0.1f;

        public float getZoom() => this.zoom;

        public override void Render(SpriteBatch spriteBatch) => throw new NotImplementedException();

        public override void HandleCollision(Entity entity) => throw new NotImplementedException();

        public override void UpdateEntity(GameTime gameTime) => throw new NotImplementedException();

        public override void OnMouseClicked() => throw new NotImplementedException();
    }
}

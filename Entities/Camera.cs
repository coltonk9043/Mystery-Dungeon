// Colton K
// A generic camera entity.
using Dungeon;
using DungeonGame.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DungeonGame.Entities
{
    public class Camera : Entity
    {
        private GenericGame game;
        private Entity entityToFollow;
        private float zoom = 4f;

        public Camera(GenericGame game, World world, GraphicsDeviceManager graphics) : base(world, new Vector3(0, 0, 0))
        {
            this.game = game;
        }



        /// <summary>
        /// Constructor for the camera.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="entityToFollow"></param>
        public Camera(GenericGame game, World world, GraphicsDeviceManager graphics, Entity entityToFollow) : base(world, entityToFollow.position)
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
            if (this.entityToFollow != null)
            {
                this.position.X = this.entityToFollow.position.X;
                this.position.Y = this.entityToFollow.position.Y - 24f;
            }
            else
            {
                this.velocity = new Vector3(0.0f, 0.0f, this.velocity.Z);

                if (Keyboard.GetState().IsKeyDown(game.settings.bindForward))
                {
                    this.velocity.Y = -2.0f;
                }
                if (Keyboard.GetState().IsKeyDown(game.settings.bindBackwards))
                {
                    this.velocity.Y = 2.0f;
                }
                if (Keyboard.GetState().IsKeyDown(game.settings.bindLeft))
                    this.velocity.X = -2.0f;

                if (Keyboard.GetState().IsKeyDown(game.settings.bindRight))
                {
                    this.velocity.X = 2.0f;
                }

                this.position.X += this.velocity.X;
                this.position.Y += this.velocity.Y;
            }
        }

        /// <summary>
        /// Gets the transformation matrix of the camera.
        /// </summary>
        /// <returns></returns>
        public Matrix GetTransform()
        {
            if (this.entityToFollow != null)
            {
                return Matrix.CreateTranslation(-this.position.X, -this.position.Y - (this.entityToFollow.GetTexture().Height / 2), 0.0f) * Matrix.CreateScale(new Vector3(this.zoom, this.zoom, 1f)) * Matrix.CreateTranslation((float)(game.ScreenWidth / 2), (float)(game.ScreenHeight / 2), 0.0f);
            }
            else
            {
                return Matrix.CreateTranslation(-this.position.X, -this.position.Y, 0.0f) * Matrix.CreateScale(new Vector3(this.zoom, this.zoom, 1f)) * Matrix.CreateTranslation((float)(game.ScreenWidth / 2), (float)(game.ScreenHeight / 2), 0.0f);
            }
        }

        public Matrix GetLightingTransform()
        {
            if (this.entityToFollow != null)
            {
                return Matrix.CreateTranslation(-this.position.X, -this.position.Y - (this.entityToFollow.GetTexture().Height / 2), 0.0f) * Matrix.CreateScale(new Vector3(this.zoom, -this.zoom, 1f)) * Matrix.CreateTranslation((float)(game.ScreenWidth / 2), (float)(game.ScreenHeight / 2), 0.0f);
            }
            else
            {
                return Matrix.CreateTranslation(-this.position.X, -this.position.Y, 0.0f) * Matrix.CreateScale(new Vector3(this.zoom, -this.zoom, 1f)) * Matrix.CreateTranslation((float)(game.ScreenWidth / 2), (float)(game.ScreenHeight / 2), 0.0f);
            }
        }


        /// <summary>
        /// Gets the mouse position relative to the world.
        /// </summary>
        /// <returns></returns>
        public Vector2 getMousePositionRelativeToWorld(MouseHelper mouseHelper)
        {
            Vector2 position = mouseHelper.getPosition();
            if (this.entityToFollow != null)
            {
                return new Vector2((this.position.X * this.zoom + position.X - (float)(game.ScreenWidth / 2) + this.entityToFollow.GetTexture().Width) / this.zoom, (this.position.Y * this.zoom + position.Y - (float)(game.ScreenHeight / 2) + this.entityToFollow.GetTexture().Height) / this.zoom);
            }
            else
            {
                return new Vector2((this.position.X * this.zoom + position.X - (game.ScreenWidth / 2)) / this.zoom, (this.position.Y * this.zoom + position.Y - (game.ScreenHeight / 2)) / this.zoom);
            }
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

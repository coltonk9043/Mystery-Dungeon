using Dungeon;
using DungeonGame.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DungeonGame.Entities.Player
{
    public class ClientPlayer : AbstractPlayer
    {
        private GenericGame game;
        public int currentHotbar = 0;

        public ClientPlayer(GenericGame game, World world, Texture2D texture, Vector3 position)
          : base(world, texture, position)
        {
            this.game = game;
            this.boundingBox = new BoundingBox(this.position.X, this.position.Y, 12f, 4f);
        }

        public override void MovePlayer(GameTime gameTime)
        {
            // Reset Velocity
            this.velocity = new Vector3(0.0f, 0.0f, this.velocity.Z);

            // If the player is not in a swinging animation.
            if (this.currentAnimation != this.SwordSwingSouth)
            {
                // Change the currently selected Hot Bar index when the user presses a numeric number.
                if (Keyboard.GetState().IsKeyDown(Keys.D1))
                    this.currentHotbar = 0;
                if (Keyboard.GetState().IsKeyDown(Keys.D2))
                    this.currentHotbar = 1;
                if (Keyboard.GetState().IsKeyDown(Keys.D3))
                    this.currentHotbar = 2;
                if (Keyboard.GetState().IsKeyDown(Keys.D4))
                    this.currentHotbar = 3;
                if (Keyboard.GetState().IsKeyDown(Keys.D5))
                    this.currentHotbar = 4;
                if (Keyboard.GetState().IsKeyDown(Keys.D6))
                    this.currentHotbar = 5;
                if (Keyboard.GetState().IsKeyDown(Keys.D7))
                    this.currentHotbar = 6;
                if (Keyboard.GetState().IsKeyDown(Keys.D8))
                    this.currentHotbar = 7;
                if (Keyboard.GetState().IsKeyDown(Keys.D9))
                    this.currentHotbar = 8;
                if (Keyboard.GetState().IsKeyDown(Keys.D0))
                    this.currentHotbar = 9;

                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    jump();
                }

                if (game.GetMouseHelper().getScrollOffset() < 0.0f)
                {
                    if (this.currentHotbar - 1 < 0)
                        this.currentHotbar = 9;
                    else
                        --this.currentHotbar;
                }
                else if (game.GetMouseHelper().getScrollOffset() > 0.0f)
                    this.currentHotbar = (this.currentHotbar + 1) % 9;


                this.CurrentItem = this.inventory.getHotbar()[this.currentHotbar];
                GamePadState state = GamePad.GetState(PlayerIndex.One);
                if (state.IsConnected)
                {
                    float x = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X;
                    float y = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y;
                    this.velocity.X = (this.MovementSpeed * x);
                    this.velocity.Y = -(this.MovementSpeed * y);
                }
                else
                {
                    bool flag = false;
                    Game1 instance = Game1.getInstance();
                    if (Game1.getInstance().getCurrentScreen() != null)
                        return;

                    if (Keyboard.GetState().IsKeyDown(instance.settings.bindForward))
                    {
                        if (!flag)
                        {
                            this.SetAnimation(this.NorthFacingAnim);
                            flag = true;
                        }
                        this.velocity.Y = -this.MovementSpeed;
                    }
                    if (Keyboard.GetState().IsKeyDown(instance.settings.bindBackwards))
                    {
                        if (!flag)
                        {
                            this.SetAnimation(this.SouthFacingAnim);
                            flag = true;
                        }
                        this.velocity.Y = this.MovementSpeed;
                    }
                    if (Keyboard.GetState().IsKeyDown(instance.settings.bindLeft))
                    {
                        if (!flag)
                        {
                            this.SetAnimation(this.WestFacingAnim);
                            flag = true;
                        }
                        this.velocity.X = -this.MovementSpeed;
                    }
                    if (Keyboard.GetState().IsKeyDown(instance.settings.bindRight))
                    {
                        if (!flag)
                        {
                            this.SetAnimation(this.EastFacingAnim);
                            flag = true;
                        }
                        this.velocity.X = this.MovementSpeed;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && this.position.Z <= 0.0f)
                        this.jump();


                    // Camera Zoom (Unnecessary, but will likely keep)
                    if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
                        game.GetCamera().zoomOut();

                    if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
                        game.GetCamera().zoomIn();

                    // Moves the player.
                    Vector3 velocity = this.velocity;
                    this.velocity.X = velocity.X * (float)Math.Abs(Math.Cos(velocity.Y * 0.785398163397448));
                    this.velocity.Y = velocity.Y * (float)Math.Abs(Math.Cos(velocity.X * 0.785398163397448));


                    if (this.currentAnimation != this.SwordSwingSouth && !flag)
                        this.currentAnimation.Stop();
                }
            }
            else if (!this.currentAnimation.isActive() && this.currentAnimation == this.SwordSwingSouth)
                this.currentAnimation = this.SouthFacingAnim;

            // Player using Item.
            if (game.GetMouseHelper().getLeftClicked() && this.CurrentItem != null)
            {
                Vector2 positionRelativeToWorld = game.GetCamera().getMousePositionRelativeToWorld(game.GetMouseHelper());
                if (positionRelativeToWorld.Y - this.position.Y < 0.0f)
                    this.facing = Direction.NORTH;
                else if (positionRelativeToWorld.Y - this.position.Y > 0.0f)
                    this.facing = Direction.SOUTH;
                if (positionRelativeToWorld.X - this.position.X > 0.0f)
                    this.facing = Direction.EAST;
                else if (positionRelativeToWorld.X - this.position.X < 0.0f)
                    this.facing = Direction.WEST;
                this.CurrentItem.getItem().Use(this, gameTime);
            }
        }
    }
}

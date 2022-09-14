using Dungeon.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using static DungeonGame.Levels.World;

namespace DungeonGame.Entities.Player
{
    public class ClientPlayer : AbstractPlayer
    {
        public int currentHotbar = 0;

        public ClientPlayer(Texture2D texture, Vector3 position)
          : base(texture, position)
        {
            this.boundingBox = new DungeonGame.Entities.BoundingBox(this.position.X, this.position.Y, 12f, 4f);
        }



        public override void MovePlayer(GameTime gameTime)
        {
            this.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            if (this.showingItem)
                return;
            if (this.currentAnimation != this.SwordSwingSouth)
            {
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
                if ((double)Game1.getInstance().mouseHelper.getScrollOffset() < 0.0)
                {
                    if (this.currentHotbar - 1 < 0)
                        this.currentHotbar = 9;
                    else
                        --this.currentHotbar;
                }
                else if ((double)Game1.getInstance().mouseHelper.getScrollOffset() > 0.0)
                    this.currentHotbar = (this.currentHotbar + 1) % 9;
                this.CurrentItem = this.inventory.getHotbar()[this.currentHotbar];
                GamePadState state = GamePad.GetState(PlayerIndex.One);
                if (state.IsConnected)
                {
                    double x = (double)GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X;
                    double y = (double)GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y;
                    this.velocity.X = (float)(this.MovementSpeed * x);
                    this.velocity.Y = (float)-(this.MovementSpeed * y);
                }
                else
                {
                    bool flag = false;
                    Game1 instance = Game1.getInstance();
                    if (Game1.getInstance().getCurrentScreen() != null)
                        return;
                    if (Keyboard.GetState().IsKeyDown(instance.settings.bindLeft))
                    {
                        this.setAnimation(this.WestFacingAnim);
                        this.velocity.X = -this.MovementSpeed;
                        flag = true;
                    }
                    if (Keyboard.GetState().IsKeyDown(instance.settings.bindRight))
                    {
                        this.setAnimation(this.EastFacingAnim);
                        this.velocity.X = this.MovementSpeed;
                        flag = true;
                    }
                    if (Keyboard.GetState().IsKeyDown(instance.settings.bindForward))
                    {
                        this.setAnimation(this.NorthFacingAnim);
                        this.velocity.Y = -this.MovementSpeed;
                        flag = true;
                    }
                    if (Keyboard.GetState().IsKeyDown(instance.settings.bindBackwards))
                    {
                        this.setAnimation(this.SouthFacingAnim);
                        this.velocity.Y = this.MovementSpeed;
                        flag = true;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && (double)this.position.Z == 0.0)
                        this.jump();
                    if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
                        Game1.getInstance().mainCamera.zoomOut();
                    if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
                        Game1.getInstance().mainCamera.zoomIn();
                    Vector3 velocity = this.velocity;
                    this.velocity.X = velocity.X * (float)Math.Abs(Math.Cos((double)velocity.Y * 0.785398163397448));
                    this.velocity.Y = velocity.Y * (float)Math.Abs(Math.Cos((double)velocity.X * 0.785398163397448));
                    if (this.currentAnimation != this.SwordSwingSouth && !flag)
                        this.currentAnimation.Stop();
                }
            }
            else if (!this.currentAnimation.isActive() && this.currentAnimation == this.SwordSwingSouth)
                this.currentAnimation = this.SouthFacingAnim;
            Vector2 positionRelativeToWorld = Game1.getInstance().mainCamera.getMousePositionRelativeToWorld();
            if ((double)positionRelativeToWorld.Y - (double)this.position.Y < 0.0)
                this.facing = Direction.NORTH;
            else if ((double)positionRelativeToWorld.Y - (double)this.position.Y > 0.0)
                this.facing = Direction.SOUTH;
            if ((double)positionRelativeToWorld.X - (double)this.position.X > 0.0)
                this.facing = Direction.EAST;
            else if ((double)positionRelativeToWorld.X - (double)this.position.X < 0.0)
                this.facing = Direction.WEST;
            if (Game1.getInstance().mouseHelper.getLeftClicked() && this.CurrentItem != null)
                this.CurrentItem.getItem().Use((LivingEntity)this, gameTime);
        }
    }
}

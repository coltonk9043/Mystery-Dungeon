// Colton K
// A class that handles most of the mouse logic ingame.
using Dungeon;
using DungeonGame.Entities;
using DungeonGame.Entities.NPCs;
using DungeonGame.Entities.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BoundingBox = DungeonGame.Entities.BoundingBox;

namespace DungeonGame
{
    public class MouseHelper
    {
        // Variables
        private GenericGame game;
        private Texture2D currentMouseCursor;
        private Texture2D defaultMouseCursor;
        private Texture2D dialogueMouseCursor;
        private BoundingBox mouseCollision = new DungeonGame.Entities.BoundingBox(0.0f, 0.0f, 1f, 1f);
        private bool playerInRange = false;

        // Mouse control Variables.
        private bool leftPreviouslyClicked;
        private bool leftClicked;
        private bool rightPreviouslyClicked;
        private bool rightClicked;

        private int previousScrollValue;
        private int currentMouseWheelValue;
        private int scrollOffset = 0;


        /// <summary>
        /// Constructor for the mouse helper.
        /// </summary>
        /// <param name="game"></param>
        public MouseHelper(GenericGame game)
        {
            this.game = game;
            this.defaultMouseCursor = this.game.GetContentManager().Load<Texture2D>("Textures/Gui/cursor");
            this.dialogueMouseCursor = this.game.GetContentManager().Load<Texture2D>("Textures/GUI/cursorDialogue");
            this.currentMouseCursor = this.defaultMouseCursor;
        }

        /// <summary>
        /// Updates the mouse helper.
        /// </summary>
        public void Update()
        {
            // Gets the position of the mouse relative to worldspace.
            Vector2 positionRelativeToWorld = this.game.GetCamera().getMousePositionRelativeToWorld(this);
            this.mouseCollision.X = positionRelativeToWorld.X;
            this.mouseCollision.Y = positionRelativeToWorld.Y + 12f;
            if (this.game.GetWorld() == null)
            {
                // For every entity on screen, check if the mouse is over top of an entity..
                Entity entity1 = (Entity)null;
                foreach (Entity entity2 in this.game.GetWorld().getEntities())
                {
                    // if the player is not a player, and it intersects, 
                    if (!(entity2 is ClientPlayer) && this.mouseCollision.Intersection(entity2.GetBoundingBox()) != null && entity2 is NPC)
                    {
                        entity1 = entity2;
                        this.currentMouseCursor = this.dialogueMouseCursor;
                        this.playerInRange = (double)entity2.DistanceFromEntity((Entity)this.game.GetPlayer()) <= 24.0;
                        break;
                    }
                }
                // If there is an Entity under the mouse and it is clicked, activate the onClicked method.
                if (entity1 == null)
                    this.currentMouseCursor = this.defaultMouseCursor;
                else if (this.getRightClicked() && this.playerInRange)
                    entity1.OnMouseClicked();
            }
            else
            {
                this.currentMouseCursor = this.defaultMouseCursor;
            }

            // Checks if the mouse wheel is used, if so, calculate the offset.
            this.currentMouseWheelValue = Mouse.GetState().ScrollWheelValue;
            if (this.currentMouseWheelValue == this.previousScrollValue)
            {
                this.scrollOffset = 0;
            }
            else if (this.currentMouseWheelValue != this.previousScrollValue)
            {
                this.scrollOffset = this.currentMouseWheelValue - this.previousScrollValue;
                this.previousScrollValue = this.currentMouseWheelValue;
            }

            // If the left mouse button is pressed, toggle it's state so that it is not spammed.
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (!this.leftPreviouslyClicked && !this.leftClicked)
                {
                    this.leftClicked = true;
                    this.leftPreviouslyClicked = true;
                }
                else
                    this.leftClicked = false;
            }
            else
            {
                this.leftPreviouslyClicked = false;
                this.rightClicked = false;
            }

            // If the right mouse button is pressed, toggle it's state so that it is not spammed.
            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                if (!this.rightPreviouslyClicked && !this.rightClicked)
                {
                    this.rightClicked = true;
                    this.rightPreviouslyClicked = true;
                }
                else
                    this.rightClicked = false;
            }
            else
            {
                this.rightPreviouslyClicked = false;
                this.rightClicked = false;
            }
        }

        /// <summary>
        /// Draws the Mouse cursor on screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, depthStencilState: DepthStencilState.None, rasterizerState: RasterizerState.CullCounterClockwise);
            Texture2D currentMouseCursor = this.currentMouseCursor;
            MouseState state = Mouse.GetState();
            Rectangle destinationRectangle = new Rectangle(state.X, state.Y, 64, 64);
            Color color = this.playerInRange ? Color.White : Color.Gray;
            spriteBatch.Draw(currentMouseCursor, destinationRectangle, color);
            spriteBatch.End();
        }

        /// <summary>
        /// Gets the position of the mouse in screen space.
        /// </summary>
        /// <returns></returns>
        public Vector2 getPosition()
        {
            MouseState state = Mouse.GetState();
            return new Vector2((float)state.X, (float)state.Y);
        }

        public int getScrollOffset() => this.scrollOffset;

        public bool getLeftClicked() => this.leftClicked;

        public bool getRightClicked() => this.rightClicked;

        public bool getLeftDown() => Mouse.GetState().LeftButton == ButtonState.Pressed;

        public bool getRightDown() => Mouse.GetState().RightButton == ButtonState.Pressed;
    }
}

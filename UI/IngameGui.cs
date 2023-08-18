using Dungeon;
using Dungeon.UI;
using DungeonGame.Entities.Player;
using DungeonGame.Item;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonGame.UI
{
    internal class IngameGui : Gui
    {
        private ClientPlayer player;
        private FrameCounter frameCounter;
        private bool debugEnabled;
        
        private Texture2D HPBackgroundL;
        private Texture2D StaminaBackgroundL;
        private Texture2D ManaBackgroundL;
        private Texture2D BarBackgroundMid;
        private Texture2D BarBackgroundR;
        private Texture2D BarSmallBackgroundMid;
        private Texture2D BarSmallBackgroundR;

        private ChatBoxGui chatGui;

        private int hpLength;
        private int staminaLength;
        private int manaLength;

        private Texture2D hpRect;
        private Texture2D inventoryHotbar;
        private Texture2D hotbarSelected;

        public IngameGui(GenericGame game, Gui parent, SpriteFont font, ClientPlayer player)
          : base(game, parent, font)
        {
            this.player = player;
            this.frameCounter = frameCounter;

            this.HPBackgroundL = game.GetContentManager().Load<Texture2D>("Textures/GUI/HPBackground_L");
            this.StaminaBackgroundL = game.GetContentManager().Load<Texture2D>("Textures/GUI/EnergyBackground_L");
            this.ManaBackgroundL = game.GetContentManager().Load<Texture2D>("Textures/GUI/ManaBackground_L");
 
            this.BarBackgroundMid = game.GetContentManager().Load<Texture2D>("Textures/GUI/BarBackground_MID");
            this.BarSmallBackgroundMid = game.GetContentManager().Load<Texture2D>("Textures/GUI/BarSmallBackground_MID");
            
            this.BarBackgroundR = game.GetContentManager().Load<Texture2D>("Textures/GUI/BarBackground_R");
            this.BarSmallBackgroundR = game.GetContentManager().Load<Texture2D>("Textures/GUI/BarSmallBackground_R");

            this.inventoryHotbar = game.GetContentManager().Load<Texture2D>("Textures/GUI/inventoryHotbar");
            this.hotbarSelected = game.GetContentManager().Load<Texture2D>("Textures/GUI/hotbar_select");
            this.hpRect = new Texture2D(Game1.getInstance().GraphicsDevice, 1, 1);
            this.hpRect.SetData<Color>(new Color[1]
            {
        new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue)
            });

            this.chatGui = new ChatBoxGui(game, this, font);
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (!this.debugEnabled)
            {
                // Drawing HP section of the UI.
                spriteBatch.Draw(this.HPBackgroundL, new Vector2(4f, 4f), new Rectangle?(new Rectangle(0, 0, 14, 12)), Color.White, 0.0f, Vector2.Zero, new Vector2(4f, 4f), SpriteEffects.None, 0.0f);
                spriteBatch.Draw(this.BarBackgroundMid, new Vector2(60f, 4f), new Rectangle?(new Rectangle(0, 0, this.hpLength - 1, 12)), Color.White, 0.0f, Vector2.Zero, new Vector2(4f, 4f), SpriteEffects.None, 0.0f);
                spriteBatch.Draw(this.BarBackgroundR, new Vector2((float)(56 + this.hpLength * 4), 4f), new Rectangle?(new Rectangle(0, 0, 3, 12)), Color.White, 0.0f, Vector2.Zero, new Vector2(4f, 4f), SpriteEffects.None, 0.0f);
                float num = this.player.HP / this.player.MaxHP;
                spriteBatch.Draw(this.hpRect, new Vector2(57f, 13f), new Rectangle?(new Rectangle(0, 0, (int)((double)this.hpLength * (double)num), 8)), Color.Red, 0.0f, Vector2.Zero, new Vector2(4f, 4f), SpriteEffects.None, 0.0f);

                // Drawing Stamina section of the UI.
                spriteBatch.Draw(this.StaminaBackgroundL, new Vector2(12f, 52f), new Rectangle?(new Rectangle(0, 0, 7, 4)), Color.White, 0.0f, Vector2.Zero, new Vector2(4f, 4f), SpriteEffects.None, 0.0f);
                spriteBatch.Draw(this.BarSmallBackgroundMid, new Vector2(40f, 52f), new Rectangle?(new Rectangle(0, 0, this.staminaLength - 1, 4)), Color.White, 0.0f, Vector2.Zero, new Vector2(4f, 4f), SpriteEffects.None, 0.0f);
                spriteBatch.Draw(this.BarSmallBackgroundR, new Vector2((float)(36 + this.staminaLength * 4), 52f), new Rectangle?(new Rectangle(0, 0, 2, 4)), Color.White, 0.0f, Vector2.Zero, new Vector2(4f, 4f), SpriteEffects.None, 0.0f);

                // Drawing Mana section of the UI.
                spriteBatch.Draw(this.ManaBackgroundL, new Vector2(12f, 68f), new Rectangle?(new Rectangle(0, 0, 7, 4)), Color.White, 0.0f, Vector2.Zero, new Vector2(4f, 4f), SpriteEffects.None, 0.0f);
                spriteBatch.Draw(this.BarSmallBackgroundMid, new Vector2(40f, 68f), new Rectangle?(new Rectangle(0, 0, this.manaLength - 1, 4)), Color.White, 0.0f, Vector2.Zero, new Vector2(4f, 4f), SpriteEffects.None, 0.0f);
                spriteBatch.Draw(this.BarSmallBackgroundR, new Vector2((float)(36 + this.manaLength * 4), 68f), new Rectangle?(new Rectangle(0, 0, 2, 4)), Color.White, 0.0f, Vector2.Zero, new Vector2(4f, 4f), SpriteEffects.None, 0.0f);


                Vector2 position = new Vector2((float)(game.ScreenWidth / 2 - this.inventoryHotbar.Width * 2), (float)(game.ScreenHeight - 100));
                spriteBatch.Draw(this.inventoryHotbar, position, new Rectangle?(new Rectangle(0, 0, this.inventoryHotbar.Width, this.inventoryHotbar.Height)), Color.White, 0.0f, Vector2.Zero, new Vector2(4f, 4f), SpriteEffects.None, 0.0f);
                spriteBatch.Draw(this.hotbarSelected, position + new Vector2((float)(this.player.currentHotbar * 72), 0.0f), new Rectangle?(new Rectangle(0, 0, this.hotbarSelected.Width, this.hotbarSelected.Height)), Color.White, 0.0f, Vector2.Zero, new Vector2(4f, 4f), SpriteEffects.None, 0.0f);
                
                ItemStack[] hotbar = this.player.inventory.getHotbar();
                for (int index = 0; index < 10; ++index)
                {
                    if (hotbar[index] != null)
                    {
                        Rectangle destinationRectangle = new Rectangle((int)position.X + (12 + 18 * index * 4), (int)position.Y + 12, 64, 64);
                        spriteBatch.Draw(hotbar[index].getItem().getTexture(), destinationRectangle, Color.White);
                        spriteBatch.DrawString(font, hotbar[index].getCount().ToString(), new Vector2((float)((int)position.X + (12 + 18 * index * 4)), (float)((int)position.Y + 12)), Color.Black);
                    }
                }

                this.chatGui.Draw(spriteBatch, font);
            }
            else
            {
                this.frameCounter.DrawFps(spriteBatch, font);
                spriteBatch.DrawString(font, "X: " + this.player.position.X.ToString(), new Vector2(4f, 20f), Color.White);
                spriteBatch.DrawString(font, "Y: " + this.player.position.Y.ToString(), new Vector2(4f, 36f), Color.White);
                spriteBatch.DrawString(font, "Velocity: " + this.player.velocity.X.ToString() + ", " + this.player.velocity.Y.ToString(), new Vector2(4f, 52f), Color.White);
                spriteBatch.DrawString(font, "HP: " + this.player.HP.ToString(), new Vector2(4f, 68f), Color.White);
            }
        }

        public override void Update(GameTime gameTime, MouseHelper mouseHelper)
        {
            this.hpLength = (int)(this.player.MaxHP / 2.0);
            this.staminaLength = (int)(this.player.Stamina / 2.0);
            this.manaLength = (int)(this.player.Mana / 2.0);

            chatGui.Update(gameTime, mouseHelper);
        }

        public void ToggleDebug() => this.debugEnabled = !this.debugEnabled;
    }
}

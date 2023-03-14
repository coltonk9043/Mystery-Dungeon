// Decompiled with JetBrains decompiler
// Type: DungeonGame.UI.DialogueBox
// Assembly: DungeonGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E78E8B53-5180-47B9-9458-06A9AF653F10
// Assembly location: C:\Users\Colton's PC\Documents\Games\Dungeon\Dungeon\bin\Debug\netcoreapp3.1\DungeonGame.dll

using DungeonGame.Entities.NPCs;
using DungeonGame.UI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DungeonGame.UI
{
    internal class DialogueBoxGui : Gui
    {
        private Texture2D texture;
        private SoundEffect effect;
        private NPC speaker;
        private Dialogue currentDialogue;
        private string chars;
        private float nextCharDelay = 25f;
        private float currentTime;
        private int currentChar;
        private bool isFinished = false;
        private LabelWidget response1Text;
        private LabelWidget response2Text;

        public DialogueBoxGui(Gui parent, SpriteFont font, NPC npc)
          : base(parent, font)
        {
            this.texture = Game1.getInstance().contentManager.Load<Texture2D>("Textures/GUI/dialogueBox");
            this.effect = Game1.getInstance().contentManager.Load<SoundEffect>("Sounds/dialogue");
            this.currentDialogue = npc.currentDialogues;
            if (this.currentDialogue.hasResponses)
            {
                this.response1Text = new LabelWidget(new Rectangle(Game1.ScreenWidth / 2 - 236, Game1.ScreenHeight - 128, 320, 16), this.currentDialogue.response1.text, font, new Action(this.goToFirstResponse));
                this.response2Text = new LabelWidget(new Rectangle(Game1.ScreenWidth / 2 - 236, Game1.ScreenHeight - 106, 320, 16), this.currentDialogue.response2.text, font, new Action(this.goToSecondResponse));
            }
            this.speaker = npc;
            this.currentChar = 0;
            this.chars = "";
            this.isIngame = true;
        }

        private string parseText(string text)
        {
            string str1 = string.Empty;
            string str2 = string.Empty;
            foreach (string str3 in text.Split(' '))
            {
                if ((double)this.font.MeasureString(str1 + str3).Length() > (double)(this.texture.Width * 4 - 160))
                {
                    str2 = str2 + str1 + "\n";
                    str1 = string.Empty;
                }
                str1 = str1 + str3 + " ";
            }
            return str2 + str1;
        }

        public override void Update(GameTime gameTime, MouseHelper mouseHelper)
        {
            if (this.isFinished)
            {
                if (this.currentDialogue.hasResponses)
                {
                    this.response1Text.Update(gameTime, mouseHelper);
                    this.response2Text.Update(gameTime, mouseHelper);
                }
                else if (mouseHelper.getLeftClicked())
                {
                    if (this.currentDialogue.nextDialogue == null)
                    {
                        this.returnToParent();
                    }
                    else
                    {
                        this.currentChar = 0;
                        this.chars = "";
                        this.currentDialogue = this.currentDialogue.nextDialogue;
                        this.isFinished = false;
                    }
                }
            }
            else if (this.currentChar == this.currentDialogue.text.Length)
            {
                this.isFinished = true;
            }
            else
            {
                this.currentTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if ((double)this.currentTime >= (double)this.nextCharDelay)
                {
                    this.chars += this.currentDialogue.text[this.currentChar].ToString();
                    this.effect.Play();
                    ++this.currentChar;
                    this.currentTime = 0.0f;
                }
                this.nextCharDelay = !mouseHelper.getLeftDown() && Keyboard.GetState().GetPressedKeyCount() <= 0 ? 25f : 0.5f;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Draw(this.texture, new Rectangle(Game1.ScreenWidth / 2 - 256, Game1.ScreenHeight - 192, 512, 128), Color.White);
            if (this.speaker != null)
                spriteBatch.Draw(this.speaker.portrait, new Rectangle(Game1.ScreenWidth / 2 + 128, Game1.ScreenHeight - 176, 96, 96), Color.White);
            spriteBatch.DrawString(this.font, this.parseText(this.chars), new Vector2((float)(Game1.ScreenWidth / 2 - 236), (float)(Game1.ScreenHeight - 176)), Color.White);
            if (!this.isFinished || !this.currentDialogue.hasResponses)
                return;
            this.response1Text.Draw(spriteBatch, font);
            this.response2Text.Draw(spriteBatch, font);
        }

        public void goToFirstResponse()
        {
            this.currentDialogue = this.currentDialogue.response1.NPCResponse;
            this.currentChar = 0;
            this.chars = "";
            this.isFinished = false;
        }

        public void goToSecondResponse()
        {
            this.currentDialogue = this.currentDialogue.response2.NPCResponse;
            this.currentChar = 0;
            this.chars = "";
            this.isFinished = false;
        }
    }
}

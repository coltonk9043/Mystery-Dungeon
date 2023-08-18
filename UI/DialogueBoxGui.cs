using Dungeon;
using DungeonGame.Entities.NPCs;
using DungeonGame.UI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

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

        private readonly List<LabelWidget> responseLabels;

        public DialogueBoxGui(GenericGame game, Gui parent, SpriteFont font, NPC npc)
          : base(game, parent, font)
        {
            this.texture = game.GetContentManager().Load<Texture2D>("Textures/GUI/dialogueBox");
            this.effect = game.GetContentManager().Load<SoundEffect>("Sounds/dialogue");

            this.responseLabels = new List<LabelWidget>();
            this.LoadDialogue(npc.currentDialogues);

            this.speaker = npc;
            this.currentChar = 0;
            this.chars = "";
            this.isIngame = true;
        }

        private void LoadDialogue(Dialogue dialogue)
        {
            this.isFinished = false;

            this.currentDialogue = dialogue;
            int offset = 128;
            this.responseLabels.Clear();
            foreach (DialogueResponse response in dialogue.responses)
            {
                this.responseLabels.Add(new LabelWidget(game.GetContentManager(), new Rectangle(game.ScreenWidth / 2 - 236, game.ScreenHeight - offset, 320, 16), response.text, font, new Action(() => this.HandleResponse(response))));
                offset += 16;
            }
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
                for (int i = 0; i < responseLabels.Count; i++)
                {
                    if (i >= responseLabels.Count) break;

                    LabelWidget responseLabel = responseLabels[i];
                    if(responseLabel != null)
                    {
                        responseLabel.Update(gameTime, mouseHelper);
                    }
                }

                if (mouseHelper.getLeftDown() && this.currentDialogue.responses.Count == 0)
                {
                    Game1.getInstance().setCurrentScreen(null);
                }
            }
            else if (this.currentChar >= this.currentDialogue.text.Length)
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
            spriteBatch.Draw(this.texture, new Rectangle(game.ScreenWidth / 2 - 256, game.ScreenHeight - 192, 512, 128), Color.White);
            if (this.speaker != null)
                spriteBatch.Draw(this.speaker.portrait, new Rectangle(game.ScreenWidth / 2 + 128, game.ScreenHeight - 176, 96, 96), Color.White);
            spriteBatch.DrawString(this.font, this.parseText(this.chars), new Vector2((game.ScreenWidth / 2 - 236), (game.ScreenHeight - 176)), Color.White);
            if (!this.isFinished)
                return;

            foreach (LabelWidget label in responseLabels)
            {
                label.Draw(spriteBatch, font);
            }
        }

        public void HandleResponse(DialogueResponse response)
        {
            LoadDialogue(response.NPCResponse);
            this.currentChar = 0;
            this.chars = "";
            this.isFinished = false;
        }
    }
}

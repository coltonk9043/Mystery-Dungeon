using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace DungeonGame.UI.Widgets
{
    internal class TextFieldWidget : Widget
    {
        public bool isSelected;
        private string text;
        private List<Keys> previousKeysPressed;

        public TextFieldWidget(Rectangle position)
          : base(position, Game1.getInstance().contentManager.Load<Texture2D>("Textures/slider_background"))
        {
            this.text = "";
            this.previousKeysPressed = new List<Keys>();
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Draw(this.texture, this.position, Color.White);
            spriteBatch.DrawString(font, this.text, new Vector2((float)(this.position.X + 16), (float)(this.position.Y + this.position.Height / 2 - 8)), Color.White);
        }

        public override void Update(GameTime gameTime, MouseHelper mouseHelper)
        {
            if (this.isSelected)
            {
                Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();
                foreach (Keys Key in pressedKeys)
                {
                    if (!this.previousKeysPressed.Contains(Key))
                    {
                        if (Key == Keys.Back && (uint)this.text.Length > 0U)
                            this.text = this.text.Remove(this.text.Length - 1);
                        char ch = this.KeyToChar(Key);
                        if (ch > char.MinValue)
                            this.text += ch.ToString();
                        this.previousKeysPressed.Add(Key);
                    }
                }
                this.previousKeysPressed = ((IEnumerable<Keys>)pressedKeys).ToList<Keys>();
            }
            if (!mouseHelper.getLeftClicked())
                return;
            if ((double)mouseHelper.getPosition().X >= (double)this.position.X && (double)mouseHelper.getPosition().X <= (double)(this.position.X + this.position.Width))
            {
                if ((double)mouseHelper.getPosition().Y >= (double)this.position.Y && (double)mouseHelper.getPosition().Y <= (double)(this.position.Y + this.position.Height))
                    this.isSelected = true;
                else
                    this.isSelected = false;
            }
            else
                this.isSelected = false;
        }

        public string getText()
        {
            return this.text;
        }

        private char KeyToChar(Keys Key)
        {
            bool flag = Keyboard.GetState().IsKeyDown(Keys.LeftShift);
            if (Key == Keys.Space)
                return ' ';
            string s = Key.ToString();
            if (s.Length == 1)
            {
                char ch = char.Parse(s);
                if (Key >= Keys.A && Key <= Keys.Z)
                    return (!flag ? ch.ToString().ToLower() : ch.ToString())[0];
            }
            switch (Key)
            {
                case Keys.D0:
                    return flag ? ')' : '0';
                case Keys.D1:
                    return flag ? '!' : '1';
                case Keys.D2:
                    return flag ? '@' : '2';
                case Keys.D3:
                    return flag ? '#' : '3';
                case Keys.D4:
                    return flag ? '$' : '4';
                case Keys.D5:
                    return flag ? '%' : '5';
                case Keys.D6:
                    return flag ? '^' : '6';
                case Keys.D7:
                    return flag ? '&' : '7';
                case Keys.D8:
                    return flag ? '*' : '8';
                case Keys.D9:
                    return flag ? '(' : '9';
                case Keys.NumPad0:
                    return '0';
                case Keys.NumPad1:
                    return '1';
                case Keys.NumPad2:
                    return '2';
                case Keys.NumPad3:
                    return '3';
                case Keys.NumPad4:
                    return '4';
                case Keys.NumPad5:
                    return '5';
                case Keys.NumPad6:
                    return '6';
                case Keys.NumPad7:
                    return '7';
                case Keys.NumPad8:
                    return '8';
                case Keys.NumPad9:
                    return '9';
                case Keys.OemSemicolon:
                    return flag ? ':' : ';';
                case Keys.OemPlus:
                    return flag ? '+' : '=';
                case Keys.OemComma:
                    return flag ? '<' : ',';
                case Keys.OemMinus:
                    return flag ? '_' : '-';
                case Keys.OemPeriod:
                    return flag ? '>' : '.';
                case Keys.OemQuestion:
                    return flag ? '?' : '/';
                case Keys.OemTilde:
                    return flag ? '~' : '`';
                case Keys.OemOpenBrackets:
                    return flag ? '{' : '[';
                case Keys.OemPipe:
                    return flag ? '|' : '\\';
                case Keys.OemCloseBrackets:
                    return flag ? '}' : ']';
                case Keys.OemQuotes:
                    return flag ? '"' : '\'';
                default:
                    return char.MinValue;
            }
        }
    }
}

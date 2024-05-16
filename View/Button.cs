using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using rpgame2.Model;

namespace rpgame2.View
{
    public class Button
    {

        public SpriteFont Font;

        public ButtonModel ButtonModel { get; set; }

        public Button(SpriteFont font, Vector2 position, string text)
        {
            Font = font;
            ButtonModel = new ButtonModel();
            ButtonModel.Text = text;
            ButtonModel.Position = position;
            ButtonModel.Rectangle = new Rectangle((int)ButtonModel.Position.X, (int)ButtonModel.Position.Y, ButtonModel.Text.Length * 40, 48);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, ButtonModel.Text, ButtonModel.Position, ButtonModel.PenColor);
        }

        public void Update()
        {
            ButtonModel.Update();
        }
    }
}
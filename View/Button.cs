using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using rpgame2.Model;

namespace rpgame2.View
{
    public class Button
    {

        public SpriteFont Font;

        public ButtonModel ButtonModel { get; set; }

        public Button(SpriteFont font, ButtonModel buttonModel)
        {
            Font = font;
            ButtonModel = buttonModel;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, ButtonModel.Text, ButtonModel.Position, ButtonModel.PenColor);
        }
    }
}
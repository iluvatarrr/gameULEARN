using Microsoft.Xna.Framework.Graphics;
using rpgame2.Controller;
using rpgame2.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace rpgame2.View
{
    public class Choise
    {
        public Texture2D BackgroundImage { get; private set; }
        public SpriteFont SpriteFont { get; private set; }
        public SpriteFont ButtonFont { get; private set; }
        public Texture2D ButtonTexture { get; private set; }
        public ChoiseModel ChoiseModel { get; private set; }
        public List<Button> Buttons { get; private set; }

        public Choise(Texture2D backgroundImage, SpriteFont spriteFont, SpriteFont buttonFont, ChoiseModel choiseModel)
        {
            ChoiseModel = choiseModel;
            Buttons = new List<Button>();
            BackgroundImage = backgroundImage;
            SpriteFont = spriteFont;
            ButtonFont = buttonFont;
            foreach (var button in ChoiseModel.ButtonsController)
                Buttons.Add(new Button(ButtonFont, button.ButtonModel));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundImage, Vector2.Zero, Color.White);
            spriteBatch.DrawString(SpriteFont, ChoiseModel.headerText, ChoiseModel.headertPosition, ChoiseModel.color);
            foreach (var button in Buttons)
                button.Draw(spriteBatch);
        }
    }
}

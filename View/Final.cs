using Microsoft.Xna.Framework.Graphics;
using rpgame2.Controller;
using rpgame2.Model;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace rpgame2.View
{
    public class Final
    {
        public Texture2D BackgroundImage { get; private set; }
        public SpriteFont SpriteFont { get; private set; }
        public SpriteFont ButtonFont { get; private set; }
        public Texture2D ButtonTexture { get; private set; }
        public FinalModel FinalModel { get; private set; }
        public List<Button> Buttons { get; private set; }

        public Final(Texture2D backgroundImage, SpriteFont spriteFont, SpriteFont buttonFont, FinalModel finalModel)
        {
            FinalModel = finalModel;
            Buttons = new List<Button>();
            BackgroundImage = backgroundImage;
            SpriteFont = spriteFont;
            ButtonFont = buttonFont;
            foreach (var button in FinalModel.ButtonsController)
                Buttons.Add(new Button(ButtonFont, button.ButtonModel));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundImage, Vector2.Zero, Color.White);
            spriteBatch.DrawString(SpriteFont, FinalModel.headerText, FinalModel.headertPosition, FinalModel.color);
            spriteBatch.DrawString(ButtonFont, FinalModel.rulesText, FinalModel.rulesPosition, FinalModel.color);
            foreach (var button in Buttons)
                button.Draw(spriteBatch);
        }
    }
}

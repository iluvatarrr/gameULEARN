using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using rpgame2.Controller;
using rpgame2.Model;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace rpgame2.View
{
    public class Rules
    {
        public Texture2D BackgroundImage { get; private set; }
        public SpriteFont SpriteFont { get; private set; }
        public SpriteFont ButtonFont { get; private set; }
        public Texture2D ButtonTexture { get; private set; }
        public RulesModel RulesModel { get; private set; }
        public List<Button> Buttons { get; private set; }

        public Rules(Texture2D backgroundImage, SpriteFont spriteFont, SpriteFont buttonFont, RulesModel rulesModel)
        {
            RulesModel = rulesModel;
            Buttons = new List<Button>();
            BackgroundImage = backgroundImage;
            SpriteFont = spriteFont;
            ButtonFont = buttonFont;
            foreach (var button in RulesModel.ButtonsController)
                Buttons.Add(new Button(ButtonFont, button.ButtonModel));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundImage, Vector2.Zero, Color.White);
            spriteBatch.DrawString(SpriteFont, RulesModel.headerText, RulesModel.headertPosition, RulesModel.color);
            spriteBatch.DrawString(ButtonFont, RulesModel.rulesText, RulesModel.rulesPosition, RulesModel.color);
            foreach (var button in Buttons)
                button.Draw(spriteBatch);
        }
    }
}

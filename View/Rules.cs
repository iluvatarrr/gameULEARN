using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using rpgame2.Controller;
using rpgame2.Model;

namespace rpgame2.View
{
    public class Rules
    {
        public Texture2D BackgroundImage { get; private set; }
        public SpriteFont SpriteFont { get; private set; }
        public SpriteFont ButtonFont { get; private set; }
        public Texture2D ButtonTexture { get; private set; }
        public RulesModel RulesModel { get; private set; }

        public Rules(Texture2D backgroundImage, SpriteFont spriteFont, SpriteFont buttonFont)
        {
            RulesModel = new RulesModel();
            BackgroundImage = backgroundImage;
            SpriteFont = spriteFont;
            ButtonFont = buttonFont;
            var BackButton = new Button(ButtonFont, new Vector2(100, 634), "Back");
            BackButton.ButtonModel.Click += ButtonController.BackButton;
            RulesModel.Buttons.Add( BackButton );
        }

        public void Update()
        {
            foreach (var button in RulesModel.Buttons)
                button.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundImage, Vector2.Zero, Color.White);
            spriteBatch.DrawString(SpriteFont, RulesModel.headerText, RulesModel.headertPosition, RulesModel.color);
            spriteBatch.DrawString(ButtonFont, RulesModel.rulesText, RulesModel.rulesPosition, RulesModel.color);
            foreach (var button in RulesModel.Buttons)
                button.Draw(spriteBatch);
        }

    }
}

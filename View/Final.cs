using Microsoft.Xna.Framework.Graphics;
using rpgame2.Controller;
using rpgame2.Model;
using Microsoft.Xna.Framework;

namespace rpgame2.View
{
    public class Final
    {
        public Texture2D BackgroundImage { get; private set; }
        public SpriteFont SpriteFont { get; private set; }
        public SpriteFont ButtonFont { get; private set; }
        public Texture2D ButtonTexture { get; private set; }
        public FinalModel FinalModel { get; private set; }

        public Final(Texture2D backgroundImage, SpriteFont spriteFont, SpriteFont buttonFont)
        {
            FinalModel = new FinalModel();
            BackgroundImage = backgroundImage;
            SpriteFont = spriteFont;
            ButtonFont = buttonFont;
            var ChoiseButton = new Button(ButtonFont, new Vector2(100, 434), "Choise Level");
            ChoiseButton.ButtonModel.Click += ButtonController.ChoiseButton;
            var MenuButton = new Button(ButtonFont, new Vector2(100, 534), "Menu");
            MenuButton.ButtonModel.Click += ButtonController.MenuButton;
            var BackButton = new Button(ButtonFont, new Vector2(100, 634), "Back");
            BackButton.ButtonModel.Click += ButtonController.BackButton;
            FinalModel.Buttons.Add(ChoiseButton);
            FinalModel.Buttons.Add(MenuButton);
            FinalModel.Buttons.Add(BackButton);
        }

        public void Update()
        {
            foreach (var button in FinalModel.Buttons)
                button.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundImage, Vector2.Zero, Color.White);
            spriteBatch.DrawString(SpriteFont, FinalModel.headerText, FinalModel.headertPosition, FinalModel.color);
            spriteBatch.DrawString(ButtonFont, FinalModel.rulesText, FinalModel.rulesPosition, FinalModel.color);
            foreach (var button in FinalModel.Buttons)
                button.Draw(spriteBatch);
        }
    }
}

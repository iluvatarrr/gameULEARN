using Microsoft.Xna.Framework.Graphics;
using rpgame2.Controller;
using rpgame2.Model;
using Microsoft.Xna.Framework;

namespace rpgame2.View
{
    public class Choise
    {
        public Texture2D BackgroundImage { get; private set; }
        public SpriteFont SpriteFont { get; private set; }
        public SpriteFont ButtonFont { get; private set; }
        public Texture2D ButtonTexture { get; private set; }
        public ChoiseModel ChoiseModel { get; private set; }

        public Choise(Texture2D backgroundImage, SpriteFont spriteFont, SpriteFont buttonFont)
        {
            ChoiseModel = new ChoiseModel();
            BackgroundImage = backgroundImage;
            SpriteFont = spriteFont;
            ButtonFont = buttonFont;
            var FirstButton = new Button(ButtonFont, new Vector2(100, 260), "1. First Level");
            FirstButton.ButtonModel.Click += ButtonController.FirstLevelButton;
            var SecondButton = new Button(ButtonFont, new Vector2(100, 330), "2. Second Level");
            SecondButton.ButtonModel.Click += ButtonController.SecondLevelButton;
            var ThirdButton = new Button(ButtonFont, new Vector2(100, 400), "3. Third Level");
            ThirdButton.ButtonModel.Click += ButtonController.ThirdLevelButton;
            var MenuButton = new Button(ButtonFont, new Vector2(100, 534), "Menu");
            MenuButton.ButtonModel.Click += ButtonController.MenuButton;
            var BackButton = new Button(ButtonFont, new Vector2(100, 634), "Back");
            BackButton.ButtonModel.Click += ButtonController.BackButton;
            ChoiseModel.Buttons.Add(FirstButton);
            ChoiseModel.Buttons.Add(SecondButton);
            ChoiseModel.Buttons.Add(ThirdButton);
            ChoiseModel.Buttons.Add(MenuButton);

            ChoiseModel.Buttons.Add(BackButton);
        }

        public void Update()
        {
            foreach (var button in ChoiseModel.Buttons)
                button.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundImage, Vector2.Zero, Color.White);
            spriteBatch.DrawString(SpriteFont, ChoiseModel.headerText, ChoiseModel.headertPosition, ChoiseModel.color);
            foreach (var button in ChoiseModel.Buttons)
                button.Draw(spriteBatch);
        }
    }
}

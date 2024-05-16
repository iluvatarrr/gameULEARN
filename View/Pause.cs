using Microsoft.Xna.Framework.Graphics;
using rpgame2.Controller;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using rpgame2.Controller;
using rpgame2.Model;


namespace rpgame2.View
{
    public class Pause
    {
        public Texture2D BackgroundImage { get; private set; }
        public SpriteFont SpriteFont { get; private set; }
        public SpriteFont ButtonFont { get; private set; }
        public Texture2D ButtonTexture { get; private set; }
        public PauseModel PauseModel { get; private set; }

        public Pause(Texture2D backgroundImage, SpriteFont spriteFont, SpriteFont buttonFont)
        {
            PauseModel = new PauseModel();
            BackgroundImage = backgroundImage;
            SpriteFont = spriteFont;
            ButtonFont = buttonFont;
            var ChoiseButton = new Button( ButtonFont, new Vector2(100, 434), "Choise Level");
            ChoiseButton.ButtonModel.Click += ButtonController.ChoiseButton;
            var MenuButton = new Button( ButtonFont, new Vector2(100, 534), "Menu");
            MenuButton.ButtonModel.Click += ButtonController.MenuButton;
            var BackButton = new Button(ButtonFont, new Vector2(100, 634), "Back");
            BackButton.ButtonModel.Click += ButtonController.BackButton;
            PauseModel.Buttons.Add(MenuButton);
            PauseModel.Buttons.Add(ChoiseButton);
            PauseModel.Buttons.Add(BackButton);
        }

        public void Update()
        {
            foreach (var button in PauseModel.Buttons)
                button.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundImage, Vector2.Zero, Color.White);
            spriteBatch.DrawString(SpriteFont, PauseModel.headerText, PauseModel.headertPosition, PauseModel.color);
            foreach (var button in PauseModel.Buttons)
                button.Draw(spriteBatch);
        }


    }
}

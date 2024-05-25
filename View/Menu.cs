using MyGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rpgame2.Controller;
using rpgame2.Model;
using rpgame2.View;
using System;
using System.Collections.Generic;

namespace rpgame2
{
    public class Menu
    {
        public Texture2D BackgroundImage { get; private set; }
        public SpriteFont SpriteFont { get; private set; }
        public SpriteFont ButtonFont { get; private set; }
        public Texture2D ButtonTexture { get; private set; }

        public ButtonFunction ButtonController { get; private set; }
        public MenuModel MenuModel { get; private set; }
        public List<Button> Buttons { get; private set; }

        public Menu(Texture2D backgroundImage, SpriteFont spriteFont, SpriteFont buttonFont, MenuModel menuModel, Game game)
        {
            MenuModel = menuModel;
            Buttons = new List<Button>();
            ButtonController = new ButtonFunction(game);
            BackgroundImage = backgroundImage;
            SpriteFont = spriteFont;
            ButtonFont = buttonFont;
            foreach (var button in MenuModel.ButtonsController)
                Buttons.Add(new Button(ButtonFont, button.ButtonModel));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundImage, Vector2.Zero, Color.White);
            spriteBatch.DrawString(SpriteFont, "Рудник", MenuModel.textPosition, MenuModel.color);
            foreach (var button in Buttons)
                button.Draw(spriteBatch);
        }
    }
}

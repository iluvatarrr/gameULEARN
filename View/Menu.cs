using Game1;
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

        public ButtonController ButtonController { get; private set; }
        public MenuModel MenuModel { get; set; }
        public Menu(Texture2D backgroundImage, SpriteFont spriteFont, SpriteFont buttonFont, Game game)
        {
            MenuModel = new MenuModel();
            ButtonController = new ButtonController(game);
            BackgroundImage = backgroundImage;
            SpriteFont = spriteFont;
            ButtonFont = buttonFont;
            var PlayButton = new Button(ButtonFont, new Vector2(100, 460), "Play");
            PlayButton.ButtonModel.Click += ButtonController.ChoiseButton;
            var RulesButton = new Button(ButtonFont, new Vector2(100, 518), "Rules");
            RulesButton.ButtonModel.Click += ButtonController.RulesButton;
            var SettingsButton = new Button( ButtonFont, new Vector2(100, 576), "Settings");
            SettingsButton.ButtonModel.Click += ButtonController.SettingsButton;
            var ExitButton = new Button(ButtonFont, new Vector2(100, 634), "Exit");
            ExitButton.ButtonModel.Click += ButtonController.ExitButton;
            MenuModel.Buttons.Add(PlayButton);
            MenuModel.Buttons.Add(RulesButton);
            MenuModel.Buttons.Add(SettingsButton);
            MenuModel.Buttons.Add(ExitButton);
        }

        public void Update()
        {
            MenuModel.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundImage, Vector2.Zero, Color.White);
            spriteBatch.DrawString(SpriteFont, "Рудник", MenuModel.textPosition, MenuModel.color);
            foreach (var button in MenuModel.Buttons)
                button.Draw(spriteBatch);
        }
    }
}

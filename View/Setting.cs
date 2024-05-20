using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using rpgame2.Controller;
using rpgame2.Model;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace rpgame2.View
{
    public class Setting
    {
        public Texture2D BackgroundImage { get; private set; }
        public SpriteFont SpriteFont { get; private set; }
        public SpriteFont ButtonFont { get; private set; }
        public Texture2D ButtonTexture { get; private set; }
        public SettingModel SettingModel { get; private set; }
        public List<Button> Buttons { get; private set; }
        public MusicValue MusicValue { get; private set; }

        public Setting(Texture2D backgroundImage, SpriteFont spriteFont, SpriteFont buttonFont, SettingModel rulesModel, MusicValue musicValue)
        {
            SettingModel = rulesModel;
            Buttons = new List<Button>();
            BackgroundImage = backgroundImage;
            SpriteFont = spriteFont;
            ButtonFont = buttonFont;
            MusicValue = musicValue;
            foreach (var button in SettingModel.ButtonsController)
                Buttons.Add(new Button(ButtonFont, button.ButtonModel));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundImage, Vector2.Zero, Color.White);
            spriteBatch.DrawString(SpriteFont, SettingModel.headerText, SettingModel.headertPosition, SettingModel.color);
            spriteBatch.DrawString(MusicValue.Font, (Math.Round(MusicValue.Volume * 100)).ToString(), MusicValue.Position, Color.White);
            spriteBatch.DrawString(ButtonFont, SettingModel.rulesText, SettingModel.rulesPosition, SettingModel.color);
            foreach (var button in Buttons)
                button.Draw(spriteBatch);
        }
    }
}


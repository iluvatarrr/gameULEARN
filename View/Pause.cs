using Microsoft.Xna.Framework.Graphics;
using rpgame2.Controller;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

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
        public List<Button> Buttons { get; private set; }

        public Pause(Texture2D backgroundImage, SpriteFont spriteFont, SpriteFont buttonFont, PauseModel pauseModel)
        {
            PauseModel = pauseModel;
            Buttons = new List<Button>();
            BackgroundImage = backgroundImage;
            SpriteFont = spriteFont;
            ButtonFont = buttonFont;
            foreach (var button in PauseModel.ButtonsController) 
                Buttons.Add(new Button(ButtonFont, button.ButtonModel));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundImage, Vector2.Zero, Color.White);
            spriteBatch.DrawString(SpriteFont, PauseModel.headerText, PauseModel.headertPosition, PauseModel.color);
            foreach (var button in Buttons)
                button.Draw(spriteBatch);
        }


    }
}

﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rpgame2.Model;

namespace rpgame2
{
    public class Menu
    {
        public Texture2D BackgroundImage { get; set; }
        public SpriteFont SpriteFont { get; set; }

        public Menu(Texture2D backgroundImage, SpriteFont spriteFont) 
        {
            BackgroundImage = backgroundImage;
            SpriteFont = spriteFont;
        }

        private int timeCounter = 0;
        private Color color;
        private Vector2 textPosition = new Vector2(100, 270);

        public void Update()
        {
            if (timeCounter <= 255)
            {
                color = Color.FromNonPremultiplied(255, 255, 255, timeCounter % 256);
                timeCounter += 2;
            }
            else color = Color.White;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (GameState.State.Equals(State.SplashScreen))
            {
                spriteBatch.Draw(BackgroundImage, Vector2.Zero, Color.White);
                spriteBatch.DrawString(SpriteFont, "Рудник", textPosition, color);
            }
        }
    }
}

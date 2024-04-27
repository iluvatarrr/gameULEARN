using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using rpgame2.Model;
using rpgame2.View;
using System;


namespace rpgame2
{
    public class Firstevel
    {
        private GameState GameState;
        private LevelModel LevelModel;
        public Texture2D BackgroundImage { get; private set; }

        public Firstevel(Texture2D backgroundImage, LevelModel levelModel)
        {
            BackgroundImage = backgroundImage;
            LevelModel = levelModel;
        }

        public void Update(GameState gameState)
        {
            GameState = gameState;
            LevelModel.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (GameState.State.Equals(State.Game))
            {
                spriteBatch.Draw(BackgroundImage, Vector2.Zero, Color.White);
                foreach (var platform in LevelModel.Platforms)
                    spriteBatch.Draw(platform.Texture, platform.Position, Color.White);
            }
        }
    }
}

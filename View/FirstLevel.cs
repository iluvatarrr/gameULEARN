using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace rpgame2
{
    public class Firstevel
    {
        private GameState GameState;
        public Texture2D BackgroundImage { get; set; }

        public Firstevel(Texture2D backgroundImage)
        {
            BackgroundImage = backgroundImage;
        }

        public void Update(GameState gameState)
        {
            GameState = gameState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (GameState.State.Equals(State.Game))
            {
                spriteBatch.Draw(BackgroundImage, Vector2.Zero, Color.White);
            }
        }
    }
}

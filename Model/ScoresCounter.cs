using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace rpgame2.Model
{
    public class ScoresCounter
    {
        public readonly Vector2 Position;
        public readonly SpriteFont Font;

        public ScoresCounter(SpriteFont font)
        {
            Font = font;
            Position = new Vector2(90, 0);
        }
    }
}

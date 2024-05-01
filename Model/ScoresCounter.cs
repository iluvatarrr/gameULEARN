using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rpgame2.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpgame2.Model
{
    public class ScoresCounter
    {
        public Vector2 Position;
        public readonly SpriteFont Font;

        public ScoresCounter(SpriteFont font, Player player)
        {
            Font = font;
            Position = new Vector2(90, 0);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rpgame2.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace rpgame2.Model
{
    public class LevelModel
    {
        public List<PlatformModel> Platforms { get; private set; }
        private Player Player;

        public LevelModel(List<PlatformModel> platforms, Player player) 
        {
            Platforms = platforms;
            Player = player;
        }

        public void Update()
        {
            if (Platforms.Any(platform => Player.PlayerModel.rectangle.isOnTopOf(platform.rectangle)))
            {
                Player.PlayerModel.Velocity.Y = 0f;
                Player.PlayerModel.hasJump = false;
            }
            else Player.PlayerModel.hasJump = true;
        }
    }
}

static class RectangleHelper
{
    public static bool isOnTopOf(this Rectangle r1, Rectangle r2)
    {
        return ((r1.Bottom >= (r2.Top - 5)) &&
            (r1.Bottom <= r2.Top) &&
            (r1.Right >= (r2.Left + 40)) &&
            (r1.Left <= (r2.Right - 40)));
    }
    public static bool isOnBottonOf(this Rectangle r1, Rectangle r2)
    {
        return r1.Bottom <= (r2.Top - 5);
    }
}

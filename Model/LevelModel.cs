using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rpgame2.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace rpgame2.Model
{
    public class LevelModel
    {
        private PlayerModel PlayerModel;
        private Mob enemy;
        public static readonly int sizeOfElement = 48;
        public static int[,] mapMatrix =
        {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 2, 3, 3, 3, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 3, 3, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 3, 3, 3, 4, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 2, 3, 3, 3, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 3, 3, 3, 4, 0, 0, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9},
            {1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9},
            {9, 9, 9, 0, 0, 0, 2, 3, 3, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9},
            {9, 9, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9},
            {9, 9, 9, 0, 2, 3, 3, 3, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9},
            {9, 9, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9},
        };

        public LevelModel(Player player)
        {
            PlayerModel = player.PlayerModel;
        }

        public void SetDamage()
        {
            if (PlayerModel.Position.X == (enemy.Position.X + 1) && PlayerModel.isHit)
                enemy.Health -= PlayerModel.Strange;
        }

        private bool InMap()
        {
            return (PlayerModel.Position.X < -50
                    || Game1.Game1.ScreenWidth < PlayerModel.Position.X
                    || Game1.Game1.ScreenHeight < PlayerModel.Position.Y);
        }
        public static List<Rectangle> GetBlockRectangle()
        {
            List<Rectangle> result = new List<Rectangle>();
            for (int y = 0; y < mapMatrix.GetLength(0); y++)
                for (int x = 0; x < mapMatrix.GetLength(1); x++)
                    if (mapMatrix[y,x] != 9 && mapMatrix[y, x] != 0)
                        result.Add(new Rectangle(x*sizeOfElement, y*sizeOfElement, sizeOfElement, sizeOfElement));
            return result;
        }
        
        public void Update()
        {
            if (InMap()) PlayerModel.IsDead = true;
            if (GetBlockRectangle().Any(platform => PlayerModel.rectangle.isOnTopOf(platform)))
            {
                PlayerModel.Velocity.Y = 0f;
                PlayerModel.hasJump = false;
            }
            else PlayerModel.hasJump = true;
        }
    }
}

static class RectangleHelper
{
    public static bool isOnTopOf(this Rectangle r1, Rectangle r2)
    {
        return ((r1.Bottom >= (r2.Top - 3)) &&
            (r1.Bottom <= r2.Top) &&
            (r1.Right >= (r2.Left + 40)) &&
            (r1.Left <= (r2.Right - 40)));
    }
}
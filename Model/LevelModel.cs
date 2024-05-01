using Microsoft.Xna.Framework;
using rpgame2.View;
using System;
using System.Collections.Generic;
using System.Linq;


namespace rpgame2.Model
{
    static class RectangleHelper
    {
        public readonly static int marginBlockLeftRight = 40;
        public readonly static int marginPlayerTop = 90;
        private static int marginFromTop = 3;

        public static bool OnPlatform(this Rectangle r1, Rectangle r2)
        {
            return ((r1.Bottom >= (r2.Top - marginFromTop)) &&
                (r1.Bottom <= r2.Top) &&
                (r1.Right >= (r2.Left + marginBlockLeftRight)) &&
                (r1.Left <= (r2.Right - marginBlockLeftRight)));
        }
    }

    public class LevelModel
    {
        public PlayerModel PlayerModel { get; private set; }
        public static MapInfo MapInform;
        private Mob enemy;
        public static int sizeOfElement { get; private set; }
        public static Point PositionOfPortal { get; private set; }

        public LevelModel(Player player)
        {
            PlayerModel = player.PlayerModel;
            MapInform = new MapInfo();
            MapInform.Blocks = new List<Rectangle>();
            MapInform.Crystal = new List<Rectangle>();
            PositionOfPortal = new Point();
            sizeOfElement = 48;
            GetBlockRectangle();
        }

        public void SetDamage()
        {
            if (PlayerModel.Position.X == (enemy.Position.X + 1) && PlayerModel.isHit)
                enemy.Health -= PlayerModel.Strange;
        }

        private bool InNotMap()
        {
            return (PlayerModel.Position.X < -RectangleHelper.marginBlockLeftRight 
                    || PlayerModel.Position.Y < -RectangleHelper.marginPlayerTop
                    || Game1.Game1.ScreenWidth < PlayerModel.Position.X
                    || Game1.Game1.ScreenHeight < PlayerModel.Position.Y);
        }
        public static void GetBlockRectangle()
        {
            for (int y = 0; y < MapInform.mapMatrixFirstLevel.GetLength(0); y++)
                for (int x = 0; x < MapInform.mapMatrixFirstLevel.GetLength(1); x++)
                    if (MapInform.mapMatrixFirstLevel[y, x] != 9 && MapInform.mapMatrixFirstLevel[y, x] != 0)
                    {
                        if (MapInform.mapMatrixFirstLevel[y, x] == 5) MapInform.Crystal.Add(new Rectangle(x * sizeOfElement, y * sizeOfElement, sizeOfElement, sizeOfElement));
                        else
                        {
                            if (MapInform.mapMatrixFirstLevel[y, x] == 6) PositionOfPortal = new Point(y, x);
                            MapInform.Blocks.Add(new Rectangle(x * sizeOfElement, y * sizeOfElement, sizeOfElement, sizeOfElement));
                        }
                    }
        }

        public void CheckCurrentGems()
        {
            foreach (var gem in MapInform.Crystal.ToArray())
                if (PlayerModel.Rectangle.Intersects(gem))
                {
                    MapInform.mapMatrixFirstLevel[gem.Y / 48, gem.X / 48] = 0;
                    MapInform.Crystal.Remove(gem);
                    PlayerModel.AddGem();
                }
        }

        public void IsCompleteLevel()
        {
            if (MapInform.Crystal.Count == 0) MapInform.mapMatrixFirstLevel[PositionOfPortal.X, PositionOfPortal.Y] = 7;
        }

        public void Update()
        {
            if (!PlayerModel.IsDead)
            {
                CheckCurrentGems();
                IsCompleteLevel();
            }
            if (MapInform.Blocks.Any(platform => PlayerModel.Rectangle.OnPlatform(platform)))
            {
                PlayerModel.Velocity.Y = 0f;
                PlayerModel.onGravity = false;
            }
            else PlayerModel.onGravity = true;
            if (InNotMap()) PlayerModel.IsDead = true;
        }
    }
}
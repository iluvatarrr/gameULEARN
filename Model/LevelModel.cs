using Microsoft.Xna.Framework;
using rpgame2.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace rpgame2.Model
{
    static class RectangleHelper
    {
        public readonly static int marginBlockLeftRight = 40;
        public readonly static int marginPlayerTop = 90;
        private static int marginFromTop = 1;

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
        public static Vector2 TileOfPlayer;
        public static Vector2 TileOfOrc;
        public static Node NodeOfOrc;
        public static Vector2 previousPositionOfOrc;
        public static Node NodeOfPlayer;
        public static List<Node> path;
        public OrcModel OrcModel { get; private set; }

        public static MapInfo MapInform;

        public static int sizeOfElement { get; private set; }
        public static Point PositionOfPortal { get; private set; }

        public LevelModel(Player player, Orc orc)
        {
            PlayerModel = player.PlayerModel;
            MapInform = new MapInfo();
            MapInform.CurrentMap = MapInform.mapMatrixFirstLevel;
            MapInform.Blocks = new List<Rectangle>();
            MapInform.Graph = new Graph();
            MapInform.MakeGraph();
            MapInform.Crystal = new List<Rectangle>();
            PositionOfPortal = new Point();
            sizeOfElement = 48;
            GetBlockRectangle();
            OrcModel = orc.OrcModel;
        }

        public void SetDamage()
        {
            if (PlayerModel.Rectangle.Intersects(OrcModel.Rectangle) && PlayerModel.isHit)
                    OrcModel.Health -= PlayerModel.HitLogic();
            if (PlayerModel.Rectangle.Intersects(OrcModel.Rectangle) && !PlayerModel.IsDead && !OrcModel.IsDead)
            {
                OrcModel.OrcState = OrcState.Hit;
                PlayerModel.Health -= OrcModel.HitLogic();
            }
            else OrcModel.OrcState = OrcState.Stay;
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
            for (int y = 0; y < MapInform.CurrentMap.GetLength(0); y++)
                for (int x = 0; x < MapInform.CurrentMap.GetLength(1); x++)
                    if (MapInform.CurrentMap[y, x] != 9 && MapInform.CurrentMap[y, x] != 0)
                    {
                        if (MapInform.CurrentMap[y, x] == 5) MapInform.Crystal.Add(new Rectangle(x * sizeOfElement, y * sizeOfElement, sizeOfElement, sizeOfElement));
                        else
                        {
                            if (MapInform.CurrentMap[y, x] == 6) PositionOfPortal = new Point(y, x);
                            MapInform.Blocks.Add(new Rectangle(x * sizeOfElement, y * sizeOfElement, sizeOfElement, sizeOfElement));
                        }
                    }
        }

        public void CheckCurrentGems()
        {
            foreach (var gem in MapInform.Crystal.ToArray())
                if (PlayerModel.Rectangle.Intersects(gem))
                {
                    MapInform.mapMatrixFirstLevel[gem.Y / sizeOfElement, gem.X / sizeOfElement] = 0;
                    MapInform.Crystal.Remove(gem);
                    PlayerModel.AddGem();
                }
        }

        public void IsCompleteLevel()
        {
            if (MapInform.Crystal.Count == 0) MapInform.CurrentMap[PositionOfPortal.X, PositionOfPortal.Y] = 7;
        }

        public void Update()
        {
            if (MapInform.Blocks.Any(platform => PlayerModel.Rectangle.OnPlatform(platform)))
            {
                TileOfPlayer = new Vector2(MapInform.Blocks.Where(platform => PlayerModel.Rectangle.OnPlatform(platform)).First().X / sizeOfElement,
                    MapInform.Blocks.Where(platform => PlayerModel.Rectangle.OnPlatform(platform)).First().Y / sizeOfElement);
                TileOfOrc = new Vector2((int)(OrcModel.Position.X / sizeOfElement), (int)(OrcModel.Rectangle.Bottom / sizeOfElement));
                if (MapInform.Graph.IsNewPosition(TileOfOrc)) TileOfOrc = previousPositionOfOrc;
                else
                {
                    NodeOfOrc = MapInform.Graph.FindNodeByPosition(TileOfOrc);
                    previousPositionOfOrc = TileOfOrc;
                }
                NodeOfPlayer = MapInform.Graph.FindNodeByPosition(TileOfPlayer);
            }
            if (GameState.State.Equals(State.Game))
                SetDamage();
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
            if (MapInform.Blocks.Any(platform => OrcModel.Rectangle.OnPlatform(platform)))
            {
                OrcModel.Velocity.Y = 0f;
                OrcModel.onGravity = false;
            }
            else OrcModel.onGravity = true;
            if (InNotMap()) PlayerModel.IsDead = true;
        }
    }
}
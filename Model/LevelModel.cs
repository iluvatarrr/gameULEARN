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
        private static int marginFromTop = 5;

        public static bool OnPlatform(this Rectangle r1, Rectangle r2)
        {
            return ((r1.Bottom >= (r2.Top - marginFromTop)) &&
                (r1.Bottom <= r2.Top) &&
                (r1.Right >= r2.Left) &&
                (r1.Left <= r2.Right));
        }
    }

    public class LevelModel
    {
        public PlayerModel PlayerModel { get; private set; }
        public static Node NodeOfOrc;
        public static Vector2 previousPositionOfOrc;
        public static Node NodeOfPlayer;

        public static Vector2 LastTileOfPlayer;
        public static bool wasOnPlatform;
        public OrcModel OrcModel { get; private set; }

        public static MapInfo MapInform;
        public List<Orc> OrcList;
        public static int sizeOfElement { get; private set; }
        public static Point PositionOfPortal { get; private set; }

        public static List<Vector2> PositionOrcList;

        public LevelModel(Player player)
        {
            PlayerModel = player.PlayerModel;
            MapInform = new MapInfo();
            MapInform.CurrentMap = MapInform.mapMatrixFirstLevel;
            MapInform.Blocks = new List<Rectangle>();
            MapInfo.Graph = new Graph();
            MapInform.MakeGraph();
            MapInform.Crystal = new List<Rectangle>();
            PositionOfPortal = new Point();
            sizeOfElement = 48;
            PositionOrcList = new List<Vector2>();
            GetBlockRectangle();
        }

        public void SetDamage()
        {
            foreach (var orc in OrcList)
            {
                if (PlayerModel.TileOfPlayer.Equals(orc.OrcModel.TileOfOrc) && PlayerModel.isHit && !orc.OrcModel.IsDead)
                    orc.OrcModel.Health -= PlayerModel.HitLogic();
                if ((orc.OrcModel.TileOfOrc.Equals(PlayerModel.TileOfPlayer)) && !PlayerModel.IsDead && !orc.OrcModel.IsDead)
                {
                    orc.OrcModel.OrcState = OrcState.Hit;
                    PlayerModel.Health -= orc.OrcModel.HitLogic();
                }
                else if (!orc.OrcModel.IsDead) orc.OrcModel.OrcState = OrcState.Stay;
                else orc.OrcModel.OrcState = OrcState.Dead;
            }
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
                        if (MapInform.CurrentMap[y, x] == 8) PositionOrcList.Add(new Vector2(x * sizeOfElement-32, y * sizeOfElement-sizeOfElement*2));
                        else if (MapInform.CurrentMap[y, x] == 5) MapInform.Crystal.Add(new Rectangle(x * sizeOfElement, y * sizeOfElement, sizeOfElement, sizeOfElement));
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
            foreach (var orc in OrcList)
            {
                if (MapInform.Blocks.Any(platform => PlayerModel.Rectangle.OnPlatform(platform)))
                {
                    wasOnPlatform = true;
                    PlayerModel.FindTile();
                    NodeOfPlayer = MapInfo.Graph.FindNodeByPosition(PlayerModel.TileOfPlayer);

                    orc.OrcModel.FindTile();
                    orc.OrcModel.NodeOfOrc = MapInfo.Graph.FindNodeByPosition(orc.OrcModel.TileOfOrc);
                    LastTileOfPlayer = PlayerModel.TileOfPlayer;
                }
                else if (wasOnPlatform && !MapInform.Blocks.Any(platform => PlayerModel.Rectangle.OnPlatform(platform)))
                {
                    orc.OrcModel.FindTile();
                    orc.OrcModel.NodeOfOrc = MapInfo.Graph.FindNodeByPosition(orc.OrcModel.TileOfOrc);

                    NodeOfPlayer = MapInfo.Graph.FindNodeByPosition(LastTileOfPlayer);
                    orc.OrcModel.way = BFS.FindWays(orc.OrcModel.NodeOfOrc, LevelModel.NodeOfPlayer);
                }
                if (MapInform.Blocks.Any(platform => orc.OrcModel.Rectangle.OnPlatform(platform)))
                {
                    orc.OrcModel.Velocity.Y = 0f;
                    orc.OrcModel.onGravity = false;
                }
                else orc.OrcModel.onGravity = true;
            }
            if (GameState.State.Equals(State.Game)) SetDamage();
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
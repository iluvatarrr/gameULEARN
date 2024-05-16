using Microsoft.Xna.Framework;
using rpgame2.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static rpgame2.Model.LevelState;

namespace rpgame2.Model
{
    static class RectangleHelper
    {
        public readonly static int marginBlockLeftRight = 40;
        public readonly static int marginPlayerTop = 90;
        public static int marginFromTop = 5;

        public static bool OnPlatform(this Rectangle r1, Rectangle r2)
        {
            return ((r1.Bottom >= (r2.Top - marginFromTop)) &&
                (r1.Bottom <= r2.Top) &&
                (r1.Right >= r2.Left + 8) &&
                (r1.Left <= r2.Right - 8));
        }
    }

    public class LevelModel
    {
        public PlayerModel PlayerModel { get; set; }

        public static Vector2 LastTileOfPlayer;
        public static bool wasOnPlatform;
        public OrcModel OrcModel { get; private set; }

        public static MapInfo MapInform;
        public List<Orc> OrcList;
        public static int sizeOfElement { get; private set; }
        public static Vector2 PositionOfPortal { get; private set; }
        public static int OrcCouut;

        public static List<Vector2> PositionOrcList;
        public bool IsLevelComplete;
        public LevelModel()
        {
            MapInform = new MapInfo();
            if (LevelState.CurrentState.Equals(LevelNumber.First))
                MapInform.CurrentMap = MapInform.mapMatrixFirstLevel;
            else if (LevelState.CurrentState.Equals(LevelNumber.Second))
                MapInform.CurrentMap = MapInform.mapMatrixSecondLevel;
            else MapInform.CurrentMap = MapInform.mapMatrixThirdLevel;
            MapInfo.Blocks = new List<Rectangle>();
            MapInfo.Graph = new Graph();
            MapInform.MakeGraph();
            MapInform.Crystal = new List<Rectangle>();
            PositionOfPortal = new Vector2();
            sizeOfElement = 48;
            PositionOrcList = new List<Vector2>();
            GetBlockRectangle();
            OrcCouut = PositionOrcList.Count;
        }

        public void SetDamage()
        {
            foreach (var orc in OrcList)
            {
                if ((PlayerModel.TileOfPlayer.Equals(orc.OrcModel.TileOfOrc) && PlayerModel.Rectangle.Intersects(orc.OrcModel.Rectangle)) && PlayerModel.isHit && !orc.OrcModel.IsDead)
                {
                    var orcsAround = OrcList.Where(orc => PlayerModel.TileOfPlayer.Equals(orc.OrcModel.TileOfOrc));
                    if (orcsAround.Count() > 1) orcsAround.First().OrcModel.Health -= PlayerModel.HitLogic();
                    else orc.OrcModel.Health -= PlayerModel.HitLogic();
                }
                if ((orc.OrcModel.TileOfOrc.Equals(PlayerModel.TileOfPlayer) && orc.OrcModel.Rectangle.Intersects(PlayerModel.Rectangle)) && !PlayerModel.IsDead && !orc.OrcModel.IsDead)
                {
                    orc.OrcModel.OrcState = OrcState.Hit;
                    PlayerModel.Health -= orc.OrcModel.HitLogic();
                }
                else if (!orc.OrcModel.IsDead) orc.OrcModel.OrcState = OrcState.Stay;
            }
        }

        public static void GetBlockRectangle()
        {
            for (int y = 0; y < MapInform.CurrentMap.GetLength(0); y++)
                for (int x = 0; x < MapInform.CurrentMap.GetLength(1); x++)
                    if (MapInform.CurrentMap[y, x] != 9 && MapInform.CurrentMap[y, x] != 0)
                    {
                        if (MapInform.CurrentMap[y, x] == 8) PositionOrcList.Add(new Vector2(x * sizeOfElement, y * sizeOfElement-sizeOfElement-RectangleHelper.marginFromTop));
                        else if (MapInform.CurrentMap[y, x] == 5) MapInform.Crystal.Add(new Rectangle(x * sizeOfElement, y * sizeOfElement, sizeOfElement, sizeOfElement));
                        else
                        {
                            if (MapInform.CurrentMap[y, x] == 6) PositionOfPortal = new Vector2(x, y);
                            MapInfo.Blocks.Add(new Rectangle(x * sizeOfElement, y * sizeOfElement, sizeOfElement, sizeOfElement));
                        }
                    }
        }

        public void CheckCurrentGems()
        {
            foreach (var gem in MapInform.Crystal.ToArray())
                if (PlayerModel.Rectangle.Intersects(gem))
                {
                    MapInform.CurrentMap[gem.Y / sizeOfElement, gem.X / sizeOfElement] = 0;
                    MapInform.Crystal.Remove(gem);
                    PlayerModel.AddGem();
                }
        }

        public void CheckKilledOrcs()
        {
            foreach (var orc in OrcList.ToArray())
                if (orc.OrcModel.IsDead && orc.OrcModel.mayDelite)
                {
                    OrcList.Remove(orc);
                    PlayerModel.AddKill();
                }
        }

        public bool IsCompleteLevel() => (MapInform.Crystal.Count == 0);

        public void EndingLevel()
        {
            if (IsCompleteLevel())
            {
                MapInform.CurrentMap[(int)PositionOfPortal.Y, (int)PositionOfPortal.X] = 7;
                if (PlayerModel.TileOfPlayer.X == PositionOfPortal.X && PlayerModel.TileOfPlayer.Y == (PositionOfPortal.Y + 1) && IsCompleteLevel())
                    GameState.ChengeState(State.Final);
            }
        }

        public void Update()
        {
            SetDamage();
            CheckKilledOrcs();
            if (!PlayerModel.IsDead)
            {
                CheckCurrentGems();
                EndingLevel();
            }
            if (MapInfo.Blocks.Any(platform => PlayerModel.Rectangle.OnPlatform(platform)))
            {
                PlayerModel.Velocity.Y = 0f;
                PlayerModel.onGravity = false;
            }
            else PlayerModel.onGravity = true;
            foreach (var orc in OrcList)
            {
                if (MapInfo.Blocks.Any(platform => PlayerModel.Rectangle.OnPlatform(platform)))
                {
                    wasOnPlatform = true;
                    PlayerModel.FindTile();
                    PlayerModel.NodeOfPlayer = MapInfo.Graph.FindNodeByPosition(PlayerModel.TileOfPlayer);

                    orc.OrcModel.FindTile();
                    orc.OrcModel.NodeOfOrc = MapInfo.Graph.FindNodeByPosition(orc.OrcModel.TileOfOrc);
                    LastTileOfPlayer = PlayerModel.TileOfPlayer;
                }
                else if (wasOnPlatform && !MapInfo.Blocks.Any(platform => PlayerModel.Rectangle.OnPlatform(platform)))
                {
                    orc.OrcModel.FindTile();
                    orc.OrcModel.NodeOfOrc = MapInfo.Graph.FindNodeByPosition(orc.OrcModel.TileOfOrc);
                    PlayerModel.NodeOfPlayer = MapInfo.Graph.FindNodeByPosition(LastTileOfPlayer);
                    orc.OrcModel.way = BFS.FindWays(orc.OrcModel.NodeOfOrc, PlayerModel.NodeOfPlayer);
                }
                if (MapInfo.Blocks.Any(platform => orc.OrcModel.Rectangle.OnPlatform(platform)))
                {
                    orc.OrcModel.Velocity.Y = 0f;
                    orc.OrcModel.onGravity = false;
                }
                else orc.OrcModel.onGravity = true;
            }
        }
    }
}
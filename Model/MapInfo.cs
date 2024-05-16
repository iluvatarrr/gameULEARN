using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace rpgame2.Model
{
    public class MapInfo
    {
        public static List<Rectangle> Blocks;
        public List<Rectangle> Crystal;
        public int[,] CurrentMap;
        public int[,] mapMatrixFirstLevel =
        {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 8, 5, 5, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 2, 3, 3, 3, 4, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 3, 3, 4, 0, 0, 5, 5, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 3, 3, 3, 4, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 5, 8},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 3, 4},
            {0, 0, 0, 0, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 5, 5, 0, 0, 0, 5, 5, 0, 8, 6, 0},
            {0, 0, 0, 2, 3, 3, 3, 4, 0, 0, 5, 5, 5, 0, 0, 2, 3, 4, 0, 0, 1, 1, 1, 1, 1, 1, 1},
            {0, 0, 5, 0, 0, 0, 0, 0, 0, 2, 3, 3, 3, 4, 0, 0, 0, 0, 5, 0, 9, 9, 9, 9, 9, 9, 9},
            {1, 1, 1, 0, 0, 0, 0, 5, 5, 0, 0, 0, 0, 0, 0, 0, 2, 3, 4, 0, 0, 0, 5, 5, 5, 9, 9},
            {9, 9, 9, 0, 0, 0, 2, 3, 3, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 5, 5, 5, 9},
            {9, 9, 9, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 9, 5, 5, 8, 5, 9},
            {9, 9, 9, 0, 2, 3, 3, 3, 3, 4, 0, 0, 5, 5, 5, 0, 0, 0, 0, 0, 9, 9, 1, 1, 1, 1, 9},
            {9, 9, 9, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 3, 3, 4, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9, 9},
        };
        public int[,] mapMatrixSecondLevel =
{
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 5, 5, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 2, 3, 3, 3, 4, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 3, 3, 4, 0, 0, 5, 5, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 3, 3, 3, 4, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 5, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 3, 4},
            {0, 0, 0, 0, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 5, 5, 0, 0, 0, 5, 5, 0, 0, 6, 0},
            {0, 0, 0, 2, 3, 3, 3, 4, 0, 0, 5, 5, 5, 0, 0, 2, 3, 4, 0, 0, 1, 1, 1, 1, 1, 1, 1},
            {0, 0, 5, 0, 0, 0, 0, 0, 0, 2, 3, 3, 3, 4, 0, 0, 0, 0, 5, 0, 9, 9, 9, 9, 9, 9, 9},
            {1, 1, 1, 0, 0, 0, 0, 5, 5, 0, 0, 0, 0, 0, 0, 0, 2, 3, 4, 0, 0, 0, 5, 5, 5, 9, 9},
            {9, 9, 9, 0, 0, 0, 2, 3, 3, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 5, 5, 5, 9},
            {9, 9, 9, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 9, 5, 5, 0, 5, 9},
            {9, 9, 9, 0, 2, 3, 3, 3, 3, 4, 0, 0, 5, 5, 5, 0, 0, 0, 0, 0, 9, 9, 1, 1, 1, 1, 9},
            {9, 9, 9, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 3, 3, 4, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9, 9},
        };
        public int[,] mapMatrixThirdLevel =
{
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 3, 3, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 3, 3, 3, 4, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 3, 4},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 6, 0},
            {0, 0, 0, 2, 3, 3, 3, 4, 0, 0, 0, 0, 0, 0, 0, 2, 3, 4, 0, 0, 1, 1, 1, 1, 1, 1, 1},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 3, 3, 4, 0, 0, 0, 0, 5, 0, 9, 9, 9, 9, 9, 9, 9},
            {1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 4, 0, 0, 0, 0, 0, 0, 9, 9},
            {9, 9, 9, 0, 0, 0, 2, 3, 3, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 9},
            {9, 9, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 9, 0, 0, 0, 0, 9},
            {9, 9, 9, 0, 2, 3, 3, 3, 3, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 9, 1, 1, 1, 1, 9},
            {9, 9, 9, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 3, 3, 4, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9, 9},
        };
        public static Graph Graph;

        public void TryConnect(int Y, int X, Vector2 currentNodePosition)
        {
            if (this.CurrentMap[Y, X] == 1 || this.CurrentMap[Y, X] == 2 || this.CurrentMap[Y, X] == 3 || this.CurrentMap[Y, X] == 4)
            {
                var aroundNodePosition = new Vector2(X, Y);
                if (Graph.IsNewPosition(aroundNodePosition)) Graph.nodes.Add(new Node(aroundNodePosition));
                if (Graph.FindNodeByPosition(currentNodePosition).IncidentNodes.All(x => x.Position != aroundNodePosition))
                    Graph.Connect(currentNodePosition, aroundNodePosition);
            }
        }

        public void MakeAroundVertex(Vector2 currentNodePosition, int y, int x, bool IsLast)
        {
            for (var i = -1; i <= 1; i++)
                for (var j = -1; j <= 1; j++)
                {
                    if (IsLast)
                        if (i == 0 && j == 0 || i == 1 && j == -1 || i == 1 && j == 0 || i == 1 && j == 1) continue;
                    if (i == 0 && j == 0) continue;
                    TryConnect((y + i), (x + j), currentNodePosition);
                }
        }

        public void MakeHeightVertex(Vector2 currentNodePosition, int y, int x)
        {
            if (y <= 2) return;
            TryConnect((y - 2), (x + 1), currentNodePosition);
            TryConnect((y - 2), (x - 1), currentNodePosition);
            TryConnect((y - 2), x, currentNodePosition);
        }

        public void MakeWidthVertex(Vector2 currentNodePosition, int y, int x, int MapWidth)
        {
            if (!(x > 1 && y > 1 && x < MapWidth - 2)) return;
            TryConnect((y - 1), (x - 2), currentNodePosition);
            TryConnect((y - 1), (x + 2), currentNodePosition);
        }

        public void MakeLongWidthVertex(Vector2 currentNodePosition, int y, int x, int MapWidth)
        {
            for (var g = 1; g < 3; g++)
            {
                if (x > g && y > 1 && x < (MapWidth - (g + 1)))
                {
                    TryConnect((y), (x - (g + 1)), currentNodePosition);
                    TryConnect((y), (x + (g + 1)), currentNodePosition);
                }
            }
        }

        public void MakeVertex(int x, int y, int MapWidth, bool IsLastString)
        {
            var currentNodePosition = new Vector2(x, y);
            if (Graph.IsNewPosition(currentNodePosition))
                Graph.nodes.Add(new Node(currentNodePosition));
            MakeAroundVertex(currentNodePosition, y, x, IsLastString);
            MakeHeightVertex(currentNodePosition, y, x);
            MakeWidthVertex(currentNodePosition, y, x, MapWidth);
            MakeLongWidthVertex(currentNodePosition, y, x, MapWidth);
        }

        public void MakeGraph()
        {
            var MapHeight = this.CurrentMap.GetLength(0);
            var MapWidth = this.CurrentMap.GetLength(1);

            Graph.nodes = new List<Node>();
            for (int y = 1; y < MapHeight - 1; y++)
                for (int x = 1; x < MapWidth - 1; x++)
                {
                    if (this.CurrentMap[y, x] == 0 || this.CurrentMap[y, x] == 9) continue;
                    else if (this.CurrentMap[y, x] == 1 || this.CurrentMap[y, x] == 2
                        || this.CurrentMap[y, x] == 3 || this.CurrentMap[y, x] == 4)
                        MakeVertex(x,y,MapWidth, false);
                }
            for (var x = 1; x < MapWidth - 1; x++)
            {
                if (this.CurrentMap[MapHeight - 1, x] == 0 || this.CurrentMap[MapHeight - 1, x] == 9) continue;
                MakeVertex(x, MapHeight - 1, MapWidth, true);
            }
        }
    }
}

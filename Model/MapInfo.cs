using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace rpgame2.Model
{
    public class MapInfo
    {
        public List<Rectangle> Blocks;
        public List<Rectangle> Crystal;
        public int[,] CurrentMap;
        public int[,] mapMatrixFirstLevel =
        {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 2, 3, 3, 3, 4, 0, 0, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 3, 3, 4, 0, 0, 5, 5, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 3, 3, 3, 4, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 5, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 3, 4},
            {0, 0, 0, 0, 5, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 5, 5, 5, 0, 0, 5, 5, 0, 0, 6, 0},
            {0, 0, 0, 2, 3, 3, 3, 4, 0, 0, 5, 5, 5, 0, 0, 0, 2, 3, 4, 0, 0, 1, 1, 1, 1, 1, 1},
            {0, 0, 5, 0, 0, 0, 0, 0, 0, 2, 3, 3, 3, 3, 4, 0, 0, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9},
            {1, 1, 1, 0, 0, 0, 0, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9},
            {9, 9, 9, 0, 0, 0, 2, 3, 3, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9},
            {9, 9, 9, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9},
            {9, 9, 9, 0, 2, 3, 3, 3, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9},
            {9, 9, 9, 0, 0, 0, 0, 0, 0, 0, 2, 3, 4, 0, 0, 0, 0, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9},
        };

        public Graph Graph;

        public void SubMethod(int Y, int X, Vector2 currentNodePosition)
        {
            if (this.CurrentMap[Y, X] == 1 || this.CurrentMap[Y, X] == 2
                || this.CurrentMap[Y, X] == 3 || this.CurrentMap[Y, X] == 4)
            {
                var aroundNodePosition = new Vector2(X, Y);
                if (Graph.IsNewPosition(aroundNodePosition))
                    Graph.nodes.Add(new Node(aroundNodePosition));
                if (Graph.FindNodeByPosition(currentNodePosition).IncidentNodes.All(x => x.Position != aroundNodePosition))
                    Graph.Connect(currentNodePosition, aroundNodePosition);
            }
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
                    {
                        var currentNodePosition = new Vector2(x, y);
                        if (Graph.IsNewPosition(currentNodePosition))
                            Graph.nodes.Add(new Node(currentNodePosition));
                        //XXX
                        //XcX
                        //XXX
                        for (var i = -1; i <= 1; i++)
                            for (var j = -1; j <= 1; j++)
                            {
                                if (i == 0 && j == 0) continue;
                                SubMethod((y + i), (x + j), currentNodePosition);
                            }
                        //XXX
                        //XXX
                        //XcX
                        //XXX
                        //XXX
                        if (y > 2)
                        {
                            SubMethod((y - 2), (x + 1), currentNodePosition);
                            SubMethod((y - 2), (x - 1), currentNodePosition);
                            SubMethod((y - 2), x, currentNodePosition);
                        }
                        // XXX
                        //XXXXX
                        // XcX
                        //XXXXX
                        // XXX
                        if (x > 1 && y > 1 && x < MapWidth - 2)
                        {
                            SubMethod((y - 1), (x - 2), currentNodePosition);
                            SubMethod((y - 1), (x + 2), currentNodePosition);
                        }
                        //   XXX
                        //  XXXXX
                        //XXXXcXXXX
                        //  XXXXX
                        //   XXX
                        for (var g = 1; g < 4; g++)
                        {
                            if (x > g && y > 1 && x < (MapWidth - (g + 1)))
                            {
                                SubMethod((y), (x - (g + 1)), currentNodePosition);
                                SubMethod((y), (x + (g + 1)), currentNodePosition);
                            }
                        }
                    }
                }
            //For last string of map
            for (var x = 1; x < MapWidth - 1; x++)
            {
                if (this.CurrentMap[MapHeight - 1, x] == 0 || this.CurrentMap[MapHeight - 1, x] == 9) continue;
                var newCurrentNodePosition = new Vector2(x, MapHeight - 1);
                if (Graph.IsNewPosition(newCurrentNodePosition))
                    Graph.nodes.Add(new Node(newCurrentNodePosition));
                for (var i = -1; i <= 1; i++)
                    for (var j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0 || i == 1 && j == -1 || i == 1 && j == 0 || i == 1 && j == 1) continue;
                        SubMethod((MapHeight - 1 + i), (x + j), newCurrentNodePosition);
                    }

                for (var g = 1; g < 4; g++)
                {
                    if (x > g && x < (MapWidth - (g + 1)))
                    {
                        SubMethod((MapHeight - 1), (x - (g + 1)), newCurrentNodePosition);
                        SubMethod((MapHeight - 1), (x + (g + 1)), newCurrentNodePosition);
                    }
                }

                SubMethod(((MapHeight - 1) - 2), (x + 1), newCurrentNodePosition);
                SubMethod(((MapHeight - 1) - 2), (x - 1), newCurrentNodePosition);
                SubMethod(((MapHeight - 1) - 2), x, newCurrentNodePosition);

                SubMethod(((MapHeight - 1) - 1), (x - 2), newCurrentNodePosition);
                SubMethod(((MapHeight - 1) - 1), (x + 2), newCurrentNodePosition);
            }
        }
    }
}

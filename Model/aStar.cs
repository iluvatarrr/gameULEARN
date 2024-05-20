using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;


namespace rpgame2.Model
{

    public class StarData
    {
        public Node CurrentNode { get; set; }
        public int Cost { get; set; }
        public Vector2 Position { get; set; }
        public int PathLengthFromStart { get; set; }
        public StarData NodeFrom { get; set; }
        public int HeuristicOfChebishevPathLength { get; set; }
        public int EstimateFullPathLength
        {
            get
            {
                return this.PathLengthFromStart + Cost + HeuristicOfChebishevPathLength;
            }
        }
    }

    public class aStar
    {
        public static List<Node> FindWays(Node start, Node end)
        {
            var visitedNodes = new Collection<StarData>();
            var openNodes = new Collection<StarData>();

            var startNode = new StarData()
            {
                CurrentNode = start,
                Position = start.Position,
                Cost = start.Weight,
                NodeFrom = null,
                PathLengthFromStart = 0,
                HeuristicOfChebishevPathLength = GetHeuristicPathLength(start.Position, end.Position)
            };

            openNodes.Add(startNode);
            while (openNodes.Count > 0)
            {
                var currentNode = openNodes.OrderBy(node => node.EstimateFullPathLength).First();
                if (currentNode.Position == end.Position) return GetWay(currentNode);

                visitedNodes.Add(currentNode);
                openNodes.Remove(currentNode);

                foreach (var incidentNode in MakeIncidentNodes(currentNode.CurrentNode.IncidentNodes, currentNode, end))
                {
                    if (visitedNodes.Count(node => node.Position == incidentNode.Position) > 0) continue;
                    var openNode = openNodes.FirstOrDefault(node => node.Position == incidentNode.Position);
                    if (openNode == null) openNodes.Add(incidentNode);
                    else
                        if (openNode.Cost > incidentNode.Cost)
                        {
                            openNode.NodeFrom = currentNode;
                            openNode.PathLengthFromStart = incidentNode.PathLengthFromStart;
                            openNode.Cost = incidentNode.Cost;
                            openNode.HeuristicOfChebishevPathLength = incidentNode.HeuristicOfChebishevPathLength;
                        }
                }
            }
            return null;
        }

        public static List<StarData> MakeIncidentNodes(IEnumerable<Node> nodes, StarData fromNode, Node goal)
        {
            var ListOfPathNode = new List<StarData>();
            foreach (var node in nodes) 
            {
                ListOfPathNode.Add(new StarData()
                {
                    CurrentNode = node,
                    Position = node.Position,
                    Cost = node.Weight + fromNode.Cost,
                    NodeFrom = fromNode,
                    PathLengthFromStart = fromNode.PathLengthFromStart + 1,
                    HeuristicOfChebishevPathLength = GetHeuristicPathLength(node.Position, goal.Position),
                });
            }
            return ListOfPathNode;
        }

        private static List<Node> GetWay(StarData pathNode)
        {
            var result = new List<Node>();
            var currentPathNode = pathNode;
            while (currentPathNode != null)
            {
                result.Add(currentPathNode.CurrentNode);
                currentPathNode = currentPathNode.NodeFrom;
            }
            result.Reverse();
            return result;
        }

        private static int GetHeuristicPathLength(Vector2 From, Vector2 Goal) => Math.Max((int)Math.Abs(From.X - Goal.X),(int)Math.Abs(From.Y - Goal.Y));
    }
}
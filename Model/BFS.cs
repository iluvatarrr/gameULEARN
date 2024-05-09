using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace rpgame2.Model
{
    public static class BFS
    {
        //IEnumerable<Node>
        public static IEnumerable<Node> FindWays(Node startNode, Node endNode)
        {
            var visited = new HashSet<Node>();
            var queue = new Queue<Node>();
            visited.Add(startNode);
            queue.Enqueue(startNode);
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                if (node.Equals(endNode))
                {
                    yield return node;
                    yield break;
                }
                yield return node;
                foreach (var nextNode in node.IncidentNodes.Where(n => !visited.Contains(n)))
                {
                    nextNode.Previous = node;
                    visited.Add(nextNode);
                    queue.Enqueue(nextNode);
                }
            }
        }

        //public static List<Node> FindWays(Node startNode, Node endNode)
        //{
        //    var path = new List<Node>();
        //    var visited = new HashSet<Node>();
        //    var queue = new Queue<Node>();
        //    visited.Add(startNode);
        //    queue.Enqueue(startNode);
        //    while (queue.Count != 0)
        //    {
        //        var node = queue.Dequeue();
        //        if (node == null) break;
        //        foreach (var nextNode in node.IncidentNodes.Where(n => !visited.Contains(n)))
        //        {
        //            nextNode.Previous = node;
        //            visited.Add(nextNode);
        //            queue.Enqueue(nextNode);
        //        }
        //    }
        //    if (visited.Contains(endNode))
        //    {
        //        var currentNode = endNode;
        //        while (currentNode != null && path.Count < 100000)
        //        {
        //            path.Add(currentNode);
        //            currentNode = currentNode.Previous;
        //        }
        //        path.Reverse();
        //        return path;

        //    }
        //    return path;
        //}
    }
}

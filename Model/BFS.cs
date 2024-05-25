
using System.Collections.Generic;
using System.Linq;

namespace rpgame2.Model
{
    public static class BFS
    {
        public static IEnumerable<Node> FindWays(Node startNode, Node endNode)
        {
            if (startNode.Position == endNode.Position)
            {
                yield return startNode;
                yield break;
            }
            var visited = new HashSet<Node>();
            var queue = new Queue<Node>();
            visited.Add(startNode);
            queue.Enqueue(startNode);
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                if (node.Equals(endNode))
                {
                    var currentNode = endNode;
                    while (currentNode != null)
                    {
                        yield return currentNode;
                        currentNode = currentNode.Previous;
                        if (currentNode.Equals(startNode)) yield break;
                    }
                }
                foreach (var nextNode in node.IncidentNodes.Where(n => !visited.Contains(n)))
                {
                    nextNode.Previous = node;
                    visited.Add(nextNode);
                    queue.Enqueue(nextNode);
                }
            }
        }
    }
}

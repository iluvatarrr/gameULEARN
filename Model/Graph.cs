using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

//Взял с курса, дописал некоторые метоы для удобства
//Сделал это для ускорения разработки

namespace rpgame2.Model
{
    public class Edge
    {
        public readonly Node From;
        public readonly Node To;
        public Edge(Node first, Node second)
        {
            this.From = first;
            this.To = second;
        }
        public bool IsIncident(Node node)
        {
            return From == node || To == node;
        }
        public Node OtherNode(Node node)
        {
            if (!IsIncident(node)) throw new ArgumentException();
            if (From == node) return To;
            return From;
        }
    }

    public class Node
    {
        readonly List<Edge> edges = new List<Edge>();
        public Vector2 Position;
        public Node Previous { get; set; }
        public Node(Vector2 position)
        {
            Position = position;
        }

        public IEnumerable<Node> IncidentNodes
        {
            get
            {
                return edges.Select(z => z.OtherNode(this));
            }
        }
        public IEnumerable<Edge> IncidentEdges
        {
            get
            {
                foreach (var e in edges) yield return e;
            }
        }

        public static Edge Connect(Node node1, Node node2, Graph graph)
        {
            if (!graph.Nodes.Contains(node1) || !graph.Nodes.Contains(node2)) throw new ArgumentException();
            var edge = new Edge(node1, node2);
            node1.edges.Add(edge);
            node2.edges.Add(edge);
            return edge;
        }
        public static void Disconnect(Edge edge)
        {
            edge.From.edges.Remove(edge);
            edge.To.edges.Remove(edge);
        }
    }

    public class Graph
    {
        public List<Node> nodes;

        //public Graph(int nodesCount)
        //{
        //    nodes = Enumerable.Range(0, nodesCount).Select(z => new Node(z)).ToArray();
        //}



        public int Length { get { return nodes.Count; } }

        //public Node this[int index] { get { return nodes[index]; } }

        public bool IsNewPosition(Vector2 Position)
        {
            return Nodes.All(s => s.Position != Position);
        }

        public IEnumerable<Node> Nodes
        {
            get
            {
                foreach (var node in nodes) yield return node;
            }
        }

        public void Connect(int index1, int index2)
        {
            Node.Connect(nodes[index1], nodes[index2], this);
        }
        public void Connect(Vector2 Position1, Vector2 Position2)
        {
            Node.Connect(nodes.Where(x => x.Position == Position1).First(), nodes.Where(x => x.Position == Position2).First(), this);
        }

        public Node FindNodeByPosition(Vector2 Position)
        {
            return nodes.Where(x => x.Position == Position).First();
        }

        public void Delete(Edge edge)
        {
            Node.Disconnect(edge);
        }

        public IEnumerable<Edge> Edges
        {
            get
            {
                return nodes.SelectMany(z => z.IncidentEdges).Distinct();
            }
        }

        //public static Graph MakeGraph(params int[] incidentNodes)
        //{
        //    var graph = new Graph(incidentNodes.Max() + 1);
        //    for (int i = 0; i < incidentNodes.Length - 1; i += 2)
        //        graph.Connect(incidentNodes[i], incidentNodes[i + 1]);
        //    return graph;
        //}
    }
}

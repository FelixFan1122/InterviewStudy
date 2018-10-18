using System;
using System.Collections.Generic;
using InterviewStudy.AdjacencyList;
using InterviewStudy.BinaryHeap;

namespace InterviewStudy.DijkstraAlgorithm
{
    public class ShortestPath
    {
        private static WeightedDirectedEdge Source = new WeightedDirectedEdge(-1, -1, 0.0);

        private readonly WeightedDirectedEdge[] shortestPathTree;

        public ShortestPath(EdgeWeightedDigraph graph, int source)
        {
            shortestPathTree = new WeightedDirectedEdge[graph.VertexNumber];
            shortestPathTree[source] = Source;

            var edges = new BinaryHeap<WeightedDirectedEdge>(graph.EdgeNumber);
            foreach(var edge in graph.GetAllEdges())
            {
                if (edge.Weight < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(graph),
                        "Dijkstra's algorithm only supports non-negative weights.");
                }

                edges.Insert(edge);
            }

            var edgeNumber = 0;
            while (edgeNumber <= shortestPathTree.Length - 1 && edges.Size > 0)
            {
                var shortestEdge = edges.Pop();
                if (shortestPathTree[shortestEdge.To] == null)
                {
                    shortestPathTree[shortestEdge.To] = shortestEdge;
                    edgeNumber++;
                }
            }
        }

        public double GetShortestDistanceTo(int destination)
        {
            if (destination < 0 || destination >= shortestPathTree.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(destination));
            }

            var current = shortestPathTree[destination];
            var distance = 0.0;
            while (current != null && current != Source)
            {
                distance += current.Weight;
                current = shortestPathTree[current.From];
            }

            return distance;
        }

        public IEnumerable<WeightedDirectedEdge> GetShortestPathTo(int destination)
        {
            if (destination < 0 || destination >= shortestPathTree.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(destination));
            }

            var current = shortestPathTree[destination];
            var path = new Stack<WeightedDirectedEdge>();
            while (current != null && current != Source)
            {
                path.Push(current);
                current = shortestPathTree[current.From];
            }

            return path;
        }

        public bool HasPathTo(int destination)
        {
            if (destination < 0 || destination >= shortestPathTree.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(destination));
            }

            return shortestPathTree[destination] != null && shortestPathTree[destination] != Source; 
        }
    }
}
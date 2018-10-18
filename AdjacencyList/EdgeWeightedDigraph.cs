using System;
using System.Collections.Generic;

namespace InterviewStudy.AdjacencyList
{
    public class EdgeWeightedDigraph
    {
        private List<WeightedDirectedEdge>[] adjacencyList;

        public EdgeWeightedDigraph(int vertexNumber)
        {
            adjacencyList = new List<WeightedDirectedEdge>[vertexNumber];
        }

        public int EdgeNumber { get; private set; }

        public int VertexNumber
        {
            get
            {
                return adjacencyList.Length;
            }
        }

        public void AddEdge(WeightedDirectedEdge edge)
        {
            if (edge == null)
            {
                throw new ArgumentNullException(nameof(edge));
            }

            if (adjacencyList[edge.From] == null)
            {
                adjacencyList[edge.From] = new List<WeightedDirectedEdge>();
            }

            adjacencyList[edge.From].Add(edge);
            EdgeNumber++;
        }

        public IEnumerable<WeightedDirectedEdge> GetAllEdges()
        {
            foreach (var edges in adjacencyList)
            {
                foreach (var edge in edges)
                {
                    yield return edge;
                }
            }
        }

        public IEnumerable<WeightedDirectedEdge> GetEdges(int vertex)
        {
            return adjacencyList[vertex];
        }
    }
}
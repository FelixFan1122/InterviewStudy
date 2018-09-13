using System;
using System.Collections.Generic;

namespace InterviewStudy.AdjacencyList
{
    public class Graph
    {
        private List<int>[] adjacencyList;

        public Graph(int vertexNumber)
        {
            if (vertexNumber <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(vertexNumber));
            }

            adjacencyList = new List<int>[vertexNumber];
        }

        public int EdgeNumber { get; private set; }

        public int VertexNumber
        {
            get
            {
                return adjacencyList.Length;
            }
        }

        public void AddEdge(int v, int w)
        {
            if (v < 0 || v >= adjacencyList.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(v));
            }

            if (w < 0 || w >= adjacencyList.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(w));
            }

            if (adjacencyList[v] == null)
            {
                adjacencyList[v] = new List<int>();
            }

            if (adjacencyList[w] == null)
            {
                adjacencyList[w] = new List<int>();
            }

            adjacencyList[v].Add(w);
            adjacencyList[w].Add(v);
            EdgeNumber++;
        }

        public IEnumerable<int> GetAdjacentVertices(int v)
        {
            return adjacencyList[v];
        }
    }
}
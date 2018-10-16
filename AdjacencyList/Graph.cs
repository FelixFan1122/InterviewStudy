using System;
using System.Collections.Generic;

namespace InterviewStudy.AdjacencyList
{
    public abstract class Graph
    {
        protected List<int>[] adjacencyList;

        protected Graph(int vertexNumber)
        {
            if (vertexNumber <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(vertexNumber));
            }

            adjacencyList = new List<int>[vertexNumber];
        }

        public int EdgeNumber { get; protected set; }

        public int VertexNumber
        {
            get
            {
                return adjacencyList.Length;
            }
        }

        public abstract void AddEdge(int v, int w);

        public IEnumerable<int> GetAdjacentVertices(int v)
        {
            return adjacencyList[v];
        }
    }
}
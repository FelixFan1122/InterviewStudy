using System;
using System.Collections.Generic;

namespace InterviewStudy.AdjacencyList
{
    public class Digraph : Graph
    {
        public Digraph(int vertexNumber) : base(vertexNumber)
        {
        }

        public override void AddEdge(int v, int w)
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

            adjacencyList[v].Add(w);
            EdgeNumber++;
        }
    }
}
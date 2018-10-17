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

        public Stack<int> GetTopologicalSort()
        {
            var stack = new Stack<int>();
            var unvisited = 0;
            var visited = new bool[VertexNumber];
            while (true)
            {
                while (unvisited < VertexNumber && visited[unvisited])
                {
                    unvisited++;
                }

                if (unvisited == VertexNumber)
                {
                    break;
                }

                DfsForTopologicalSort(new HashSet<int>(), stack, unvisited, visited);
            }

            return stack;
        }

        private void DfsForTopologicalSort(HashSet<int> leadingPath, Stack<int> stack, int vertex, bool[] visited)
        {
            var leadingPathClone = new HashSet<int>(leadingPath);
            leadingPathClone.Add(vertex);
            var neighours = GetAdjacentVertices(vertex);
            foreach (var neighour in neighours)
            {
                if (visited[neighour])
                {
                    if (leadingPath.Contains(neighour))
                    {
                        throw new InvalidOperationException("The graph is not a DAG.");
                    }
                }
                else
                {
                    DfsForTopologicalSort(leadingPathClone, stack, neighour, visited);
                }
            }

            visited[vertex] = true;
            stack.Push(vertex);
        }
    }
}
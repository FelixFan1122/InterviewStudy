using System.Collections.Generic;
using InterviewStudy.AdjacencyList;

namespace InterviewStudy.BreadthFirstSearch
{
    public class BreadthFirstSearch
    {
        private const int NotDefined = -1;

        private int[] traces;

        public BreadthFirstSearch(Graph graph, int source)
        {
            traces = new int[graph.VertexNumber];
            for (var i = 0; i < traces.Length; i++)
            {
                traces[i] = NotDefined;
            }

            var vertices = new Queue<int>();
            vertices.Enqueue(source);
            while (vertices.Count > 0)
            {
                var vertex = vertices.Dequeue();
                var adjacentVertices = graph.GetAdjacentVertices(vertex);
                foreach (var adjacentVertex in adjacentVertices)
                {
                    if (traces[adjacentVertex] == NotDefined && adjacentVertex != source)
                    {
                        traces[adjacentVertex] = vertex;
                        vertices.Enqueue(adjacentVertex);
                    }
                }
            }
        }

        public IEnumerable<int> GetPath(int destination)
        {
            var path = new Stack<int>();
            var vertex = destination;
            while (vertex != NotDefined)
            {
                path.Push(vertex);
                vertex = traces[vertex];
            }

            return path;
        }

        public bool HasPath(int destination)
        {
            return traces[destination] != NotDefined;
        }
    }
}
using System.Collections.Generic;
using InterviewStudy.AdjacencyList;

namespace InterviewStudy.DepthFirstSearch
{
    public class DepthFirstSearch
    {
        private const int NotDefined = -1;

        private Graph graph;
        private int[] traces;

        public DepthFirstSearch(Graph graph, int source)
        {
            this.graph = graph;
            traces = new int[graph.VertexNumber];
            for (var i = 0; i < traces.Length; i++)
            {
                traces[i] = NotDefined;
            }

            Dfs(source);
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

        private void Dfs(int vertex)
        {
            var neighours = graph.GetAdjacentVertices(vertex);
            foreach (var neighour in neighours)
            {
                if (traces[neighour] == NotDefined)
                {
                    traces[neighour] = vertex;
                    Dfs(neighour);
                }
            }
        }
    }
}
using System;

namespace InterviewStudy
{
    public class WeightedDirectedEdge : IComparable<WeightedDirectedEdge>
    {
        public WeightedDirectedEdge(int from, int to, double weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }

        public int From { get; private set; }
        public int To { get; private set; }
        public double Weight { get; private set; }

        public int CompareTo(WeightedDirectedEdge other)
        {
            if (Weight < other.Weight)
            {
                return -1;
            }
            else if (Weight > other.Weight)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
using System;

namespace InterviewStudy.BinarySearch
{
    public static class BinarySearch
    {
        public static int BinarySearchIteratively<T> (T[] sorted, T value) where T : IComparable<T>
        {
            var start = 0;
            var end = sorted.Length - 1;
            while (start < end)
            {
                var mid = (start + end) / 2;
                if (sorted[mid].CompareTo(value) == 0)
                {
                    return mid;
                }
                else if (sorted[mid].CompareTo(value) > 0)
                {
                    end = mid - 1;
                }
                else
                {
                    start = mid + 1;
                }
            }

            return -1;
        }

        public static int BinarySearchRecursively<T> (T[] sorted, T value) where T : IComparable<T>
        {
            return BinarySearchRecursivelyImpl(sorted, value, 0, sorted.Length - 1);
        }

        private static int BinarySearchRecursivelyImpl<T> (T[] sorted, T value, int start, int end) where T : IComparable<T>
        {
            if (start > end)
            {
                return -1;
            }

            var mid = (start + end) / 2;
            var comparisonResult = value.CompareTo(sorted[mid]);
            if (comparisonResult == 0)
            {
                return mid;
            }
            else if (comparisonResult < 0)
            {
                return BinarySearchRecursivelyImpl(sorted, value, start, mid - 1);
            }
            else
            {
                return BinarySearchRecursivelyImpl(sorted, value, mid + 1, end);
            }
        }
    }
}
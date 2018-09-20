using System;

namespace InterviewStudy.QuickSort
{
    public static class QuickSort
    {
        public static void RandomizedQuickSort<T>(T[] unsorted) where T : IComparable<T>
        {
            if (unsorted == null)
            {
                throw new ArgumentNullException(nameof(unsorted));
            }

            Randomize(unsorted);
            Sort(unsorted);
        }

        public static void Sort<T>(T[] unsorted) where T : IComparable<T>
        {
            if (unsorted == null)
            {
                throw new ArgumentNullException(nameof(unsorted));
            }

            Sort(unsorted, 0, unsorted.Length - 1);
        }

        private static void Randomize<T>(T[] input)
        {
            var rand = new Random();
            for (var i = 0; i < input.Length; i++)
            {
                var j = rand.Next(i + 1, input.Length);
                var temp = input[i];
                input[i] = input[j];
                input[j] = temp;
            }
        }

        // Hardcode naive pivot picking strategy.
        private static void Sort<T>(T[] unsorted, int start, int end) where T : IComparable<T>
        {
            if (start >= end)
            {
                return;
            }

            var pivot = unsorted[start];
            var less = start + 1;
            var greater = end;
            T temp;
            while (true)
            {
                while (less <= end && unsorted[less].CompareTo(pivot) <= 0)
                {
                    less++;
                }

                while (greater > start && unsorted[greater].CompareTo(pivot) >= 0)
                {
                    greater--;
                }

                if (less >= greater)
                {
                    break;
                }

                temp = unsorted[less];
                unsorted[less] = unsorted[greater];
                unsorted[greater] = temp;
            }

            if (less > end)
            {
                less = end;
            }

            temp = unsorted[start];
            unsorted[start] = unsorted[less];
            unsorted[less] = temp;

            Sort(unsorted, start, less - 1);
            Sort(unsorted, less + 1, end);
        }
    }
}
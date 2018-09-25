using System;

namespace InterviewStudy.MergeSort
{
    public static class MergeSort
    {
        public static void Sort<T>(T[] unsorted) where T : IComparable<T>
        {
            for (var blockSize = 1; blockSize < unsorted.Length; blockSize *= 2)
            {
                var buffer = new T[blockSize * 2];
                for (var blockNumber = 0; blockNumber <= unsorted.Length / blockSize + 1; blockNumber += 2)
                {
                    var start = blockNumber * blockSize;
                    var first = start;
                    var second = first + blockSize;
                    var mid = second;
                    var end = second + blockSize;
                    var index = 0;
                    while (first < mid && second < end && second < unsorted.Length)
                    {
                        if (unsorted[first].CompareTo(unsorted[second]) < 0)
                        {
                            buffer[index] = unsorted[first];
                            first++;
                        }
                        else
                        {
                            buffer[index] = unsorted[second];
                            second++;
                        }

                        index++;
                    }

                    if (first == mid)
                    {
                        // First block exausted, second block may have additional elments.
                        while (second < end && second < unsorted.Length)
                        {
                            buffer[index] = unsorted[second];
                            index++;
                            second++;
                        }
                    }
                    else
                    {
                        // Second block exausted or there's no second block, first block may have additional elments.
                        while (first < mid && first < unsorted.Length)
                        {
                            buffer[index] = unsorted[first];
                            index++;
                            first++;
                        }
                    }

                    Array.Copy(buffer, 0, unsorted, start, end < unsorted.Length ? blockSize * 2 : unsorted.Length - start);
                }
            }
        }
    }
}
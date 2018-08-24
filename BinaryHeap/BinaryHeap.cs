using System;

namespace InterviewStudy.BinaryHeap
{
    /// TODO: Implement auto resizing.
    public class BinaryHeap<T> where T : IComparable<T>
    {
        private const int Capacity = 100;

        private T[] storage;

        public BinaryHeap() : this(Capacity)
        {

        }

        public BinaryHeap(int capacity)
        {
            storage = new T[capacity];
        }

        public int Size { get; private set; }

        public void Insert(T value)
        {
            Size++;
            storage[Size] = value;
            var child = Size;
            var parent = child / 2;
            while (parent > 0 && storage[child].CompareTo(storage[parent]) < 0)
            {
                var temp = storage[child];
                storage[child] = storage[parent];
                storage[parent] = temp;
                child = parent;
                parent /= 2;
            }
        }

        public T Peek()
        {
            if (Size == 0)
            {
                throw new InvalidOperationException();
            }
            
            return storage[1];
        }

        public T Pop()
        {
            if (Size == 0)
            {
                throw new InvalidOperationException();
            }

            var result = storage[1];
            storage[1] = storage[Size];
            Size--;
            var parent = 1;
            var leftChild = parent * 2;
            var rightChild = leftChild + 1;
            while (leftChild <= Size)
            {
                var smaller = leftChild;
                if (rightChild <= Size && storage[leftChild].CompareTo(storage[rightChild]) > 0)
                {
                    smaller = rightChild;
                }

                if (storage[parent].CompareTo(storage[smaller]) <= 0)
                {
                    break;
                }

                var temp = storage[parent];
                storage[parent] = storage[smaller];
                storage[smaller] = temp;
                parent = smaller;
                leftChild = parent * 2;
                rightChild = leftChild + 1;
            }

            return result;
        }
    }
}
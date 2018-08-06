using System;

namespace InterviewStudy.LinkedList
{
    public class LinkedList<T>
    {
        private Node<T> head;

        public int Size { get; private set; }

        public T Get(int position)
        {
            if (position < 0 || position >= Size)
            {
                throw new ArgumentOutOfRangeException(nameof(position));
            }

            return getNode(position).Value;
        }

        public void Insert(T value, int position)
        {
            if (position < 0 || position > Size)
            {
                throw new ArgumentOutOfRangeException(nameof(position));
            }

            var node = new Node<T>(value);
            if (position == 0)
            {
                node.Next = head;
                head = node;
            }
            else
            {
                var previous = getNode(position - 1);
                node.Next = previous.Next;
                previous.Next = node;
            }

            Size++;
        }

        public void Remove(int position)
        {
            if (position < 0 || position >= Size)
            {
                throw new ArgumentOutOfRangeException(nameof(position));
            }

            if (position == 0)
            {
                head = head.Next;
            }
            else
            {
                var previous = getNode(position - 1);
                previous.Next = previous.Next.Next;
            }

            Size--;
        }

        private Node<T> getNode(int position)
        {
            var node = head;
            while (position > 0)
            {
                node = node.Next;
                position--;
            }

            return node;
        }
    }
}
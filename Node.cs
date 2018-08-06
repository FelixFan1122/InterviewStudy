namespace InterviewStudy
{
    internal class Node<T>
    {
        internal Node(T value)
        {
            Value = value;
        }

        internal Node<T> Next { get; set; }

        internal T Value { get; set; }
    }
}
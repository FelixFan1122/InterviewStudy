namespace InterviewStudy
{
    internal class BinaryTreeNode<T>
    {
        internal BinaryTreeNode(T value)
        {
            Value = value;
        }

        internal BinaryTreeNode<T> LeftChild { get; set; }
        internal BinaryTreeNode<T> RightChild { get; set; }
        internal T Value { get; set; }
    }
}
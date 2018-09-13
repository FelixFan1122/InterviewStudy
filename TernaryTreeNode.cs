namespace InterviewStudy
{
    internal class TernaryTreeNode<TKey, TValue>
    {
        internal TernaryTreeNode(TKey key, TValue value = default(TValue))
        {
            Key = key;
            Value = value;
        }

        internal TKey Key { get; set; }
        internal TernaryTreeNode<TKey, TValue> LeftChild { get; set; }
        internal TernaryTreeNode<TKey, TValue> MiddleChild { get; set; }
        internal TernaryTreeNode<TKey, TValue> RightChild { get; set; }
        internal TValue Value { get; set; }
    }
}
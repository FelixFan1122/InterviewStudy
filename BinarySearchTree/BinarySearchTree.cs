using System;
using System.Collections.Generic;

namespace InterviewStudy.BinarySearchTree
{
    public class BinarySearchTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        private BinaryTreeNode<KeyValuePair<TKey, TValue>> root;

        public IEnumerable<TKey> Keys
        {
            get
            {
                if (Size == 0)
                {
                    throw new InvalidOperationException();
                }

                var nodes = new Queue<BinaryTreeNode<KeyValuePair<TKey, TValue>>>();
                nodes.Enqueue(root);
                while (nodes.Count > 0)
                {
                    var node = nodes.Dequeue();
                    if (node.LeftChild != null)
                    {
                        nodes.Enqueue(node.LeftChild);
                    }

                    if (node.RightChild != null)
                    {
                        nodes.Enqueue(node.RightChild);
                    }

                    yield return node.Value.Key;
                }
            }
        }

        public int Size { get; private set; }

        public bool Contains(TKey key)
        {
            return Get(key).Equals(default(TValue));
        }

        public void Delete(TKey key)
        {
            BinaryTreeNode<KeyValuePair<TKey, TValue>> parent = null;
            var current = root;
            while (current != null)
            {
                var comparisonResult = current.Value.Key.CompareTo(key);
                if (comparisonResult < 0)
                {
                    parent = current;
                    current = current.RightChild;
                }
                else if (comparisonResult > 0)
                {
                    parent = current;
                    current = current.LeftChild;
                }
                else
                {
                    if (current.LeftChild == null)
                    {
                        FixParentLink(parent, current, current.RightChild);
                    }
                    else if (current.LeftChild.RightChild == null)
                    {
                        current.LeftChild.RightChild = current.RightChild;
                        FixParentLink(parent, current, current.LeftChild);
                    }
                    else
                    {
                        var maxParent = current.LeftChild;
                        var max = maxParent.RightChild;
                        while (max.RightChild != null)
                        {
                            maxParent = max;
                            max = max.RightChild;
                        }

                        maxParent.RightChild = null;
                        max.LeftChild = current.LeftChild;
                        max.RightChild = current.RightChild;
                        FixParentLink(parent, current, max);
                    }

                    Size--;
                }
            }
        }

        public TValue Get(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Get(root, key);
        }

        public void Put(TKey key, TValue value)
        {
            if (root == null)
            {
                root = new BinaryTreeNode<KeyValuePair<TKey, TValue>>(new KeyValuePair<TKey, TValue>(key, value));
                Size++;
            }
            else
            {
                Put(root, key, value);
            }
        }

        private void FixParentLink(BinaryTreeNode<KeyValuePair<TKey, TValue>> parent,
            BinaryTreeNode<KeyValuePair<TKey, TValue>> oldChild,
            BinaryTreeNode<KeyValuePair<TKey, TValue>> newChild)
        {
            if (parent == null)
            {
                root = newChild;
            }
            else if (parent.LeftChild == oldChild)
            {
                parent.LeftChild = newChild;
            }
            else
            {
                parent.RightChild = newChild;
            }
        }

        private TValue Get(BinaryTreeNode<KeyValuePair<TKey, TValue>> node, TKey key)
        {
            if (node == null)
            {
                return default(TValue);
            }

            var comparisonResult = node.Value.Key.CompareTo(key);
            if (comparisonResult == 0)
            {
                return node.Value.Value;
            }
            else if (comparisonResult < 0)
            {
                return Get(node.RightChild, key);
            }
            else
            {
                return Get(node.LeftChild, key);
            }
        }

        private void Put(BinaryTreeNode<KeyValuePair<TKey, TValue>> node, TKey key, TValue value)
        {
            var comparisonResult = node.Value.Key.CompareTo(key);
            if (comparisonResult == 0)
            {
                node.Value = new KeyValuePair<TKey, TValue>(key, value);
            }
            else if (comparisonResult < 0)
            {
                if (node.RightChild == null)
                {
                    node.RightChild = new BinaryTreeNode<KeyValuePair<TKey, TValue>>(
                        new KeyValuePair<TKey, TValue>(key, value));
                    Size++;
                }
                else
                {
                    Put(node.RightChild, key, value);
                }
            }
            else
            {
                if (node.LeftChild == null)
                {
                    node.LeftChild = new BinaryTreeNode<KeyValuePair<TKey, TValue>>(
                        new KeyValuePair<TKey, TValue>(key, value));
                    Size++;
                }
                else
                {
                    Put(node.LeftChild, key, value);
                }
            }
        }
    }
}
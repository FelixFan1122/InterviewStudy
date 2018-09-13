using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewStudy.Trie
{
    public class TernarySearchTrie<T>
    {
        private const char Wildcard = '.';

        private TernaryTreeNode<char, T> root;

        public bool IsEmpty
        {
            get
            {
                return Size == 0;
            }
        }

        public IEnumerable<string> Keys
        {
            get
            {
                return GetKeys(root);
            }
        }
        public int Size { get; private set; }

        public bool Contains(string key)
        {
            return !(Get(key).Equals(default(T)));
        }

        public void Delete(string key)
        {
            var current = root;
            var index = 0;
            var previousIndex = 0;
            TernaryTreeNode<char, T> previousKey = root;
            while (current != null && index < key.Length)
            {
                if (!current.Value.Equals(default(T)))
                {
                    previousIndex = index;
                    previousKey = current;
                }

                var comparisonResult = key[index].CompareTo(current.Key);
                if (comparisonResult < 0)
                {
                    current = current.LeftChild;
                }
                else if (comparisonResult > 0)
                {
                    current = current.RightChild;
                }
                else
                {
                    if (index == key.Length - 1)
                    {
                        if (!current.Value.Equals(default(T)))
                        {
                            Size--;
                            if (current.LeftChild == null && current.MiddleChild == null && current.RightChild == null)
                            {
                                if (key[previousIndex].CompareTo(previousKey.Key) < 0)
                                {
                                    previousKey.LeftChild = null;
                                }
                                else if (key[previousIndex].CompareTo(previousKey.Key) > 0)
                                {
                                    previousKey.RightChild = null;
                                }
                                else
                                {
                                    previousKey.MiddleChild = null;
                                }
                            }
                            else
                            {
                                current.Value = default(T);
                            }
                        }
                    }
                    else
                    {
                        current = current.MiddleChild;
                        index++;
                    }
                }
            }
        }

        public T Get(string key)
        {
            var node = root;
            var index = 0;
            while (node != null && index < key.Length)
            {
                var comparisonResult = key[index].CompareTo(node.Key);
                if (comparisonResult < 0)
                {
                    node = node.LeftChild;
                }
                else if (comparisonResult > 0)
                {
                    node = node.RightChild;
                }
                else
                {
                    if (index == key.Length - 1)
                    {
                        return node.Value;
                    }
                    else
                    {
                        node = node.MiddleChild;
                        index++;
                    }
                }
            }

            return default(T);
        }

        public IEnumerable<string> Match(string pattern)
        {   
            var queue = new Queue<KeyCollectionEntry>();
            queue.Enqueue(new KeyCollectionEntry { Index = 0, Key = new StringBuilder(), Node = root });
            while (queue.Count > 0)
            {
                var entry = queue.Dequeue();
                var index = entry.Index;
                var node = entry.Node;
                var key = entry.Key;
                while (node != null && index < pattern.Length)
                {
                    if (pattern[index] == Wildcard)
                    {   
                        if (node.LeftChild != null)
                        {
                            queue.Enqueue(new KeyCollectionEntry
                            {
                                Index = index + 1,
                                Key = new StringBuilder(key.ToString()).Append(node.LeftChild.Key),
                                Node = node.LeftChild.MiddleChild
                            });
                        }

                        if (node.RightChild != null)
                        {
                            queue.Enqueue(new KeyCollectionEntry
                            {
                                Index = index + 1,
                                Key = new StringBuilder(key.ToString()).Append(node.RightChild.Key),
                                Node = node.RightChild.MiddleChild
                            });
                        }

                        key.Append(node.Key);
                        if (index == pattern.Length - 1)
                        {
                            yield return key.ToString();
                        }
                        else
                        {
                            index++;
                            node = node.MiddleChild;
                        }
                    }
                    else
                    {
                        var comparisonResult = pattern[index].CompareTo(node.Key);
                        if (comparisonResult < 0)
                        {
                            node = node.LeftChild;
                        }
                        else if (comparisonResult > 0)
                        {
                            node = node.RightChild;
                        }
                        else
                        {
                            key.Append(node.Key);
                            if (index == pattern.Length - 1)
                            {
                                yield return key.ToString();
                            }
                            else
                            {
                                index++;
                                node = node.MiddleChild;
                            }
                        }
                    }
                }
            }
        }

        public IEnumerable<string> MatchPrefix(string prefix)
        {
            var node = root;
            var index = 0;
            while (node != null && index < prefix.Length)
            {
                var comparisonResult = prefix[index].CompareTo(node.Key);
                if (comparisonResult < 0)
                {
                    node = node.LeftChild;
                }
                else if (comparisonResult > 0)
                {
                    node = node.RightChild;
                }
                else
                {
                    if (index == prefix.Length - 1)
                    {
                        return GetKeys(node);
                    }
                    else
                    {
                        node = node.MiddleChild;
                        index++;
                    }
                }
            }

            return new string[0];
        }

        public string MatchStart(string s)
        {
            var node = root;
            var index = 0;
            var sb = new StringBuilder();
            while (node != null && index < s.Length)
            {
                var comparisonResult = s[index].CompareTo(node.Key);
                if (comparisonResult < 0)
                {
                    node = node.LeftChild;
                }
                else if (comparisonResult > 0)
                {
                    node = node.RightChild;
                }
                else
                {
                    node = node.MiddleChild;
                    sb.Append(node.Key);
                    index++;
                }
            }

            return sb.ToString();
        }

        public void Put(string key, T value)
        {
            root = Put(key, value, 0, root);
        }

        private IEnumerable<string> GetKeys(TernaryTreeNode<char, T> node)
        {
            if (node == null)
            {
                throw new InvalidOperationException();
            }
            
            var queue = new Queue<Tuple<TernaryTreeNode<char, T>, StringBuilder>>();
                queue.Enqueue(new Tuple<TernaryTreeNode<char, T>, StringBuilder>(node, 
                    new StringBuilder(node.Key.ToString())));
            while (queue.Count > 0)
            {
                var item = queue.Dequeue();
                var current = item.Item1;
                var key = item.Item2;
                if (!current.Value.Equals(default(T)))
                {
                    yield return key.ToString();
                }

                if (current.LeftChild != null)
                {
                    queue.Enqueue(new Tuple<TernaryTreeNode<char, T>, StringBuilder>(current.LeftChild, 
                        new StringBuilder(key.ToString())));
                }

                if (current.MiddleChild != null)
                {
                    queue.Enqueue(new Tuple<TernaryTreeNode<char, T>, StringBuilder>(current.MiddleChild, 
                        new StringBuilder(key.Append(current.MiddleChild.Key).ToString())));
                }

                if (current.RightChild != null)
                {
                    queue.Enqueue(new Tuple<TernaryTreeNode<char, T>, StringBuilder>(current.RightChild, 
                        new StringBuilder(key.ToString())));
                }
            }
        }

        private TernaryTreeNode<char, T> Put(string key, T value, int index, TernaryTreeNode<char, T> node)
        {
            if (node == null)
            {
                node = new TernaryTreeNode<char, T>(key[index]);
            }

            var comparisonResult = key[index].CompareTo(node.Key);
            if (comparisonResult < 0)
            {
                node.LeftChild = Put(key, value, index, node.LeftChild);
            }
            else if (comparisonResult > 0)
            {
                node.RightChild = Put(key, value, index, node.RightChild);
            }
            else
            {
                if (index == key.Length - 1)
                {
                    node.Value = value;
                    Size++;
                }
                else
                {
                    node.MiddleChild = Put(key, value, index + 1, node.MiddleChild);
                }
            }

            return node;
        }

        private class KeyCollectionEntry
        {
            internal int Index { get; set; }
            internal StringBuilder Key { get; set; }
            internal TernaryTreeNode<char, T> Node { get; set; }
        }
    }
}
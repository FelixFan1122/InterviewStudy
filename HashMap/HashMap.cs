using System.Collections.Generic;

namespace InterviewStudy.HashMap
{
    /// TODO: Implement auto grow and shrink.
    public class HashMap<TKey, TValue>
    {
        private const int Capacity = 100;

        private InterviewStudy.LinkedList.LinkedList<KeyValuePair<TKey，TValue>>[] storage;

        public HashMap() : this(Capacity)
        {

        }

        public HashMap(int capacity)
        {
            storage = new InterviewStudy.LinkedList.LinkedList<KeyValuePair<TKey, TValue>>[capacity];
        }

        public IEnumerable<TKey> Keys {
            get
            {
                foreach (var chain in storage)
                {
                    for (var i = 0; i < chain.Size; i++)
                    {
                        yield return chain.Get(i).Key;
                    }
                }
            }
        }

        public int Size { get; private set; }

        public bool Contains(TKey key)
        {
            try
            {
                Get(key);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        public void Delete(TKey key)
        {
            var chain = storage[key.GetHashCode() % storage.Length];
            if (chain != null && chain.Size > 0)
            {
                for (var i = 0; i < chain.Size; i++)
                {
                    if (chain.Get(i).Key.Equals(key))
                    {
                        chain.Remove(i);
                        Size--;
                        return;
                    }
                }
            }
        }

        public TValue Get(TKey key)
        {
            var chain = storage[key.GetHashCode() % storage.Length];
            if (chain != null && chain.Size > 0)
            {
                for (var i = 0; i < chain.Size; i++)
                {
                    if (chain.Get(i).Key.Equals(key))
                    {
                        return chain.Get(i).Value;
                    }
                }
            }

            throw new KeyNotFoundException();
        }

        public void Put(TKey key, TValue value)
        {
            Delete(key);
            var chain = storage[key.GetHashCode() % storage.Length];
            if (chain == null)
            {
                chain = new InterviewStudy.LinkedList.LinkedList<KeyValuePair<TKey, TValue>>();
            }
            
            chain.Insert(new KeyValuePair<TKey, TValue>(key, value), 0);
            Size++;
        }
    }
}
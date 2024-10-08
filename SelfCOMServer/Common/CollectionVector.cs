using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SelfCOMServer.Common
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly struct CollectionVector<T>(ICollection<T> values) : IList<T>
    {
        T IList<T>.this[int index]
        {
            get => values.ElementAt(index);
            set => throw new NotImplementedException();
        }

        public int Count => values.Count;

        public bool IsReadOnly => values.IsReadOnly;

        public void Add(T item) => values.Add(item);

        public void Clear() => values.Clear();

        public bool Contains(T item) => values.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => values.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => values.GetEnumerator();

        int IList<T>.IndexOf(T item) => throw new NotImplementedException();

        void IList<T>.Insert(int index, T item) => throw new NotImplementedException();

        public bool Remove(T item) => values.Remove(item);

        void IList<T>.RemoveAt(int index) => throw new NotImplementedException();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)values).GetEnumerator();
    }
}

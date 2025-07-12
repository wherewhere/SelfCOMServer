using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using WinRTWrapper.CodeAnalysis;

namespace SelfCOMServer.Common
{
    /// <summary>
    /// A wrapper for <see cref="ICollection{T}"/> that implements <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="target">The underlying collection to wrap.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [WinRTWrapperMarshaller(typeof(ICollection<>), typeof(IList<>))]
    public readonly struct CollectionVector<T>(params ICollection<T> target) : IList<T>
    {
        T IList<T>.this[int index]
        {
            get => target.ElementAt(index);
            set => throw new NotImplementedException();
        }

        public int Count => target.Count;

        public bool IsReadOnly => target.IsReadOnly;

        public void Add(T item) => target.Add(item);

        public void Clear() => target.Clear();

        public bool Contains(T item) => target.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => target.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => target.GetEnumerator();

        int IList<T>.IndexOf(T item) => throw new NotImplementedException();

        void IList<T>.Insert(int index, T item) => throw new NotImplementedException();

        public bool Remove(T item) => target.Remove(item);

        void IList<T>.RemoveAt(int index) => throw new NotImplementedException();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)target).GetEnumerator();

        /// <summary>
        /// Converts a managed type <see cref="ICollection{T}"/> to a wrapper type <see cref="CollectionVector{T}"/>.
        /// </summary>
        /// <param name="managed">The managed type to convert.</param>
        /// <returns>The converted wrapper type.</returns>
        public static CollectionVector<T> ConvertToWrapper(params ICollection<T> managed) => new(managed);
    }
}

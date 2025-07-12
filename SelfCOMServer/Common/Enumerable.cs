using System.Collections.Generic;

namespace SelfCOMServer.Common
{
    public static class Enumerable
    {
        /// <summary>
        /// Get the <see cref="CollectionVector{TSource}"/> of <see cref="ICollection{TSource}"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="ICollection{TSource}"/> to be redden.</param>
        /// <returns>The <see cref="CollectionVector{TSource}"/> of <see cref="ICollection{TSource}"/>.</returns>
        public static CollectionVector<TSource> AsVector<TSource>(this ICollection<TSource> source) => new(source);
    }
}

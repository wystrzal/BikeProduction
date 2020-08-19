using System;

namespace BikeSortFilter
{
    /// <summary>
    /// Interface for retrieving the sorting instruction.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TKey">The type of the returned key.</typeparam>
    public interface IConcreteSort<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// Get concrete sort needed to search data.
        /// </summary>   
        /// <returns></returns>
        Func<TEntity, TKey> GetConcreteSort();
    }
}

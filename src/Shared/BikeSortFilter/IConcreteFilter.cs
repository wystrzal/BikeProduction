using System;

namespace BikeSortFilter
{
    /// <summary>
    /// <para>Interface for retrieving the filtering instruction.</para>
    /// <para>Important: The inheriting class must have a constructor
    /// with a class that has the data to filter.</para>
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IConcreteFilter<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get concrete filter needed to search data.
        /// </summary>
        /// <returns></returns>
        Predicate<TEntity> GetConcreteFilter();
    }
}

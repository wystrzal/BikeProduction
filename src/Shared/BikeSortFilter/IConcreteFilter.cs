using System;
using System.Collections.Generic;
using System.Text;

namespace BikeSortFilter
{
    /// <summary>
    /// Interface for retrieving the filtering instruction.
    /// Important: The inheriting class must have a constructor
    /// with a class that has the data to filter.
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

using System;
using System.Collections.Generic;
using System.Text;

namespace BikeSortFilter
{
    /// <summary>
    /// Interface for retrieving the filtering instruction
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

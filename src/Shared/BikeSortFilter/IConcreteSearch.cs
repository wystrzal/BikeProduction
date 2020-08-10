using System;
using System.Collections.Generic;
using System.Text;

namespace BikeSortFilter
{
    public interface IConcreteSearch<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get concrete filter needed to serach data.
        /// </summary>
        /// <param name="filterValue">The value on which the filter search for data.</param>
        /// <returns></returns>
        Predicate<TEntity> GetConcreteFilter<T>(T filterValue);
        /// <summary>
        /// Get concrete filter needed to search data.
        /// </summary>
        /// <returns></returns>
        Func<TEntity, bool> GetConcreteSort();
    }
}

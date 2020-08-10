using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static BikeBaseRepository.OrderByTypeEnum;

namespace BikeSortFilter
{
    public interface ISortFilterService<TEntity> where TEntity : class
    {
        /// <summary>
        /// Set filter to search data.
        /// </summary>
        /// <param name="typeOfFilter">Type of filter to set (typeof(Type)).</param>
        /// <param name="filterValue">Value used for filtering.</param>
        void SetConcreteFilter<T, TValue>(T typeOfFilter, TValue filterValue) where T : class where TValue : class;
        /// <summary>
        /// Set sort to search data.
        /// </summary>
        /// <param name="typeOfSort">Type of sort to set (typeof(Type)).</param>
        void SetConcreteSort<T>(T typeOfSort) where T : class;
        /// <summary>
        /// Search for data based on set filters and sort.
        /// </summary>
        /// <param name="orderByType">Set type of order (ACS or DESC).</param>
        /// <param name="skip">Amount of items to skip.</param>
        /// <param name="take">Amount of items to take.</param>
        /// <returns></returns>
        Task<List<TEntity>> Search(OrderByType orderByType, int skip = 0, int take = 0);
    }
}

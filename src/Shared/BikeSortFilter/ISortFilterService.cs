using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeSortFilter
{
    public interface ISortFilterService<TEntity> where TEntity : class
    {
        /// <summary>
        /// Set filter to search data.
        /// </summary>
        /// <param name="typeOfFilter">The type of the filter (typeof(Filter)).</param>
        void SetConcreteFilter<TFilter>(TFilter typeOfFilter) where TFilter : class;
        /// <summary>
        /// Set sort to search data.
        /// </summary>
        /// <typeparam name="TSort">The type of the sort (typeof(Sort)).</typeparam>
        /// <typeparam name="TKey">The type of the returned key.</typeparam>
        /// <param name="typeOfSort">The type of the sort (typeof(Sort)).</param>
        void SetConcreteSort<TSort, TKey>(TSort typeOfSort) where TSort : class;
        /// <summary>
        /// Search for data based on set filters and sort.
        /// </summary>
        /// <param name="orderDesc">Choose whether to sort in descending order.</param>
        /// <param name="skip">Amount of items to skip.</param>
        /// <param name="take">Amount of items to take.</param>
        /// <returns></returns>
        Task<List<TEntity>> Search(bool orderDesc, int skip = 0, int take = 0);
    }
}

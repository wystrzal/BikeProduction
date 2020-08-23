using System.Collections.Generic;
using System.Threading.Tasks;

namespace BikeSortFilter
{
    /// <summary>
    /// <para>Interface to search for sorted and filtered data.</para>
    /// <para>Important: You must set concrete sort
    /// before start search data.</para>
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TFilteringData">The type of the data to filter.</typeparam>
    public interface ISortFilterService<TEntity, TFilteringData>
        where TEntity : class
        where TFilteringData : class
    {
        /// <summary>
        /// Set filter to search data.
        /// </summary>
        /// <typeparam name="TFilter">The type of the filter.</typeparam>
        /// <param name="filteringData">Data used for filtering.</param>
        void SetConcreteFilter<TFilter>(TFilteringData filteringData) where TFilter : class;
        /// <summary>
        /// Set sort to search data.
        /// </summary>
        /// <typeparam name="TSort">The type of the sort.</typeparam>
        /// <typeparam name="TKey">The type of the returned key.</typeparam>
        void SetConcreteSort<TSort, TKey>() where TSort : class;
        /// <summary>
        /// Search for data based on set filters and sort.
        /// </summary>
        /// <param name="orderDesc">Choose whether to sort in descending order.</param>
        /// <param name="skip">Amount of items to skip.</param>
        /// <param name="take">Amount of items to take.</param>
        /// <returns></returns>
        Task<List<TEntity>> Search(bool orderDesc, int skip = 0, int take = 6);
    }
}

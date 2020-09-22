using System.Collections.Generic;
using System.Threading.Tasks;

namespace BikeSortFilter
{
    /// <summary>
    /// <para>Interface to search for sorted and filtered data.</para>
    /// <para>Important: You must set concrete sort
    /// before start search data.</para>
    /// </summary>
    public interface ISearchSortFilterData<TEntity, TFilteringData>
        where TEntity : class
        where TFilteringData : class
    {
        void SetConcreteFilter<TFilter>(TFilteringData filteringData) where TFilter : class;
        void SetConcreteSort<TSort, TKey>() where TSort : class;
        Task<List<TEntity>> Search(bool orderDesc, int skip = 0, int take = 0);
    }
}

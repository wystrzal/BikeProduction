using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BikeSortFilter
{
    public interface ISearchSortFilterService<TEntity, TFilteringData>
        where TEntity : class
        where TFilteringData : class
    {
        void SetConcreteFilter(Predicate<TEntity> predicate);
        void SetConcreteSort<TKey>(Func<TEntity, TKey> func);
        Task<List<TEntity>> Search(bool orderDesc = false, int skip = 0, int take = 0);
    }
}

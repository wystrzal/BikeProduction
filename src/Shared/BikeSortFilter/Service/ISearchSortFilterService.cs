using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BikeSortFilter
{
    public interface ISearchSortFilterService<TEntity, TFilteringData>
        where TEntity : class
        where TFilteringData : class
    {
        void SetConcreteFilter(Expression<Func<TEntity, bool>> predicate);
        void SetConcreteSort<TKey>(Expression<Func<TEntity, TKey>> func);
        Task<List<TEntity>> Search(bool orderDesc = false, int skip = 0, int take = 0);
    }
}

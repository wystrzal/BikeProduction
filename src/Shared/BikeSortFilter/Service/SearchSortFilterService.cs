using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BikeSortFilter
{
    public class SearchSortFilterService<TEntity, TFilteringData> : ISearchSortFilterService<TEntity, TFilteringData>
        where TEntity : class
        where TFilteringData : class
    {
        private readonly ISortFilterRepository<TEntity> repository;
        private readonly List<Expression<Func<TEntity, bool>>> filtersToUse;
        private dynamic sortToUse;

        public SearchSortFilterService(ISortFilterRepository<TEntity> repository)
        {
            filtersToUse = new List<Expression<Func<TEntity, bool>>>();
            this.repository = repository;
        }

        public void SetConcreteFilter(Expression<Func<TEntity, bool>> predicate)
        {
            filtersToUse.Add(predicate);
        }

        public void SetConcreteSort<TKey>(Expression<Func<TEntity, TKey>> func)
        {
            sortToUse = func;
        }

        public async Task<List<TEntity>> Search(bool orderDesc = false, int skip = 0, int take = 0)
        {
            var data = sortToUse != null ? await repository.GetSortedFilteredData(filtersToUse, orderDesc, skip, take, sortToUse)
                : await repository.GetSortedFilteredData<dynamic>(filtersToUse, orderDesc, skip, take);
   
            sortToUse = null;
            filtersToUse.Clear();

            return data;
        }
    }
}
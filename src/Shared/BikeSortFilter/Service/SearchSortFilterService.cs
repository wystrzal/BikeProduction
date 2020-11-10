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
        private readonly List<Predicate<TEntity>> filtersToUse;
        private dynamic sortToUse;

        public SearchSortFilterService(ISortFilterRepository<TEntity> repository)
        {
            filtersToUse = new List<Predicate<TEntity>>();
            this.repository = repository;
        }

        public void SetConcreteFilter(Predicate<TEntity> predicate)
        {
            filtersToUse.Add(predicate);
        }

        public void SetConcreteSort<TKey>(Func<TEntity, TKey> func)
        {
            sortToUse = func;
        }

        public async Task<List<TEntity>> Search(bool orderDesc = false, int skip = 0, int take = 0)
        {
            dynamic data;

            Expression<Func<TEntity, bool>> expression = x => filtersToUse.All(all => all(x));
            var compiledFilters = expression.Compile();

            if (sortToUse == null)
            {
                data = await repository.GetFilteredData(compiledFilters, skip, take);
            }
            else
            {
                data = await repository.GetSortedFilteredData(compiledFilters, sortToUse, orderDesc, skip, take);
            }

            sortToUse = null;
            filtersToUse.Clear();

            return data;
        }
    }
}

using BikeBaseRepository;
using System;
using System.Collections;
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

        public void SetConcreteFilter<TFilter>(TFilteringData filteringData)
            where TFilter : class
        {
            var typeOfFilter = typeof(TFilter);

            var concreteFilters = new Hashtable();

            if (!concreteFilters.ContainsKey(typeOfFilter))
            {
                var concreteFilter = Activator.CreateInstance(typeOfFilter as Type, filteringData);

                concreteFilters.Add(typeOfFilter, concreteFilter);
            }

            var selectedFilter = concreteFilters[typeOfFilter] as ConcreteFilter<TEntity, TFilteringData>;

            filtersToUse.Add(selectedFilter.GetFilteringCondition());
        }

        public void SetConcreteSort<TSort, TKey>() where TSort : class
        {
            var typeOfSort = typeof(TSort);

            var sort = Activator.CreateInstance(typeOfSort as Type) as IConcreteSort<TEntity, TKey>;

            sortToUse = sort.GetSortCondition();
        }

        public async Task<List<TEntity>> Search(bool orderDesc, int skip = 0, int take = 0)
        {
            dynamic data;

            Expression<Func<TEntity, bool>> expression = x => filtersToUse.All(all => all(x));

            var compiledFilterBy = expression.Compile();

            if (sortToUse == null)
                data = await repository.GetFilteredData(compiledFilterBy, skip, take);
            else
                data = await repository.GetSortedFilteredData(compiledFilterBy, sortToUse, orderDesc, skip, take);
            
            sortToUse = null;
            filtersToUse.Clear();

            return data;
        }
    }
}

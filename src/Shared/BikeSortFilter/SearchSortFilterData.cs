using BikeBaseRepository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BikeSortFilter
{
    public class SearchSortFilterData<TEntity, TFilteringData> : ISearchSortFilterData<TEntity, TFilteringData>
        where TEntity : class
        where TFilteringData : class
    {
        private readonly IBaseRepository<TEntity> repository;
        private readonly List<Predicate<TEntity>> filtersToUse;
        private dynamic sortToUse;

        public SearchSortFilterData(IBaseRepository<TEntity> repository)
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

            var selectedFilter = concreteFilters[typeOfFilter] as IConcreteFilter<TEntity>;

            filtersToUse.Add(selectedFilter.GetConcreteFilter());
        }

        public void SetConcreteSort<TSort, TKey>() where TSort : class
        {
            var typeOfSort = typeof(TSort);

            var sort = Activator.CreateInstance(typeOfSort as Type) as IConcreteSort<TEntity, TKey>;

            sortToUse = sort.GetSortCondition();
        }

        public async Task<List<TEntity>> Search(bool orderDesc, int skip = 0, int take = 0)
        {
            Expression<Func<TEntity, bool>> expression = x => filtersToUse.All(all => all(x));

            var compiledFilterBy = expression.Compile();

            var data = await repository.GetFilterSortData(compiledFilterBy, sortToUse, orderDesc, skip, take);
            
            sortToUse = null;
            filtersToUse.Clear();

            return data;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static BikeBaseRepository.OrderByTypeEnum;

namespace BikeSortFilter
{
    public class SortFilterService<TEntity> : ISortFilterService<TEntity> where TEntity : class
    {
        private readonly ISearchService<TEntity> searchService;
        private readonly List<Predicate<TEntity>> predicates;

        public SortFilterService(ISearchService<TEntity> searchService)
        {
            predicates = new List<Predicate<TEntity>>();
            this.searchService = searchService;
        }

        public void SetConcreteFilter<T>(T typeOfFilter) where T : class
        {
            var filters = new Hashtable();

            if (!filters.ContainsKey(typeOfFilter))
            {
                var filter = Activator.CreateInstance(typeOfFilter as Type);

                filters.Add(typeOfFilter, filter);
            }

            var selectedFilter = filters[typeOfFilter] as IConcreteSearch<TEntity>;

            predicates.Add(selectedFilter.getConcreteFilter());
        }

        public async Task<List<TEntity>> SearchFiltered()
        {
            Expression<Func<TEntity, bool>> expression = x => predicates.All(all => all(x));

            var data = await searchService.Filter(expression);

            predicates.Clear();

            return data;
        }

        public async Task<List<TEntity>> SearchSorted<T>(T typeOfSort, OrderByType orderByType)
        {
            var sort = Activator.CreateInstance(typeOfSort as Type) as IConcreteSearch<TEntity>;

            var data = await searchService.Sort(sort.getConcreteSort(), orderByType);

            return data;
        }
    }
}

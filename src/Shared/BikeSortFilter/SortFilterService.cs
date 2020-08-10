using BikeBaseRepository;
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
        private readonly IBaseRepository<TEntity> repository;
        private readonly List<Predicate<TEntity>> filtersToUse;
        private Func<TEntity, bool> sortToUse;

        public SortFilterService(IBaseRepository<TEntity> repository)
        {
            filtersToUse = new List<Predicate<TEntity>>();
            this.repository = repository;
        }

        public void SetConcreteFilter<T, TValue>(T typeOfFilter, TValue filterValue) where T : class where TValue : class
        {
            var concreteFilters = new Hashtable();

            if (!concreteFilters.ContainsKey(typeOfFilter))
            {
                var concreteFilter = Activator.CreateInstance(typeOfFilter as Type);

                concreteFilters.Add(typeOfFilter, concreteFilter);
            }

            var selectedFilter = concreteFilters[typeOfFilter] as IConcreteSearch<TEntity>;

            filtersToUse.Add(selectedFilter.GetConcreteFilter(filterValue));
        }

        public void SetConcreteSort<T>(T typeOfSort) where T : class
        {
            var sort = Activator.CreateInstance(typeOfSort as Type) as IConcreteSearch<TEntity>;

            sortToUse = sort.GetConcreteSort();
        }

        public async Task<List<TEntity>> Search(OrderByType orderByType, int skip = 0, int take = 0)
        {
            Expression<Func<TEntity, bool>> expression = x => filtersToUse.All(all => all(x));

           var compiledFilterBy = expression.Compile();

            var data = await repository.FilterSortData(compiledFilterBy, sortToUse, orderByType, skip, take);

            return data;
        }
    }
}

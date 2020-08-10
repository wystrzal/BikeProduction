using BikeBaseRepository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static BikeBaseRepository.OrderByTypeEnum;

namespace BikeSortFilter
{
    public class SearchService<TEntity> : ISearchService<TEntity> where TEntity : class
    {
        private readonly IBaseRepository<TEntity> repository;

        public SearchService(IBaseRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        public async Task<List<TEntity>> Filter(Expression<Func<TEntity, bool>> filterBy, int skip = 0, int take = 0)
        {
            //Compile expression from given parameters.
            var filterByValue = filterBy.Compile();

            var filteredData = await repository.GetFilteredData(filterByValue, skip, take);

            return filteredData;
        }

        public async Task<List<TEntity>> Sort(Func<TEntity, bool> sortBy, OrderByType orderByType, int skip = 0, int take = 0)
        {
            var sortedData = await repository.GetSortedData(sortBy, orderByType, skip, take);

            return sortedData;
        }
    }
}

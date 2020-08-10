using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static BikeBaseRepository.OrderByTypeEnum;

namespace BikeSortFilter
{
    public interface ISearchService<TEntity> where TEntity : class
    {
        Task<List<TEntity>> Filter(Expression<Func<TEntity, bool>> filterBy, int skip = 0, int take = 0);
        Task<List<TEntity>> Sort(Func<TEntity, bool> sortBy, OrderByType orderByType, int skip = 0, int take = 0);
    }
}

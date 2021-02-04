using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BikeSortFilter
{
    public interface ISortFilterRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetSortedFilteredData<TReturned>(List<Expression<Func<TEntity, bool>>> filterBy,
            bool orderDesc, int skip, int take, Expression<Func<TEntity, TReturned>> sortBy = null);
    }
}

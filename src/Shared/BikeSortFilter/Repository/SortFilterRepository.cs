using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BikeSortFilter
{
    public class SortFilterRepository<TEntity, TDataContext> : ISortFilterRepository<TEntity>
        where TEntity : class
        where TDataContext : DbContext
    {
        private readonly TDataContext dataContext;
        public SortFilterRepository(TDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<List<TEntity>> GetSortedFilteredData<TReturned>(List<Expression<Func<TEntity, bool>>> filterBy,
            bool orderDesc, int skip, int take, Expression<Func<TEntity, TReturned>> sortBy = null)
        {
            var query = dataContext.Set<TEntity>().AsNoTracking();

            foreach (var filter in filterBy)
            {
                query = query.Where(filter);
            }

            if (sortBy != null)
            {
                query = SetSorting(sortBy, orderDesc, query);
            }

            if (take == 0)
            {
                return await query.ToListAsync();
            }

            return await query.Skip(skip).Take(take).ToListAsync();
        }

        private IQueryable<TEntity> SetSorting<TKey>(Expression<Func<TEntity, TKey>> sortBy, bool orderDesc, IQueryable<TEntity> query)
        {
            if (!orderDesc)
            {
                query = query.OrderBy(sortBy);
            }
            else
            {
                query = query.OrderByDescending(sortBy);
            }

            return query;
        }
    }
}
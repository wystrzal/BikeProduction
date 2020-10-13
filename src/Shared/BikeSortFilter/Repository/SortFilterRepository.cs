using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private async Task<List<TEntity>> GetAllSortedFilteredData<TKey>(Func<TEntity, bool> filterBy, Func<TEntity, TKey> sortBy,
            bool orderDesc)
        {
            if (!orderDesc)
            {
                return await dataContext.Set<TEntity>().AsNoTracking().Where(filterBy).OrderBy(sortBy)
                    .AsQueryable().ToListAsync();
            }

            return await dataContext.Set<TEntity>().AsNoTracking().Where(filterBy).OrderByDescending(sortBy)
                .AsQueryable().ToListAsync();
        }

        public async Task<List<TEntity>> GetSortedFilteredData<TKey>(Func<TEntity, bool> filterBy, Func<TEntity, TKey> sortBy,
            bool orderDesc, int skip, int take)
        {
            if (take == 0)
            {
                return await GetAllSortedFilteredData(filterBy, sortBy, orderDesc);
            }

            if (!orderDesc)
            {
                return await dataContext.Set<TEntity>().AsNoTracking().Where(filterBy).OrderBy(sortBy).Skip(skip).Take(take)
                    .AsQueryable().ToListAsync();
            }

            return await dataContext.Set<TEntity>().AsNoTracking().Where(filterBy).OrderByDescending(sortBy).Skip(skip).Take(take)
                .AsQueryable().ToListAsync();   
        }

        public async Task<List<TEntity>> GetFilteredData(Func<TEntity, bool> filterBy, int skip, int take)
        {
            if (take == 0)
            {
                return await dataContext.Set<TEntity>().AsNoTracking().Where(filterBy).AsQueryable().ToListAsync();
            }

            return await dataContext.Set<TEntity>().AsNoTracking().Where(filterBy).Skip(skip).Take(take).AsQueryable().ToListAsync();
        }
    }
}

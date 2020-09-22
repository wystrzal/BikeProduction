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

        private async Task<List<TEntity>> GetAllSortFilterData<TKey>(Func<TEntity, bool> filterBy, Func<TEntity, TKey> sortBy,
            bool orderDesc)
        {
            List<TEntity> data = null;

            if (!orderDesc)
            {
                data = dataContext.Set<TEntity>().AsNoTracking().Where(filterBy).OrderBy(sortBy).ToList();
            }
            else
            {
                data = dataContext.Set<TEntity>().AsNoTracking().Where(filterBy).OrderByDescending(sortBy).ToList();
            }

            return await Task.FromResult(data);
        }

        public async Task<List<TEntity>> GetSortFilterData<TKey>(Func<TEntity, bool> filterBy, Func<TEntity, TKey> sortBy,
            bool orderDesc, int skip, int take)
        {
            List<TEntity> data = null;

            if (take == 0)
            {
                return await GetAllSortFilterData(filterBy, sortBy, orderDesc);
            }

            if (!orderDesc)
            {
                data = dataContext.Set<TEntity>().AsNoTracking().Where(filterBy)
                    .OrderBy(sortBy).Skip(skip).Take(take).ToList();
            }
            else
            {
                data = dataContext.Set<TEntity>().AsNoTracking().Where(filterBy)
                    .OrderByDescending(sortBy).Skip(skip).Take(take).ToList();
            }

            return await Task.FromResult(data);
        }
    }
}

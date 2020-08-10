using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BikeBaseRepository
{
    public class BaseRepository<TEntity, DataContext> : IBaseRepository<TEntity> where TEntity : class where DataContext : DbContext
    {
        private readonly DataContext dataContext;

        public BaseRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void Add(TEntity entity)
        {
            dataContext.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            dataContext.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            dataContext.Set<TEntity>().Update(entity);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await dataContext.Set<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> GetByConditionToList(Func<TEntity, bool> func)
        {
            var data = dataContext.Set<TEntity>().Where(func).ToList();
            return await Task.FromResult(data);
        }

        public async Task<TEntity> GetByConditionFirst(Func<TEntity, bool> func)
        {
            var data = dataContext.Set<TEntity>().Where(func).FirstOrDefault();
            return await Task.FromResult(data);
        }

        public async Task<TEntity> GetById(int id)
        {
            return await dataContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetByConditionWithIncludeFirst<TProp>(Func<TEntity, bool> where, Expression<Func<TEntity, TProp>> include)
        {
            var data = dataContext.Set<TEntity>().Include(include).Where(where).FirstOrDefault();
            return await Task.FromResult(data);
        }

        public async Task<List<TEntity>> GetByConditionWithIncludeToList<TProp>(Func<TEntity, bool> where, Expression<Func<TEntity, TProp>> include)
        {
            var data = dataContext.Set<TEntity>().Include(include).Where(where).ToList();
            return await Task.FromResult(data);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await dataContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<List<TEntity>> FilterSortData<TKey>(Func<TEntity, bool> filterBy, Func<TEntity, TKey> sortBy,
            bool orderDesc, int skip, int take)
        {
            List<TEntity> data = null;

            if (take == 0)
            {
                if (!orderDesc)
                {
                    data = dataContext.Set<TEntity>().AsNoTracking().Where(filterBy).OrderBy(sortBy).ToList();
                }
                else
                {
                    data = dataContext.Set<TEntity>().AsNoTracking().Where(filterBy).OrderByDescending(sortBy).ToList();
                }
            }
            else
            {
                if (!orderDesc)
                {
                    data = dataContext.Set<TEntity>().AsNoTracking().Where(filterBy).OrderBy(sortBy).Skip(skip).Take(take).ToList();
                }
                else
                {
                    data = dataContext.Set<TEntity>().AsNoTracking().Where(filterBy).OrderByDescending(sortBy).Skip(skip).Take(take).ToList();
                }
            }

            return await Task.FromResult(data);
        }
    }
}

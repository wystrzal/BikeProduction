using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BikeBaseRepository
{
    public class BaseRepository<TEntity, TDataContext> : IBaseRepository<TEntity>
        where TEntity : class 
        where TDataContext : DbContext
    {
        private readonly TDataContext dataContext;

        public BaseRepository(TDataContext dataContext)
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

        public async Task<List<TEntity>> GetByConditionToList(Func<TEntity, bool> condition)
        {
            var data = dataContext.Set<TEntity>().Where(condition).ToList();
            return await Task.FromResult(data);
        }

        public async Task<TEntity> GetByConditionFirst(Func<TEntity, bool> condition)
        {
            var data = dataContext.Set<TEntity>().Where(condition).FirstOrDefault();
            return await Task.FromResult(data);
        }

        public async Task<TEntity> GetById(int id)
        {
            return await dataContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetByConditionWithIncludeFirst<TProp>(Func<TEntity, bool> condition, Expression<Func<TEntity, TProp>> include)
        {
            var data = dataContext.Set<TEntity>().Include(include).Where(condition).FirstOrDefault();
            return await Task.FromResult(data);
        }

        public async Task<List<TEntity>> GetByConditionWithIncludeToList<TProp>(Func<TEntity, bool> condition, Expression<Func<TEntity, TProp>> include)
        {
            var data = dataContext.Set<TEntity>().Include(include).Where(condition).ToList();
            return await Task.FromResult(data);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await dataContext.SaveChangesAsync() > 0 ? true : false;
        }
    }
}

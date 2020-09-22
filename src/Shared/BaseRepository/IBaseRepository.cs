using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BikeBaseRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        Task<List<TEntity>> GetByConditionToList(Func<TEntity, bool> condition);
        Task<List<TEntity>> GetByConditionWithIncludeToList<TProp>(Func<TEntity, bool> condition, Expression<Func<TEntity, TProp>> include);
        Task<TEntity> GetByConditionFirst(Func<TEntity, bool> condition);
        Task<TEntity> GetByConditionWithIncludeFirst<TProp>(Func<TEntity, bool> condition, Expression<Func<TEntity, TProp>> include);
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task<bool> SaveAllAsync();
        Task<List<TEntity>> GetFilterSortData<TKey>(Func<TEntity, bool> filterBy, Func<TEntity, TKey> sortBy,
           bool orderDesc, int skip, int take);

    }
}

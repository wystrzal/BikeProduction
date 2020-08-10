using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static BikeBaseRepository.OrderByTypeEnum;

namespace BikeBaseRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        Task<List<TEntity>> GetByConditionToList(Func<TEntity, bool> func);
        Task<List<TEntity>> GetByConditionWithIncludeToList<TProp>(Func<TEntity, bool> where, Expression<Func<TEntity, TProp>> include);
        Task<TEntity> GetByConditionFirst(Func<TEntity, bool> func);
        Task<TEntity> GetByConditionWithIncludeFirst<TProp>(Func<TEntity, bool> where, Expression<Func<TEntity, TProp>> include);
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task<bool> SaveAllAsync();
        Task<List<TEntity>> GetSortedData(Func<TEntity, bool> sortBy, OrderByType orderByType, int skip, int take);
        Task<List<TEntity>> GetFilteredData(Func<TEntity, bool> filterBy, int skip, int take);

    }
}

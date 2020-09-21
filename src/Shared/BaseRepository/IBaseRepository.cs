using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BikeBaseRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Add data to database.
        /// </summary>
        /// <param name="entity">Set entity.</param>
        void Add(TEntity entity);
        /// <summary>
        /// Delete data from database.
        /// </summary>
        /// <param name="entity">Set entity.</param>
        void Delete(TEntity entity);
        /// <summary>
        /// Update data in database.
        /// </summary>
        /// <param name="entity">Set entity.</param>
        void Update(TEntity entity);
        /// <summary>
        /// Get data to list by condition.
        /// </summary>
        /// <param name="condition">Set condition (e.q. x => x.Id == Id).</param>
        /// <returns></returns>
        Task<List<TEntity>> GetByConditionToList(Func<TEntity, bool> condition);
        /// <summary>
        /// Get data to list by condition, with included entity.
        /// </summary>
        /// <param name="condition">Set condition (e.q. x => x.Id == Id).</param>
        /// <param name="include">Set entity to include (e.g. x => x.Entity).</param>
        /// <returns></returns>
        Task<List<TEntity>> GetByConditionWithIncludeToList<TProp>(Func<TEntity, bool> condition, Expression<Func<TEntity, TProp>> include);
        /// <summary>
        /// Get data by condition.
        /// </summary>
        /// <param name="condition">Set condition (e.q. x => x.Id == Id).</param>
        /// <returns></returns>
        Task<TEntity> GetByConditionFirst(Func<TEntity, bool> condition);
        /// <summary>
        /// Get data by condition, with included entity.
        /// </summary>
        /// <param name="condition">Set condition (e.q. x => x.Id == Id).</param>
        /// <param name="include">Set entity to include (e.q. x => x.Entity).</param>
        /// <returns></returns>
        Task<TEntity> GetByConditionWithIncludeFirst<TProp>(Func<TEntity, bool> condition, Expression<Func<TEntity, TProp>> include);
        /// <summary>
        /// Get all data.
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetAll();
        /// <summary>
        /// Get data by Id.
        /// </summary>
        /// <param name="id">Set ID.</param>
        /// <returns></returns>
        Task<TEntity> GetById(int id);
        /// <summary>
        /// Save data.
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveAllAsync();
        /// <summary>
        /// Get filtered and sorted data.
        /// </summary>
        /// <typeparam name="TKey">The type of the returned key.</typeparam>
        /// <param name="filterBy">Set filter.</param>
        /// <param name="sortBy">Set sort.</param>
        /// <param name="orderDesc">Choose if order descending.</param>
        /// <param name="skip">Set skip.</param>
        /// <param name="take">Set take.</param>
        /// <returns></returns>
        Task<List<TEntity>> GetFilterSortData<TKey>(Func<TEntity, bool> filterBy, Func<TEntity, TKey> sortBy,
           bool orderDesc, int skip, int take);

    }
}

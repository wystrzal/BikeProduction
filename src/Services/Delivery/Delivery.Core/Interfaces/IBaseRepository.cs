using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task<List<T>> GetByConditionToList(Func<T, bool> func);
        Task<List<T>> GetByConditionWithIncludeToList<Tprop>(Func<T, bool> where, Expression<Func<T, Tprop>> include);
        Task<T> GetByConditionFirst(Func<T, bool> func);
        Task<T> GetByConditionWithIncludeFirst<Tprop>(Func<T, bool> where, Expression<Func<T, Tprop>> include);
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<bool> SaveAllAsync();
    }
}

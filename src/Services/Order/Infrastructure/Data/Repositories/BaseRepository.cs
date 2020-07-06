﻿using Microsoft.EntityFrameworkCore;
using Order.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Order.Infrastructure.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DataContext dataContext;

        public BaseRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void Add(T entity)
        {
            dataContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            dataContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            dataContext.Set<T>().Update(entity);
        }

        public async Task<List<T>> GetAll()
        {
            return await dataContext.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetByConditionToList(Func<T, bool> func)
        {
            var data = dataContext.Set<T>().Where(func).ToList();
            return await Task.FromResult(data);
        }

        public async Task<T> GetByConditionFirst(Func<T, bool> func)
        {
            var data = dataContext.Set<T>().Where(func).FirstOrDefault();
            return await Task.FromResult(data);
        }

        public async Task<T> GetById(int id)
        {
            return await dataContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByConditionWithIncludeFirst<Tprop>(Func<T, bool> where, Expression<Func<T, Tprop>> include)
        {
            var data = dataContext.Set<T>().Include(include).Where(where).FirstOrDefault();
            return await Task.FromResult(data);
        }

        public async Task<List<T>> GetByConditionWithIncludeToList<Tprop>(Func<T, bool> where, Expression<Func<T, Tprop>> include)
        {
            var data = dataContext.Set<T>().Include(include).Where(where).ToList();
            return await Task.FromResult(data);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await dataContext.SaveChangesAsync() > 0 ? true : false;
        }
    }
}

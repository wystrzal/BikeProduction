using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeSortFilter
{
    public interface ISortFilterRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetSortedFilteredData<TKey>(Func<TEntity, bool> filterBy, Func<TEntity, TKey> sortBy,
         bool orderDesc, int skip, int take);
        Task<List<TEntity>> GetFilteredData(Func<TEntity, bool> filterBy, int skip, int take);
    }
}

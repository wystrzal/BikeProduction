using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static BikeBaseRepository.OrderByTypeEnum;

namespace BikeSortFilter
{
    public interface ISortFilterService<TEntity> where TEntity : class
    {
        void SetConcreteFilter<T>(T typeOfFilter) where T : class;
        Task<List<TEntity>> SearchFiltered();
        Task<List<TEntity>> SearchSorted<T>(T typeOfSort, OrderByType orderByType);
    }
}

﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace BikeSortFilter
{
    public interface ISearchSortFilterService<TEntity, TFilteringData>
        where TEntity : class
        where TFilteringData : class
    {
        void SetConcreteFilter<TFilter>(TFilteringData filteringData) where TFilter : class;
        void SetConcreteSort<TSort, TKey>() where TSort : class;
        Task<List<TEntity>> Search(bool orderDesc, int skip = 0, int take = 0);
    }
}
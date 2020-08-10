using System;
using System.Collections.Generic;
using System.Text;

namespace BikeSortFilter
{
    public interface IConcreteSearch<TEntity> where TEntity : class
    {
        Predicate<TEntity> getConcreteFilter();
        Func<TEntity, bool> getConcreteSort();
    }
}

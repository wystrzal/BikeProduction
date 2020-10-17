using System;

namespace BikeSortFilter
{
    public interface IConcreteSort<TEntity, TReturned>
        where TEntity : class
    {
        Func<TEntity, TReturned> GetSortCondition();
    }
}

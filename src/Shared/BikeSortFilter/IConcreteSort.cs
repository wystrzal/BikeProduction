using System;

namespace BikeSortFilter
{
    public interface IConcreteSort<TEntity, TKey> where TEntity : class
    {
        Func<TEntity, TKey> GetSortCondition();
    }
}

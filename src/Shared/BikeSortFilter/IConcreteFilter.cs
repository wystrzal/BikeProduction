using System;

namespace BikeSortFilter
{
    /// <summary>
    /// <para>Important: The inheriting class must have a constructor
    /// with a class that has the data to filter.</para>
    /// </summary>
    public interface IConcreteFilter<TEntity> where TEntity : class
    {
        Predicate<TEntity> GetConcreteFilter();
    }
}

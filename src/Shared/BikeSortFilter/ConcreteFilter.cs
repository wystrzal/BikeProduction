using System;

namespace BikeSortFilter
{
    public abstract class ConcreteFilter<TEntity, TFilteringData> 
        where TEntity : class
        where TFilteringData : class
    {
        protected readonly TFilteringData filteringData;

        public ConcreteFilter(TFilteringData filteringData)
        {
            this.filteringData = filteringData;
        }

        public abstract Predicate<TEntity> GetConcreteFilter();
    }
}

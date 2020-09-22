using BikeSortFilter;
using Catalog.Core.Models;
using System;

namespace Catalog.Core.SearchSpecification.FilterClasses
{
    public class TypeFilter : ConcreteFilter<Product, FilteringData>
    {
        public TypeFilter(FilteringData filteringData) : base(filteringData)
        {
        }

        public override Predicate<Product> GetFilteringCondition()
        {
            return x => x.BikeType == filteringData.BikeType;
        }
    }
}
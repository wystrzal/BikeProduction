using BikeSortFilter;
using Catalog.Core.Models;
using System;

namespace Catalog.Core.SearchSpecification.FilterClasses
{
    public class ColorFilter : ConcreteFilter<Product, FilteringData>
    {
        public ColorFilter(FilteringData filteringData) : base(filteringData)
        {
        }

        public override Predicate<Product> GetFilteringCondition()
        {
            return x => x.Colors == filteringData.Colors;
        }
    }
}
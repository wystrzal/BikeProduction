using BikeSortFilter;
using Catalog.Core.Models;
using System;

namespace Catalog.Core.SearchSpecification.FilterClasses
{
    public class ColorFilter : IConcreteFilter<Product>
    {
        private readonly FilteringData filteringData;

        public ColorFilter(FilteringData filteringData)
        {
            this.filteringData = filteringData;
        }

        public Predicate<Product> GetConcreteFilter()
        {
            return x => x.Colors == filteringData.Colors;
        }
    }
}

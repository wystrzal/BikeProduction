using BikeSortFilter;
using Catalog.Core.Models;
using System;

namespace Catalog.Core.SearchSpecification.FilterClasses
{
    public class BrandFilter : ConcreteFilter<Product, FilteringData>
    {
        public BrandFilter(FilteringData filteringData) : base(filteringData)
        {
        }

        public override Predicate<Product> GetConcreteFilter()
        {
            return x => x.BrandId == filteringData.BrandId;
        }
    }
}

using BikeSortFilter;
using Catalog.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Core.SearchSpecification.FilterClasses
{
    public class TypeFilter : IConcreteFilter<Product>
    {
        private readonly FilteringData filteringData;

        public TypeFilter(FilteringData filteringData)
        {
            this.filteringData = filteringData;
        }

        public Predicate<Product> GetConcreteFilter()
        {
            return x => x.BikeType == filteringData.BikeType;
        }
    }
}

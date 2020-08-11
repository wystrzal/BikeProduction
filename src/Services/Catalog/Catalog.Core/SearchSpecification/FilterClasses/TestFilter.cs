using BikeSortFilter;
using Catalog.Core.Models;
using Catalog.Core.SearchSpecification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Infrastructure.Services.FilterClasses
{
    public class TestFilter : IConcreteFilter<Product>
    {
        private readonly FilteringData filteringData;

        public TestFilter(FilteringData filteringData)
        {
            this.filteringData = filteringData;
        }

        public Predicate<Product> GetConcreteFilter()
        {
            return x => x.Id != filteringData.Id;
        }
    }
}

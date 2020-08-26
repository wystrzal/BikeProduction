using BikeSortFilter;
using CustomerOrder.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerOrder.Core.SearchSpecification.FilterClasses
{
    public class FilterByUserId : IConcreteFilter<Order>
    {
        private readonly FilteringData filteringData;

        public FilterByUserId(FilteringData filteringData)
        {
            this.filteringData = filteringData;
        }

        public Predicate<Order> GetConcreteFilter()
        {
            return x => x.UserId == filteringData.UserId;
        }
    }
}

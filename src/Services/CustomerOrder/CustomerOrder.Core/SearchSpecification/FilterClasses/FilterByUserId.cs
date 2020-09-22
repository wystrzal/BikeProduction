using BikeSortFilter;
using CustomerOrder.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerOrder.Core.SearchSpecification.FilterClasses
{
    public class FilterByUserId : ConcreteFilter<Order, FilteringData>
    {
        public FilterByUserId(FilteringData filteringData) : base(filteringData)
        {
        }

        public override Predicate<Order> GetConcreteFilter()
        {
            return x => x.UserId == filteringData.UserId;
        }
    }
}

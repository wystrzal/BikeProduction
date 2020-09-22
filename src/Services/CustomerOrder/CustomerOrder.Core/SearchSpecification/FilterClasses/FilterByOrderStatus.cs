using BikeSortFilter;
using CustomerOrder.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerOrder.Core.SearchSpecification.FilterClasses
{
    public class FilterByOrderStatus : ConcreteFilter<Order, FilteringData>
    {
        public FilterByOrderStatus(FilteringData filteringData) : base(filteringData)
        {
        }

        public override Predicate<Order> GetConcreteFilter()
        {
            return x => x.OrderStatus == filteringData.OrderStatus;
        }
    }
}

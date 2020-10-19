using BikeSortFilter;
using Delivery.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery.Core.SearchSpecification.FilterClasses
{
    public class FilterByPackStatus : ConcreteFilter<PackToDelivery, OrderFilteringData>
    {
        public FilterByPackStatus(OrderFilteringData filteringData) : base(filteringData)
        {
        }

        public override Predicate<PackToDelivery> GetFilteringCondition()
        {
            return x => x.PackStatus == filteringData.PackStatus;
        }
    }
}

using BikeSortFilter;
using Delivery.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery.Core.SearchSpecification.SortClasses
{
    public class SortByDate : IConcreteSort<PackToDelivery, DateTime>
    {
        public Func<PackToDelivery, DateTime> GetSortCondition()
        {
            return x => x.Date;
        }
    }
}

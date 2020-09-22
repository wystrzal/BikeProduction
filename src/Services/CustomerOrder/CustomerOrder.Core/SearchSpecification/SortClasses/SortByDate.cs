using BikeSortFilter;
using CustomerOrder.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerOrder.Core.SearchSpecification.SortClasses
{
    public class SortByDate : IConcreteSort<Order, DateTime>
    {
        public Func<Order, DateTime> GetSortCondition()
        {
            return x => x.OrderDate;
        }
    }
}


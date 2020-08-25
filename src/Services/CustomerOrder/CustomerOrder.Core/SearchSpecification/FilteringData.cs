using System;
using System.Collections.Generic;
using System.Text;
using static CustomerOrder.Core.Models.Enums.OrderStatusEnum;

namespace CustomerOrder.Core.SearchSpecification
{
    public class FilteringData
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}

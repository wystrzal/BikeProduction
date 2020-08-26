using System;
using System.Collections.Generic;
using System.Text;
using static CustomerOrder.Core.Models.Enums.OrderStatusEnum;

namespace CustomerOrder.Core.SearchSpecification
{
    public class FilteringData
    {
        public OrderStatus OrderStatus { get; set; }
        public string UserId { get; set; }
    }
}

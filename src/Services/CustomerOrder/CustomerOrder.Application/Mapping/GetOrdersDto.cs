﻿using CustomerOrder.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static CustomerOrder.Core.Models.Enums.OrderStatusEnum;

namespace CustomerOrder.Application.Mapping
{
    public class GetOrdersDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

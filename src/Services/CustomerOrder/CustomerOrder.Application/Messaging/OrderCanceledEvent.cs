using CustomerOrder.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Application.Messaging
{
    public class OrderCanceledEvent
    {
        public int OrderId { get; set; }
        public OrderCanceledEvent(int orderId)
        {
            OrderId = orderId;
        }
    }
}

using CustomerOrder.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Messaging
{
    public class OrderCreatedEvent
    {
        public List<OrderItem> OrderItems { get; set; }

        public OrderCreatedEvent(List<OrderItem> orderItems)
        {
            OrderItems = orderItems;
        }
    }
}

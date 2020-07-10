using Production.Application.Messaging.MessagingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Application.Messaging
{
    public class OrderCreatedEvent
    {
        public List<OrderItem> OrderItems { get; set; }
        public int OrderId { get; set; }

        public OrderCreatedEvent(List<OrderItem> orderItems, int orderId)
        {
            OrderItems = orderItems;
            OrderId = orderId;
        }
    }
}

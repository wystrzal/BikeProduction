using Production.Application.Messaging.MessagingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Production.Application.Messaging
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

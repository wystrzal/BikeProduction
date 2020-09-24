using Production.Core.Models.MessagingModels;
using System.Collections.Generic;

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


using CustomerOrder.Core.Models;
using System.Collections.Generic;

namespace Common.Application.Messaging
{
    public class OrderCreatedEvent
    {
        public List<OrderItem> OrderItems { get; set; }
        public int OrderId { get; set; }
        public string UserId { get; set; }

        public OrderCreatedEvent(List<OrderItem> orderItems, int orderId, string userId)
        {
            OrderItems = orderItems;
            OrderId = orderId;
            UserId = userId;
        }
    }
}


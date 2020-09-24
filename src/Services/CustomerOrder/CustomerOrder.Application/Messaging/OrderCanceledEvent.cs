using CustomerOrder.Core.Models;
using System.Collections.Generic;

namespace Common.Application.Messaging
{
    public class OrderCanceledEvent
    {
        public List<OrderItem> OrderItems { get; set; }
        public int OrderId { get; set; }
        public OrderCanceledEvent(List<OrderItem> orderItems, int orderId)
        {
            OrderItems = orderItems;
            OrderId = orderId;
        }
    }
}

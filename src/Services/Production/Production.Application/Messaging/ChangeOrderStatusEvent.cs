using System;
using System.Collections.Generic;
using System.Text;
using static Production.Application.Messaging.MessagingModels.OrderStatusEnum;

namespace Common.Application.Messaging
{
    public class ChangeOrderStatusEvent 
    {
        public int OrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public ChangeOrderStatusEvent(int orderId, OrderStatus orderStatus)
        {
            OrderId = orderId;
            OrderStatus = orderStatus;
        }
    }
}

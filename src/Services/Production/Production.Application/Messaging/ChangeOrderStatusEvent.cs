﻿using static Production.Core.Models.MessagingModels.OrderStatusEnum;

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

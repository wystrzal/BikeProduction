﻿namespace Delivery.Core.Models.MessagingModels
{
    public class OrderStatusEnum
    {
        public enum OrderStatus
        {
            WaitingForConfirm = 1,
            Confirmed = 2,
            ReadyToSend = 3,
            Sended = 4,
            Delivered = 5
        }
    }
}

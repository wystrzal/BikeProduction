using CustomerOrder.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Application.Messaging
{
    public class OrderCanceledEvent
    {
        public int OrderId { get; set; }
        public List<string> References { get; set; }
        public OrderCanceledEvent(List<string> references, int orderId)
        {
            References = references;
            OrderId = orderId;
        }
    }
}

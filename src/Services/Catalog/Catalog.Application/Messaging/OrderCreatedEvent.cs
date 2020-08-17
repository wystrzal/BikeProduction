using Catalog.Application.Messaging.MessagingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Application.Messaging
{
    public class OrderCreatedEvent
    {
        public List<OrderItem> OrderItems { get; set; }
    }
}


using Catalog.Core.Models.MessagingModels;
using System.Collections.Generic;

namespace Common.Application.Messaging
{
    public class OrderCreatedEvent
    {
        public List<OrderItem> OrderItems { get; set; }
    }
}



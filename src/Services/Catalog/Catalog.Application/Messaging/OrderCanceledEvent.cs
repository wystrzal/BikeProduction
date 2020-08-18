using Catalog.Application.Messaging.MessagingModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Application.Messaging
{
    public class OrderCanceledEvent
    {
        public List<string> References { get; set; }
    }
}


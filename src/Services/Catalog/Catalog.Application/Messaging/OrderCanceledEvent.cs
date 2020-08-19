using System.Collections.Generic;

namespace Common.Application.Messaging
{
    public class OrderCanceledEvent
    {
        public List<string> References { get; set; }
    }
}


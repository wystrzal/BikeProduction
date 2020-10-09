using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Application.Messaging
{
    public class ProductUpdatedEvent
    {
        public string ProductName { get; set; }
        public string Reference { get; set; }
        public string OldReference { get; set; }
    }
}

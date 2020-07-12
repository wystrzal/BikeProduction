using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Application.Messaging
{
    public class ProductionConfirmedEvent
    {
        public string Reference { get; set; }
        public int Quantity { get; set; }
    }
}

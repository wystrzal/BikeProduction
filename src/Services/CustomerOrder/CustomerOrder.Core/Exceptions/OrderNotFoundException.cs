using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerOrder.Core.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException() : base("Could not found order.")
        {
        }
    }
}

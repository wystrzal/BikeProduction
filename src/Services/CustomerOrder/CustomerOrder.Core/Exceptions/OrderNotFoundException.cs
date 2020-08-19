using System;

namespace CustomerOrder.Core.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException() : base("Could not found order.")
        {
        }
    }
}

using System;

namespace CustomerOrder.Core.Exceptions
{
    public class OrderNotAddedException : Exception
    {
        public OrderNotAddedException() : base("Could not add order.")
        {
        }
    }
}

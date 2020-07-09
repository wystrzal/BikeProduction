using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerOrder.Core.Exceptions
{
    public class OrderNotAddedException : Exception
    {
        public OrderNotAddedException() : base("Could not add order.")
        {
        }
    }
}

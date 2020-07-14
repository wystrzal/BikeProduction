using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerOrder.Core.Exceptions
{
    public class OrderIsConfirmedException : Exception
    {
        public OrderIsConfirmedException() : base("Could not delete confirmed orders.")
        {
        }
    }
}

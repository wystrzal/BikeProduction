using System;
using System.Collections.Generic;
using System.Text;

namespace Production.Core.Exceptions
{
    public class ProductionQueueNotFoundException : Exception
    {
        public ProductionQueueNotFoundException() : base("Could not found production queue.")
        {

        }
    }
}

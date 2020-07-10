using System;
using System.Collections.Generic;
using System.Text;

namespace Production.Core.Exceptions
{
    public class ProductionQueueNotConfirmedException : Exception
    {
        public ProductionQueueNotConfirmedException() : base("Production queue was not confirmed.")
        {
        }
    }
}

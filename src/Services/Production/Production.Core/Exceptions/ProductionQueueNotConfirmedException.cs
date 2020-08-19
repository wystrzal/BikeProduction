using System;

namespace Production.Core.Exceptions
{
    public class ProductionQueueNotConfirmedException : Exception
    {
        public ProductionQueueNotConfirmedException() : base("Production queue was not confirmed.")
        {
        }
    }
}

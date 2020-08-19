using System;

namespace Warehouse.Core.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException() : base("Could not found product.")
        {

        }
    }
}

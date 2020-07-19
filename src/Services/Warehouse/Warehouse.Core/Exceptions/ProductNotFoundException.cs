using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Core.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException() : base("Could not found product.")
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Core.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException() : base("Could not found product.")
        {

        }
    }
}

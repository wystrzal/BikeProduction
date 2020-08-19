using System;

namespace Catalog.Core.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException() : base("Could not found product.")
        {

        }
    }
}

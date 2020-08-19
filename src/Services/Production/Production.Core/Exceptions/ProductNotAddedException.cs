using System;

namespace Production.Core.Exceptions
{
    public class ProductNotAddedException : Exception
    {
        public ProductNotAddedException() : base("Could not add product to queue.")
        {
        }
    }
}

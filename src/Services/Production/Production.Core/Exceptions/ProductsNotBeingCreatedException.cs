using System;

namespace Production.Core.Exceptions
{
    public class ProductsNotBeingCreatedException : Exception
    {
        public ProductsNotBeingCreatedException() : base("Can not finish production, if products not being created.")
        {
        }
    }
}

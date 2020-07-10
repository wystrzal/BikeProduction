using System;
using System.Collections.Generic;
using System.Text;

namespace Production.Core.Exceptions
{
    public class ProductsNotBeingCreatedException : Exception
    {
        public ProductsNotBeingCreatedException() : base("Can not finish production, if products not being created.")
        {
        }
    }
}

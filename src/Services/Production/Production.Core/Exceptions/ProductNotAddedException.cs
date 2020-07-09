using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Production.Core.Exceptions
{
    public class ProductNotAddedException : Exception
    {
        public ProductNotAddedException() : base("Could not add product to queue.")
        {
        }
    }
}

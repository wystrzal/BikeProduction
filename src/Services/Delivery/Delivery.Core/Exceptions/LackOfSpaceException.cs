using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery.Core.Exceptions
{
    public class LackOfSpaceException : Exception
    {
        public LackOfSpaceException() : base("Not enough space at loading place.")
        {

        }
    }
}

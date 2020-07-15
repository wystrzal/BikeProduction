using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery.Core.Exceptions
{
    public class PackNotFoundException : Exception
    {
        public PackNotFoundException() : base("Could not found pack.")
        {

        }
    }
}

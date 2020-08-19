using System;

namespace Delivery.Core.Exceptions
{
    public class PackNotFoundException : Exception
    {
        public PackNotFoundException() : base("Could not found pack.")
        {

        }
    }
}

using System;

namespace Delivery.Core.Exceptions
{
    public class LackOfSpaceException : Exception
    {
        public LackOfSpaceException() : base("Not enough space at loading place.")
        {

        }
    }
}

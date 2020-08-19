using System;

namespace Delivery.Core.Exceptions
{
    public class LoadingPlaceNotFoundException : Exception
    {
        public LoadingPlaceNotFoundException() : base("Could not found loading place.")
        {

        }
    }
}

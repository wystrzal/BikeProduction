using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery.Core.Exceptions
{
    public class LoadingPlaceNotFoundException : Exception
    {
        public LoadingPlaceNotFoundException() : base("Could not found loading place.")
        {

        }
    }
}

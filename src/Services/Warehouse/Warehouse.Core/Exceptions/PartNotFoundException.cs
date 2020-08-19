using System;

namespace Warehouse.Core.Exceptions
{
    public class PartNotFoundException : Exception
    {
        public PartNotFoundException() : base("Could not found part.")
        {

        }
    }
}

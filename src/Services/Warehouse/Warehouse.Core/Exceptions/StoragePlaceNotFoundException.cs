using System;

namespace Warehouse.Core.Exceptions
{
    public class StoragePlaceNotFoundException : Exception
    {
        public StoragePlaceNotFoundException() : base("Could not found storage place.")
        {

        }
    }
}

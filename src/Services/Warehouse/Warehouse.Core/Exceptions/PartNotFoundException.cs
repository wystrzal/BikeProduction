using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Core.Exceptions
{
    public class PartNotFoundException : Exception
    {
        public PartNotFoundException() : base("Could not found part.")
        {

        }
    }
}

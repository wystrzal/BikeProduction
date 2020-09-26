using System;
using System.Collections.Generic;
using System.Text;

namespace BikeBaseRepository
{
    public class NullDataException : Exception
    {
        public NullDataException() : base("Could not get any data.")
        {

        }
    }
}

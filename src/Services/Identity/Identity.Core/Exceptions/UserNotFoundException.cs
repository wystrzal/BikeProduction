using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Core.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("Could not found user.")
        {
        }
    }
}

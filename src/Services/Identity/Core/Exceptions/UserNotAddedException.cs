using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Core.Exceptions
{
    public class UserNotAddedException : Exception
    {
        public UserNotAddedException() : base("Could not add user.")
        {

        }
    }
}

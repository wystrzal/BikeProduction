using System;

namespace Identity.Core.Exceptions
{
    public class UserNotAddedException : Exception
    {
        public UserNotAddedException() : base("Could not add user.")
        {

        }
    }
}

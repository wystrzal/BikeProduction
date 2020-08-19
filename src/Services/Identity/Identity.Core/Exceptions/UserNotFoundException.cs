using System;

namespace Identity.Core.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("Could not found user.")
        {
        }
    }
}

using System;

namespace Identity.Core.Exceptions
{
    public class LoginFailedException : Exception
    {
        public LoginFailedException() : base("Login failed.")
        {

        }
    }
}

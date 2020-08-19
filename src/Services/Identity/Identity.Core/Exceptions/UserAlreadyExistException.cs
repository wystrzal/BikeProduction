using System;

namespace Identity.Core.Exceptions
{
    public class UserAlreadyExistException : Exception
    {
        public UserAlreadyExistException(string username) : base($"User with login '{username}' already exist.")
        {
        }
    }
}

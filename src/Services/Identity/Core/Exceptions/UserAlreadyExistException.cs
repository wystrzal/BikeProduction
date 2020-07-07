using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Core.Exceptions
{
    public class UserAlreadyExistException : Exception
    {
        public UserAlreadyExistException(string username) : base($"User with login '{username}' already exist.")
        {
        }
    }
}

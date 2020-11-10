using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Basket.Application.Commands
{
    public abstract class UserIdCommand : IRequest
    {
        [Required]
        public string UserId { get; set; }

        public UserIdCommand()
        {
        }

        public UserIdCommand(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException("UserId");
            }

            UserId = userId;
        }
    }
}

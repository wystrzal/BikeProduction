using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Basket.Application.Commands
{
    public abstract class BaseCommand : IRequest
    {
        [Required]
        public string UserId { get; set; }

        public BaseCommand()
        {
        }

        public BaseCommand(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException("UserId");
            }

            UserId = userId;
        }
    }
}

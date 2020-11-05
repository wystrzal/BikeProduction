using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Basket.Application.Commands
{
    public class ClearBasketCommand : IRequest
    {
        public string UserId { get; set; }

        public ClearBasketCommand(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException("UserId");
            }

            UserId = userId;
        }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Application.Commands
{
    public class ClearBasketCommand : IRequest
    {
        public string UserId { get; set; }

        public ClearBasketCommand(string userId)
        {
            UserId = userId;
        }
    }
}

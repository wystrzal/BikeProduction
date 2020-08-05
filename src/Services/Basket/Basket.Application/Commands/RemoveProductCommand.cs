using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Application.Commands
{
    public class RemoveProductCommand : IRequest
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }

        public RemoveProductCommand(string userId, int productId)
        {
            UserId = userId;
            ProductId = productId;
        }
    }
}

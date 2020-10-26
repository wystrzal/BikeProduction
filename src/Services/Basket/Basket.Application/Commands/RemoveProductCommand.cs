using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Basket.Application.Commands
{
    public class RemoveProductCommand : IRequest
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }

        public RemoveProductCommand(string userId, int productId)
        {
            if (productId <= 0)
            {
                throw new ArgumentException("ProductId must be greater than zero.");
            }

            UserId = userId;
            ProductId = productId;
        }
    }
}

using MediatR;
using System;

namespace Catalog.Application.Commands
{
    public class DeleteProductCommand : IRequest
    {
        public int ProductId { get; set; }

        public DeleteProductCommand(int productId)
        {
            if (productId <= 0)
            {
                throw new ArgumentException("ProductId must be greater than zero.");
            }

            ProductId = productId;
        }
    }
}

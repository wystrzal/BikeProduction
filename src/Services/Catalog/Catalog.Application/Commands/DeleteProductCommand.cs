using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Commands
{
    public class DeleteProductCommand : IRequest
    {
        public int ProductId { get; set; }

        public DeleteProductCommand(int productId)
        {
            ProductId = productId;
        }
    }
}

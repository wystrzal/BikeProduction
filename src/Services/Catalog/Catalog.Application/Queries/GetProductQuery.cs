using Catalog.Application.Mapping;
using MediatR;
using System;

namespace Catalog.Application.Queries
{
    public class GetProductQuery : IRequest<GetProductDto>
    {
        public int ProductId { get; set; }

        public GetProductQuery(int productId)
        {
            if (productId <= 0)
            {
                throw new ArgumentException("ProductId must be greater than zero.");
            }

            ProductId = productId;
        }
    }
}

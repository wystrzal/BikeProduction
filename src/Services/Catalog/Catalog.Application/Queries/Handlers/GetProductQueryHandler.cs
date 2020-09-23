using AutoMapper;
using Catalog.Application.Mapping;
using Catalog.Core.Exceptions;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Queries.Handlers
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, GetProductDto>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public GetProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<GetProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository
                .GetByConditionWithIncludeFirst(x => x.Id == request.ProductId, y => y.Brand);

            ThrowsProductNotFoundExceptionIfProductIsNull(product);

            return mapper.Map<GetProductDto>(product);
        }

        private void ThrowsProductNotFoundExceptionIfProductIsNull(Product product)
        {
            if (product == null)
                throw new ProductNotFoundException();
        }
    }
}

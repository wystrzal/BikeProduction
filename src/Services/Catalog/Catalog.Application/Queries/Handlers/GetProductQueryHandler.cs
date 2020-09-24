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
            var product = await GetProduct(request.ProductId);

            return mapper.Map<GetProductDto>(product);
        }

        private async Task<Product> GetProduct(int productId)
        {
            var product = await productRepository
                .GetByConditionWithIncludeFirst(x => x.Id == productId, y => y.Brand);

            return product ?? throw new ProductNotFoundException();
        }
    }
}

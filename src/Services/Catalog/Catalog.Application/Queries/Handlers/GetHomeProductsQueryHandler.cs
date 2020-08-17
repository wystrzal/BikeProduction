using AutoMapper;
using Catalog.Application.Mapping;
using Catalog.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Queries.Handlers
{
    public class GetHomeProductsQueryHandler : IRequestHandler<GetHomeProductsQuery, List<GetHomeProductsDto>>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public GetHomeProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }
        public async Task<List<GetHomeProductsDto>> Handle(GetHomeProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetHomePageProducts(request.HomeProduct);

            return mapper.Map<List<GetHomeProductsDto>>(products);
        }
    }
}

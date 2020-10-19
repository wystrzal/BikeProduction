using AutoMapper;
using Catalog.Application.Mapping;
using Catalog.Core.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Queries.Handlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<GetProductsDto>>
    {
        private readonly IMapper mapper;
        private readonly ISearchProductsService searchProductService;

        public GetProductsQueryHandler(IMapper mapper, ISearchProductsService searchProductService)
        {
            this.mapper = mapper;
            this.searchProductService = searchProductService;
        }

        public async Task<IEnumerable<GetProductsDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await searchProductService
                .GetProducts(request.FilteringData);

            return mapper.Map<List<GetProductsDto>>(products);
        }
    }
}

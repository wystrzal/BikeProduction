using AutoMapper;
using Catalog.Application.Mapping;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Catalog.Core.SearchSpecification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Queries.Handlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<GetProductsDto>>
    {
        private readonly IMapper mapper;
        private readonly ISearchProductService searchProductService;

        public GetProductsQueryHandler(IMapper mapper, ISearchProductService searchProductService)
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

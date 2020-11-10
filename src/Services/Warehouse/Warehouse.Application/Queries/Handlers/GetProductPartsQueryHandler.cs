using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.Application.Mapping;
using Warehouse.Core.Interfaces;

namespace Warehouse.Application.Queries.Handlers
{
    public class GetProductPartsQueryHandler : IRequestHandler<GetProductPartsQuery, List<GetProductPartsDto>>
    {
        private readonly IProductPartRepo productPartRepo;
        private readonly IMapper mapper;

        public GetProductPartsQueryHandler(IProductPartRepo productPartRepo, IMapper mapper)
        {
            this.productPartRepo = productPartRepo;
            this.mapper = mapper;
        }

        public async Task<List<GetProductPartsDto>> Handle(GetProductPartsQuery request, CancellationToken cancellationToken)
        {
            var productParts = await productPartRepo.GetProductParts(request.Reference);

            return mapper.Map<List<GetProductPartsDto>>(productParts);
        }
    }
}

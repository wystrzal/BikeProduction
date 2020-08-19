using AutoMapper;
using Catalog.Application.Mapping;
using Catalog.Core.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Queries.Handlers
{
    public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, List<GetBrandsDto>>
    {
        private readonly IBrandRepository brandRepository;
        private readonly IMapper mapper;

        public GetBrandsQueryHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            this.brandRepository = brandRepository;
            this.mapper = mapper;
        }

        public async Task<List<GetBrandsDto>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        {
            var brands = await brandRepository.GetAll();

            return mapper.Map<List<GetBrandsDto>>(brands);
        }
    }
}

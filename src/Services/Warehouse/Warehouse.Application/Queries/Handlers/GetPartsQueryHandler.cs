using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.Application.Mapping;
using Warehouse.Core.Interfaces;

namespace Warehouse.Application.Queries.Handlers
{
    public class GetPartsQueryHandler : IRequestHandler<GetPartsQuery, IEnumerable<GetPartsDto>>
    {
        private readonly IPartRepository partRepository;
        private readonly IMapper mapper;

        public GetPartsQueryHandler(IPartRepository partRepository, IMapper mapper)
        {
            this.partRepository = partRepository;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<GetPartsDto>> Handle(GetPartsQuery request, CancellationToken cancellationToken)
        {
            var parts = await partRepository.GetAll();

            return mapper.Map<IEnumerable<GetPartsDto>>(parts);
        }
    }
}

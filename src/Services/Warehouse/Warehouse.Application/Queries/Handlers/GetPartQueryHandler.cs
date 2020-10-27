using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.Application.Mapping;
using Warehouse.Core.Interfaces;

namespace Warehouse.Application.Queries.Handlers
{
    public class GetPartQueryHandler : IRequestHandler<GetPartQuery, GetPartDto>
    {
        private readonly IPartRepository partRepository;
        private readonly IMapper mapper;

        public GetPartQueryHandler(IPartRepository partRepository, IMapper mapper)
        {
            this.partRepository = partRepository;
            this.mapper = mapper;
        }
        public async Task<GetPartDto> Handle(GetPartQuery request, CancellationToken cancellationToken)
        {
            var part = await partRepository.GetByConditionWithIncludeFirst(x => x.Id == request.PartId, y => y.ProductsParts);

            return mapper.Map<GetPartDto>(part);
        }
    }
}

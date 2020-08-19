using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.Application.Mapping;
using Warehouse.Core.Interfaces;

namespace Warehouse.Application.Queries.Handlers
{
    public class GetStoragePlacesQueryHandler : IRequestHandler<GetStoragePlacesQuery, IEnumerable<GetStoragePlacesDto>>
    {
        private readonly IStoragePlaceRepo storagePlaceRepo;
        private readonly IMapper mapper;

        public GetStoragePlacesQueryHandler(IStoragePlaceRepo storagePlaceRepo, IMapper mapper)
        {
            this.storagePlaceRepo = storagePlaceRepo;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GetStoragePlacesDto>> Handle(GetStoragePlacesQuery request, CancellationToken cancellationToken)
        {
            var storagePlaces = await storagePlaceRepo.GetStoragePlaces();

            return mapper.Map<IEnumerable<GetStoragePlacesDto>>(storagePlaces);
        }
    }
}

using AutoMapper;
using Delivery.Application.Mapping;
using Delivery.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Application.Queries.Handlers
{
    public class GetLoadingPlaceQueryHandler : IRequestHandler<GetLoadingPlaceQuery, GetLoadingPlaceDto>
    {
        private readonly ILoadingPlaceRepo loadingPlaceRepo;
        private readonly IMapper mapper;

        public GetLoadingPlaceQueryHandler(ILoadingPlaceRepo loadingPlaceRepo, IMapper mapper)
        {
            this.loadingPlaceRepo = loadingPlaceRepo;
            this.mapper = mapper;
        }

        public async Task<GetLoadingPlaceDto> Handle(GetLoadingPlaceQuery request, CancellationToken cancellationToken)
        {
            var loadingPlace = await loadingPlaceRepo
                .GetByConditionWithIncludeFirst(x => x.Id == request.LoadingPlaceId, y => y.PacksToDelivery);

            return mapper.Map<GetLoadingPlaceDto>(loadingPlace);
        }
    }
}

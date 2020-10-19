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
    public class GetLoadingPlacesQueryHandler : IRequestHandler<GetLoadingPlacesQuery, List<GetLoadingPlacesDto>>
    {
        private readonly ISearchLoadingPlacesService searchLoadingPlacesService;
        private readonly IMapper mapper;

        public GetLoadingPlacesQueryHandler(ISearchLoadingPlacesService searchLoadingPlacesService, IMapper mapper)
        {
            this.searchLoadingPlacesService = searchLoadingPlacesService;
            this.mapper = mapper;
        }

        public async Task<List<GetLoadingPlacesDto>> Handle(GetLoadingPlacesQuery request, CancellationToken cancellationToken)
        {
            var loadingPlaces = await searchLoadingPlacesService.SearchLoadingPlaces(request.FilteringData);
            return mapper.Map<List<GetLoadingPlacesDto>>(loadingPlaces);
        }
    }
}

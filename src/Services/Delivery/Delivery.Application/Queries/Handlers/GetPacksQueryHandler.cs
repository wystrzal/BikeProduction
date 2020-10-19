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
    public class GetPacksQueryHandler : IRequestHandler<GetPacksQuery, List<GetPacksDto>>
    {
        private readonly ISearchPacksService searchPacksService;
        private readonly IMapper mapper;

        public GetPacksQueryHandler(ISearchPacksService searchPacksService, IMapper mapper)
        {
            this.searchPacksService = searchPacksService;
            this.mapper = mapper;
        }

        public async Task<List<GetPacksDto>> Handle(GetPacksQuery request, CancellationToken cancellationToken)
        {
            var packs = await searchPacksService.SearchPacks(request.FilteringData);
            return mapper.Map<List<GetPacksDto>>(packs);
        }
    }
}

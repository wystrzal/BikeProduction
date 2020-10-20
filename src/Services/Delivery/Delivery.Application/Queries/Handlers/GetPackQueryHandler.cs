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
    public class GetPackQueryHandler : IRequestHandler<GetPackQuery, GetPackDto>
    {
        private readonly IPackToDeliveryRepo packToDeliveryRepo;
        private readonly IMapper mapper;

        public GetPackQueryHandler(IPackToDeliveryRepo packToDeliveryRepo, IMapper mapper)
        {
            this.packToDeliveryRepo = packToDeliveryRepo;
            this.mapper = mapper;
        }

        public async Task<GetPackDto> Handle(GetPackQuery request, CancellationToken cancellationToken)
        {
            var pack = await packToDeliveryRepo.GetByConditionWithIncludeFirst(x => x.Id == request.PackId, y => y.LoadingPlace);
            return mapper.Map<GetPackDto>(pack);
        }
    }
}

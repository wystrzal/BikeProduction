using AutoMapper;
using CustomerOrder.Application.Mapping;
using CustomerOrder.Core.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerOrder.Application.Queries.Handlers
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<GetOrdersDto>>
    {
        private readonly ISearchOrderService searchOrderService;
        private readonly IMapper mapper;

        public GetOrdersQueryHandler(ISearchOrderService searchOrderService, IMapper mapper)
        {
            this.searchOrderService = searchOrderService;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GetOrdersDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await searchOrderService.GetOrders(request.FilteringData);

            return mapper.Map<List<GetOrdersDto>>(orders);
        }
    }
}

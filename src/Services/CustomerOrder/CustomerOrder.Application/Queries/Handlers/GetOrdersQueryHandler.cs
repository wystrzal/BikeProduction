using AutoMapper;
using CustomerOrder.Application.Mapping;
using CustomerOrder.Core.Interfaces;
using CustomerOrder.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerOrder.Application.Queries.Handlers
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<GetOrdersDto>>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public GetOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GetOrdersDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            List<Order> orders = await orderRepository
                    .GetByConditionToList(x => x.UserId == request.UserId);

            return mapper.Map<List<GetOrdersDto>>(orders);
        }
    }
}

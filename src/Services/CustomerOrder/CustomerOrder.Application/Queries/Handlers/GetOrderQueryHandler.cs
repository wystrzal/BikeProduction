using AutoMapper;
using CustomerOrder.Application.Mapping;
using CustomerOrder.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerOrder.Application.Queries.Handlers
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, GetOrderDto>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public GetOrderQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }

        public async Task<GetOrderDto> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await orderRepository
                .GetByConditionWithIncludeFirst(x => x.OrderId == request.OrderId, y => y.OrderItems);

            return mapper.Map<GetOrderDto>(order);
        }
    }
}

using AutoMapper;
using CustomerOrder.Application.Mapping;
using CustomerOrder.Core.Interfaces;
using CustomerOrder.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
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
            List<Order> orders = null;

            if (request.UserId != null)
            {
                orders = await orderRepository
                    .GetByConditionWithIncludeToList(x => x.UserId == request.UserId, y => y.OrderItems);
            }
            else
            {
                orders = await orderRepository.GetOrders();
            }

            return mapper.Map<List<GetOrdersDto>>(orders);
        }
    }
}

using AutoMapper;
using CustomerOrder.Application.Mapping;
using CustomerOrder.Core.Exceptions;
using CustomerOrder.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
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
            var order = await orderRepository.GetByConditionFirst(x => x.OrderId == request.OrderId);

            if (order == null)
            {
                throw new OrderNotFoundException();
            }

            return mapper.Map<GetOrderDto>(order);        
        }
    }
}

using AutoMapper;
using MediatR;
using Order.Core.Interfaces;
using Order.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.Commands.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IMapper mapper;
        private readonly IOrderRepository orderRepository;

        public CreateOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository)
        {
            this.mapper = mapper;
            this.orderRepository = orderRepository;
        }
        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderForAdd = mapper.Map<Orders>(request);

            orderRepository.Add(orderForAdd);

            if (!await orderRepository.SaveAllAsync())
            {
                throw new Exception("Could not add order");
            }

            return Unit.Value;
        }
    }
}

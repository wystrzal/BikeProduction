using AutoMapper;
using Common.Application.Messaging;
using CustomerOrder.Core.Exceptions;
using CustomerOrder.Core.Interfaces;
using CustomerOrder.Core.Models;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerOrder.Application.Commands.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IMapper mapper;
        private readonly IOrderRepository orderRepository;
        private readonly IBus bus;

        public CreateOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository, IBus bus)
        {
            this.mapper = mapper;
            this.orderRepository = orderRepository;
            this.bus = bus;
        }
        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderForAdd = mapper.Map<Order>(request);

            orderRepository.Add(orderForAdd);

            if (!await orderRepository.SaveAllAsync())
            {
                throw new OrderNotAddedException();
            }

            await bus.Publish(new OrderCreatedEvent(orderForAdd.OrderItems as List<OrderItem>));

            return Unit.Value;
        }
    }
}

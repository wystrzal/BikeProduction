﻿using AutoMapper;
using Common.Application.Messaging;
using CustomerOrder.Core.Interfaces;
using CustomerOrder.Core.Models;
using MassTransit;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static CustomerOrder.Core.Models.Enums.OrderStatusEnum;

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

            orderForAdd.OrderStatus = OrderStatus.Waiting_For_Confirm;

            await AddOrder(orderForAdd, request.UserId);

            return Unit.Value;
        }

        private async Task AddOrder(Order orderForAdd, string userId)
        {
            orderRepository.Add(orderForAdd);
            await orderRepository.SaveAllAsync();
            await bus.Publish(new OrderCreatedEvent(orderForAdd.OrderItems as List<OrderItem>, orderForAdd.OrderId, userId));
        }
    }
}

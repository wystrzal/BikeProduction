using CustomerOrder.Core.Exceptions;
using CustomerOrder.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static CustomerOrder.Core.Models.Enums.OrderStatusEnum;

namespace CustomerOrder.Application.Commands.Handlers
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository orderRepository;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetById(request.OrderId);

            if (order == null)
            {
                throw new OrderNotFoundException();
            }

            if (order.OrderStatus == OrderStatus.WaitingForConfirm || order.OrderStatus == OrderStatus.Delivered)
            {
                orderRepository.Delete(order);

                await orderRepository.SaveAllAsync();
            }

            return Unit.Value;
        }
    }
}

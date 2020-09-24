using Common.Application.Messaging;
using CustomerOrder.Core.Exceptions;
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
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IBus bus;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository, IBus bus)
        {
            this.orderRepository = orderRepository;
            this.bus = bus;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await orderRepository
                .GetByConditionWithIncludeFirst(x => x.OrderId == request.OrderId, y => y.OrderItems);

            if (order == null)
                throw new OrderNotFoundException();

            if (order.OrderStatus != OrderStatus.Waiting_For_Confirm || order.OrderStatus != OrderStatus.Delivered)
                return Unit.Value;

            await PublishOrderCanceledEventIfOrderStatusIsWaitingForConfirm(order);

            await DeleteOrderFromRepository(order);

            return Unit.Value;
        }

        private async Task PublishOrderCanceledEventIfOrderStatusIsWaitingForConfirm(Order order)
        {
            if (order.OrderStatus == OrderStatus.Waiting_For_Confirm)
            {
                List<string> references = new List<string>();

                foreach (var item in order.OrderItems)
                    references.Add(item.Reference);

                await bus.Publish(new OrderCanceledEvent(references, order.OrderId));
            }
        }

        private async Task DeleteOrderFromRepository(Order order)
        {
            orderRepository.Delete(order);

            await orderRepository.SaveAllAsync();
        }
    }
}

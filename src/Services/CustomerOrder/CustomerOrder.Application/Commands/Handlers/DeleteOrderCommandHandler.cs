using Common.Application.Messaging;
using CustomerOrder.Core.Interfaces;
using CustomerOrder.Core.Models;
using MassTransit;
using MediatR;
using System.Linq;
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

            if (order.OrderStatus == OrderStatus.Waiting_For_Confirm || order.OrderStatus == OrderStatus.Delivered)
            {
                if (order.OrderStatus == OrderStatus.Waiting_For_Confirm)
                    await bus.Publish(new OrderCanceledEvent(order.OrderItems.Cast<OrderItem>().ToList(), order.OrderId));

                orderRepository.Delete(order);
                await orderRepository.SaveAllAsync();
            }

            return Unit.Value;
        }
    }
}

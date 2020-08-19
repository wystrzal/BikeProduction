using Common.Application.Messaging;
using CustomerOrder.Core.Exceptions;
using CustomerOrder.Core.Interfaces;
using MassTransit;
using System.Threading.Tasks;

namespace CustomerOrder.Application.Messaging.Consumers
{
    public class ChangeOrderStatusConsumer : IConsumer<ChangeOrderStatusEvent>
    {
        private readonly IOrderRepository orderRepository;

        public ChangeOrderStatusConsumer(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task Consume(ConsumeContext<ChangeOrderStatusEvent> context)
        {
            var order = await orderRepository.GetById(context.Message.OrderId);

            if (order == null)
            {
                throw new OrderNotFoundException();
            }

            order.OrderStatus = context.Message.OrderStatus;

            await orderRepository.SaveAllAsync();
        }
    }
}

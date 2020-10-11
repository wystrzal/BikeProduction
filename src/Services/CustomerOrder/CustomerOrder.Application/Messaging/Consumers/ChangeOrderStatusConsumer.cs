using Common.Application.Messaging;
using CustomerOrder.Core.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CustomerOrder.Application.Messaging.Consumers
{
    public class ChangeOrderStatusConsumer : IConsumer<ChangeOrderStatusEvent>
    {
        private readonly IOrderRepository orderRepository;
        private readonly ILogger<ChangeOrderStatusConsumer> logger;

        public ChangeOrderStatusConsumer(IOrderRepository orderRepository, ILogger<ChangeOrderStatusConsumer> logger)
        {
            this.orderRepository = orderRepository;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<ChangeOrderStatusEvent> context)
        {
            try
            {
                var order = await orderRepository.GetById(context.Message.OrderId);

                order.OrderStatus = context.Message.OrderStatus;

                await orderRepository.SaveAllAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }

            logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
        }
    }
}

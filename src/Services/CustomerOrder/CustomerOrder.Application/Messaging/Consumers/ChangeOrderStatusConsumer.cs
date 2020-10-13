using Common.Application.Messaging;
using CustomerOrder.Core.Interfaces;
using CustomerOrder.Core.Models;
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
                await ChangeOrderStatus(context, order);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }

            logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
        }

        private async Task ChangeOrderStatus(ConsumeContext<ChangeOrderStatusEvent> context, Order order)
        {
            order.OrderStatus = context.Message.OrderStatus;
            await orderRepository.SaveAllAsync();
        }
    }
}

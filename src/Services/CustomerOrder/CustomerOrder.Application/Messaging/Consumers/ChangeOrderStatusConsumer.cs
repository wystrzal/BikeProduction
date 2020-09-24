using Common.Application.Messaging;
using CustomerOrder.Core.Exceptions;
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
            var order = await orderRepository.GetById(context.Message.OrderId);

            if (order == null)
            {
                var exception = new OrderNotFoundException();
                logger.LogError($"{exception.Message} at {this}");
                throw exception;
            }

            order.OrderStatus = context.Message.OrderStatus;

            await orderRepository.SaveAllAsync();

            logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
        }
    }
}

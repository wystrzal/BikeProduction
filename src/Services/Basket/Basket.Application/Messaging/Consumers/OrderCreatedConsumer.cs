using Basket.Core.Interfaces;
using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Basket.Application.Messaging.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IBasketRedisService basketService;
        private readonly ILogger<OrderCreatedConsumer> logger;

        public OrderCreatedConsumer(IBasketRedisService basketService, ILogger<OrderCreatedConsumer> logger)
        {
            this.basketService = basketService;
            this.logger = logger;
        }
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            await basketService.RemoveBasket(context.Message.UserId);

            logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
        }
    }
}

using Basket.Core.Interfaces;
using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
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
            ValidateUserId(context.Message.UserId);

            await RemoveBasket(context.Message.UserId);

            logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
        }

        private void ValidateUserId(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                logger.LogError("UserId could not be null.");
                throw new ArgumentNullException();
            }
        }

        private async Task RemoveBasket(string userId)
        {
            try
            {
                await basketService.RemoveBasket(userId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }
}

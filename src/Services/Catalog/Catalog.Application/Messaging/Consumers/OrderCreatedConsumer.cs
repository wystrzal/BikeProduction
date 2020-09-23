using Catalog.Application.Messaging.MessagingModels;
using Catalog.Core.Interfaces;
using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Application.Messaging.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IProductRepository productRepository;
        private readonly ILogger<OrderCreatedConsumer> logger;

        public OrderCreatedConsumer(IProductRepository productRepository, ILogger<OrderCreatedConsumer> logger)
        {
            this.productRepository = productRepository;
            this.logger = logger;
        }
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            if (context.Message.OrderItems != null && context.Message.OrderItems.Count > 0)
            {
                await IncreaseProductsPopularity(context.Message.OrderItems);

                logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
            }
        }

        private async Task IncreaseProductsPopularity(List<OrderItem> orderItems)
        {
            foreach (var orderItem in orderItems)
            {
                var product = await productRepository.GetByConditionFirst(x => x.Reference == orderItem.Reference);
                product.Popularity++;
            }

            await productRepository.SaveAllAsync();
        }
    }
}

using Catalog.Core.Interfaces;
using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
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
                foreach (var item in context.Message.OrderItems)
                {
                    var product = await productRepository.GetByConditionFirst(x => x.Reference == item.Reference);
                    product.Popularity++;
                }

                await productRepository.SaveAllAsync();

                logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
            }
        }
    }
}

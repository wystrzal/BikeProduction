using Catalog.Core.Interfaces;
using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Catalog.Application.Messaging.Consumers
{
    public class OrderCanceledConsumer : IConsumer<OrderCanceledEvent>
    {
        private readonly IProductRepository productRepository;
        private readonly ILogger<OrderCanceledConsumer> logger;

        public OrderCanceledConsumer(IProductRepository productRepository, ILogger<OrderCanceledConsumer> logger)
        {
            this.productRepository = productRepository;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderCanceledEvent> context)
        {
            if (context.Message.References != null && context.Message.References.Count > 0)
            {
                foreach (var item in context.Message.References)
                {
                    var product = await productRepository.GetByConditionFirst(x => x.Reference == item);
                    product.Popularity--;
                }

                await productRepository.SaveAllAsync();

                logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
            }
        }
    }
}

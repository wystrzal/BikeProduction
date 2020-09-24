using Catalog.Application.Messaging.MessagingModels;
using Catalog.Core.Exceptions;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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
                await DecreaseProductsPopularity(context.Message.References);

                logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
            }
        }

        private async Task DecreaseProductsPopularity(List<string> references)
        {
            foreach (var orderItemReference in references)
            {
                var product = await productRepository.GetByConditionFirst(x => x.Reference == orderItemReference);

                ThrowsProductNotFoundExceptionIfProductIsNull(product);

                product.Popularity--;
            }

            await productRepository.SaveAllAsync();
        }

        private void ThrowsProductNotFoundExceptionIfProductIsNull(Product product)
        {
            if (product == null)
            {
                var exception = new ProductNotFoundException();
                logger.LogError($"{exception.Message} at {this}");
                throw exception;
            } 
        }
    }
}

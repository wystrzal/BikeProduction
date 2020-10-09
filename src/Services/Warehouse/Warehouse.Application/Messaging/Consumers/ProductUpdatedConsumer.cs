using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Core.Interfaces;

namespace Warehouse.Application.Messaging.Consumers
{
    public class ProductUpdatedConsumer : IConsumer<ProductUpdatedEvent>
    {
        private readonly IProductRepository productRepository;
        private readonly ILogger<ProductUpdatedConsumer> logger;

        public ProductUpdatedConsumer(IProductRepository productRepository, ILogger<ProductUpdatedConsumer> logger)
        {
            this.productRepository = productRepository;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<ProductUpdatedEvent> context)
        {
            try
            {
                var product = await productRepository.GetByConditionFirst(x => x.Reference == context.Message.OldReference);

                product.ProductName = context.Message.ProductName;
                product.Reference = context.Message.Reference;

                await productRepository.SaveAllAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }

            logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
        }
    }
}

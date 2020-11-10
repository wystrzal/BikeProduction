using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;

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
                ValidateContext(context);

                var product = await productRepository.GetByConditionFirst(x => x.Reference == context.Message.OldReference);

                await ChangeProductNameAndReference(context, product);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }

            logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
        }

        private void ValidateContext(ConsumeContext<ProductUpdatedEvent> context)
        {
            if (string.IsNullOrWhiteSpace(context.Message.OldReference))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrWhiteSpace(context.Message.Reference))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrWhiteSpace(context.Message.ProductName))
            {
                throw new ArgumentNullException();
            }
        }

        private async Task ChangeProductNameAndReference(ConsumeContext<ProductUpdatedEvent> context, Product product)
        {
            product.ProductName = context.Message.ProductName;
            product.Reference = context.Message.Reference;
            await productRepository.SaveAllAsync();
        }
    }
}

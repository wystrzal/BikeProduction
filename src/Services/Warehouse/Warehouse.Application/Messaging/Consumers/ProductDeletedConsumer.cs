using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Warehouse.Core.Exceptions;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;

namespace Warehouse.Application.Messaging.Consumers
{
    public class ProductDeletedConsumer : IConsumer<ProductDeletedEvent>
    {
        private readonly IProductRepository productRepository;
        private readonly ILogger<ProductDeletedConsumer> logger;

        public ProductDeletedConsumer(IProductRepository productRepository, ILogger<ProductDeletedConsumer> logger)
        {
            this.productRepository = productRepository;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<ProductDeletedEvent> context)
        {
            var product = await GetProductByReference(context.Message.Reference);

            productRepository.Delete(product);

            await productRepository.SaveAllAsync();

            logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
        }

        private async Task<Product> GetProductByReference(string reference)
        {
            var product = await productRepository.GetByConditionFirst(x => x.Reference == reference);

            if (product == null)
            {
                var exception = new ProductNotFoundException();
                logger.LogError(exception.Message);
                throw exception;
            }

            return product;
        }
    }
}

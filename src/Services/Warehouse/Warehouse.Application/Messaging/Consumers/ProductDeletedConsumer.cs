using Common.Application.Messaging;
using MassTransit;
using System.Threading.Tasks;
using Warehouse.Core.Exceptions;
using Warehouse.Core.Interfaces;

namespace Warehouse.Application.Messaging.Consumers
{
    public class ProductDeletedConsumer : IConsumer<ProductDeletedEvent>
    {
        private readonly IProductRepository productRepository;

        public ProductDeletedConsumer(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task Consume(ConsumeContext<ProductDeletedEvent> context)
        {
            var product = await productRepository.GetByConditionFirst(x => x.Reference == context.Message.Reference);

            if (product == null)
            {
                throw new ProductNotFoundException();
            }

            productRepository.Delete(product);

            await productRepository.SaveAllAsync();
        }
    }
}

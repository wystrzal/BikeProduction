using Catalog.Core.Interfaces;
using Common.Application.Messaging;
using MassTransit;
using System.Threading.Tasks;

namespace Catalog.Application.Messaging.Consumers
{
    public class OrderCanceledConsumer : IConsumer<OrderCanceledEvent>
    {
        private readonly IProductRepository productRepository;

        public OrderCanceledConsumer(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
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
            }
        }
    }
}

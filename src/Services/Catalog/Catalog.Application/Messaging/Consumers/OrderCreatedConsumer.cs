using Catalog.Core.Interfaces;
using Common.Application.Messaging;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Messaging.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IProductRepository productRepository;

        public OrderCreatedConsumer(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
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
            }
        }
    }
}

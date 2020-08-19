using AutoMapper;
using Common.Application.Messaging;
using MassTransit;
using System.Threading.Tasks;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;

namespace Warehouse.Application.Messaging.Consumers
{
    public class ProductAddedConsumer : IConsumer<ProductAddedEvent>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductAddedConsumer(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task Consume(ConsumeContext<ProductAddedEvent> context)
        {
            var productToAdd = mapper.Map<Product>(context.Message);

            productRepository.Add(productToAdd);

            await productRepository.SaveAllAsync();
        }
    }
}

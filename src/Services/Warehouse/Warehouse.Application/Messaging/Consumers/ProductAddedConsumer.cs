using AutoMapper;
using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;

namespace Warehouse.Application.Messaging.Consumers
{
    public class ProductAddedConsumer : IConsumer<ProductAddedEvent>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly ILogger<ProductAddedConsumer> logger;

        public ProductAddedConsumer(IProductRepository productRepository, IMapper mapper, ILogger<ProductAddedConsumer> logger)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<ProductAddedEvent> context)
        {
            try
            {
                ValidateContext(context);
                var productToAdd = mapper.Map<Product>(context.Message);
                await AddProduct(productToAdd);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }

            logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
        }

        private void ValidateContext(ConsumeContext<ProductAddedEvent> context)
        {
            if (string.IsNullOrWhiteSpace(context.Message.ProductName))
            {
                throw new ArgumentNullException("ProductName");
            }

            if (string.IsNullOrWhiteSpace(context.Message.Reference))
            {
                throw new ArgumentNullException("Reference");
            }
        }

        private async Task AddProduct(Product product)
        {
            productRepository.Add(product);
            await productRepository.SaveAllAsync();
        }
    }
}

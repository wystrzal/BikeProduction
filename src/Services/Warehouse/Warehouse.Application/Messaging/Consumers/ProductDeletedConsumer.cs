﻿using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
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
            try
            {
                var product = await productRepository.GetByConditionFirst(x => x.Reference == context.Message.Reference);

                productRepository.Delete(product);

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

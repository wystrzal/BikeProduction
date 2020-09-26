using Catalog.Core.Interfaces;
using Catalog.Core.Models.MessagingModels;
using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Application.Messaging.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IChangeProductsPopularityService changeProductsPopularityService;
        private readonly ILogger<OrderCreatedConsumer> logger;

        public OrderCreatedConsumer(IChangeProductsPopularityService changeProductsPopularityService,
            ILogger<OrderCreatedConsumer> logger)
        {
            this.changeProductsPopularityService = changeProductsPopularityService;
            this.logger = logger;
        }
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            try
            {
                if (await changeProductsPopularityService.ChangeProductsPopularity(context.Message.OrderItems, true))
                    logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}

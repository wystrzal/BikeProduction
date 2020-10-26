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
    public class OrderCanceledConsumer : IConsumer<OrderCanceledEvent>
    {
        private readonly IChangeProductsPopularityService changeProductsPopularityService;
        private readonly ILogger<OrderCanceledConsumer> logger;

        public OrderCanceledConsumer(IChangeProductsPopularityService changeProductsPopularityService,
            ILogger<OrderCanceledConsumer> logger)
        {
            this.changeProductsPopularityService = changeProductsPopularityService;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderCanceledEvent> context)
        {
            ValidateOrderItems(context.Message.OrderItems);

            await ChangeProductsPopularity(context);

            logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
        }

        private void ValidateOrderItems(List<OrderItem> orderItems)
        {
            if (orderItems == null || orderItems.Count <= 0)
            {
                throw new ArgumentNullException();
            }
        }

        private async Task ChangeProductsPopularity(ConsumeContext<OrderCanceledEvent> context)
        {
            try
            {
                await changeProductsPopularityService.ChangeProductsPopularity(context.Message.OrderItems, false);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }
}

using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using Production.Core.Exceptions;
using Production.Core.Interfaces;
using Production.Core.Models;
using Production.Core.Models.MessagingModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Production.Core.Models.Enums.ProductionStatusEnum;

namespace Production.Application.Messaging.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IProductionQueueRepo productionQueueRepo;
        private readonly ILogger<OrderCreatedConsumer> logger;

        public OrderCreatedConsumer(IProductionQueueRepo productionQueueRepo, ILogger<OrderCreatedConsumer> logger)
        {
            this.productionQueueRepo = productionQueueRepo;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            AddOrderedItemsToProductionQueue(context.Message.OrderItems, context.Message.OrderId);

            try
            {
                await productionQueueRepo.SaveAllAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
     
            logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
        }

        private void AddOrderedItemsToProductionQueue(List<OrderItem> orderItems, int orderId)
        {
            foreach (var orderItem in orderItems)
            {
                productionQueueRepo.Add(new ProductionQueue
                {
                    Reference = orderItem.Reference,
                    Quantity = orderItem.Quantity,
                    ProductionStatus = ProductionStatus.Waiting,
                    OrderId = orderId
                });
            };
        }
    }
}

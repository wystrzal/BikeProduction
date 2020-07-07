using Common.Messaging;
using MassTransit;
using Production.Core.Interfaces;
using Production.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Production.Core.Models.Enums.ProductionStatusEnum;

namespace Production.Application.Messaging.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IProductionQueueRepo productionQueueRepo;

        public OrderCreatedEventConsumer(IProductionQueueRepo productionQueueRepo)
        {
            this.productionQueueRepo = productionQueueRepo;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            foreach (var item in context.Message.OrderItems)
            {
                productionQueueRepo.Add(new ProductionQueue
                {
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    ProductionStatus = ProductionStatus.Waiting
                });
            };

            if (!await productionQueueRepo.SaveAllAsync())
            {
                throw new Exception("Could not add products to queue.");
            }
        }
    }
}

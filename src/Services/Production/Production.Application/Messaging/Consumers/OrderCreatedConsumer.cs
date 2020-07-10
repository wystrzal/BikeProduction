using Common.Application.Messaging;
using MassTransit;
using Production.Core.Exceptions;
using Production.Core.Interfaces;
using Production.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Production.Core.Models.Enums.ProductionStatusEnum;

namespace Production.Application.Messaging.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IProductionQueueRepo productionQueueRepo;

        public OrderCreatedConsumer(IProductionQueueRepo productionQueueRepo)
        {
            this.productionQueueRepo = productionQueueRepo;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            foreach (var item in context.Message.OrderItems)
            {
                productionQueueRepo.Add(new ProductionQueue
                {
                    Reference = item.Reference,
                    Quantity = item.Quantity,
                    ProductionStatus = ProductionStatus.Waiting
                });
            };

            if (!await productionQueueRepo.SaveAllAsync())
            {
                throw new ProductNotAddedException();
            }
        }
    }
}

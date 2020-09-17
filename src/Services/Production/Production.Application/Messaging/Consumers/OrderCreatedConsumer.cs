using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using Production.Core.Exceptions;
using Production.Core.Interfaces;
using Production.Core.Models;
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
            foreach (var item in context.Message.OrderItems)
            {
                productionQueueRepo.Add(new ProductionQueue
                {
                    Reference = item.Reference,
                    Quantity = item.Quantity,
                    ProductionStatus = ProductionStatus.Waiting,
                    OrderId = context.Message.OrderId
                });
            };

            if (!await productionQueueRepo.SaveAllAsync())
            {
                var exception = new ProductNotAddedException();

                logger.LogError(exception.Message);

                throw exception;
            }

            logger.LogInformation($"Successfully handled event: {context.MessageId} at {this} - {context}");
        }
    }
}

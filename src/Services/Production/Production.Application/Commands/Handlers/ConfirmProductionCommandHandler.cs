using Common.Application.Messaging;
using MassTransit;
using MediatR;
using Production.Application.Messaging;
using Production.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Production.Application.Messaging.MessagingModels.OrderStatusEnum;
using static Production.Core.Models.Enums.ProductionStatusEnum;

namespace Production.Application.Commands.Handlers
{
    public class ConfirmProductionCommandHandler : IRequestHandler<ConfirmProductionCommand>
    {
        private readonly IBus bus;
        private readonly IProductionQueueRepo productionQueueRepo;

        public ConfirmProductionCommandHandler(IBus bus, IProductionQueueRepo productionQueueRepo)
        {
            this.bus = bus;
            this.productionQueueRepo = productionQueueRepo;
        }
        public async Task<Unit> Handle(ConfirmProductionCommand request, CancellationToken cancellationToken)
        {
            var productionQueue = await productionQueueRepo.GetById(request.ProductionQueueId);

            if (productionQueue.ProductionStatus == ProductionStatus.Waiting 
                || productionQueue.ProductionStatus == ProductionStatus.NoParts)
            {
                var serviceAddress = new Uri("rabbitmq://localhost/production_confirmed");
                var client = bus.CreateRequestClient<ProductionConfirmedEvent>(serviceAddress);
                var response = await client.GetResponse<ProductionConfirmedResult>(
                    new { productionQueue.Reference, productionQueue.Quantity });

                if (response.Message.StartProduction)
                {
                    productionQueue.ProductionStatus = ProductionStatus.Confirmed;
                    await bus.Publish(new ChangeOrderStatusEvent(productionQueue.OrderId, OrderStatus.Confirmed));
                }
                else
                {
                    productionQueue.ProductionStatus = ProductionStatus.NoParts;
                }

                await productionQueueRepo.SaveAllAsync();        
            }

            return Unit.Value;
        }
    }
}

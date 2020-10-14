using Common.Application.Messaging;
using MassTransit;
using MediatR;
using Production.Core.Exceptions;
using Production.Core.Interfaces;
using Production.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Production.Core.Models.Enums.ProductionStatusEnum;
using static Production.Core.Models.MessagingModels.OrderStatusEnum;

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

            if (ProductionWaitingOrDontHaveParts(productionQueue))
            {
                var response = await RequestForConfirmProduction(productionQueue);
                await ChangeProductionStatusDependingOnResponse(productionQueue, response);
            }

            return Unit.Value;
        }

        private bool ProductionWaitingOrDontHaveParts(ProductionQueue productionQueue)
        {
            return productionQueue.ProductionStatus == ProductionStatus.Waiting
                || productionQueue.ProductionStatus == ProductionStatus.NoParts;
        }

        private async Task<Response<ProductionConfirmedResult>> RequestForConfirmProduction(ProductionQueue productionQueue)
        {
            var serviceAddress = new Uri("rabbitmq://host.docker.internal/production_confirmed");
            var client = bus.CreateRequestClient<ProductionConfirmedEvent>(serviceAddress);

            return await client.GetResponse<ProductionConfirmedResult>(
                new { productionQueue.Reference, productionQueue.Quantity });          
        }

        private async Task ChangeProductionStatusDependingOnResponse(ProductionQueue productionQueue,
            Response<ProductionConfirmedResult> response)
        {
            if (response.Message.ConfirmProduction)
            {
                productionQueue.ProductionStatus = ProductionStatus.Confirmed;
                await bus.Publish(new ChangeOrderStatusEvent(productionQueue.OrderId, OrderStatus.Confirmed));
            }
            else
            {
                if (productionQueue.ProductionStatus == ProductionStatus.NoParts)
                {
                    return;
                }

                productionQueue.ProductionStatus = ProductionStatus.NoParts;
            }

            await productionQueueRepo.SaveAllAsync();
        }
    }
}

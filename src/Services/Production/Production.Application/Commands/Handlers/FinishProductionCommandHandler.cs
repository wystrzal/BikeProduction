using Common.Application.Commands;
using Common.Application.Messaging;
using MassTransit;
using MediatR;
using Production.Core.Exceptions;
using Production.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using static Production.Core.Models.Enums.ProductionStatusEnum;

namespace Production.Application.Commands.Handlers
{
    public class FinishProductionCommandHandler : IRequestHandler<FinishProductionCommand>
    {
        private readonly IProductionQueueRepo productionQueueRepo;
        private readonly IBus bus;

        public FinishProductionCommandHandler(IProductionQueueRepo productionQueueRepo, IBus bus)
        {
            this.productionQueueRepo = productionQueueRepo;
            this.bus = bus;
        }

        public async Task<Unit> Handle(FinishProductionCommand request, CancellationToken cancellationToken)
        {
            var productionQueue = await productionQueueRepo.GetById(request.ProductionQueueId);

            if (productionQueue == null || productionQueue.ProductionStatus != ProductionStatus.BeingCreated)
                throw new ProductsNotBeingCreatedException();

            productionQueue.ProductionStatus = ProductionStatus.Finished;

            await productionQueueRepo.SaveAllAsync();

            await bus.Publish(new ProductionFinishedEvent(productionQueue.OrderId, productionQueue.Quantity));

            await PublishPackReadyToSendEventIfOrderedProductsFinished(productionQueue.OrderId);

            return Unit.Value;
        }

        private async Task PublishPackReadyToSendEventIfOrderedProductsFinished(int orderId)
        {
            var orderedProducts = await productionQueueRepo
                .GetByConditionToList(x => x.OrderId == orderId && x.ProductionStatus != ProductionStatus.Finished);

            if (orderedProducts.Count == 0)
                await bus.Publish(new PackReadyToSendEvent(orderId));
        }
    }
}

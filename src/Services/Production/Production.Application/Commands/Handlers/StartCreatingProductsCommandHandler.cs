using MediatR;
using Production.Core.Exceptions;
using Production.Core.Interfaces;
using Production.Core.Models;
using System.Threading;
using System.Threading.Tasks;
using static Production.Core.Models.Enums.ProductionStatusEnum;

namespace Production.Application.Commands.Handlers
{
    public class StartCreatingProductsCommandHandler : IRequestHandler<StartCreatingProductsCommand>
    {
        private readonly IProductionQueueRepo productionQueueRepo;

        public StartCreatingProductsCommandHandler(IProductionQueueRepo productionQueueRepo)
        {
            this.productionQueueRepo = productionQueueRepo;
        }
        public async Task<Unit> Handle(StartCreatingProductsCommand request, CancellationToken cancellationToken)
        {
            var productionQueue = await productionQueueRepo.GetById(request.ProductionQueueId);

            CheckIfProductionConfirmed(productionQueue);

            await ChangeProductionStatusToBeingCreated(productionQueue);

            return Unit.Value;
        }

        private void CheckIfProductionConfirmed(ProductionQueue productionQueue)
        {
            if (productionQueue.ProductionStatus != ProductionStatus.Confirmed)
            {
                throw new ProductionQueueNotConfirmedException();
            }
        }

        private async Task ChangeProductionStatusToBeingCreated(ProductionQueue productionQueue)
        {
            productionQueue.ProductionStatus = ProductionStatus.BeingCreated;
            await productionQueueRepo.SaveAllAsync();
        }
    }
}

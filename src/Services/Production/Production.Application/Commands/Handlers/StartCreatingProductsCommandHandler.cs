using MediatR;
using Production.Core.Exceptions;
using Production.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using static Production.Core.Models.Enums.ProductionStatusEnum;

namespace Production.Application.Commands.Handlers
{
    class StartCreatingProductsCommandHandler : IRequestHandler<StartCreatingProductsCommand>
    {
        private readonly IProductionQueueRepo productionQueueRepo;

        public StartCreatingProductsCommandHandler(IProductionQueueRepo productionQueueRepo)
        {
            this.productionQueueRepo = productionQueueRepo;
        }
        public async Task<Unit> Handle(StartCreatingProductsCommand request, CancellationToken cancellationToken)
        {
            var productionQueue = await productionQueueRepo.GetById(request.ProductionQueueId);

            if (productionQueue.ProductionStatus != ProductionStatus.Confirmed)
            {
                throw new ProductionQueueNotConfirmedException();
            }

            productionQueue.ProductionStatus = ProductionStatus.BeingCreated;

            await productionQueueRepo.SaveAllAsync();

            return Unit.Value;
        }
    }
}

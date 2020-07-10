using MediatR;
using Production.Core.Exceptions;
using Production.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Production.Core.Models.Enums.ProductionStatusEnum;

namespace Production.Application.Commands.Handlers
{
    public class ProductionFinishedCommandHandler : IRequestHandler<ProductionFinishedCommand>
    {
        private readonly IProductionQueueRepo productionQueueRepo;

        public ProductionFinishedCommandHandler(IProductionQueueRepo productionQueueRepo)
        {
            this.productionQueueRepo = productionQueueRepo;
        }

        public async Task<Unit> Handle(ProductionFinishedCommand request, CancellationToken cancellationToken)
        {
            var productionQueue = await productionQueueRepo.GetById(request.ProductionQueueId);

            if (productionQueue.ProductionStatus == ProductionStatus.BeingCreated)
            {
                productionQueue.ProductionStatus = ProductionStatus.Finished;
                await productionQueueRepo.SaveAllAsync();
            } 
            else
            {
                throw new ProductsNotBeingCreatedException();
            }

            return Unit.Value;
        }
    }
}

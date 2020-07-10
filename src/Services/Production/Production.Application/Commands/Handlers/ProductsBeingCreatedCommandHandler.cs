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
    class ProductsBeingCreatedCommandHandler : IRequestHandler<ProductsBeingCreatedCommand>
    {
        private readonly IProductionQueueRepo productionQueueRepo;

        public ProductsBeingCreatedCommandHandler(IProductionQueueRepo productionQueueRepo)
        {
            this.productionQueueRepo = productionQueueRepo;
        }
        public async Task<Unit> Handle(ProductsBeingCreatedCommand request, CancellationToken cancellationToken)
        {
            var productionQueue = await productionQueueRepo.GetById(request.ProductionQueueId);

            if (productionQueue.ProductionStatus == ProductionStatus.Confirmed)
            {
                productionQueue.ProductionStatus = ProductionStatus.BeingCreated;
                await productionQueueRepo.SaveAllAsync();
            } else
            {
                throw new ProductionQueueNotConfirmedException();
            }

            return Unit.Value;
        }
    }
}

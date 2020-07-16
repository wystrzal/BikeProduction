﻿using Common.Application.Commands;
using Common.Application.Messaging;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;
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

            if (productionQueue.ProductionStatus == ProductionStatus.BeingCreated)
            {
                productionQueue.ProductionStatus = ProductionStatus.Finished;
                await productionQueueRepo.SaveAllAsync();

                await bus.Publish(new ProductionFinishedEvent(productionQueue.OrderId, productionQueue.Quantity));
            } 
            else
            {
                throw new ProductsNotBeingCreatedException();
            }

            var checkProductionStatus = await productionQueueRepo
                .GetByConditionToList(x => x.OrderId == productionQueue.OrderId && x.ProductionStatus != ProductionStatus.Finished);

            if (checkProductionStatus.Count == 0)
            {
                await bus.Publish(new PackReadyToSendEvent(productionQueue.OrderId));
            }

            return Unit.Value;
        }
    }
}
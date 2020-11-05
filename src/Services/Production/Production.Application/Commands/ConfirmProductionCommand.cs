using MediatR;
using System;

namespace Production.Application.Commands
{
    public class ConfirmProductionCommand : IRequest
    {
        public int ProductionQueueId { get; set; }

        public ConfirmProductionCommand(int productionQueueId)
        {
            if (productionQueueId <= 0)
            {
                throw new ArgumentException("ProductionQueueId must be greater than zero.");
            }

            ProductionQueueId = productionQueueId;
        }
    }
}

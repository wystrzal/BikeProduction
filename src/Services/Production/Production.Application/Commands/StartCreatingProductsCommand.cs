using MediatR;
using System;

namespace Production.Application.Commands
{
    public class StartCreatingProductsCommand : IRequest
    {
        public int ProductionQueueId { get; set; }

        public StartCreatingProductsCommand(int productionQueueId)
        {
            if (productionQueueId <= 0)
            {
                throw new ArgumentException("ProductionQueueId must be greater than zero.");
            }

            ProductionQueueId = productionQueueId;
        }
    }
}

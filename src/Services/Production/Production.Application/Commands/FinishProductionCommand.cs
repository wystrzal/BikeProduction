using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Production.Application.Commands
{
    public class FinishProductionCommand : IRequest
    {
        public int ProductionQueueId { get; set; }

        public FinishProductionCommand(int productionQueueId)
        {
            ProductionQueueId = productionQueueId;
        }
    }
}

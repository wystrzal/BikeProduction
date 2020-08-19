using MediatR;

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

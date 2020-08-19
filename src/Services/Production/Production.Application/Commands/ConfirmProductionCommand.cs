using MediatR;

namespace Production.Application.Commands
{
    public class ConfirmProductionCommand : IRequest
    {
        public int ProductionQueueId { get; set; }

        public ConfirmProductionCommand(int productionQueueId)
        {
            ProductionQueueId = productionQueueId;
        }
    }
}

using MediatR;

namespace Production.Application.Commands
{
    public class FinishProductionCommand : IRequest
    {
        public int ProductionQueueId { get; set; }
        public string Token { get; set; }

        public FinishProductionCommand(int productionQueueId, string token)
        {
            ProductionQueueId = productionQueueId;
            Token = token;
        }
    }
}

using MediatR;

namespace Production.Application.Commands
{
    public class StartCreatingProductsCommand : IRequest
    {
        public int ProductionQueueId { get; set; }

        public StartCreatingProductsCommand(int productionQueueId)
        {
            ProductionQueueId = productionQueueId;
        }
    }
}

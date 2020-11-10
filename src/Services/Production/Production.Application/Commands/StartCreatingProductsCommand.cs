namespace Production.Application.Commands
{
    public class StartCreatingProductsCommand : ProductionQueueIdCommand
    {
        public StartCreatingProductsCommand(int productionQueueId) : base(productionQueueId)
        {
        }
    }
}

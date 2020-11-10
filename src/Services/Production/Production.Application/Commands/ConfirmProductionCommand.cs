namespace Production.Application.Commands
{
    public class ConfirmProductionCommand : ProductionQueueIdCommand
    {
        public ConfirmProductionCommand(int productionQueueId) : base(productionQueueId)
        {
        }
    }
}

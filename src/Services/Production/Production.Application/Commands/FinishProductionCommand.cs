using System;

namespace Production.Application.Commands
{
    public class FinishProductionCommand : ProductionQueueIdCommand
    {
        public string Token { get; set; }

        public FinishProductionCommand(int productionQueueId, string token) : base(productionQueueId)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException("Token");
            }

            Token = token;
        }
    }
}

using MediatR;
using System;

namespace Production.Application.Commands
{
    public class FinishProductionCommand : IRequest
    {
        public int ProductionQueueId { get; set; }
        public string Token { get; set; }

        public FinishProductionCommand(int productionQueueId, string token)
        {
            if (productionQueueId <= 0)
            {
                throw new ArgumentException("ProductionQueueId must be greater than zero.");
            }

            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException("Token");
            }

            ProductionQueueId = productionQueueId;
            Token = token;
        }
    }
}

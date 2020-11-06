using MediatR;
using System;

namespace Production.Application.Commands
{
    public class ConfirmProductionCommand : BaseCommand
    {
        public ConfirmProductionCommand(int productionQueueId) : base(productionQueueId)
        {
        }
    }
}

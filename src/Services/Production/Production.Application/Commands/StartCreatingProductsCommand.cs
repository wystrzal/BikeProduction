using MediatR;
using System;

namespace Production.Application.Commands
{
    public class StartCreatingProductsCommand : BaseCommand
    {
        public StartCreatingProductsCommand(int productionQueueId) : base(productionQueueId)
        {
        }
    }
}

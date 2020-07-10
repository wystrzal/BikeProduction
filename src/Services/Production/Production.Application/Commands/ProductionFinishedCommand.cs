using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Production.Application.Commands
{
    public class ProductionFinishedCommand : IRequest
    {
        public int ProductionQueueId { get; set; }
    }
}

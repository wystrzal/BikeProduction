using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using static Production.Core.Models.Enums.ProductionStatusEnum;

namespace Production.Application.Commands
{
    public class ConfirmProductionCommand : IRequest
    {
        public int ProductionQueueId { get; set; }
    }
}

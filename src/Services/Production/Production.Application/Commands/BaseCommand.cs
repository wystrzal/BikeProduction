using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Production.Application.Commands
{
    public abstract class BaseCommand : IRequest
    {
        [Required]
        public int ProductionQueueId { get; set; }

        public BaseCommand()
        {
        }

        public BaseCommand(int productionQueueId)
        {
            if (productionQueueId <= 0)
            {
                throw new ArgumentException("ProductionQueueId must be greater than zero.");
            }

            ProductionQueueId = productionQueueId;
        }
    }
}

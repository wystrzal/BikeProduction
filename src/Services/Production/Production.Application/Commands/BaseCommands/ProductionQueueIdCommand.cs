using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Production.Application.Commands
{
    public abstract class ProductionQueueIdCommand : IRequest
    {
        [Required]
        public int ProductionQueueId { get; set; }

        public ProductionQueueIdCommand()
        {
        }

        public ProductionQueueIdCommand(int productionQueueId)
        {
            if (productionQueueId <= 0)
            {
                throw new ArgumentException("ProductionQueueId must be greater than zero.");
            }

            ProductionQueueId = productionQueueId;
        }
    }
}

using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Warehouse.Application.Commands
{
    public abstract class PartIdCommand : IRequest
    {
        [Required]
        public int PartId { get; set; }

        public PartIdCommand()
        {
        }

        public PartIdCommand(int partId)
        {
            if (partId <= 0)
            {
                throw new ArgumentException("PartId must be greater than zero.");
            }

            PartId = partId;
        }
    }
}

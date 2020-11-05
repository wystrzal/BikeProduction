using MediatR;
using System;

namespace Warehouse.Application.Commands
{
    public class DeletePartCommand : IRequest
    {
        public int PartId { get; set; }

        public DeletePartCommand(int partId)
        {
            if (partId <= 0)
            {
                throw new ArgumentException("PartId must be greater than zero.");
            }

            PartId = partId;
        }
    }
}

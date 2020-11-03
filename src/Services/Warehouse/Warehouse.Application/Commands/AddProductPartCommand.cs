using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Application.Commands
{
    public class AddProductPartCommand : IRequest
    {
        public int PartId { get; set; }
        public string Reference { get; set; }

        public AddProductPartCommand(int partId, string reference)
        {
            if (partId <= 0)
            {
                throw new ArgumentException("PartId must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(reference))
            {
                throw new ArgumentNullException("Reference");
            }

            PartId = partId;
            Reference = reference;
        }
    }
}

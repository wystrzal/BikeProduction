using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Application.Commands
{
    public class UpdatePartCommand : IRequest
    {
        public int Id { get; set; }
        public string PartName { get; set; }
        public string Reference { get; set; }
        public int QuantityForProduction { get; set; }
        public int Quantity { get; set; }
    }
}

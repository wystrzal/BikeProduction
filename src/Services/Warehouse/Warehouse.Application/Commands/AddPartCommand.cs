using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Application.Commands
{
    public class AddPartCommand : IRequest
    {
        public string PartName { get; set; }
        public string Reference { get; set; }
    }
}

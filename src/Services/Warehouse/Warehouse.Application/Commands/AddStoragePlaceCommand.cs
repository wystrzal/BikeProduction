using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Application.Commands
{
    public class AddStoragePlaceCommand : IRequest
    {
        public string Name { get; set; }
    }
}

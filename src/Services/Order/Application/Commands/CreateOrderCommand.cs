using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Order.Core.Models;

namespace Order.Application.Commands
{
    public class CreateOrderCommand : IRequest
    {
        public Orders Orders { get; set; }
    }
}

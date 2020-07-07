using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerOrder.Core.Models;
using MediatR;

namespace CustomerOrder.Application.Commands
{
    public class CreateOrderCommand : IRequest
    {
        public Order Orders { get; set; }
    }
}

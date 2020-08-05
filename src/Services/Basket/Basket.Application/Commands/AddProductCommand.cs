using Basket.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Application.Commands
{
    public class AddProductCommand : IRequest
    {
        public BasketProduct Product { get; set; }
        public string UserId { get; set; }
    }
}

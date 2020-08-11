using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using static Catalog.Core.Models.ColorsEnum;

namespace Catalog.Application.Commands
{
    public class AddProductCommand : IRequest
    {
        public string ProductName { get; set; }
        public string Reference { get; set; }
        public decimal Price { get; set; }
        public string PhotoUrl { get; set; }
        public Colors Colors { get; set; }
    }
}

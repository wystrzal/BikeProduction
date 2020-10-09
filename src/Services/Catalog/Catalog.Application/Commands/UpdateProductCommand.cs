using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using static Catalog.Core.Models.Enums.BikeTypeEnum;
using static Catalog.Core.Models.Enums.ColorsEnum;

namespace Catalog.Application.Commands
{
    public class UpdateProductCommand : IRequest
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Reference { get; set; }
        public decimal Price { get; set; }
        public string PhotoUrl { get; set; }
        public Colors Colors { get; set; }
        public BikeType BikeType { get; set; }
        public int BrandId { get; set; }
    }
}
